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
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
//using NeoG.AcessDB;

namespace VistasDeaWs.bciti
{
    public partial class dearecepcionciti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Respuesta()
        {

            string[] posts = new string[]
            {
                "NUM_CTE", "REFER_PGO", "IMPORTE", "CONCEPTO", "FEC_PGO",
                "FEC_LPGO", "HORA", "TPO_ABO", "AUTORIZA", "AUTORIZA2"
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
                SqlCommand cmd = new SqlCommand("VtaSch.VTA_CU801_Pag3_RespuestaDatosBanco2_IU", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@psNumCte",respuesta[0]),
                    new SqlParameter("@psReferPgo",respuesta[1]),
                    new SqlParameter("@psImporte",respuesta[2]),
                    new SqlParameter("@psConcepto",respuesta[3]),
                    new SqlParameter("@psFecPgo",respuesta[4]),
                    new SqlParameter("@psFecLPgo",respuesta[5]),
                    new SqlParameter("@psHora",respuesta[6]),
                    new SqlParameter("@psTpoAbo",respuesta[7]),
                    new SqlParameter("@psAutoriza",respuesta[8]),
                    new SqlParameter("@psAutoriza2",respuesta[9])
                };

                cmd.Parameters.AddRange(sqlParams);

                try
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    Response.Write("Registro guardado con éxito");
                    conn.Close();
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
