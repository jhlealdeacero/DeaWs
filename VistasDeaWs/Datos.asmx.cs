using System;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace VistasDeaWs
{
    /// <summary>
    /// Summary description for Datos
    /// </summary>
    [WebService]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[ToolboxItem(false)]
    public class Datos //: System.Web.Services.WebService
    {
        //private NeoG.AcessDB.SqlDB sql;
        //private string conn = "Server=DEAOFINET05;Database=Ventas;User Id=VtaUsr;Password=vtausr;";
        private string strcon = ConfigurationManager.ConnectionStrings["Prod"].ConnectionString;

        private string ObtieneNomServicio(string claveServ, int servicio)
        {
            if (!TokenOk(claveServ))
                return string.Empty;

            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("select String1, String2 from VtaSch.VtaCfgConfiguracion where IdConfiguracion = 200", con);
            DataTable dt = new DataTable();
            con.Open();
            dt.Load(cmd.ExecuteReader());
            //DataTable dt = sql.DtEjecuta("select String1, String2 from VtaSch.VtaCfgConfiguracion where IdConfiguracion = @conf",
            //    new string[] { "conf" }, new object[] { 200 });

            string[] servicios = dt.Rows[0]["String2"].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (servicio <= 0 || servicio > servicios.Length)
                return string.Empty;
            con.Close();
            return servicios[servicio - 1].Trim();

        }

        private bool TokenOk(string miToken)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("select String1, String2 from VtaSch.VtaCfgConfiguracion where IdConfiguracion = 200", con);
            DataTable dt = new DataTable();
            con.Open();
            dt.Load(cmd.ExecuteReader());
            if (dt == null || dt.Rows.Count == 0)
                return false;
            string miClave = dt.Rows[0]["String1"].ToString().ToLower();
            if (miClave.Length == 0)
                return false;
            if (miClave != miToken.Trim().ToLower())
                return false;
            return true;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ObtieneDatos(string clave, int servicio)
        {
            //sql = new NeoG.AcessDB.SqlDB(conn);
            SqlConnection con1 = new SqlConnection(strcon);
            string nomServicio = ObtieneNomServicio(clave, servicio);
            if (nomServicio == string.Empty)
                return string.Empty;


            nomServicio = "select * from vtasch." + nomServicio;
            SqlCommand cmd = new SqlCommand(nomServicio, con1);
            DataTable dt = new DataTable();
            con1.Open();

            dt.Load(cmd.ExecuteReader());
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        /*
                [WebMethod]
                [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
                public string ObtieneEstatusFactura(string clave, string facturas)
                {
                    sql = new NeoG.AcessDB.SqlDB(conn);
                    if (!TokenOk(clave))
                        return string.Empty;

                    string nomServicio = string.Empty;

                    sql.CommandType = CommandType.StoredProcedure;
                    nomServicio = "vtasch.VtaObtieneDatosDAngelsPorFacturaSrv";
                    DataSet ds = sql.DsEjecuta(nomServicio, 
                        new string[] { "@psFacturas", "@pnSoloEstatus" },
                        new object[] { facturas, 1 });
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                        return string.Empty;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;

                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;

                    DataTable dtenc = ds.Tables[0];

                    foreach (DataRow dr in dtenc.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dtenc.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    return serializer.Serialize(rows);
                }
        */
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ObtieneDatosNuevo(string clave, string facturas, int tipoBusqueda)
        {
            //tipoBusqueda 1: placas
            //   2: Fabricaciones

            //sql = new NeoG.AcessDB.SqlDB(conn);
            SqlConnection con = new SqlConnection(strcon);
            if (!TokenOk(clave))
                return string.Empty;
            if (tipoBusqueda < 1 || tipoBusqueda > 2)
                return string.Empty;

            string nomServicio = string.Empty;
            nomServicio = "vtasch.VtaObtieneDatosDAngelsPorFacturaSrv";
            SqlCommand cmd = new SqlCommand(nomServicio,con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand = cmd;
            DataSet ds = new DataSet();

            adapter.Fill(ds);
            
            //DataSet ds = sql.DsEjecuta(nomServicio,
            //    new string[] { "@psFacturas", "@pnTipoFiltro" },
            //    new object[] { facturas, tipoBusqueda });
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            DataTable dtenc = ds.Tables[0];
            DataTable dtdet = ds.Tables[1];
            DataTable dtfac = ds.Tables[2];
            DataTable dtfacdet = ds.Tables[3];
            DataRow[] rowsdet;
            DataRow[] rowsfac;
            DataRow[] rowsfacdet;

            int factura = 0;
            int mid = 0;
            foreach (DataRow dr in dtenc.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtenc.Columns)
                {
                    if (col.ColumnName == "ID")
                    {
                        mid = (int)dr[col];
                        continue;
                    }
                    row.Add(col.ColumnName, dr[col]);
                }

                /////////////////////////////////////////
                // inserta los detalles de la fabricacion
                List<Dictionary<string, object>> dets = new List<Dictionary<string, object>>();
                Dictionary<string, object> det;
                rowsdet = dtdet.Select("ID=" + mid.ToString());
                foreach (DataRow drdet in rowsdet)
                {
                    det = new Dictionary<string, object>();
                    foreach (DataColumn col in dtdet.Columns)
                    {
                        if (col.ColumnName == "ID")
                            continue;
                        det.Add(col.ColumnName, drdet[col]);
                    }
                    dets.Add(det);
                }
                row.Add("detalleFabricacion", dets);

                /////////////////////////////////////////
                // inserta el encabezado de la factura
                List<Dictionary<string, object>> facs = new List<Dictionary<string, object>>();
                Dictionary<string, object> fac;
                rowsfac = dtfac.Select("ID=" + mid.ToString());
                foreach (DataRow drfac in rowsfac)
                {
                    fac = new Dictionary<string, object>();
                    foreach (DataColumn col in dtfac.Columns)
                    {
                        if (col.ColumnName == "ID")
                            continue;
                        else if (col.ColumnName == "IdFactura")
                            factura = (int)drfac[col];
                        fac.Add(col.ColumnName, drfac[col]);
                    }

                    // inserta los detalles de la factura dentro del encabezado
                    List<Dictionary<string, object>> fdets = new List<Dictionary<string, object>>();
                    Dictionary<string, object> fdet;
                    rowsfacdet = dtfacdet.Select("IdFactura=" + factura.ToString());
                    foreach (DataRow drdet in rowsfacdet)
                    {
                        fdet = new Dictionary<string, object>();
                        foreach (DataColumn col in dtfacdet.Columns)
                        {
                            if (col.ColumnName == "ID")
                                continue;
                            fdet.Add(col.ColumnName, drdet[col]);
                        }
                        fdets.Add(fdet);
                    }
                    fac.Add("detalleFactura", fdets);
                    facs.Add(fac);
                }
                row.Add("encabezadoFactura", facs);
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}
