using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ionic.Zip;
using System.IO;
using System.Data.SqlClient;

namespace VistasDeaWs.bridge
{
    public partial class FacturacionMes : System.Web.UI.Page
    {
        //private string conn = "Server=DEAVTSHIS;Database=portal2;User Id=consulta;Password=consulta;";
        //private NeoG.AcessDB.SqlDB sql;
        string strcon = ConfigurationManager.ConnectionStrings["DEAVTSHIS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["a"] == null)
                Response.End();
            string uuid = Request.QueryString["a"].ToString();
            if (uuid == string.Empty)
                Response.End();
            SqlConnection sqlcon = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("VPO_202_Pag1_Grid_MesCompleto_Sel", sqlcon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter uUid = new SqlParameter("@psUuid", Request.QueryString["a"]);
            cmd.Parameters.Add(uUid);

            sqlcon.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            ZipFile zip = new ZipFile();
            string mPath = string.Empty;
            int maxRead = 500000;
            int cont = 0;
            byte[] byt = new byte[maxRead];
            FileStream str;
            MemoryStream mstr;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mPath = dt.Rows[i]["XmlPathReal"].ToString();
                if (mPath == string.Empty)
                    continue;
                if (!File.Exists(mPath))
                    continue;
                try
                {
                    str = new FileStream(mPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    mstr = new MemoryStream();
                    while ((cont = str.Read(byt, 0, maxRead)) > 0)
                    {
                        mstr.Write(byt, 0, cont);
                    }
                    str.Close();
                    mstr.Position = 0;
                    zip.AddEntry(dt.Rows[i]["IdFacturaAlfanumerico"].ToString() + ".xml", mstr);

                    mPath = dt.Rows[i]["PdfPathReal"].ToString();
                    str = new FileStream(mPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    mstr = new MemoryStream();
                    while ((cont = str.Read(byt, 0, maxRead)) > 0)
                    {
                        mstr.Write(byt, 0, cont);
                    }
                    str.Close();
                    mstr.Position = 0;
                    zip.AddEntry(dt.Rows[i]["IdFacturaAlfanumerico"].ToString() + ".pdf", mstr);
                }
                catch (Exception err)
                {
                    Response.End();
                    return;
                }
            }
            mstr = new MemoryStream();
            zip.Save(mstr);
            mstr.Position = 0;
            cont = maxRead;
            while ((cont = mstr.Read(byt, 0, maxRead)) > 0)
            {
                if (cont == maxRead)
                {
                    Response.BinaryWrite(byt);
                }
                else
                {
                    byte[] byt2 = new byte[cont];
                    Array.Copy(byt, byt2, cont);
                    Response.BinaryWrite(byt2);
                }
            }
            mstr.Close();
            Response.AddHeader("Content-Disposition", "attachment;filename=\"facturas.zip\"");
            Response.ContentType = "application/octet-stream";
            Response.End();
        }
    }
}
