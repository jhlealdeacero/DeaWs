<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redirstd.aspx.cs" Inherits="VistasDeaWs.interbanco.redirstd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function redir() {
            santanderForm.submit();
            }
    </script>
</head>
<body onload="redir()">
    <form action="https://www.santander.com.mx/Supernet2007/homeMicrositio.jsp" method="post" name="santanderForm">
        <input  type="hidden" name="url_resp"  value="<%=url_resp %>" />
        <input  type="hidden" name="convenio"  value="<%=convenio %>"/>
        <input  type="hidden" name="referencia"  value="<%=referencia %>" />
        <input  type="hidden" name="importe" value="<%=importe %>"/>
        <%--<input  type="submit" value="Enviar" />--%>
    </form>
</body>
</html>