<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminSite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truong day nghe phun xam - Admin Site</title>
    <link rel="apple-touch-icon" sizes="57x57" href="~/Favicon/apple-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="60x60" href="~/Favicon/apple-icon-60x60.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="~/Favicon/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="76x76" href="~/Favicon/apple-icon-76x76.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="~/Favicon/apple-icon-114x114.png" />
    <link rel="apple-touch-icon" sizes="120x120" href="~/Favicon/apple-icon-120x120.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="~/Favicon/apple-icon-144x144.png" />
    <link rel="apple-touch-icon" sizes="152x152" href="~/Favicon/apple-icon-152x152.png" />
    <link rel="apple-touch-icon" sizes="180x180" href="~/Favicon/apple-icon-180x180.png" />
    <link rel="icon" type="image/png" sizes="192x192" href="~/Favicon/android-icon-192x192.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="~/Favicon/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="96x96" href="~/Favicon/favicon-96x96.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="~/Favicon/favicon-16x16.png" />
    <link rel="manifest" href="~/Favicon/manifest.json" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />
    <script type="text/javascript" src="Scripts/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.validate.min.js"></script>

    
    <link rel="stylesheet" type="text/css" href="Content/Login.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnLogin").click(function () {
                ValidatePage();
            });
        });

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }

            if (Page_IsValid) {
                // do something
                __doPostBack("", "");
            }
            
            return false;
        }
    </script>
</head>
<body>
    <div class="wrapper">
      <form id="form1" runat="server" class="login">
        <p class="title">Đăng Nhập</p>
          <label id="lbError" runat="server" style="display: block; color: red; margin-top: -20px; margin-bottom: 10px;"></label>
        <asp:TextBox ID="tbxUsername" runat="server" placeholder="Tên đăng nhập"/>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbxUsername" CssClass="required" SetFocusOnError="true" ValidationGroup="Login"></asp:RequiredFieldValidator>
        <i class="fa fa-user"></i>
        <asp:TextBox ID="tbxPassword" runat="server" placeholder="Mật khẩu" TextMode="Password"/>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbxPassword" SetFocusOnError="true" CssClass="required" ValidationGroup="Login"></asp:RequiredFieldValidator>
        <i class="fa fa-key"></i>
        <%--<a href="#">Forgot your password?</a>--%>
        <button id="btnLogin" runat="server" validationgroup="Login">
          <i class="spinner"></i>
          <span class="state">Đăng nhập</span>
        </button>
      </form>
    </div>
</body>
</html>
