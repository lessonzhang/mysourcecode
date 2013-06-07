<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myVideo.aspx.cs" Inherits="myVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="centerdiv"><embed src="<%= this.myPath%>"  type='application/x-shockwave-flash' allowFullScreen='true' width="600px" height="400px" allowNetworking='all' allowScriptAccess='always'></embed>
        </div>
        
    </form>
</body>
</html>
