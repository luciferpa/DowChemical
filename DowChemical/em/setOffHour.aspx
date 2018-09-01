<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setOffHour.aspx.vb" Inherits="DowChemical.setOffHour" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
        }

        .riTextBox {
            height: 30px !important;
            margin-top: 2px !important;
        }

        .rcTimePopup {
            height: 28px !important;
            margin-top: 0px !important;
        }
    </style>

    <script lang="javascript" type="text/javascript">
        
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 8px 0 0 16px;">
                    Off-Hour Setting
                </div>
                <div id="f-leftsidebar">
                    <div class="row">
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
                            <div class="col-md-12" style="padding: 0 0 0 16px; height: 20px;">
                                <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding: 2px -1px 0px 1px;">
                        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="CREATE NEW OBSERVATION" Height="36px" NavigateUrl="~/observer/observer.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" NavigateUrl="~/Report/rpOverallPerformance.aspx" PreventCollapse="True">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Data Participation" NavigateUrl="~/Report/rpDataParticipation.aspx">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Department" NavigateUrl="~/Report/rpPerformDepartment.aspx">
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
                                        <telerik:RadPanelItem runat="server" Text="OFF HOUR SETTING" NavigateUrl="~/em/setOffHour.aspx?sel=setoffhour" Selected="true">
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
                            <div class="fieldset-box" style="border: 1px solid #CCCCCC;">
                                <div class="row" style="height: 32px; background-color: #f0f0f0;">
                                    <div class="col-md-6" style="font-size: small; font-weight: 600; margin-top: 6px; color: #666666;">Work-Hour Time </div>
                                    <div class="col-md-3" style="padding-top: 4px; padding-bottom: 4px;">
                                    </div>
                                    <div class="col-md-3" style="padding-top: 4px; padding-right: 20px; text-align: right;">
                                        <asp:CheckBox ID="chkEnEdit" runat="server" Text="&nbsp;&nbsp;Enable Edit" CssClass="chkBT2m" Checked="False" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row" style="height: 52px;">
                                    <div class="col-md-3" style="text-align: right; padding-top: 18px">Open</div>
                                    <div class="col-md-3" style="padding-top: 9px;">
                                        <telerik:RadTimePicker ID="rtpOfficeOpen" runat="server" Skin="Bootstrap" Width="120px" Culture="en-US" AutoPostBack="true">
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                            <TimeView runat="server" CellSpacing="-1" Columns="6" Culture="th-TH" Interval="00:30:00">
                                            </TimeView>
                                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadTimePicker>
                                    </div>
                                    <div class="col-md-1" style="text-align: right; padding-top: 18px">Closed</div>
                                    <div class="col-md-3" style="padding-top: 9px;">
                                        <telerik:RadTimePicker ID="rtpOfficeClose" runat="server" Skin="Bootstrap" Width="120px" Culture="en-US" AutoPostBack="true">
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" Visible="False" />
                                            <TimeView runat="server" CellSpacing="-1" Columns="6" Culture="th-TH" Interval="00:30:00">
                                            </TimeView>
                                            <TimePopupButton HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadTimePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="fieldset-box" style="margin-top: 12px; border: 1px solid #CCCCCC;">
                                <div class="row" style="height: 32px; background-color: #f0f0f0;">
                                    <div class="col-md-6" style="font-size: small; font-weight: 600; margin-top: 6px; color: #666666;">Holiday </div>
                                    <div class="col-md-5" style="padding-top: 4px; padding-bottom: 4px;">
                                    </div>
                                    <div class="col-md-1">&nbsp;</div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3" style="text-align: right; padding-top: 18px">Year</div>
                                    <div class="col-md-3" style="padding-top: 11px;">
                                        <telerik:RadComboBox ID="rcbYear" runat="server" Skin="Metro" Width="118px" AutoPostBack="True">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                                <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                                <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                                <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                                <telerik:RadComboBoxItem runat="server" Text="2021" Value="2021" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3" style="text-align: right; padding-top: 13px">Date</div>
                                    <div class="col-md-3" style="padding-top: 4px;">
                                        <telerik:RadDatePicker ID="rdpDate" runat="server" Skin="Bootstrap" Width="180px" Culture="en-US">
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
                                </div>
                                <div class="row">
                                    <div class="col-md-3" style="text-align: right; padding-top: 13px">Description</div>
                                    <div class="col-md-3" style="padding-top: 6px;">
                                        <asp:TextBox ID="tbDesc" runat="server" CssClass="form-control input-sm" Width="384px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="padding-bottom: 20px">
                                    <div class="col-md-3" style="text-align: right; padding-top: 18px">&nbsp;</div>
                                    <div class="col-md-3" style="padding-top: 6px;">
                                        <asp:Button ID="btAddHoliday" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Add Holiday" Width="118px" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height: 20px; padding: 4px 0px 0px 20px;">
                                <asp:Label ID="lbInfo" runat="server" Text="" ForeColor="#CC0000" Font-Size="1em"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-5" style="padding: 16px 20px 16px 0px;">
                            <asp:SqlDataSource ID="srcHoliday" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT holidayId, holidayYear, CONVERT (VARCHAR(11), holidayDate, 106) AS holidayDate, holidayDesc FROM tblOffHourHoliday WHERE (holidayYear = @holidayYear) ORDER BY CONVERT (DAtetime, holidayDate, 1)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="rcbYear" Name="holidayYear" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <telerik:RadGrid ID="rgHolidayList" runat="server" Skin="Metro" PageSize="20" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%"
                                DataSourceID="srcHoliday" HeaderStyle-Height="60px" ShowFooter="True">
                                <MasterTableView DataKeyNames="holidayId" DataSourceID="srcHoliday">
                                    <EditItemTemplate></EditItemTemplate>
                                    <NoRecordsTemplate>
                                        <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <div style="margin-top: -14px; margin-left: -2px; position: absolute;">
                                                    <asp:ImageButton ID="imgbDel" runat="server" ImageUrl="~/Images/bt_delete-24.png" OnClientClick="javascript:if(!confirm('&nbsp;&nbsp;&nbsp;&nbsp;Confirm Delete?')){return false;}" ToolTip="delete" CommandName="Delete" Visible="true" OnClick="imgbDel_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="24px" />
                                            <ItemStyle Width="24px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="departId" DataType="System.Int32" HeaderText="holidayId" UniqueName="holidayId" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="holidayDate" HeaderText="Holiday" UniqueName="holidayDate">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="88px" HorizontalAlign="Left" />
                                            <ItemStyle Width="88px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Description" UniqueName="holidayDesc">
                                            <ItemTemplate>
                                                <asp:Label ID="lbHolidayDesc" runat="server" Text='<%# Eval("holidayDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right; color: #999999;">
                                                    <asp:Label ID="lbTotalItems" runat="server" Text=""></asp:Label>
                                                    items (Holidays)
                                                </div>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <HeaderStyle Height="34px" BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                <HeaderStyle Height="60px" />
                                <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                <FooterStyle Height="30px" />
                            </telerik:RadGrid>
                            <div style="text-align: right; padding-right: 12px; padding-top: 4px; font-size: 0.95em; height: 24px;">
                                <asp:CheckBox ID="cbShow_Del" runat="server" Text="&nbsp;&nbsp;Show delete button"
                                    CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" />
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
