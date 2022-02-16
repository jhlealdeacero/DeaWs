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
using System.Configuration;
using System.Data.SqlClient;
//using NeoG.AcessDB;

namespace VistasDeaWs.bbva
{
    public partial class dearecbcomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Respuesta()
        {

            string[] posts = new string[] {
                "codigo", "mensaje", "autorizacion", "referencia", "importe",
                "mediopago", "financiado", "plazos", "s_transm", "hash", "tarjetahabiente"
                };
            string[] respuesta = new string[posts.Length];

            for (int i = 0; i < posts.Length; i++)
            {
                respuesta[i] = F(posts[i]);
            }

            if (respuesta[0] == string.Empty)
            {
                Response.Write("SIN FORMA");
                return;
            }
            string strcon = ConfigurationManager.ConnectionStrings["Prod"].ConnectionString;
            SqlConnection conn = new SqlConnection(strcon);

            SqlCommand cmd = new SqlCommand("VtaSch.VTA_CU801_Pag3_RespuestaDatosBanco_IU", conn);
            cmd.CommandType = CommandType.StoredProcedure;
           
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@psCodigo",respuesta[0]),
                    new SqlParameter("@psMensaje",respuesta[1]),
                    new SqlParameter("@psAutorizacion",respuesta[2]),
                    new SqlParameter("@psReferencia",respuesta[3]),
                    new SqlParameter("@psImporte",respuesta[4]),
                    new SqlParameter("@psMedioPago",respuesta[5]),
                    new SqlParameter("@psFinanciado",respuesta[6]),
                    new SqlParameter("@psPlazos",respuesta[7]),
                    new SqlParameter("@psTransm",respuesta[8]),
                    new SqlParameter("@psHashes",respuesta[9]),
                    new SqlParameter("@psTarjetabiente",respuesta[10])
                };

                cmd.Parameters.AddRange(sqlParams);
                try
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    Response.Write("Registro guardado con éxito");

                }
                catch (Exception e)
                {
                    Response.Write(e.Message);
                    throw;
                }  
        }
        private string F(string par)
        {
            if (Request.Form[par] != null)
                return Request.Form[par].ToString();
            return string.Empty;
        }
    }
}
