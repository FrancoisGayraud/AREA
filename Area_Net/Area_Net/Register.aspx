<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebFormsIdentity.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!--Import materialize.css-->
    <link type="text/css" rel="stylesheet" href="css/materialize.min.css" media="screen,projection" />
    <link type="text/css" rel="stylesheet" href="css/home.css">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,900" rel="stylesheet">
		<style>
			body {
			color: black;
			background-color: #616161;
			}
			h4 {
			font-size: 35px;
			text-align: center;
			margin: 5vh 5vh;
			font-style: italic;
            color: #26a69a;
			}
			.registerBtn {
			text-align: center;
			}
			.contain {
			text-align: center;
			}
			.inputArea {
			width: 30vh !important;
			color: white;
			}
			label {
			color: white;
			}
		</style>
</head>
<body style="font-family: Arial, Helvetica, sans-serif; font-size: small">
    <form id="form1" runat="server">
    <div class="contain">
        <h4>REGISTER</h4>
        <p style="color : red; text-align: center;">
            <asp:Literal runat="server" ID="StatusMessage" />
        </p>                
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="UserName" class="inputArea"/>                
            </div>
        </div>
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" class="inputArea"/>                
            </div>
        </div>
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" class="inputArea"/>                
            </div>
        </div>
        <div>
            <div class="registerBtn"=>
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" class="btn"/>
            </div>
        </div>
    </div>
    </form>
</body>
</html>