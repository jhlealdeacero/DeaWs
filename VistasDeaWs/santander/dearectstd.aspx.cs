using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace VistasDeaWs.santander
{
    public partial class dearectstd : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public class Datos
        {
            public string referencia { get; set; }
            public int estatus { get; set; }
            public  string mensaje { get; set; }
            public string folio_oper { get; set; }
            public string  importe { get; set; }
        }
        protected void Respuesta()
        {
            string strcon = ConfigurationManager.ConnectionStrings["PreProd"].ConnectionString;
            SqlConnection conn = new SqlConnection(strcon);

            Datos datos = new Datos();
            datos.referencia = Request.QueryString["referencia"];
            datos.estatus = Convert.ToInt32(Request.QueryString["estatus"]);
            datos.mensaje = Request.QueryString["mensaje"];
            datos.folio_oper = Request.QueryString["folio_oper"];
            datos.importe = Request.QueryString["importe"];
            //string json = new StreamReader(Request.InputStream).ReadToEnd();
            //Datos datos = JsonConvert.DeserializeObject<Datos>(json);

            if (Request.QueryString.Count>0) {  

                SqlCommand cmd = new SqlCommand("VtaSch.VTA_GuardaRespuestaDatosBancoSantander", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@psReferencia", datos.referencia),
                    new SqlParameter("@pnEstatus", datos.estatus),
                    new SqlParameter("@psMensaje",datos.mensaje),
                    new SqlParameter("@psFolio", datos.folio_oper),
                    new SqlParameter("@pnImporte", datos.importe)

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
            else
                Response.Write("Favor de enviar datos");
        }
    }
}