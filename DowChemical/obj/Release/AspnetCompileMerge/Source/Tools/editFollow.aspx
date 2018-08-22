<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="editFollow.aspx.vb" Inherits="DowChemical.editFollow" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/upload.js"></script>
    <style type="text/css">
        .listbox-info {
            border-style: none;
        }

        .RadGrid_Metro .rgDataDiv {
            overflow-x: hidden !important;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 8px 0 0 16px;">
            Import Data
        </div>
        <div id="f-leftsidebar">
            <div style="height: 100px">
                <div class="row">
                    <div style="display: block; float: left; width: 74px; margin: 14px 0 0 16px;">
                        <img alt="" src="../Images/avatar.png" />
                    </div>
                    <asp:Panel ID="pnAvatar" runat="server">
                        <div style="display: block; float: left; width: 120px; margin: 10px 0 0 0; padding-right: 8px;">
                            <div style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #cccccc; padding: 0 0 2px 0;">
                                <asp:Label ID="lbName" Font-Bold="true" ForeColor="#337AB7" Font-Size="1.2em" runat="server" Text=""></asp:Label>
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
            <div style="padding: 2px 0px 0px 1px;">
                <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont">
                    <Items>
                        <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" NavigateUrl="~/Report/rpOverallPerformance.aspx" PreventCollapse="True">
                            <Items>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Data Participation">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Department">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Historical Data" NavigateUrl="~/Report/rpHistoricalData.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Export to Excel" NavigateUrl="~/Report/exportToExcel.aspx">
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="SETTING" Height="36px" Expanded="True" PreventCollapse="True" Selected="true">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="USER / EMPLOYEE" NavigateUrl="~/em/setUser.aspx?sel=setuser">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="DEPARTMENT" NavigateUrl="~/em/setDepartment.aspx?sel=setdepart">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="CONTRACTOR" NavigateUrl="~/em/setContractors.aspx?sel=setcontractor">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="GOAL SETTING" NavigateUrl="~/em/setGoal.aspx?sel=setgoal">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="CATEGORY" NavigateUrl="~/em/setCategory.aspx?sel=cate">
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
            <div style="padding: 8px 0px 0px 0px">
                <div class="row" style="padding: 0px 0px 0px 16px">
                    <div class="col-md-9" style="padding: 12px 0 0 0;">
                        &nbsp;
                    </div>
                    <div class="col-md-3">
                        <div class="pull-right" style="display: block;">
                        </div>
                    </div>
                </div>
                <div class="row" style="padding: 0px 0px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 16px;">Action Number : </div>
                    <div class="col-md-9" style="margin-top: 10px">
                        <div class="row">
                            <div style="display: block; float: left; width: 240px;">
                                <asp:TextBox ID="tbActNumber" runat="server" CssClass="form-control input-sm" placeholder="Select Action Number, Please type here.."></asp:TextBox>
                            </div>
                            <div style="display: block; float: left; width: 200px; padding-left: 8px;">
                                <asp:Button ID="btSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="Search" Width="100px" />
                            </div>
                        </div>
                    </div>
                </div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRecordDetail.detailId, dbo.tblRecordDetail.observItem AS Item, dbo.tblRecordDetail.title AS Title, dbo.tblRecordDetail.secondEye, dbo.tblRecordDetail.recognition AS Recog, dbo.tblRecordDetail.proposeEnable_A AS enA, dbo.tblRecordDetail.proposeAction_A AS actionA, dbo.tblRecordDetail.proposeRespPerson_A AS respA, dbo.tblRecordDetail.proposeStatus_A AS statusA, dbo.tblRecordDetail.proposeComplete_A AS compA, dbo.tblRecordDetail.proposeEnable_B AS enB, dbo.tblRecordDetail.proposeAction_B AS actionB, dbo.tblRecordDetail.proposeRespPerson_B AS respB, dbo.tblRecordDetail.proposeStatus_B AS statusB, dbo.tblRecordDetail.proposeComplete_B AS compB, dbo.tblRecordDetail.proposeEnable_C AS enC, dbo.tblRecordDetail.proposeAction_C AS actionC, dbo.tblRecordDetail.proposeRespPerson_C AS respC, dbo.tblRecordDetail.proposeStatus_C AS statusC, dbo.tblRecordDetail.proposeComplete_C AS compC, dbo.tblRecordDetail.observComplete AS allComplete, dbo.tblRecord.recActNo AS actNo, dbo.tblRecord.recId FROM dbo.tblRecordDetail INNER JOIN dbo.tblRecord ON dbo.tblRecordDetail.recId = dbo.tblRecord.recId WHERE (dbo.tblRecord.recActNo = @recActNo) ORDER BY Item">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="tbActNumber" Name="recActNo" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <div class="row" style="padding: 4px 6px 0px 1px">
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" ClientSettings-Scrolling-AllowScroll="true" Skin="Simple" DataSourceID="SqlDataSource1" GroupPanelPosition="Top">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ClientSettings>
                            <Scrolling AllowScroll="True"></Scrolling>
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="detailId,recId" DataSourceID="SqlDataSource1">
                            <Columns>
                                <telerik:GridBoundColumn DataField="detailId" DataType="System.Int32" FilterControlAltText="Filter detailId column" HeaderText="detailId" ReadOnly="True" SortExpression="detailId" UniqueName="detailId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Item" DataType="System.Int32" FilterControlAltText="Filter Item column" HeaderText="Item" SortExpression="Item" UniqueName="Item">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Title" FilterControlAltText="Filter Title column" HeaderText="Title" SortExpression="Title" UniqueName="Title">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="secondEye" DataType="System.Boolean" FilterControlAltText="Filter secondEye column" HeaderText="secondEye" SortExpression="secondEye" UniqueName="secondEye">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="Recog" DataType="System.Boolean" FilterControlAltText="Filter Recog column" HeaderText="Recog" SortExpression="Recog" UniqueName="Recog">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="enA" DataType="System.Boolean" FilterControlAltText="Filter enA column" HeaderText="enA" SortExpression="enA" UniqueName="enA">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="actionA" FilterControlAltText="Filter actionA column" HeaderText="actionA" SortExpression="actionA" UniqueName="actionA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="respA" DataType="System.Int32" FilterControlAltText="Filter respA column" HeaderText="respA" SortExpression="respA" UniqueName="respA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="statusA" DataType="System.Int32" FilterControlAltText="Filter statusA column" HeaderText="statusA" SortExpression="statusA" UniqueName="statusA">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="compA" DataType="System.Boolean" FilterControlAltText="Filter compA column" HeaderText="compA" SortExpression="compA" UniqueName="compA">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="enB" DataType="System.Boolean" FilterControlAltText="Filter enB column" HeaderText="enB" SortExpression="enB" UniqueName="enB">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="actionB" FilterControlAltText="Filter actionB column" HeaderText="actionB" SortExpression="actionB" UniqueName="actionB">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="respB" DataType="System.Int32" FilterControlAltText="Filter respB column" HeaderText="respB" SortExpression="respB" UniqueName="respB">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="statusB" DataType="System.Int32" FilterControlAltText="Filter statusB column" HeaderText="statusB" SortExpression="statusB" UniqueName="statusB">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="compB" DataType="System.Boolean" FilterControlAltText="Filter compB column" HeaderText="compB" SortExpression="compB" UniqueName="compB">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="enC" DataType="System.Boolean" FilterControlAltText="Filter enC column" HeaderText="enC" SortExpression="enC" UniqueName="enC">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="actionC" FilterControlAltText="Filter actionC column" HeaderText="actionC" SortExpression="actionC" UniqueName="actionC">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="respC" DataType="System.Int32" FilterControlAltText="Filter respC column" HeaderText="respC" SortExpression="respC" UniqueName="respC">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="statusC" DataType="System.Int32" FilterControlAltText="Filter statusC column" HeaderText="statusC" SortExpression="statusC" UniqueName="statusC">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="compC" DataType="System.Boolean" FilterControlAltText="Filter compC column" HeaderText="compC" SortExpression="compC" UniqueName="compC">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="allComplete" DataType="System.Int32" FilterControlAltText="Filter allComplete column" HeaderText="allComplete" SortExpression="allComplete" UniqueName="allComplete">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="actNo" FilterControlAltText="Filter actNo column" HeaderText="actNo" SortExpression="actNo" UniqueName="actNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="recId" DataType="System.Int32" FilterControlAltText="Filter recId column" HeaderText="recId" ReadOnly="True" SortExpression="recId" UniqueName="recId">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div class="row" style="padding: 16px 0px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Select Item :  </div>
                    <div class="col-md-9" style="margin-top: 2px">
                        <div class="row">
                            <div style="display: block; float: left; width: 240px;">
                                <asp:TextBox ID="tbObsItem" runat="server" CssClass="form-control input-sm" placeholder="Please type number of Item."></asp:TextBox>
                            </div>
                            <div style="display: block; float: left; width: 200px; padding-left: 8px;">
                                <asp:Button ID="btChange" runat="server" CssClass="btn btn-sm btn-primary" Text="Change" Width="100px" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding: 16px 0px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;"> </div>
                    <div class="col-md-9" style="margin-top: 2px">
                        <div class="row">
                            <div style="display: block; float: left; width: 240px;">
                                <asp:CheckBox runat="server" ID="chkRecogEn" CssClass="chkBT2m" Text="&nbsp;&nbsp;Change to Recognitionr" />
                            </div>
                            <div style="display: block; float: left; width: 200px;">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding: 0px 0px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;"> </div>
                    <div class="col-md-9" style="margin-top: 2px">
                        <div class="row">
                            <div style="display: block; float: left; width: 240px;">
                                <asp:CheckBox runat="server" ID="chkRecogDis" CssClass="chkBT2m" Text="&nbsp;&nbsp;Cancel Recognitionr" />
                            </div>
                            <div style="display: block; float: left; width: 200px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbtDataSource">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkContinue" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap"></telerik:RadAjaxLoadingPanel>
</asp:Content>
