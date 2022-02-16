using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VistasDeaWs.interbanco
{
    public partial class redirbmx : System.Web.UI.Page
    {
        //private NeoG.AcessDB.SqlDB sql;
        string strcon = ConfigurationManager.ConnectionStrings["Prod"].ConnectionString;
        protected int mdebug = 1;
        protected double imp = 0;
        protected string referencia = string.Empty;
        protected string idEmpresaBco = string.Empty;
        protected string cadena = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strcon);
            Session["ValAccion"] = string.Empty;
            Session["CadenaBmx"] = string.Empty;

            string uuid = string.Empty;
            int banco = 0;
            int idInterna = 0;
            if (Request.QueryString["a"] != null)
                uuid = Request.QueryString["a"].Trim();
            if (uuid.Length != 36)
                TerminaError("Error al procesar petición: token invalido");

            SqlCommand cmd = new SqlCommand("VtaSch.VTA_CU801_Pag3_ConsultaDatosBanco_Sel",conn);
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
            if (idInterna != 2)
                TerminaError("Esta ficha no pertenece a este banco");

            cadena = idEmpresaBco + "|" + "https://gps.deacero.com" + "|" +
                "1" + "|" + ((double)(imp * 100)).ToString("F0") + "|" + "99/99/9999" + "||" + referencia;
           
        }

        private void TerminaError(string mensaje)
        {
            Response.Write(mensaje);
            Response.End();
        }
    }
}
