using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Data.SqlClient;

namespace VistasDeaWs.interbanco
{
    public partial class redirectbco : System.Web.UI.Page
    {
        //private NeoG.AcessDB.SqlDB sql;
        //private string conn = "Server=DEAOFINET05;Database=Ventas;User Id=consulta;Password=consulta;";
        string strcon = ConfigurationManager.ConnectionStrings["Prod"].ConnectionString;
        protected int mdebug = 1;
        protected double imp = 0;
        protected string referencia = string.Empty;
        protected string idEmpresaBco = string.Empty;
        int banco = 0;
        int idInterna = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string uuid = string.Empty;
            if (Request.QueryString["a"] != null)
                uuid = Request.QueryString["a"].Trim();
            if (uuid.Length != 36)
                TerminaError("Error al procesar petición: token invalido");

            SqlConnection conn = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("VtaSch.VTA_CU801_Pag3_ConsultaDatosBanco_Sel", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUuid = new SqlParameter("@psUuid",uuid);
            
            cmd.Parameters.Add(paramUuid);
            DataTable dt = new DataTable();
            conn.Open();
            dt.Load(cmd.ExecuteReader());

            if (dt == null || dt.Rows.Count == 0)
                TerminaError("Error al procesar petición: no existe información");

            int mid = (int)dt.Rows[0]["Id"];
            mdebug = (int)dt.Rows[0]["Debug"];
            imp = Convert.ToDouble(dt.Rows[0]["Importe"]);
            referencia = dt.Rows[0]["Referencia"].ToString();
            idEmpresaBco = dt.Rows[0]["IdEmpresaBanco"].ToString();
            banco = (int)dt.Rows[0]["IdBanco"];
            idInterna = (int)dt.Rows[0]["IdInterna"];

            if (idEmpresaBco == string.Empty || idEmpresaBco == "0")
                TerminaError("No fue posible determinar la empresa destino del pago");
            // IdInterna = 1 es bancomer
            if (idInterna != 1)
                TerminaError("Esta ficha no pertenece a este banco");


            //    sql = new NeoG.AcessDB.SqlDB(conn);
            //    sql.CommandType = CommandType.StoredProcedure;
            //    DataTable dt = sql.DtEjecuta("VtaSch.VTA_CU801_Pag3_ConsultaDatosBanco_Sel",
            //        new string[] { "psUuid" },
            //        new object[] { uuid });

            //    if (dt == null || dt.Rows.Count == 0)
            //        TerminaError("Error al procesar petición: no existe información");

            //    int mid = (int)dt.Rows[0]["Id"];
            //    mdebug = (int)dt.Rows[0]["Debug"];
            //    imp = Convert.ToDouble(dt.Rows[0]["Importe"]);
            //    referencia = dt.Rows[0]["Referencia"].ToString();
            //    idEmpresaBco = dt.Rows[0]["IdEmpresaBanco"].ToString();
            //    banco = (int)dt.Rows[0]["IdBanco"];
            //    idInterna = (int)dt.Rows[0]["IdInterna"];

            //    if (idEmpresaBco == string.Empty || idEmpresaBco == "0")
            //        TerminaError("No fue posible determinar la empresa destino del pago");
            //    // IdInterna = 1 es bancomer
            //    if (idInterna != 1)
            //        TerminaError("Esta ficha no pertenece a este banco");
        }

        private void TerminaError(string mensaje)
        {
            Response.Write(mensaje);
            Response.End();
        }
    }
}
