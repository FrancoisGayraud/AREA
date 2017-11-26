<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="Area_Net.user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!--Import materialize.css-->
    <link type="text/css" rel="stylesheet" href="css/materialize.min.css" media="screen,projection" />
    <link type="text/css" rel="stylesheet" href="css/home.css">
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,900" rel="stylesheet">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <style>
        nav .brand-logo {
            display: flex !important;
        }
        .btn {
            margin: 4vh 4vh;
        }
     .logOut {
          text-align: center;
      }
     .buttonLogOut {
         background-color:darkred;
     }
      body {
          background-color: #616161;
      }
      h3, h4 {
          text-align :center;
          margin-top: 20vh;
          margin-bottom: 10vh;
          color: white;
      }
      h3 {
          color: #4db6ac!important ;
          font-style: italic;
      }
      .inputArea {
        width: 30vh !important;
        color: white;
      }
       .contain {
        text-align: center;
      }
       .dropdown {
           width: 25% !important;
           margin: auto;
       }
       .placeholder h5 th{
           color: white;
       }
       table {
           width: 80%;
           margin: auto;
           background-color: #424242;
           border: 3px solid white;
           border-radius : 10px;
           color: white;
       }
       select {
           background-color: #424242 !important;
       }
    </style>
</head>
<body>
    <div class="contain">
    <form id="form1" runat="server">
               <nav>
                <div class="nav-wrapper grey darken-3">
                  <a href="#" class="brand-logo">Area</a>
                </div>
              </nav>
             <h3>
                <asp:Literal runat="server" ID="StatusText" />
            </h3>
        <h5>Your events : </h5>
        <asp:PlaceHolder ID="ActionsPlaceHolder" runat="server"></asp:PlaceHolder>
         <h4>
            Add an event
        </h4>
        <div>
            <asp:Label Text="Process name:" runat="server" AssociatedControlID="ActionName"></asp:Label>
             <div class="input-field col s6">
                <asp:TextBox runat="server" id="ActionName" class="inputArea validate"/>
             </div>
        </div>
       
        <div>
            <p></p>
            <asp:Label Text="Action API" runat="server" AssociatedControlID="ActionApi"></asp:Label>
            <asp:DropDownList CssClass="browser-default dropdown" runat="server" id="ActionApi">
                <asp:ListItem Value="GMail">Gmail</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div>
            <asp:Label Text="Trigger API" runat="server" AssociatedControlID="TriggerApi"></asp:Label>
            <asp:DropDownList CssClass="browser-default dropdown" runat="server" id="TriggerApi">
                <asp:ListItem Value="GMail">Gmail</asp:ListItem>
                <asp:ListItem Value="Crypto"></asp:ListItem>
                <asp:ListItem Value="Twitch">Twitch</asp:ListItem>
                <asp:ListItem Value="League">League</asp:ListItem>
                <asp:ListItem Value="Reddit">Reddit</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label  Text="Action Data" runat="server" AssociatedControlID="ActionData"></asp:Label>
             <div class="input-field col s6">
                <asp:TextBox type="text" class="validate inputArea" runat="server" id="ActionData"/>
            </div>  
        </div>
        <div>
            <asp:Label Text="Trigger Data" runat="server" AssociatedControlID="TriggerData"></asp:Label>
               <div class="input-field col s6">
                   <asp:TextBox class="validate inputArea " runat="server" id="TriggerData"/>
               </div>
        </div>
        <div>
            <asp:Button class="btn" runat="server" Text="Save this action" OnClick="AddAction" />
        </div>
         <h4>
            Remove an event
        </h4>
        <div>
            <asp:Label Text="Action ID" runat="server" AssociatedControlID="todelete"></asp:Label>
                <div class="input-field col s6">
                    <asp:TextBox class="validate inputArea" runat="server" id="todelete"/>
                </div>
        </div>
        <div>
            <asp:Button class="btn" runat="server" Text="Delete this action" OnClick="RemoveAction" />
        </div>
        <asp:PlaceHolder runat="server" ID="LogoutButton">
            <div>
               <div class="logOut">
                  <asp:Button class="btn buttonLogOut" runat="server" OnClick="SignOut" Text="Log out" />
               </div>
            </div>
         </asp:PlaceHolder>
    </form>
        </div>
</body>
</html>
