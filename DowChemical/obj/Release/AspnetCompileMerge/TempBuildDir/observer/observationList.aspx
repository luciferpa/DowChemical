<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="observationList.aspx.vb" Inherits="DowChemical.observationList" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <script lang="javascript" type="text/javascript">
        function OnClientEntryAddingProposeAction(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("You can select only one entry.");
            }
        }
        function OnClientEntryAddingOtherObservers(sender, eventArgs) {
            if (sender.get_entries().get_count() > 4) {
                eventArgs.set_cancel(true);
                alert("You can select only five entry.");
            }
        }
    </script>

    <style type="text/css">
        .statusIcon {
            width: 30px;
            display: block;
            float: left;
            margin-top: 36px;
            margin-left: 1px;
        }

        .statusText {
            width: 160px;
            display: block;
            float: left;
            margin-top: 36px;
            margin-left: 1px;
            font-weight: bold;
        }

        .statusComplete {
            width: 160px;
            display: block;
            float: left;
            margin-top: 27px;
            margin-left: 0px;
            padding: 4px 20px 0px 20px;
            border-style: solid;
            border-width: 1px;
            border-color: #cccccc;
            border-radius: 3px;
        }

        .editIcon {
            width: 28px;
            display: block;
            float: left;
            margin-top: 28px;
            margin-left: 0px;
        }

        .alignleft {
            float: left;
        }

        .alignright {
            float: right;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <script src="../../Scripts/confirmscripts.js"></script>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
        <div>
            <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 8px 0 0 16px;">
                Observation
            </div>
            <div id="f-leftsidebar">
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
                    <div class="col-md-12" style="padding: 0 0 0 16px; height: 20px;">
                        <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                    </div>
                </div>
                <div style="padding: 2px -1px 0px 1px;">
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx" Selected="true">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True">
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
                            <telerik:RadPanelItem runat="server" Text="SETTING" Height="36px" Visible="false">
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
                <div style="padding: 8px 0px 0px 0px;">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel2">
                        <div class="row" style="padding: 0px 6px 0px 16px; margin-right: -15px">
                            <asp:SqlDataSource ID="srcStatus" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT statusId, statusDesc, sort_idx1 FROM tblStatus ORDER BY sort_idx1"></asp:SqlDataSource>
                            <div class="col-md-7">
                                <asp:Panel ID="pnNormalSearch" runat="server">
                                    <div style="display: block; float: left; width: 120px; text-align: right; margin-top: 6px;">
                                        Action Number Search
                                    </div>
                                    <div style="display: block; float: left; width: 28px; margin-top: 0px; padding-left: 6px;">
                                        <img alt="" src="../Images/search-28-normal.png" />
                                    </div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 236px;">
                                            <asp:TextBox ID="tbSearchKeyword" runat="server" CssClass="form-control input-sm" Width="228px"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 76px;">
                                            <asp:Button ID="btSearchBox" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="68px" />
                                        </div>
                                        <div style="display: block; float: left; width: 40px; margin-top: 3px; margin-left: 312px; position: absolute">
                                            <asp:ImageButton ID="imgbClearKeyword" runat="server" ImageUrl="~/Images/bt_close-24.png" ToolTip="clear" Visible="false" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnAdvanceSearchMenu" runat="server" Visible="false">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="3" Skin="Bootstrap" MultiPageID="RadMultiPage1">
                                        <Tabs>
                                            <telerik:RadTab runat="server" Text="Observed by">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Responsible Person">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Date Range">
                                            </telerik:RadTab>
                                            <telerik:RadTab runat="server" Text="Combine Search" Selected="True">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </asp:Panel>
                            </div>
                            <div class="col-md-5" style="padding-bottom: 4px;">
                                <div class="pull-right" style="display: block;">
                                    <span style="padding-right: 5px;">
                                        <asp:Button ID="btAdvanceSeach" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Advance Search" /></span>
                                    <span style="padding-right: 4px;">
                                        <telerik:RadComboBox ID="rcbDepartmentView" runat="server" Skin="Metro" Width="153px" DataTextField="departName" DataValueField="departId" AutoPostBack="True" DataSourceID="srcDepartment" ItemsPerRequest="0"></telerik:RadComboBox>
                                    </span>
                                    <asp:Button ID="btNewObserv" runat="server" Height="30px" class="btn btn-primary btn-mo30" Text="New Observation" PostBackUrl="~/observer/observer.aspx" Width="136px" />
                                    <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnAdvanceSearch" runat="server" Visible="false">
                            <div style="padding: 0px 6px 0px 0px; margin-top: -1px;">
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="3">
                                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <div style="height: 56px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                            <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;">Observer name </div>
                                            <div style="display: block; float: left; width: 276px; margin-top: 8px; margin-left: 15px;">
                                                <asp:TextBox ID="tbObserved" runat="server" CssClass="form-control input-sm" Width="268px"></asp:TextBox>
                                            </div>
                                            <div style="display: block; float: left; width: 80px; margin-top: 8px;">
                                                <asp:Button ID="btSearchObserved" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="76px" />
                                            </div>
                                            <div style="display: block; float: left; width: 76px; margin-top: 8px;">
                                                <asp:Button ID="btClearObserved" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Clear" Width="60px" />
                                            </div>
                                            <div style="display: block; float: left; margin-top: 14px;">
                                                <asp:Label ID="lbInfoObserved" runat="server" Text="" ForeColor="#cd5c5c"></asp:Label>
                                            </div>
                                            <div style="text-align: right; padding-right: 6px; padding-top: 0px;">
                                                <asp:ImageButton ID="CloseObserve" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                            </div>
                                            <div style="text-align: right; padding-right: 48px; padding-top: 8px; font-size: 0.95em; height: 24px;">
                                                <asp:CheckBox ID="chkAllDataObserved" runat="server" Text="&nbsp;&nbsp;Show all data"
                                                    CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" Visible="false" />
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView2" runat="server">
                                        <div style="height: 56px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                            <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;">Responsible Person </div>
                                            <div style="display: block; float: left; width: 276px; margin-top: 8px; margin-left: 15px;">
                                                <asp:TextBox ID="tbRespon" runat="server" CssClass="form-control input-sm" Width="268px"></asp:TextBox>
                                            </div>
                                            <div style="display: block; float: left; width: 80px; margin-top: 8px;">
                                                <asp:Button ID="btSearchRespon" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="76px" />
                                            </div>
                                            <div style="display: block; float: left; width: 76px; margin-top: 8px;">
                                                <asp:Button ID="btCloseRespon" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Clear" Width="60px" />
                                            </div>
                                            <div style="display: block; float: left; margin-top: 14px;">
                                                <asp:Label ID="lbInfoRespon" runat="server" Text="" ForeColor="#cd5c5c"></asp:Label>
                                            </div>
                                            <div style="text-align: right; padding-right: 6px; padding-top: 0px;">
                                                <asp:ImageButton ID="CloseRespon" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                            </div>
                                            <div style="text-align: right; padding-right: 48px; padding-top: 8px; font-size: 0.95em; height: 24px;">
                                                <asp:CheckBox ID="chkAllDataRespon" runat="server" Text="&nbsp;&nbsp;Show all data"
                                                    CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" Visible="false" />
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView3" runat="server">
                                        <div style="height: 56px; border: 1px solid #DDDDDD; padding: 2px 0px 0px 0px;">
                                            <asp:Panel ID="pnDateRange" runat="server">
                                                <div style="display: block; float: left; width: 158px; margin-top: 16px; text-align: right;">From </div>
                                                <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 15px;">
                                                    <telerik:RadDatePicker ID="rdpDocDateFrom" runat="server" Skin="Bootstrap" Width="160px" Culture="en-US">
                                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                                        <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj">
                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 41px; margin-top: 16px; text-align: right;">To </div>
                                                <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 15px;">
                                                    <telerik:RadDatePicker ID="rdpDocDateTo" runat="server" Skin="Bootstrap" Width="160px" Culture="en-US">
                                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                                        <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj">
                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnMonthToMonth" runat="server" Visible="false">
                                                <div style="display: block; float: left; width: 158px; margin-top: 16px; text-align: right;">From </div>
                                                <div style="display: block; float: left; width: 180px; margin-top: 10px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbSelMonthFrm" runat="server" Skin="Metro" Width="76px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="JAN" Value="1" />
                                                            <telerik:RadComboBoxItem runat="server" Text="FEB" Value="2" />
                                                            <telerik:RadComboBoxItem runat="server" Text="MAR" Value="3" />
                                                            <telerik:RadComboBoxItem runat="server" Text="APR" Value="4" />
                                                            <telerik:RadComboBoxItem runat="server" Text="MAY" Value="5" />
                                                            <telerik:RadComboBoxItem runat="server" Text="JUN" Value="6" />
                                                            <telerik:RadComboBoxItem runat="server" Text="JUL" Value="7" />
                                                            <telerik:RadComboBoxItem runat="server" Text="AUG" Value="8" />
                                                            <telerik:RadComboBoxItem runat="server" Text="SEP" Value="9" />
                                                            <telerik:RadComboBoxItem runat="server" Text="OCT" Value="10" />
                                                            <telerik:RadComboBoxItem runat="server" Text="NOV" Value="11" />
                                                            <telerik:RadComboBoxItem runat="server" Text="DEC" Value="12" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <span style="padding-left: 3px;">
                                                        <telerik:RadComboBox ID="rcbSelYearFrm" runat="server" Skin="Metro" Width="76px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </span>
                                                </div>
                                                <div style="display: block; float: left; width: 41px; margin-top: 16px; text-align: right;">To </div>
                                                <div style="display: block; float: left; width: 180px; margin-top: 10px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbSelMonthTo" runat="server" Skin="Metro" Width="76px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="JAN" Value="1" />
                                                            <telerik:RadComboBoxItem runat="server" Text="FEB" Value="2" />
                                                            <telerik:RadComboBoxItem runat="server" Text="MAR" Value="3" />
                                                            <telerik:RadComboBoxItem runat="server" Text="APR" Value="4" />
                                                            <telerik:RadComboBoxItem runat="server" Text="MAY" Value="5" />
                                                            <telerik:RadComboBoxItem runat="server" Text="JUN" Value="6" />
                                                            <telerik:RadComboBoxItem runat="server" Text="JUL" Value="7" />
                                                            <telerik:RadComboBoxItem runat="server" Text="AUG" Value="8" />
                                                            <telerik:RadComboBoxItem runat="server" Text="SEP" Value="9" />
                                                            <telerik:RadComboBoxItem runat="server" Text="OCT" Value="10" />
                                                            <telerik:RadComboBoxItem runat="server" Text="NOV" Value="11" />
                                                            <telerik:RadComboBoxItem runat="server" Text="DEC" Value="12" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <span style="padding-left: 3px;">
                                                        <telerik:RadComboBox ID="rcbSelYearTo" runat="server" Skin="Metro" Width="76px">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                                                <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </span>
                                                </div>
                                            </asp:Panel>
                                            <div style="display: block; float: left; width: 92px; padding-top: 10px;">
                                                <asp:Button ID="btSearchFromTo" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="76px" />
                                            </div>
                                            <div style="display: block; float: left; margin-top: 16px;">
                                                <asp:Label ID="lbInfoFromTo" runat="server" Text="" ForeColor="#cd5c5c"></asp:Label>
                                            </div>
                                            <div style="text-align: right; padding-right: 6px; padding-top: 2px;">
                                                <asp:ImageButton ID="CloseFromTo" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                            </div>
                                            <div style="text-align: right; padding-right: 168px; padding-top: 6px; font-size: 1em; height: 24px;">
                                                <asp:CheckBox ID="chkMonthToMonth" runat="server" Text="&nbsp;&nbsp;Month to Month"
                                                    CssClass="chkBT2m" Font-Size="1em" AutoPostBack="True" Checked="False" />
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView4" runat="server">
                                        <div style="height: 168px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 15px; text-align: right;">Observer name : </div>
                                                <div style="display: block; float: left; width: 380px; margin-top: 8px; margin-left: 15px;">
                                                    <asp:TextBox ID="tbObservedCombine" runat="server" CssClass="form-control input-sm" Width="376px"></asp:TextBox>
                                                </div>
                                                <div style="display: block; float: left; width: 100px; margin-top: 15px; text-align: right;">Category : </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 8px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbCategoryCB" runat="server" Skin="Metro" Width="376px" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True"
                                                        DataSourceID="srcCategory" ToolTip="Category">
                                                    </telerik:RadComboBox>
                                                </div>
                                                <div style="text-align: right; padding-top: 0px; padding-right: 6px;">
                                                    <asp:ImageButton ID="CloseCombine" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                                </div>
                                                <div style="text-align: right; height: 24px;"></div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 7px; text-align: right;">Responsible Person : </div>
                                                <div style="display: block; float: left; width: 380px; margin-top: 0px; margin-left: 15px;">
                                                    <asp:TextBox ID="tbResponCombine" runat="server" CssClass="form-control input-sm" Width="376px"></asp:TextBox>
                                                </div>
                                                <div style="display: block; float: left; width: 100px; margin-top: 7px; text-align: right;">Sub Category : </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 0px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbCategorySubCB" runat="server" Skin="Metro" Width="376px" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True"
                                                        DataSourceID="srcCategorySubCB" ToolTip="Sub Category">
                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="srcCategorySubCB" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = '1000')">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="rcbCategoryCB" Name="CateId" PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 13px; text-align: right;">Date Range :&nbsp;&nbsp;&nbsp;&nbsp;From </div>
                                                <div style="display: block; float: left; width: 168px; margin-top: 4px; margin-left: 15px;">
                                                    <telerik:RadDatePicker ID="rdpFromCombine" runat="server" Skin="Bootstrap" Width="160px" Culture="en-US">
                                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                                        <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj">
                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 35px; margin-top: 13px; text-align: right;">To </div>
                                                <div style="display: block; float: left; width: 162px; margin-top: 4px; margin-left: 15px;">
                                                    <telerik:RadDatePicker ID="rdpToCombine" runat="server" Skin="Bootstrap" Width="160px" Culture="en-US">
                                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                                        <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj">
                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                        </DateInput><DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 100px; margin-top: 13px; text-align: right;">Failure Point : </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 6px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbFailPointCB" runat="server" Skin="Metro" Width="376px" DataTextField="failpointName" DataValueField="failpointId" AutoPostBack="True"
                                                        DataSourceID="srcFailurePointCB" ToolTip="Failure Point">
                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="srcFailurePointCB" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = '1000')">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="rcbCategorySubCB" Name="catesubId" PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;"></div>
                                                <div style="display: block; float: left; width: 260px; margin-top: 8px; margin-left: 15px;">
                                                    <asp:Button ID="btSearchCombine" runat="server" Height="30px" class="btn btn-default btn-mo31" Text="Search" Width="158px" />
                                                </div>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                        </asp:Panel>
                    </telerik:RadAjaxPanel>
                </div>
                <div style="padding: 0px 6px 0px 0px;">
                    <asp:SqlDataSource ID="srcCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT cateId, cateName, cateDesc FROM tblObsvCate"></asp:SqlDataSource>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                        <telerik:RadGrid ID="rgRecordList" runat="server" Skin="Metro" PageSize="20" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%">
                            <MasterTableView DataKeyNames="recId" AllowMultiColumnSorting="False" Name="ParentGrid">
                                <NoRecordsTemplate>
                                    <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="ActionColumn">
                                        <HeaderTemplate>
                                            <div style="height: 25px">
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="padding-right: 0; padding-left: 16px;">
                                                <asp:ImageButton ID="imgbEdit" runat="server" CommandName="edit" ImageUrl="~/Images/pen-checkbox-24-gray36.png" ToolTip="Edit" />
                                                <asp:HiddenField ID="hfEmpId" runat="server" Value='<%# Bind("empId") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="recActNo" HeaderText="Action Number" SortExpression="recActNo" UniqueName="actionId">
                                        <HeaderStyle Width="140px" HorizontalAlign="Left" />
                                        <ItemStyle Width="140px" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="recActDate" HeaderText="Date" SortExpression="recActDate" UniqueName="actionDate" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TimeHHMM" HeaderText="Time" SortExpression="TimeHHMM" UniqueName="actionTime">
                                        <HeaderStyle Width="72px" HorizontalAlign="Left" />
                                        <ItemStyle Width="72px" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="empFullName" HeaderText="Created by" SortExpression="empFullName" UniqueName="empFullName">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Duration" SortExpression="durationValue" UniqueName="duration" AllowSorting="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDurationH" runat="server" Text='<%# Bind("durationH") %>'></asp:Label>H&nbsp;:&nbsp;<asp:Label ID="lbDurationM" runat="server" Text='<%# Bind("durationM") %>'></asp:Label>M
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="noObserve" HeaderText="No. of Observe" SortExpression="noObserve" UniqueName="noObserve">
                                        <HeaderStyle Width="120px" HorizontalAlign="Left" />
                                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="ActionColumn2" HeaderText="Status">
                                        <ItemTemplate>
                                            <div style="position: relative; top: 2px; right: 0px;">
                                                <div class="col-md-10" style="margin-left: -8px;">
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve1" runat="server" ImageUrl="~/Images/status-orange-1-20.png" ToolTip="" CommandName="observe1" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve2" runat="server" ImageUrl="~/Images/status-orange-2-20.png" ToolTip="" CommandName="observe2" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve3" runat="server" ImageUrl="~/Images/status-orange-3-20.png" ToolTip="" CommandName="observe3" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve4" runat="server" ImageUrl="~/Images/status-orange-4-20.png" ToolTip="" CommandName="observe4" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve5" runat="server" ImageUrl="~/Images/status-orange-5-20.png" ToolTip="" CommandName="observe5" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbObserve6" runat="server" ImageUrl="~/Images/status-orange-6-20.png" ToolTip="" CommandName="observe6" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="pull-right" style="display: block; margin-top: 1px; margin-right: -16px;">
                                                        <asp:ImageButton ID="imbDelete" runat="server" ImageUrl="~/Images/bt_delete-16h16.png" ToolTip="delete" CommandName="Delete" Visibl="False"
                                                            OnClick="btCancelRecord_Click" OnClientClick="confirmAspButton(this); return false;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="262px" HorizontalAlign="Left" />
                                        <ItemStyle Width="262px" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div class="row" style="padding: 20px 16px 4px 16px">
                                            <div style="display: block; float: left; width: 32px; text-align: right; position: absolute; margin-top: -8px;">
                                                <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/back-1-32.png" ToolTip="" CommandName="Cancel" />
                                            </div>
                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">
                                                Observed Department
                                            </div>
                                            <div class="col-md-9">
                                                <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataTextField="departName" DataValueField="departId" AutoPostBack="True" Enabled="False">
                                                </telerik:RadComboBox>
                                                <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 2px 16px 4px 16px">
                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 8px;">
                                                Date
                                            </div>
                                            <div class="col-md-9">
                                                <div style="display: block; float: left; width: 180px;">
                                                    <telerik:RadDatePicker ID="rdpDocDate" runat="server" Skin="Bootstrap" Width="174px" Culture="en-US">
                                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                                        <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj">
                                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                            <FocusedStyle Resize="None"></FocusedStyle>
                                                            <DisabledStyle Resize="None"></DisabledStyle>
                                                            <InvalidStyle Resize="None"></InvalidStyle>
                                                            <HoveredStyle Resize="None"></HoveredStyle>
                                                            <EnabledStyle Resize="None"></EnabledStyle>
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 180px;">
                                                    <div style="display: block; float: left; width: 36px; text-align: right; margin-top: 8px; margin-right: 15px;">
                                                        Time
                                                    </div>
                                                    <div style="display: block; float: left; width: 124px; padding-top: 2px;">
                                                        <div style="display: block; float: left; width: 64px;">
                                                            <telerik:RadComboBox ID="rcbTimeHH" runat="server" Skin="Metro" Width="56px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="07" Value="7" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="08" Value="8" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="09" Value="9" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="00" Value="0" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="01" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="02" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="03" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="04" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="05" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="06" Value="6" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </div>
                                                        <div style="display: block; float: left; width: 56px;">
                                                            <telerik:RadComboBox ID="rcbTimeMM" runat="server" Skin="Metro" Width="56px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="00" Value="0" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="05" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="25" Value="25" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="30" Value="30" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="35" Value="35" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="40" Value="40" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="45" Value="45" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="50" Value="50" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="55" Value="55" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="display: block; float: left; width: 348px;">
                                                    <div style="display: block; float: left; width: 166px; text-align: right; margin-top: 8px; margin-right: 14px;">
                                                        Duration
                                                    </div>
                                                    <div style="display: block; float: left; width: 124px; padding-top: 2px;">
                                                        <div style="display: block; float: left; width: 64px;">
                                                            <telerik:RadComboBox ID="rcbDurationH" runat="server" Skin="Metro" Width="56px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="0h" Value="0" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="1h" Value="1" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="2h" Value="2" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="3h" Value="3" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="4h" Value="4" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="5h" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="6h" Value="6" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="7h" Value="7" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="8h" Value="8" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="9h" Value="9" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="10h" Value="10" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </div>
                                                        <div style="display: block; float: left; width: 56px;">
                                                            <telerik:RadComboBox ID="rcbDurationM" runat="server" Skin="Metro" Width="56px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem runat="server" Text="0m" Value="0" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="5m" Value="5" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="10m" Value="10" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="15m" Value="15" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="20m" Value="20" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="30m" Value="30" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="40m" Value="40" />
                                                                    <telerik:RadComboBoxItem runat="server" Text="50m" Value="50" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="display: block; float: left; width: 20px; margin-top: 4px; margin-left: 8px;">
                                                    <asp:ImageButton ID="imgAddEntry" runat="server" ImageUrl="~/Images/blank2h2.png"></asp:ImageButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 4px 16px 4px 16px">
                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">
                                                Other Observers
                                            </div>
                                            <div class="col-md-9">
                                                <div style="display: block; float: left; width: 712px;">
                                                    <telerik:RadAutoCompleteBox ID="racObservBox" runat="server" Width="712px" RenderMode="Lightweight" Skin="Bootstrap"
                                                        DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage=""
                                                        MaxResultCount="5" OnClientEntryAdding="OnClientEntryAddingOtherObservers" AllowCustomEntry="True" AutoPostBack="True">
                                                        <TokensSettings AllowTokenEditing="true" />
                                                    </telerik:RadAutoCompleteBox>
                                                    <%--<asp:Panel ID="pnEditObserver" runat="server" Visible="false">
                                                        <div style="padding: 8px 16px 2px 0px">
                                                            <telerik:RadAutoCompleteBox ID="racObservBox1" runat="server" Width="712px" RenderMode="Lightweight" Skin="Bootstrap"
                                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage=""
                                                                MaxResultCount="5" OnClientEntryAdding="OnClientEntryAddingOtherObservers" AllowCustomEntry="True" AutoPostBack="True">
                                                                <TokensSettings AllowTokenEditing="true" />
                                                            </telerik:RadAutoCompleteBox>
                                                        </div>
                                                    </asp:Panel>--%>
                                                    <asp:HiddenField ID="hfOtherObCount" runat="server" Value="0" />
                                                </div>
                                                <div style="display: block; float: left; width: 20px; margin-top: 1px; margin-left: 0px;">
                                                    <asp:ImageButton ID="editOthObserve" runat="server" ImageUrl="~/Images/edit-28-blue.png" CommandName="EditObs" OnClick="editOthObserve_Click" />
                                                </div>
                                                <asp:HiddenField ID="hfEmpIdSelect" runat="server" Value="0" />
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 4px 0px 20px 16px">
                                            <asp:Panel ID="pnUpdateButtonMaster" runat="server">
                                                <div class="col-md-12">
                                                    <div style="display: block; float: left; width: 169px;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="display: block; float: left; width: 728px;">
                                                        <asp:Button ID="btUpdate" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" CausesValidation="true" />&nbsp;
                                                        <asp:Button ID="btClose" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Close" CommandName="Cancel" />
                                                        <span style="padding-left: 24px;">
                                                            <asp:Label ID="lbUpdateInfo" runat="server" Text="" ForeColor="OrangeRed"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="detailId, recId" Name="Observe" Width="100%">
                                        <DetailTables>
                                        </DetailTables>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="detailId" HeaderText="detailId" UniqueName="detailId" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="ActionColumn">
                                                <HeaderTemplate>
                                                    <div style="height: 25px">
                                                    </div>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="18px" />
                                                <ItemStyle Width="18px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="observItem" HeaderText="">
                                                <ItemTemplate>
                                                    <div style="display: block; float: left; width: 96px;">
                                                        <asp:Button ID="imgbMore" runat="server" CssClass="btn btn-xs btn-default" Text="EDIT OBSERVE" CommandName="edit" />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="96px" />
                                                <ItemStyle Width="96px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="observItem" HeaderText="Observ No" UniqueName="observItem" AllowSorting="false">
                                                <HeaderStyle Width="72px" HorizontalAlign="Center" />
                                                <ItemStyle Width="72px" HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="title" HeaderText="Title" SortExpression="title" UniqueName="title">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="ActionColumn2" HeaderText="&nbsp;Propose Status">
                                                <ItemTemplate>
                                                    <div style="position: relative; top: 2px; right: 0px;">
                                                        <div class="col-md-10" style="margin-left: -12px;">
                                                            <div style="display: block; float: left; width: 26px;">
                                                                <asp:HiddenField ID="hfProposeStatus_A" runat="server" Value='<%# Bind("proposeStatus_A") %>' />
                                                                <asp:Image ID="imgActionA" runat="server" ImageUrl="~/Images/msg-orange-0-20.png" />
                                                            </div>
                                                            <div style="display: block; float: left; width: 26px;">
                                                                <asp:HiddenField ID="hfProposeStatus_B" runat="server" Value='<%# Bind("proposeStatus_B") %>' />
                                                                <asp:Image ID="imgActionB" runat="server" ImageUrl="~/Images/msg-orange-0-20.png" />
                                                            </div>
                                                            <div style="display: block; float: left; width: 26px;">
                                                                <asp:HiddenField ID="hfProposeStatus_C" runat="server" Value='<%# Bind("proposeStatus_C") %>' />
                                                                <asp:Image ID="imgActionC" runat="server" ImageUrl="~/Images/msg-orange-0-20.png" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="pull-right" style="display: block;">
                                                                <div style="display: block; float: left; width: 20px;">
                                                                    <asp:HiddenField ID="hfObservComplete" runat="server" Value='<%# Bind("observComplete") %>' />
                                                                    <asp:Image ID="imbObserve" runat="server" ImageUrl="~/Images/status-orange-20.png" ToolTip="" CommandName="imbObserve" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="232px" HorizontalAlign="Left" />
                                                <ItemStyle Width="232px" HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div style="padding: 8px 0px 8px 0px; border-left-style: solid; border-left-width: 8px; border-left-color: #4AB0E1;">
                                                    <div class="row" style="padding: 0px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 32px; position: absolute;">
                                                            <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/back-1-32.png" ToolTip="" CommandName="Cancel" />
                                                            <asp:HiddenField ID="hfRecId" runat="server" Value='<%# Eval("recId")%>' />
                                                            <asp:HiddenField ID="hfObsItem" runat="server" Value='<%# Eval("observItem")%>' />
                                                        </div>
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Title</div>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Category</div>
                                                        <div class="col-md-9">
                                                            <div style="display: block; float: left; width: 172px;">
                                                                <telerik:RadComboBox ID="rcbCategory" runat="server" Skin="Metro" Width="172px" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" OnSelectedIndexChanged="rcbCategory_SelectedIndexChanged" EnableItemCaching="True"></telerik:RadComboBox>
                                                                <asp:SqlDataSource ID="srcCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT cateId, cateName, cateDesc FROM dbo.tblObsvCate WHERE (cateId &lt;&gt; 1000)"></asp:SqlDataSource>
                                                            </div>
                                                            <div style="display: block; float: left; width: 540px;">
                                                                <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 6px; margin-right: 14px;">Sub Category</div>
                                                                <div style="display: block; float: left; width: 352px;">
                                                                    <telerik:RadComboBox ID="rcbCategorySub" runat="server" Skin="Metro" Width="352px" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" OnSelectedIndexChanged="rcbCategorySub_SelectedIndexChanged" EnableItemCaching="True"></telerik:RadComboBox>
                                                                    <asp:SqlDataSource ID="srcCategorySub" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM dbo.tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="rcbCategory" Name="CateId" PropertyName="SelectedValue" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Failure Point</div>
                                                        <div class="col-md-9">
                                                            <div style="display: block; float: left; width: 712px;">
                                                                <telerik:RadComboBox ID="rcbFailurePoint" runat="server" Skin="Metro" Width="712px" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                                                <asp:SqlDataSource ID="srcFailurePoint" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM dbo.tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="rcbCategorySub" Name="catesubId" PropertyName="SelectedValue" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Equipment/Location</div>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="tbEquipment" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">&nbsp;&nbsp;</div>
                                                        <div class="col-md-9">
                                                            <div>
                                                                <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                                                    <asp:CheckBox ID="chkHRO" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" AutoPostBack="True" OnCheckedChanged="chkHRO_CheckedChanged" />
                                                                </div>
                                                                <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                                                    <asp:CheckBox ID="chk2Eye" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                                                </div>
                                                                <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                                                    <asp:CheckBox ID="chkRecognition" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnCheckedChanged="chkRecognition_CheckedChanged" Enabled="False" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnHRO1" runat="server" Visible="True">
                                                        <div class="row" style="padding: 0px 0px 8px 11px;">
                                                            <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">&#160;&#160;</div>
                                                            <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                                                <div>
                                                                    <asp:CheckBox ID="chkHROop1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="chkHROop2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="chkHROop3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="chkHROop4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="chkHROop5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="padding: 2px 16px 2px 16px"></div>
                                                    </asp:Panel>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Observed Type</div>
                                                        <div class="col-md-9">
                                                            <div style="display: block; float: left; width: 180px;">
                                                                <telerik:RadComboBox ID="rcbObserveType" runat="server" Skin="Metro" Width="172px" AutoPostBack="True" OnSelectedIndexChanged="rcbObserveType_SelectedIndexChanged">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                                                        <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                            <div style="display: block; float: left; width: 360px;">
                                                                <telerik:RadComboBox ID="rcbContractor" runat="server" Skin="Metro" Width="532px" Visible="False" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                                                <asp:SqlDataSource ID="srcContractor" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblContractor.* FROM tblContractor ORDER BY contractorName"></asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnShowImage" runat="server" Visible="True">
                                                        <div class="row" style="padding: 4px 16px 4px 16px">
                                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Picture</div>
                                                            <div class="col-md-9">
                                                                <div style="display: block; float: left; width: 360px;">
                                                                </div>
                                                                <div style="display: block; float: left; width: 200px;">
                                                                </div>
                                                                <div class="col-md-12" style="margin: 10px 0px 0px -14px;">
                                                                    <asp:DataList ID="PictureList" runat="server" RepeatDirection="Horizontal">
                                                                        <ItemTemplate>
                                                                            <div style="display: block; float: left; width: 178px;">
                                                                                <div style="position: absolute; margin-left: 160px;">
                                                                                    <asp:ImageButton ID="imbtClose" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" />
                                                                                </div>
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                                                <telerik:RadToolTip ID="RadToolTip" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image1">
                                                                                    <asp:Image ID="ImageView1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="520px" />
                                                                                </telerik:RadToolTip>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                    <asp:SqlDataSource ID="srcPicture" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPictureO WHERE (recId = @recId) AND (observeItem = @observeItem)">
                                                                        <SelectParameters>
                                                                            <asp:Parameter Name="recId" />
                                                                            <asp:Parameter Name="observeItem" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="row" style="padding: 12px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 8px;">Description :</div>
                                                        <div class="col-md-9">
                                                            <div style="display: block; float: left; width: 360px;">
                                                                <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 4px 16px 4px 16px">
                                                        <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Propose Action #1</div>
                                                        <div class="col-md-9">
                                                            <div style="display: block; float: left; width: 360px;">
                                                                <asp:TextBox ID="tbActionA" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                            <div style="display: block; float: left; width: 352px;">
                                                                <div style="display: block; float: left; width: 352px;">
                                                                    <asp:TextBox ID="tbResponA" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                                </div>
                                                                <div class="statusIcon">
                                                                    <asp:Image ID="imgStatusA" runat="server" ImageUrl="~/Images/status-orange-20.png" />
                                                                </div>
                                                                <div class="statusText">
                                                                    <asp:Label ID="lbStatusA" runat="server" Text="STATUS"></asp:Label>
                                                                </div>
                                                                <asp:Panel ID="pnCompleteA" runat="server" Visible="false">
                                                                    <div class="statusComplete">
                                                                        <asp:CheckBox ID="chkCompleteA" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" Enabled="False" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div style="display: block; float: left; width: 20px; margin-top: 58px; margin-left: 4px;">
                                                                <asp:ImageButton ID="editCompleteA" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" Visible="false" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:Panel ID="pnResponB" runat="server" Visible="false">
                                                        <div class="row" style="padding: 4px 16px 4px 16px">
                                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Propose Action #2</div>
                                                            <div class="col-md-9">
                                                                <div style="display: block; float: left; width: 360px;">
                                                                    <asp:TextBox ID="tbActionB" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                                <div style="display: block; float: left; width: 352px;">
                                                                    <div style="display: block; float: left; width: 352px;">
                                                                        <asp:TextBox ID="tbResponB" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                                    </div>
                                                                    <div class="statusIcon">
                                                                        <asp:Image ID="imgStatusB" runat="server" ImageUrl="~/Images/status-orange-20.png" />
                                                                    </div>
                                                                    <div class="statusText">
                                                                        <asp:Label ID="lbStatusB" runat="server" Text="STATUS"></asp:Label>
                                                                    </div>
                                                                    <asp:Panel ID="pnCompleteB" runat="server" Visible="false">
                                                                        <div class="statusComplete">
                                                                            <asp:CheckBox ID="chkCompleteB" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" Enabled="False" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div style="display: block; float: left; width: 20px; margin-top: 58px; margin-left: 4px;">
                                                                    <asp:ImageButton ID="editCompleteB" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnResponC" runat="server" Visible="false">
                                                        <div class="row" style="padding: 4px 16px 4px 16px">
                                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">Propose Action #3</div>
                                                            <div class="col-md-9">
                                                                <div style="display: block; float: left; width: 360px;">
                                                                    <asp:TextBox ID="tbActionC" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                                <div style="display: block; float: left; width: 352px;">
                                                                    <div style="display: block; float: left; width: 352px;">
                                                                        <asp:TextBox ID="tbResponC" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                                    </div>
                                                                    <div class="statusIcon">
                                                                        <asp:Image ID="imgStatusC" runat="server" ImageUrl="~/Images/status-orange-20.png" />
                                                                    </div>
                                                                    <div class="statusText">
                                                                        <asp:Label ID="lbStatusC" runat="server" Text="STATUS"></asp:Label>
                                                                    </div>
                                                                    <asp:Panel ID="pnCompleteC" runat="server" Visible="false">
                                                                        <div class="statusComplete">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" Enabled="False" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div style="display: block; float: left; width: 20px; margin-top: 58px; margin-left: 4px;">
                                                                    <asp:ImageButton ID="chkCompleteC" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" Visible="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="row" style="padding: 4px 16px 24px 16px">
                                                        <asp:Panel ID="pnUpdateButtonDetail" runat="server">
                                                            <div style="display: block; float: left; width: 169px; text-align: right; margin-top: 6px;">&nbsp;</div>
                                                            <div class="col-md-9">
                                                                <span style="float: left; width: 471px;">
                                                                    <asp:Button ID="btUpdate" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" />&nbsp;
                                                                    <asp:Button ID="btClose" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Close" CommandName="Cancel" />
                                                                    <span style="padding-left: 28px;">
                                                                        <asp:Label ID="lbUpdateInfo" runat="server" Text=" " ForeColor="OrangeRed"></asp:Label>
                                                                    </span>
                                                                </span>
                                                                <span style="float: left; width: 175px; top: 0px;">
                                                                    <asp:Panel ID="Panel1" runat="server" Width="175px">
                                                                        <asp:Button ID="btChangeRecog" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Change to Recognition" CommandName="Change" Visible="false" />&nbsp;
                                                                    </asp:Panel>
                                                                    <telerik:RadToolTip ID="RadToolTip1" runat="server" AutoCloseDelay="6000" RelativeTo="Element" Skin="Metro" TargetControlID="btChangeRecog" Position="TopCenter" OffsetX="26">Warning : All data at Propose Action will lose.</telerik:RadToolTip>
                                                                </span>
                                                                <span style="float: left; width: 66px; top: 0px;">
                                                                    <asp:Panel ID="pnDelete" runat="server" Width="66px" Enabled="false">
                                                                        <asp:Button ID="btDelete" runat="server" Height="30px" class="btn btn-danger btn-mo30" Text="Delete" CommandName="Delete" />
                                                                    </asp:Panel>
                                                                </span>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                            <ItemStyle Height="32px" BackColor="#D5DCE3" />
                            <FooterStyle Height="32px" />
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <asp:SqlDataSource ID="srcEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                        SelectCommand="SELECT tblRecord.recId, tblRecord.tempFlag, tblRecord.tempLock, tblRecord.timestamp, tblRecord.aspNetUserId, tblRecord.departId, tblRecord.recActive, tblRecord.recActNo, tblRecord.recActNoValue, tblRecord.recActMonth, tblRecord.recActYear, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationH, tblRecord.durationM, tblRecord.empId, tblRecord.oEmpCount, tblRecord.noObserve, tblEmployee.empFullName, CONVERT (VARCHAR(5), tblRecord.recActTime, 108) AS TimeHHMM FROM tblRecord INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="srcAutoName" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT empId, empDowId, empName, empSurname, empFullName, empEmail, empContact, empMobile, departId, joblvCode, empDisplay FROM tblEmployee WHERE (empEnable = 'true')"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="srcSendEmailList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, emailType, email, empId, empFullName, IsSuggest FROM dbo.tblSendEmail WHERE (recId = @recId) ORDER BY emailType">
                        <SelectParameters>
                            <asp:Parameter Name="recId" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div style="text-align: right; padding-right: 48px; padding-top: 4px; font-size: 0.95em; height: 24px;">
                        <asp:CheckBox ID="cbShow_Del" runat="server" Text="&nbsp;&nbsp;Show delete button"
                            CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" />
                    </div>
                </div>
            </div>
            <br />
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btSearchBox">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgbClearKeyword">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbDepartmentView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btSearchObserved">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btClearObserved">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CloseObserve">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btSearchRespon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btCloseRespon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CloseRespon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btSearchFromTo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="CloseFromTo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btSearchCombine">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Bootstrap" BackgroundPosition="Top"></telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" AutoSize="True" AutoSizeBehaviors="Width" Behaviors="None" Modal="true" Skin="Metro" VisibleStatusbar="false" VisibleTitlebar="false">
        <AlertTemplate>
            <div style="padding: 20px 0 0 0; float: left; width: 100%;">
                <div style="padding: 0 16px 8px 24px; width: 32px; float: left; vertical-align: top;">
                    <img src="../../Images/msgbox-info.png" alt="" />
                </div>
                <div style="padding: 0 16px 0 72px; text-align: left; height: 56px;">
                    {1}
                </div>
                <div style="padding-left: 8px; padding-right: 8px;">
                    <div style="padding: 0 1px 0 12px; text-align: right; border-top-style: solid; border-top-width: 1px; border-top-color: #25A0DA;">
                        <div class="btok_right">
                            <div class="btok_hover">
                                <asp:ImageButton ID="Img_OK" runat="server" ImageUrl="~/Images/ok.png" OnClientClick="$find('{0}').close(true); return false;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
</asp:Content>
