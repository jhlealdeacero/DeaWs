<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dearecbcomer.aspx.cs" Inherits="VistasDeaWs.bbva.dearecbcomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
        <script>
            function redir() {
                window.location = "https://clientes.deacero.com/Pages/VTA_CU801_Pag3.aspx";
            }
        </script>
<body onload="redir()">
    <form id="form1" runat="server">
    <div>
        <% Respuesta(); %>
    </div>
    </form>
</body>
</html>
