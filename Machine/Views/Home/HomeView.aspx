<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeView.aspx.cs" Inherits="WebForms.Forms.HomeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<style type="text/css">
    div.wrapper {
        width: 450px;
    }

    div.left_block {
        float: left;
        width: 200px;
    }

    div.right_block {
        float: right;
        width: 200px;
    }
</style>
<body>
    <div class="wrapper">
        <div class="left_block">
            <input id="Button1" type="button" value="button" />
        </div>
        <div class="right_block">
            <input id="Button2" type="button" value="button" />
        </div>
    </div>
</body>
</html>
