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

namespace VistasDeaWs
{
    public partial class _default : System.Web.UI.Page
    {
        protected bool esValidoBmx = false;
        protected string cadenaBmx = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (ValSesion("ValAccion"))
            {
                case "EsBmx":
                    esValidoBmx = true;
                    cadenaBmx = ValSesion("CadenaBmx");
                    break;
                default:
                    Response.Write("Hello World!");
                    Response.End();
                    break;
            }
        }

        private string ValSesion(string par)
        {
            if (Session[par] == null)
                return string.Empty;
            return Session[par].ToString();
        }

        protected void MiForma()        
        {
            if (esValidoBmx)
            {
                //<%= cadena %>
                Response.Write("<form action=\"https://boveda.banamex.com.mx/scripts/IBanking/cgiclnt/Ibanking%20-%20Core/ND000_\" method=\"post\" name=\"forma1\">");
                Response.Write("<input type=\"hidden\" name=\"EWF_SYS_0\" value=\"4eebd5b1-3824-11d5-929d-0050dae9973a\"/>");
                Response.Write("<input type=\"hidden\" name=\"EWF_FORM_NAME\" value=\"index\"/>");
                Response.Write("<input type=\"hidden\" name=\"BANKID\" value=\"EDIFY\"/>");
                Response.Write("<input type=\"hidden\" name=\"PRODUCTNAME\" value=\"EBS\"/>");
                Response.Write("<input type=\"hidden\" name=\"EWFBUTTON\" value=\"\"/>");
                Response.Write("<input type=\"hidden\" name=\"EXTRA1\" value=\"SPANISH\"/>");
                Response.Write("<input type=\"hidden\" name=\"EXTRA2\" value=\"" + cadenaBmx + "\"/>");
                Response.Write("<input type=\"hidden\" name=\"EXTRA3\" value=\"\"/>");
                Response.Write("<input type=\"hidden\" name=\"EXTRA4\" value=\"NO_ERROR\"/>");
                Response.Write("<input type=\"hidden\" name=\"LENGUAJEID\" value=\"1\"/>");
                Response.Write("<input type=\"submit\" value=\"ENVIAR\"/>");
                Response.Write("</form>");
            }
        }
    }
}
