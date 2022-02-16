<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redirbmx.aspx.cs" Inherits="VistasDeaWs.interbanco.redirbmx" %>

<!DOCTYPE>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script>
function redir() {
<% if (mdebug == 1) { %>    
            alert('Este es un folio de prueba');
            window.location = "../bbva/pruebarecbcomer.aspx";
<% } else { %>            
            forma1.submit();
<% } %>            
        }

    </script>
</head>
<body onload="redir()">
<form action="https://boveda.banamex.com.mx/scripts/IBanking/cgiclnt/Ibanking%20-%20Core/ND000_" method="post" name="forma1">
    <input type="hidden" name="EWF_SYS_0" value="4eebd5b1-3824-11d5-929d-0050dae9973a"/>
    <input type="hidden" name="EWF_FORM_NAME" value="index"/>
    <input type="hidden" name="BANKID" value="EDIFY"/>
    <input type="hidden" name="PRODUCTNAME" value="EBS"/>
    <input type="hidden" name="EWFBUTTON" value=""/>
    <input type="hidden" name="EXTRA1" value="SPANISH"/>
    <input type="hidden" name="EXTRA2" value="<%= cadena %>"/>
    <input type="hidden" name="EXTRA3" value=""/>
    <input type="hidden" name="EXTRA4" value="NO_ERROR"/>
    <input type="hidden" name="LENGUAJEID" value="1"/>    
</form>
</body>
</html>
