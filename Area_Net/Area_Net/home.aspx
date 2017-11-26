<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Area_Net.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Area Home</title>
    <!--Import Google Icon Font-->
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!--Import materialize.css-->
    <link type="text/css" rel="stylesheet" href="css/materialize.min.css" media="screen,projection" />
    <link type="text/css" rel="stylesheet" href="css/home.css">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,900" rel="stylesheet"> 
    <!--Let browser know website is optimized for mobile-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
		<style>
			.page-footer {
			background-color: black!important;
			}
		</style>
</head>
<body>
    <!--Import jQuery before materialize.js-->
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="js/materialize.min.js"></script>
    <form id="form1" runat="server">
        <div>
            <div style="background: linear-gradient(#616161  -10%,#fcc05d 120%); display: block; background-color: #e4e2df;">
                <section class="splashscreen valign-wrapper">
                    <div class="container">
                        <h1 class="center-align">AREA.net</h1>
                        <h3 class="center-align">Connectez vos sites préférés de manère rapide et flexible.</h3>
                        <div id="links" class="center-align">
                            <a class="btn" style="background-color: #26a69a!important; border-radius: 2px!important" href="Register.aspx">Register</a>
                            <a class="btn" style="background-color: #26a69a!important; border-radius: 2px!important" href="Login.aspx">Sign-In!   </a>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </form>
    <footer class="page-footer grey darken-3">
          <div class="container">
            <div class="row">
              <div class="col l6 s12">
                <h5 class="white-text">AREA DOTNET</h5>
                <p class="grey-text text-lighten-4">A project for Epitech 3rd year</p>
              </div>
              <div class="col l4 offset-l2 s12">
                <h5 class="white-text">Made By</h5>
                <ul>
                  <li><a class="grey-text text-lighten-3" href="#!">Paul Belloc</a></li>
                  <li><a class="grey-text text-lighten-3" href="#!">Marko Mitrovic</a></li>
                  <li><a class="grey-text text-lighten-3" href="#!">François Gayraud</a></li>
                  <li><a class="grey-text text-lighten-3" href="#!">Dylan Dinh</a></li>
                  <li><a class="grey-text text-lighten-3" href="#!">Antoine El Samra</a></li>
                </ul>
              </div>
            </div>
          </div>
          <div class="footer-copyright grey darken-4">
            <div class="container">
            © 2017 Copyright
            </div>
          </div>
        </footer>
</body>
</html>
