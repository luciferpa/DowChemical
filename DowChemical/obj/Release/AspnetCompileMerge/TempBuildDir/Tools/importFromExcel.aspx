<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="importFromExcel.aspx.vb" Inherits="DowChemical.importFromExcel" %>

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
                                <telerik:RadPanelItem runat="server" Text="CATEGORY" NavigateUrl="~/em/setCategory.aspx?sel=cate">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="GOAL SETTING" NavigateUrl="~/em/setGoal.aspx?sel=setgoal">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OFF HOUR SETTING" NavigateUrl="~/em/setOffHour.aspx?sel=setoffhour">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="IMPORT DATA" NavigateUrl="~/Tools/importFromExcel.aspx?sel=imxls" BeginGroup="True" Selected="true">
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
                            <asp:Button ID="btNewImport" runat="server" Height="30px" Width="136px" class="btn btn-primary btn-mo30" Text="New Import" PostBackUrl="~/Tools/importFromExcel.aspx" />
                        </div>
                    </div>
                </div>
                <div class="row" style="padding: 0px 0px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 20px;">Import from Excel File : </div>
                    <div class="col-md-9" style="margin-top: 12px">
                        <div class="row">
                            <div style="display: block; float: left; width: 360px;">
                                <telerik:RadAsyncUpload ID="RadUpload1" runat="server" Height="32px" MaxFileSize="4194304" ChunkSize="4194304" AllowedFileExtensions="xls,xlsx,xlsm"
                                    MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                </telerik:RadAsyncUpload>
                                <asp:Label ID="lbUploadInfo1" Text="File Size limit 1000KB, (xls only)" runat="server" Font-Size="X-Small" />
                            </div>
                            <div style="display: block; float: left; width: 200px;">
                                <asp:Button ID="btUploadXls" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" Width="100px" /><span id="asyncUpload1" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                    <div class="row" style="padding: 8px 16px 4px 0px">
                        <div style="display: block; float: left; width: 176px; text-align: right; margin-top: 32px;"></div>
                        <div class="col-md-2">
                            <div style="padding-top: 4px; padding-left: 0px;">Document's year</div>
                            <div style="padding-top: 8px; padding-left: 0px;">
                                <telerik:RadComboBox ID="rcbYear" runat="server" Skin="Metro" Width="80px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                        <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                        <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                        <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="padding-top: 4px; padding-left: 2px;">Data Source</div>
                            <div style="padding-top: 8px; padding-left: 12px;">
                                <asp:RadioButtonList ID="rbtDataSource" runat="server" Font-Bold="false" CssClass="chkBT2m" AutoPostBack="True">
                                    <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp;VBA Macro Sheet</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp;&nbsp;New MPT Operations Tool</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div style="padding-top: 8px; padding-left: 12px;">
                                <asp:CheckBox runat="server" ID="chkContinue" CssClass="chkBT2m" Text="&nbsp;&nbsp;Continue ActionNumber" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="padding-top: 4px; margin-left: -36px;">Action Number format</div>
                            <div style="padding-top: 8px; margin-left: -26px;">
                                <asp:RadioButtonList ID="rbtActNumberFormat" runat="server" Font-Bold="false" CssClass="chkBT2m">
                                    <asp:ListItem Value="1">&nbsp;&nbsp;RCYYMMDDXXXX</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">&nbsp;&nbsp;RCYYYYMMDDXXXX</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding: 4px 16px 4px 16px">
                        <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 32px;"></div>
                        <div class="col-md-9">
                            <asp:Label ID="lbInfo" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="padding: 4px 16px 4px 16px">
                        <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 32px;"></div>
                        <div class="col-md-5" style="margin-top: 8px">
                            <asp:ListBox ID="infobox" runat="server" CssClass="listbox-info" Width="460px" Height="300px"></asp:ListBox>
                        </div>
                        <div class="col-md-4" style="margin-top: 8px">
                            <asp:TextBox ID="tbError" runat="server" CssClass="listbox-info" Width="300px" Height="300px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" ClientSettings-Scrolling-AllowScroll="true" Skin="Simple">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ClientSettings>
                            <Scrolling AllowScroll="True" ScrollHeight="720px" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </telerik:RadAjaxPanel>
            </div>
        </div>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap"></telerik:RadAjaxLoadingPanel>
</asp:Content>
