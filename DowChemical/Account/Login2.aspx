<%@ Page Title="Log in" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLogIn.Master" CodeBehind="Login2.aspx.vb" Inherits="DowChemical.Login2" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/logIn.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="containerLogin">
        <div style="height: 32px"></div>
        <div id="login-form">
            <h3>Login</h3>
            <fieldset>
                <section id="loginForm">
                    <div style="padding: 4px 20px 8px 20px;">
                        <div style="padding: 0 0 8px 0;"><span class="info">?</span>Use a Account to log in.</div>
                        <div style="padding: 0">
                            <asp:Label runat="server" AssociatedControlID="Username" CssClass="control-label">Username</asp:Label>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="Username" CssClass="form-control input-sm" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username" CssClass="text-danger" ErrorMessage="The username field is required." />
                        </div>
                        <div style="padding: 4px 0 0 0;">
                            <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Password</asp:Label>
                        </div>
                        <div>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control input-sm" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                        <div class="col-md-8" style="padding: 22px 0 0 0;">
                            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                <p class="text-danger">
                                    <asp:Literal runat="server" ID="FailureText" />
                                </p>
                            </asp:PlaceHolder>
                        </div>
                        <div class="col-md-4" style="padding: 0 0 0 0; text-align: right; top: 0px; left: 0px;">
                            <asp:Button runat="server" OnClick="LogIn" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Log in&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" CssClass="btn btn-default" />
                        </div>
                        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" Visible="False">Register as a new user</asp:HyperLink>
                        <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled" Visible="False">Forgot your password?</asp:HyperLink>
                    </div>
                </section>
            </fieldset>
            <div style="font-size: 12px; padding-left: 20px;">
                <div>
                    <asp:CheckBox runat="server" ID="RememberMe" CssClass="chkBT2mLogin" Text=" Remember me" />
                </div>
                <p class="text-danger">
                    <asp:CheckBox runat="server" ID="chkDebugInfo" CssClass="chkBT2mLogin" Text=" redirect test" />
                    <asp:Literal runat="server" ID="lbDebugInfo" />
                </p>
            </div>
        </div>
        <div style="padding: 0 0 0 4px; font-size: 8px; color: #C0C0C0">v.2.0</div>
    </div>
    <div style="height: 824px"></div>
    <section id="socialLoginForm">
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>
</asp:Content>
