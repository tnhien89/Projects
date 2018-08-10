<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminSite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Định Tân - Admin Site</title>

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
