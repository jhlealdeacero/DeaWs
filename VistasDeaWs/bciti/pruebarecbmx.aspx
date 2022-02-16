<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebarecbmx.aspx.cs" Inherits="VistasDeaWs.bciti.pruebarecbmx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script>
        function redir()
        {
            forma1.submit();
        }
    </script>    
</head>
<body onload="redir()">
    <form action="deacerpcionciti.aspx" method="post" name="forma1">
    <input type="hidden" name="NUM_CTE" value="0" />
    <input type="hidden" name="REFER_PGO" value="123456" />
    <input type="hidden" name="IMPORTE" value="123.45" />
    <input type="hidden" name="CONCEPTO" value="sin concepto" />
    <input type="hidden" name="FEC_PGO" value="17/04/2018" />
    <input type="hidden" name="FEC_LPGO" value="99/99/9999" />
    <input type="hidden" name="HORA" value="12:34:56 AM" />
    <input type="hidden" name="TPO_ABO" value="1" />
    <input type="hidden" name="AUTORIZA" value="000111" />
    <input type="hidden" name="AUTORIZA2" value="ok" />
    </form>
</body>
</html>
