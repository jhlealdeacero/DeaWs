<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebarecbcomer.aspx.cs" Inherits="VistasDeaWs.bbva.pruebarecbcomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script>
        function redir()
        {
            forma1.submit();
        }
    </script>    
</head>
<body onload="redir()">
    <form action="dearecbcomer.aspx" method="post" name="forma1">
    <input type="hidden" name="codigo" value="0" />
    <input type="hidden" name="mensaje" value="pago exitoso" />
    <input type="hidden" name="autorizacion" value="1223434456" />
    <input type="hidden" name="referencia" value="REF01" />
    <input type="hidden" name="importe" value="15600" />
    <input type="hidden" name="mediopago" value="SPEI" />
    <input type="hidden" name="financiado" value="REV" />
    <input type="hidden" name="plazos" value="0" />
    <input type="hidden" name="s_transm" value="1290382389JFHKSDFJ" />
    <input type="hidden" name="hash" value="HJDSDFH8W45YU89DFG789578956389Q7R6W34789563789W4659348563489756234789" />
    <input type="hidden" name="tarjetahabiente" value="JAVIER GUAJARDO" />

    </form>
</body>
</html>
