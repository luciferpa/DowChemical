<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Defaults.aspx.vb" Inherits="DowChemical.Defaults" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="color: dimgray; font-size: 1.8em; font-weight: 600; padding: 68px 0 4px 16px;">
                    Welcome to EZ Path Tool
                </div>
                <div style="color: dimgray; font-size: 1.3em; margin: 0 20px 0 16px; padding: 16px 24px 16px 16px; background-color: #ddebf7">
                    EZ Path Tool is to support Maptaphut Operations staff who observed plant activities in part of safety, quality, reliability and productivity, help follow up and leverage an event both positive and negative impact on processes, people, or environment. 
                </div>
                <br />
                <br />
                <div style="color: dimgray; font-size: 1.3em; padding: 48px 0 0 32px;">
                    Basics EZ Path Tool Guidelines
                </div>
                <div id="f-leftsidebar">
                    <div style="height: 100px">
                        <div class="row">
                            <div style="display: block; float: left; width: 74px; margin: 14px 0 0 16px;">
                                <img alt="" src="../Images/avatar.png" />
                            </div>
                            <asp:Panel ID="pnAvatar" runat="server">
                                <div style="display: block; float: left; width: 120px; margin: 10px 0 0 0; padding-right: 8px;">
                                    <div style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #cd5c5c; padding: 0 0 2px 0;">
                                        <asp:Label ID="lbName" Font-Bold="true" ForeColor="#333333" Font-Size="1.2em" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div style="padding-top: 4px;">
                                        Dow ID :
                                        <asp:Label ID="lbDowId" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div>
                                        Department :
                                        <asp:Label ID="lbDepartName" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="lbAccountType" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div style="padding: 2px 0 0 16px;">
                                <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding: 2px -1px 0px 1px; margin-right: 0px;">
                        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx" Selected="true">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True" >
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Data Participation" NavigateUrl="~/Report/rpDataParticipation.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Department" NavigateUrl="~/Report/rpDepartmentPerform.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Historical Data" NavigateUrl="~/Report/rpHistoricalData.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Export to Excel" NavigateUrl="~/Report/exportToExcel.aspx">
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="SETTING" Height="36px" Expanded="True" NavigateUrl="~/em/setUser.aspx" >
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Text="USER / EMPLOYEE" NavigateUrl="~/em/setUser.aspx?sel=setuser">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="DEPARTMENT" NavigateUrl="~/em/setDepartment.aspx?sel=setdepart">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="CONTRACTOR" NavigateUrl="~/em/setContractors.aspx?sel=setcontractor">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="CATEGORY" NavigateUrl="~/em/setCategory.aspx?sel=cate">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="GOAL SETTING" NavigateUrl="~/em/setGoal.aspx?sel=setgoal" Selected="true">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="OFF HOUR SETTING" NavigateUrl="~/em/setOffHour.aspx?sel=setoffhour">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="IMPORT DATA" NavigateUrl="~/Tools/importFromExcel.aspx?sel=imxls" BeginGroup="True">
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>
                    </div>
                    <div style="height: 8px; border-top-style: solid; border-top-width: 1px; border-top-color: #A8A8A8;"></div>
                </div>
                <div id="content">
                    <div class="row LeelawadeeFont">
                        <div class="col-md-7" style="padding: 16px 0px 16px 0px;">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
