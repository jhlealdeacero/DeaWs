<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redirectbco.aspx.cs" Inherits="VistasDeaWs.interbanco.redirectbco" %>

<!DOCTYPE>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Page</title>
    <script>
        function redir() {
            forma1.submit();
        }
    </script>
</head>
<body onload="redir()">
    <form action="https://www.adquiramexico.com.mx:443/mExpress/pago/avanzado" method="post" name="forma1">
    <input type="hidden" name="importe" value="<%= imp.ToString() %>"/>
    <input type="hidden" name="referencia" value="<%= referencia %>"/>
    <input type="hidden" name="urlretorno" value="https://gps.deacero.com/bbva/dearecbcomer.aspx"/>
    <input type="hidden" name="idexpress" value="<%= idEmpresaBco %>"/>
    <input type="hidden" name="financiamiento" value="0"/>
    <input type="hidden" name="plazos" value=""/>
    <input type="hidden" name="mediospago" value="010000"/>
    </form>
</body>
</html>
