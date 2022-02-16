using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VistasDeaWs.interbanco
{
    public partial class redirstd : System.Web.UI.Page
    {
        
        protected string convenio = string.Empty;
        protected string referencia = string.Empty;
        protected string importe = string.Empty;
        protected string url_resp = string.Empty;
        protected string url = string.Empty;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            url = ConfigurationManager.AppSettings["URLSantander"];
            string strcon = ConfigurationManager.ConnectionStrings["PreProd"].ConnectionString;
            SqlConnection conn = new SqlConnection(strcon);
            try
            {
                SqlCommand cmd = new SqlCommand("VtaSch.VTA_CU801_Pag3_ConsultaDatosBanco_Sel", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUuid = new SqlParameter("@psUuid", Request.QueryString.Get("a"));
                paramUuid.SqlDbType = SqlDbType.VarChar;

                cmd.Parameters.Add(paramUuid);

                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    convenio = sdr["IdEmpresaBanco"].ToString();
                    referencia = sdr["Referencia"].ToString();
                    importe = sdr["Importe"].ToString();
                }
                url_resp = ConfigurationManager.AppSettings["URLRespuestaSantander"];
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}