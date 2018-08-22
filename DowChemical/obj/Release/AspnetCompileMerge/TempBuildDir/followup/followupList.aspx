<%@ Page Title="Follow Up" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="followupList.aspx.vb" Inherits="DowChemical.followupList" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

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
            width: 28px;
            display: block;
            float: left;
            margin-top: 36px;
            margin-left: 1px;
        }

        .statusText {
            width: 80px;
            display: block;
            float: left;
            margin-top: 36px;
            margin-left: 1px;
            font-weight: bold;
        }

        .statusComplete {
            width: 164px;
            display: block;
            float: left;
            margin-top: 27px;
            margin-left: 2px;
            background-color: #fcfcfc;
            border-style: solid;
            border-width: 1px;
            border-color: #cccccc;
            border-radius: 3px;
            padding: 4px 14px 0px 20px;
        }

        .editIcon {
            width: 76px;
            display: block;
            float: left;
            margin-top: 28px;
            margin-left: 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
        <div>
            <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 8px 0 0 16px;">
                Follow Up
            </div>
            <div id="f-leftsidebar">
                <div class="row">
                    <div class="row">
                        <div style="display: block; float: left; width: 74px; margin: 14px 0 0 16px;">
                            <img alt="" src="../Images/avatar.png" />
                        </div>
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
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="padding: 0 0 0 16px; height: 20px;">
                            <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div style="padding: 2px -1px 0px 1px;">
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx" Selected="true">
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
                                        Search
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
                                    <telerik:RadComboBox ID="rcbStatusView" runat="server" Skin="Metro" Width="136px" DataTextField="statusDesc" DataValueField="statusId" AutoPostBack="True" DataSourceID="srcStatus" ItemsPerRequest="0"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnAdvanceSearch" runat="server" Visible="false">
                            <div style="padding: 0px 6px 0px 0px; margin-top: -1px;">
                                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="3">
                                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <div style="height: 56px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                            <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;">
                                                Observer name
                                            </div>
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
                                            <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;">
                                                Responsible Person
                                            </div>
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
                                                <div style="display: block; float: left; width: 158px; margin-top: 16px; text-align: right;">
                                                    From
                                                </div>
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
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 41px; margin-top: 16px; text-align: right;">
                                                    To
                                                </div>
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
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnMonthToMonth" runat="server" Visible="false">
                                                <div style="display: block; float: left; width: 158px; margin-top: 16px; text-align: right;">
                                                    From
                                                </div>
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
                                                <div style="display: block; float: left; width: 41px; margin-top: 16px; text-align: right;">
                                                    To
                                                </div>
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
                                                <div style="display: block; float: left; width: 158px; margin-top: 15px; text-align: right;">
                                                    Observer name :
                                                </div>
                                                <div style="display: block; float: left; width: 380px; margin-top: 8px; margin-left: 15px;">
                                                    <asp:TextBox ID="tbObservedCombine" runat="server" CssClass="form-control input-sm" Width="376px"></asp:TextBox>
                                                </div>
                                                <div style="display: block; float: left; width: 120px; margin-top: 15px; text-align: right;">
                                                    Category :
                                                </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 8px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbCategoryCB" runat="server" Skin="Metro" Width="376px" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True"
                                                        DataSourceID="srcCategory" ToolTip="Category" Enabled="True">
                                                    </telerik:RadComboBox>
                                                </div>
                                                <div style="text-align: right; padding-top: 0px; padding-right: 6px;">
                                                    <asp:ImageButton ID="CloseCombine" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                                </div>
                                                <div style="text-align: right; height: 24px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 7px; text-align: right;">
                                                    Responsible Person :
                                                </div>
                                                <div style="display: block; float: left; width: 380px; margin-top: 0px; margin-left: 15px;">
                                                    <asp:TextBox ID="tbResponCombine" runat="server" CssClass="form-control input-sm" Width="376px"></asp:TextBox>
                                                </div>
                                                <div style="display: block; float: left; width: 120px; margin-top: 7px; text-align: right;">
                                                    Sub Category :
                                                </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 0px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbCategorySubCB" runat="server" Skin="Metro" Width="376px" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True"
                                                        DataSourceID="srcCategorySubCB" ToolTip="Sub Category" Enabled="True">
                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="srcCategorySubCB" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM dbo.tblObsvCateSub WHERE (cateId = @CateId) OR (cateId = '1000')">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="rcbCategoryCB" Name="CateId" PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                                <div style="display: block; float: left; width: 30px; margin-top: 3px; margin-left: 12px;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 13px; text-align: right;">
                                                    Date Range :&nbsp;&nbsp;&nbsp;&nbsp;From
                                                </div>
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
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 35px; margin-top: 13px; text-align: right;">
                                                    To
                                                </div>
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
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                </div>
                                                <div style="display: block; float: left; width: 120px; margin-top: 13px; text-align: right;">
                                                    Failure Point :
                                                </div>
                                                <div style="display: block; float: left; width: 376px; margin-top: 6px; margin-left: 15px;">
                                                    <telerik:RadComboBox ID="rcbFailPointCB" runat="server" Skin="Metro" Width="376px" DataTextField="failpointName" DataValueField="failpointId" AutoPostBack="True"
                                                        DataSourceID="srcFailurePointCB" ToolTip="Failure Point" Enabled="True">
                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="srcFailurePointCB" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = '1000')">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="rcbCategorySubCB" Name="catesubId" PropertyName="SelectedValue" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div style="display: block; float: left; width: 158px; margin-top: 14px; text-align: right;">
                                                </div>
                                                <div style="display: block; float: left; width: 260px; margin-top: 8px; margin-left: 15px;">
                                                    <asp:Button ID="btSearchCombine" runat="server" Height="30px" class="btn btn-default btn-mo31" Text="Search" Width="158px" />
                                                </div>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <%--<telerik:RadPageView ID="RadPageView1" runat="server">
                                    <div style="height: 56px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                        <div style="display: block; float: left; width: 180px; margin-top: 14px; text-align: right;">
                                            Select Department
                                        </div>
                                        <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 15px;">                                            
                                            
                                        </div>
                                        <div style="display: block; float: left; width: 76px; margin-top: 8px;">
                                            <asp:Button ID="SearchDepartment" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="68px" />
                                        </div>
                                        <div style="text-align: right; padding-right: 6px; padding-top: 0px;">
                                            <asp:ImageButton ID="CloseDepartment" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                        </div>
                                        <div style="text-align: right; padding-right: 48px; padding-top: 8px; font-size: 0.95em; height: 24px;">
                                            <asp:CheckBox ID="chkAllDataDepartment" runat="server" Text="&nbsp;&nbsp;Show all data"
                                                CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" />
                                        </div>
                                    </div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView2" runat="server">
                                    <div style="height: 56px; border: 1px solid #DDDDDD; padding: 4px 0px 0px 0px;">
                                        <div style="display: block; float: left; width: 180px; margin-top: 14px; text-align: right;">
                                            Select Action Status
                                        </div>
                                        <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 15px;">
                                            <telerik:RadComboBox ID="rcbStatus" runat="server" Skin="Metro" Width="172px" DataSourceID="srcStatus" DataTextField="statusDesc" DataValueField="statusId" AutoPostBack="True">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div style="display: block; float: left; width: 76px; margin-top: 8px;">
                                            <asp:Button ID="SearchStatus" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="68px" />
                                        </div>
                                        <div style="text-align: right; padding-right: 6px; padding-top: 0px;">
                                            <asp:ImageButton ID="CloseStatus" runat="server" ImageUrl="~/Images/bt_close.png" ToolTip="close" />
                                        </div>
                                        <div style="text-align: right; padding-right: 48px; padding-top: 8px; font-size: 0.95em; height: 24px;">
                                            <asp:CheckBox ID="chkAllDataStatus" runat="server" Text="&nbsp;&nbsp;Show all data"
                                                CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" />
                                        </div>
                                    </div>
                                </telerik:RadPageView>--%>
                                </telerik:RadMultiPage>
                            </div>
                        </asp:Panel>
                    </telerik:RadAjaxPanel>
                </div>
                <div style="padding: 0px 6px 0px 0px;">
                    <asp:SqlDataSource ID="srcCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT cateId, cateName FROM tblObsvCate"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="srcContractor" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblContractor.* FROM tblContractor"></asp:SqlDataSource>
                    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                        <telerik:RadGrid ID="rgRecordList" runat="server" Skin="Metro" PageSize="20" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%">
                            <MasterTableView DataKeyNames="detailId" AllowMultiColumnSorting="False" Name="ParentGrid">
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
                                            <div style="padding-right: 0; padding-left: 8px;">
                                                <div style="display: block; float: left; width: 96px;">
                                                    <asp:Button ID="imgbFollowUp" runat="server" CssClass="btn btn-xs btn-default" Text="FOLLOW UP" CommandName="edit" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="96px" />
                                        <ItemStyle Width="96px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Action Number" SortExpression="recActNo" UniqueName="actionno">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lbActionNumber" runat="server" Text='<%# Eval("recActNo") %>'></asp:Label>
                                            </div>
                                            <telerik:RadToolTip ID="RadToolTip2" runat="server" Position="TopLeft" RelativeTo="Element" TargetControlID="lbActionNumber" OffsetY="-4" OffsetX="4" ShowDelay="100" AutoCloseDelay="8000">
                                                <div style="color: steelblue; padding: 0 8px;">
                                                    <%--<div class="row" style="height: 32px; border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #999999;">
                                                        <div style="display: block; float: left; width: 40px; margin-top: 4px;"><img alt="" src="../Images/user-24-gray68.png" /></div>
                                                        <div style="display: block; float: left; width: 104px;">
                                                            <div class="row" style="font-size: x-small">Created by</div>
                                                            <div class="row">
                                                                <asp:Label ID="lb0" runat="server" Text='<%# Eval("empDisplay") %>' Font-Bold="True" ForeColor="#666666"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row" style="padding-top: 4px">
                                                        <div style="display: block; float: left; width: 64px;">Date :</div>
                                                        <div style="display: block; float: left; width: 80px;">
                                                            <asp:Label ID="lb1" runat="server" Text='<%# Eval("recActDate", "{0:d}") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div style="display: block; float: left; width: 64px;">Time :</div>
                                                        <div style="display: block; float: left; width: 80px;">
                                                            <asp:Label ID="lb2" runat="server" Text='<%# Eval("recActTime") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div style="display: block; float: left; width: 64px;">Duration :</div>
                                                        <div style="display: block; float: left; width: 80px;">
                                                            <asp:Label ID="lb3" runat="server" Text='<%# Eval("durationValue") %>'></asp:Label>
                                                            min.
                                                        </div>
                                                    </div>
                                                </div>
                                            </telerik:RadToolTip>
                                        </ItemTemplate>
                                        <HeaderStyle Width="140px" HorizontalAlign="Left" />
                                        <ItemStyle Width="140px" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="observItem" HeaderText="Observe No." SortExpression="observItem" UniqueName="observitem">
                                        <HeaderStyle Width="104px" HorizontalAlign="Left" />
                                        <ItemStyle Width="104px" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="title" HeaderText="Title" SortExpression="title" UniqueName="title">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="StatusColumn" HeaderText="Action Status">
                                        <ItemTemplate>
                                            <div style="position: relative; top: 2px; right: 0px;">
                                                <div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:ImageButton ID="imbPropose1" runat="server" ImageUrl="~/Images/status-orange-1-20.png" ToolTip="" CommandName="propose1" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:ImageButton ID="imbPropose2" runat="server" ImageUrl="~/Images/status-orange-2-20.png" ToolTip="" CommandName="propose2" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 50px;">
                                                        <asp:ImageButton ID="imbPropose3" runat="server" ImageUrl="~/Images/status-orange-3-20.png" ToolTip="" CommandName="propose3" />
                                                    </div>
                                                    <div style="display: block; float: left; width: 26px;">
                                                        <asp:Image ID="imbAllPropose" runat="server" ImageUrl="~/Images/status-orange-3-20.png" ToolTip="" CommandName="allpropose" />
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="144px" HorizontalAlign="Left" />
                                        <ItemStyle Width="144px" HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div style="padding: 8px 0px 8px 0px; border-left-style: solid; border-left-width: 8px; border-left-color: #4AB0E1;">
                                            <div class="row" style="padding: 0px 16px 2px 16px">
                                                <div style="display: block; float: left; width: 32px; text-align: right; position: absolute;">
                                                    <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/back-1-32.png" ToolTip="" CommandName="Cancel" />
                                                </div>
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Category : </div>
                                                <div class="col-md-9">
                                                    <div style="display: block; float: left; width: 172px;">
                                                        <telerik:RadComboBox ID="rcbCategory" runat="server" Skin="Metro" Width="172px" DataTextField="cateName" DataValueField="cateId" DataSourceID="srcCategory" Enabled="false" CssClass="readonly"></telerik:RadComboBox>
                                                    </div>
                                                    <div style="display: block; float: left; width: 540px;">
                                                        <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 6px; margin-right: 14px;">Sub Category : </div>
                                                        <div style="display: block; float: left; width: 352px;">
                                                            <telerik:RadComboBox ID="rcbCategorySub" runat="server" Skin="Metro" Width="352px" DataTextField="catesubName" DataValueField="catesubId" Enabled="false"></telerik:RadComboBox>
                                                            <asp:SqlDataSource ID="srcCategorySub" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName, catesubDesc, cateId, IsShow FROM tblObsvCateSub WHERE (cateId = @CateId)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="rcbCategory" Name="CateId" PropertyName="SelectedValue" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 2px 16px 2px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Failure Point : </div>
                                                <div class="col-md-9">
                                                    <div style="display: block; float: left; width: 712px;">
                                                        <telerik:RadComboBox ID="rcbFailurePoint" runat="server" Skin="Metro" Width="712px" DataTextField="failpointName" DataValueField="failpointId" Enabled="false"></telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="srcFailurePoint" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName, failpointDesc, catesubId FROM tblObsvFailPoint WHERE (catesubId = @catesubId)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="rcbCategorySub" Name="catesubId" PropertyName="SelectedValue" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 2px 16px 2px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Equipment/Location : </div>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="tbEquipment" runat="server" CssClass="form-control input-sm readonly" ReadOnly="true" Width="712px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 2px 16px 2px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">&nbsp;&nbsp;</div>
                                                <div class="col-md-9">
                                                    <div>
                                                        <div style="display: block; float: left; width: 180px; padding: 6px 0px 2px 16px;">
                                                            <asp:CheckBox ID="chkHRO" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" Enabled="false" />
                                                        </div>
                                                        <div style="display: block; float: left; width: 180px; padding: 6px 0px 2px 16px;">
                                                            <asp:CheckBox ID="chk2Eye" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" Enabled="false" />
                                                        </div>
                                                        <div style="display: block; float: left; width: 180px; padding: 6px 0px 2px 16px;">
                                                            <asp:CheckBox ID="chkRecognition" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" Enabled="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnHRO1" runat="server" Visible="True">
                                                <div class="row" style="padding: 0px 0px 8px 11px;">
                                                    <div style="display: block; float: left; width: 209px; text-align: right; margin-top: 6px;">&#160;&#160;</div>
                                                    <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                                        <div>
                                                            <asp:CheckBox ID="chkHROop1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" Enabled="False" />
                                                        </div>
                                                        <div>
                                                            <asp:CheckBox ID="chkHROop2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" Enabled="False" />
                                                        </div>
                                                        <div>
                                                            <asp:CheckBox ID="chkHROop3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" Enabled="False" />
                                                        </div>
                                                        <div>
                                                            <asp:CheckBox ID="chkHROop4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" Enabled="False" />
                                                        </div>
                                                        <div>
                                                            <asp:CheckBox ID="chkHROop5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" Enabled="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row" style="padding: 2px 16px 2px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Observed Type : </div>
                                                <div class="col-md-9">
                                                    <div style="display: block; float: left; width: 180px;">
                                                        <telerik:RadComboBox ID="rcbObserveType" runat="server" Skin="Metro" Width="172px" Enabled="false">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div style="display: block; float: left; width: 360px;">
                                                        <telerik:RadComboBox ID="rcbContractor" runat="server" Skin="Metro" Width="532px" Visible="False" Enabled="false" DataTextField="contractorName" DataValueField="contractorId" DataSourceID="srcContractor"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnShowImage" runat="server" Visible="false">
                                                <div class="row" style="padding: 0px 16px 2px 16px">
                                                    <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Picture : </div>
                                                    <div class="col-md-9">
                                                        <div style="display: block; float: left; width: 360px;">
                                                        </div>
                                                        <div style="display: block; float: left; width: 200px;">
                                                        </div>
                                                        <div class="col-md-12" style="margin: 8px 0px 0px -14px;">
                                                            <asp:DataList ID="PictureList" runat="server" RepeatDirection="Horizontal">
                                                                <ItemTemplate>
                                                                    <div style="display: block; float: left; width: 178px;">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                                        <telerik:RadToolTip ID="RadToolTip1" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image1">
                                                                            <asp:Image ID="ImageView1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="520px" />
                                                                        </telerik:RadToolTip>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row" style="padding: 6px 16px 4px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Description : </div>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control input-sm readonly" ReadOnly="true" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 4px 16px 4px 16px">
                                                <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Propose Action #1</div>
                                                <div class="col-md-9">
                                                    <div style="display: block; float: left; width: 360px;">
                                                        <asp:TextBox ID="tbActionA" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                    <div style="display: block; float: left; width: 352px;">
                                                        <div style="display: block; float: left; width: 352px;">
                                                            <asp:TextBox ID="tbResponA" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                        </div>
                                                        <div class="statusIcon">
                                                            <asp:HiddenField ID="hfStatusA" runat="server" Value='<%# Eval("proposeStatus_A") %>' />
                                                            <asp:Image ID="imgStatusA" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                                        </div>
                                                        <div class="statusText">
                                                            <asp:Label ID="lbStatusA" runat="server" Text="STATUS"></asp:Label>
                                                        </div>
                                                        <div class="editIcon">
                                                            &nbsp;
                                                        </div>
                                                        <asp:Panel ID="pnCompleteA" runat="server">
                                                            <div class="statusComplete">
                                                                <asp:CheckBox ID="chkCompleteA" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="false" />
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div style="display: block; float: left; width: 20px; margin-top: 58px;">
                                                        <asp:ImageButton ID="editCompleteA" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" OnClick="editCompleteA_Click" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnResponB" runat="server" Visible="false">
                                                <div class="row" style="padding: 4px 16px 4px 16px">
                                                    <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Propose Action #2</div>
                                                    <div class="col-md-9">
                                                        <div style="display: block; float: left; width: 360px;">
                                                            <asp:TextBox ID="tbActionB" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div style="display: block; float: left; width: 352px;">
                                                            <div style="display: block; float: left; width: 352px;">
                                                                <asp:TextBox ID="tbResponB" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                            </div>
                                                            <asp:HiddenField ID="hfStatusB" runat="server" Value='<%# Eval("proposeStatus_B") %>' />
                                                            <div class="statusIcon">
                                                                <asp:Image ID="imgStatusB" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                                            </div>
                                                            <div class="statusText">
                                                                <asp:Label ID="lbStatusB" runat="server" Text="STATUS"></asp:Label>
                                                            </div>
                                                            <div class="editIcon">
                                                                &nbsp;
                                                            </div>
                                                            <asp:Panel ID="pnCompleteB" runat="server">
                                                                <div class="statusComplete">
                                                                    <asp:CheckBox ID="chkCompleteB" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="false" />
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div style="display: block; float: left; width: 20px; margin-top: 58px;">
                                                            <asp:ImageButton ID="editCompleteB" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" OnClick="editCompleteB_Click" Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnResponC" runat="server" Visible="false">
                                                <div class="row" style="padding: 4px 16px 4px 16px">
                                                    <div style="display: block; float: left; width: 189px; text-align: right; margin-top: 6px;">Propose Action #3</div>
                                                    <div class="col-md-9">
                                                        <div style="display: block; float: left; width: 360px;">
                                                            <asp:TextBox ID="tbActionC" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div style="display: block; float: left; width: 352px;">
                                                            <div style="display: block; float: left; width: 352px;">
                                                                <asp:TextBox ID="tbResponC" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                                            </div>
                                                            <asp:HiddenField ID="hfStatusC" runat="server" Value='<%# Eval("proposeStatus_C") %>' />
                                                            <div class="statusIcon">
                                                                <asp:Image ID="imgStatusC" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                                            </div>
                                                            <div class="statusText">
                                                                <asp:Label ID="lbStatusC" runat="server" Text="STATUS"></asp:Label>
                                                            </div>
                                                            <div class="editIcon">
                                                                &nbsp;
                                                            </div>
                                                            <asp:Panel ID="pnCompleteC" runat="server">
                                                                <div class="statusComplete">
                                                                    <asp:CheckBox ID="chkCompleteC" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="false" />
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div style="display: block; float: left; width: 20px; margin-top: 58px;">
                                                            <asp:ImageButton ID="editCompleteC" runat="server" ImageUrl="~/Images/edit-28-blue.png" ToolTip="Edit Complete Status" OnClick="editCompleteC_Click" Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="row" style="padding: 4px 21px 28px 16px">
                                                <div class="col-md-12">
                                                    <div style="display: block; float: left; width: 189px;">
                                                        &nbsp;
                                                    </div>
                                                    <div style="display: block; float: left; width: 728px;">
                                                        <asp:Panel ID="pnUpdateButton" runat="server">
                                                            <asp:Button ID="btUpdate" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" />&nbsp;
                                                            <asp:Button ID="btClose" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Close" CommandName="Cancel" />&nbsp;
                                                            <asp:Button ID="btResendEmail" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="120px" Text="Re-Send Email" OnClick="btResendEmail_Click" />
                                                            <span style="padding-left: 24px;">
                                                                <asp:Label ID="lbUpdateInfo" runat="server" Text="" ForeColor="OrangeRed"></asp:Label></span>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnResendEmail" runat="server" Visible="False">
                                                            <div style="padding: 8px 0px 0px 0px;">
                                                                <asp:SqlDataSource ID="srcSendEmailList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, emailType, email, empId, empFullName, IsSuggest FROM dbo.tblSendEmail WHERE (recId = @recId) AND (emailType &lt; 1010) OR (recId = @recId) AND (emailType &gt; 2020) AND (emailType &lt; 2100)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter Name="recId" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                                <telerik:RadGrid ID="rgEmailList" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" Height="102px" ShowHeader="False" GroupPanelPosition="Top">
                                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                    <MasterTableView>
                                                                        <NoRecordsTemplate>
                                                                            <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                                                        </NoRecordsTemplate>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelectSend" runat="server" Checked='<%# Bind("IsSuggest") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="48px" HorizontalAlign="Right" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" Visible="False">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("empFullName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="280px" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbObserveNo" runat="server" ForeColor="#0067B0" Text="&nbsp;"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="96px" />
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                    </ClientSettings>
                                                                    <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                                                    <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                                                </telerik:RadGrid>
                                                            </div>
                                                            <div style="padding: 0px 0px 0px 0px; margin-top: -2px;">
                                                                <asp:SqlDataSource ID="srcSendListEachOb" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observItem, emailType, email, empId, empFullName, IsSuggest FROM dbo.tblSendEmail WHERE (recId = @recId) AND (observItem = @obItem) AND (emailType &gt; 1100) AND (emailType &lt; 2000)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter Name="recId" />
                                                                        <asp:Parameter Name="obItem" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                                <telerik:RadGrid ID="rgEmailListEachOb" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" Height="162px" ShowHeader="False" GroupPanelPosition="Top" OnItemDataBound="rgEmailListEachOb_ItemDataBound">
                                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                    <MasterTableView>
                                                                        <NoRecordsTemplate>
                                                                            <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                                                        </NoRecordsTemplate>
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelectSend" runat="server" Checked='<%# Bind("IsSuggest") %>' />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="48px" HorizontalAlign="Right" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("empFullName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="280px" />
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbObserveNo" runat="server" ForeColor="#0067B0" Text="OBSERVE 0"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="96px" />
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                    </ClientSettings>
                                                                    <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                                                    <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                                                </telerik:RadGrid>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div style="display: block; float: left; width: 120px; padding-top: 8px;">
                                                        <asp:Button ID="btBackReSend" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="120px" Text="Back" OnClick="btResendEmail_Click" Visible="false" />
                                                        <div style="padding: 6px 0px 0px 0px;">
                                                            <asp:Button ID="btCloseReSend" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="120px" Text="Close" Visible="false" CommandName="Cancel" />
                                                        </div>
                                                        <asp:Panel ID="pnSendButton" runat="server" Visible="false">
                                                            <div style="vertical-align: bottom; padding-top: 166px">
                                                                <asp:Button ID="btSendEmail" runat="server" Height="30px" class="btn btn-primary btn-mo30" Width="120px" Text="Re-Send" OnClick="btSendEmail_Click" />
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                                <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                            <ItemStyle Height="32px" BackColor="#D5DCE3" />
                            <FooterStyle Height="32px" />
                        </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
                    <div class="row" style="padding: 8px 0px 0px 0px">
                        <div class="col-md-6">&nbsp;</div>
                        <div class="col-md-6">
                            <div class="pull-right" style="display: block;">
                                <div style="display: block; float: left; width: 24px;">
                                    <img alt="" src="../Images/status-blue-0-20.png" />
                                </div>
                                <div style="display: block; float: left; width: 100px;">= Recognition</div>
                                <div style="display: block; float: left; width: 24px;">
                                    <img alt="" src="../Images/status-orange-0-20.png" />
                                </div>
                                <div style="display: block; float: left; width: 100px;">= In-progress</div>
                                <div style="display: block; float: left; width: 24px;">
                                    <img alt="" src="../Images/status-green-0-20.png" />
                                </div>
                                <div style="display: block; float: left; width: 100px;">= Complete</div>
                            </div>
                        </div>
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
            <telerik:AjaxSetting AjaxControlID="rcbStatusView">
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
                    <img src="../Images/msgbox-info.png" alt="" />
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
