<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpTemplate.aspx.vb" Inherits="DowChemical.rpTemplate" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .RadListView_Bootstrap .rlvI, .RadListView_Bootstrap .rlvA {
            font-family: 'Leelawadee', 'Segoe UI', Arial, Verdana, Helvetica, sans-serif !important;
            font-size: 0.85em !important;
            padding: 0px !important;
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
                <div id="f-header-setting" style="font-size: 1.6em; padding: 8px 0 0 16px;">Report Test</div>
                <div id="f-leftsidebar">
                    <div style="height: 100px">
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
                            <div style="padding: 2px 0 0 16px;">
                                <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding: 2px -1px 0px 1px;">
                        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True" PreventCollapse="True" Selected="true">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx" Selected="true">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Data Participation">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Safety Category">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Other Categories">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Department">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="EZ Path by Custom">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Off-Hour Observation">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Contractor Performance">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Historical Data">
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Export to Excel" NavigateUrl="~/Report/exportToExcel.aspx">
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="SETTING" Height="36px" NavigateUrl="~/em/setUser.aspx" Visible="false">
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>
                    </div>
                    <div style="height: 8px; border-top-style: solid; border-top-width: 1px; border-top-color: #A8A8A8;"></div>
                </div>
                <div id="content">
                    <div class="row LeelawadeeFont">
                        <div style="border-style: none none solid none; height: 63px; border-bottom-color: #DDDDDD; border-bottom-width: 1px; padding-top: 12px; padding-right: 0px; padding-left: 0px;">
                            <div class="row">
                                <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">Department</div>
                                <div style="display: block; float: left; width: 154px; margin-top: 8px; margin-left: 15px;">
                                    <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="153px" DataTextField="departName" DataValueField="departId" DataSourceID="srcDepartment" ItemsPerRequest="0"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 80px; margin-top: 16px; text-align: right;">Year</div>
                                <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 15px;">
                                    <telerik:RadComboBox ID="rcbYear" runat="server" Skin="Metro" Width="80px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                            <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                            <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                            <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                            <telerik:RadComboBoxItem runat="server" Text="2021" Value="2021" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 120px; padding-top: 8px; padding-left: 8px;">
                                    <asp:Button ID="btViewReport" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="View Report" Width="150px" />
                                </div>
                                <div style="text-align: right; padding-right: 12px; padding-top: 8px;">
                                    <asp:Button ID="btGenReport" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Generate Report" Width="150px" />
                                </div>
                            </div>
                            <div class="row" style="margin-top: 14px; padding-bottom: 1px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #DDDDDD;">
                                <div style="display: block; float: left; width: 22%; margin-top: 7px; text-align: right; padding-right: 8px;">
                                    <div>
                                        <asp:Label ID="rpIdLabel" runat="server" Text="&nbsp;" />
                                    </div>
                                    <div>
                                        <asp:Label ID="pLifeNearMissLabel" runat="server" Text="p-Life Near Miss Incident (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="PSCE_ContainmentLabel" runat="server" Text="PSCE_Containment Loss (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="PSCE_PSNMLabel" runat="server" Text="PSCE_PSNM (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="leadershipVisibilityLabel" runat="server" Text="Leadership Visibility (FSFL) (all)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="secondEyeLabel" runat="server" Text="2nd eye (all)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="injuryNearMissLabel" runat="server" Text="Injury Near Miss (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="proactiveComplianceLabel" runat="server" Text="Proactive Compliance (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="actionTotalLabel" runat="server" Text="Action Total" />
                                    </div>
                                    <div>
                                        <asp:Label ID="actionCompletetedLabel" runat="server" Text="Action Completeted" />
                                    </div>
                                    <div>
                                        <asp:Label ID="recognitionLabel" runat="server" Text="Recognition" />
                                    </div>
                                    <div>
                                        <asp:Label ID="reliabilityLabel" runat="server" Text="Reliability (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="qualityLabel" runat="server" Text="Quality (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="HROLabel" runat="server" Text="HRO (1st)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="percentOFILabel" runat="server" Text="%OFI Identified" />
                                    </div>
                                    <div>
                                        <asp:Label ID="offHourRateLabel" runat="server" Text="Off Hour Participation (count/total)" />
                                    </div>
                                    <div>
                                        <asp:Label ID="offHourPercentLabel" runat="server" Text="Off Hour Participation (%)" />
                                    </div>
                                </div>
                                <div style="display: block; float: left; width: 78%;">
                                    <telerik:RadListView ID="RadListView1" runat="server" DataKeyNames="rpId" DataSourceID="srcPerformance" Skin="Bootstrap" CssClass="LeelawadeeFont" Font-Names="Leelawadee">
                                        <LayoutTemplate>
                                            <div class="RadListView RadListView_Bootstrap">
                                                <table>
                                                    <tr>
                                                        <td id="itemPlaceholder" runat="server"></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <td class="rlvI" style="width: 6%">
                                                <div style="background-color: #c9c9c9; padding: 3px 4px 3px 4px;">
                                                    <asp:Label ID="monthDescLabel" runat="server" Text='<%# Eval("monthDesc") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="pLifeNearMissLabel" runat="server" Text='<%# Eval("pLifeNearMiss") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="PSCE_ContainmentLabel" runat="server" Text='<%# Eval("PSCE_Containment") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="PSCE_PSNMLabel" runat="server" Text='<%# Eval("PSCE_PSNM") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="leadershipVisibilityLabel" runat="server" Text='<%# Eval("leadershipVisibility") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="secondEyeLabel" runat="server" Text='<%# Eval("secondEye") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="injuryNearMissLabel" runat="server" Text='<%# Eval("injuryNearMiss") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="proactiveComplianceLabel" runat="server" Text='<%# Eval("proactiveCompliance") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="actionTotalLabel" runat="server" Text='<%# Eval("actionTotal") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="actionCompletetedLabel" runat="server" Text='<%# Eval("actionCompleteted") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="recognitionLabel" runat="server" Text='<%# Eval("recognition") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="reliabilityLabel" runat="server" Text='<%# Eval("reliability") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="qualityLabel" runat="server" Text='<%# Eval("quality") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="HROLabel" runat="server" Text='<%# Eval("HRO") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="percentOFILabel" runat="server" Text='<%# Eval("percentOFI") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="offHourRateLabel" runat="server" Text='<%# Eval("offHourRate") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="offHourPercentLabel" runat="server" Text='<%# Eval("offHourPercent") %>' />
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <td class="rlvA" style="width: 6%">
                                                <div style="background-color: #c9c9c9; padding: 3px 4px 3px 4px;">
                                                    <asp:Label ID="monthDescLabel" runat="server" Text='<%# Eval("monthDesc") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="pLifeNearMissLabel" runat="server" Text='<%# Eval("pLifeNearMiss") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="PSCE_ContainmentLabel" runat="server" Text='<%# Eval("PSCE_Containment") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="PSCE_PSNMLabel" runat="server" Text='<%# Eval("PSCE_PSNM") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="leadershipVisibilityLabel" runat="server" Text='<%# Eval("leadershipVisibility") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="secondEyeLabel" runat="server" Text='<%# Eval("secondEye") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="injuryNearMissLabel" runat="server" Text='<%# Eval("injuryNearMiss") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="proactiveComplianceLabel" runat="server" Text='<%# Eval("proactiveCompliance") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="actionTotalLabel" runat="server" Text='<%# Eval("actionTotal") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="actionCompletetedLabel" runat="server" Text='<%# Eval("actionCompleteted") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="recognitionLabel" runat="server" Text='<%# Eval("recognition") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="reliabilityLabel" runat="server" Text='<%# Eval("reliability") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="qualityLabel" runat="server" Text='<%# Eval("quality") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="HROLabel" runat="server" Text='<%# Eval("HRO") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="percentOFILabel" runat="server" Text='<%# Eval("percentOFI") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="offHourRateLabel" runat="server" Text='<%# Eval("offHourRate") %>' />
                                                </div>
                                                <div>
                                                    <asp:Label ID="offHourPercentLabel" runat="server" Text='<%# Eval("offHourPercent") %>' />
                                                </div>
                                            </td>
                                        </AlternatingItemTemplate>
                                        <EmptyDataTemplate>
                                            <div class="RadListView RadListView_Bootstrap">
                                                <div class="rlvEmpty">
                                                    <div>
                                                        <asp:Label ID="monthDescLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="pLifeNearMissLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="PSCE_ContainmentLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="PSCE_PSNMLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="leadershipVisibilityLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="secondEyeLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="injuryNearMissLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="proactiveComplianceLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="actionTotalLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="actionCompletetedLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="recognitionLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="reliabilityLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="qualityLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="HROLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="percentOFILabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="offHourRateLabel" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="offHourPercentLabel" runat="server" Text="" />
                                                    </div>
                                                </div>
                                            </div>
                                        </EmptyDataTemplate>
                                    </telerik:RadListView>
                                    <asp:SqlDataSource ID="srcPerformance" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT monthDesc, pLifeNearMiss, PSCE_Containment, PSCE_PSNM, leadershipVisibility, secondEye, injuryNearMiss, proactiveCompliance, actionTotal, actionCompleteted, recognition, reliability, quality, HRO, percentOFI, offHourRate, offHourPercent, rpId FROM tblRpPerformance WHERE (departid = @departId) AND (year = @year)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbDepartment" Name="departId" PropertyName="SelectedValue" />
                                            <asp:ControlParameter ControlID="rcbYear" Name="year" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 16px;">
                                <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">
                                    Month
                                </div>
                                <div style="display: block; float: left; width: 100px; margin-top: 9px; margin-left: 15px;">
                                    <telerik:RadComboBox ID="rcbMonth" runat="server" Skin="Metro" Width="80px">
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
                                </div>
                                <div style="display: block; float: left; width: 38px; margin-top: 16px; text-align: right;">
                                    Year
                                </div>
                                <div style="display: block; float: left; width: 40px; margin-top: 9px; margin-left: 15px;">
                                    <telerik:RadComboBox ID="rcbSelectYear" runat="server" Skin="Metro" Width="80px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                            <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                            <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                            <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 76px; padding-top: 9px; padding-left: 52px;">
                                    <asp:Button ID="btSearchReport" runat="server" class="btn btn-default btn-mo30" Height="30px" Text="p-Life Near Miss" Width="200px" />
                                </div>
                                <div style="display: block; float: left; padding-top: 9px; padding-left: 140px;">
                                    <asp:Label ID="Label1" runat="server" ForeColor="#0066FF"></asp:Label>
                                </div>
                                <div style="text-align: right; padding-right: 12px; padding-top: 9px;">
                                </div>
                            </div>
                            <div class="row">
                                <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 100px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 38px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 40px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 76px; padding-top: 9px; padding-left: 52px;">
                                    <asp:Button ID="Button1" runat="server" class="btn btn-default btn-mo30" Height="30px" Text="PSCE_Containment Loss" Width="200px" />
                                </div>
                                <div style="display: block; float: left; padding-top: 9px; padding-left: 140px;">
                                    <asp:Label ID="Label2" runat="server" ForeColor="#0066FF"></asp:Label>
                                </div>
                                <div style="text-align: right; padding-right: 12px; padding-top: 9px;">
                                </div>
                            </div>
                            <div class="row">
                                <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 100px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 38px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 40px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 76px; padding-top: 9px; padding-left: 52px;">
                                    <asp:Button ID="Button2" runat="server" class="btn btn-default btn-mo30" Height="30px" Text="p-Life Near Miss" Width="200px" />
                                </div>
                                <div style="display: block; float: left; padding-top: 9px; padding-left: 140px;">
                                    <asp:Label ID="Label3" runat="server" ForeColor="#0066FF"></asp:Label>
                                </div>
                                <div style="text-align: right; padding-right: 12px; padding-top: 9px;">
                                </div>
                            </div>
                            <div class="row">
                                <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 100px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 38px; margin-top: 16px; text-align: right;">
                                </div>
                                <div style="display: block; float: left; width: 40px; margin-top: 9px; margin-left: 15px;">
                                </div>
                                <div style="display: block; float: left; width: 76px; padding-top: 9px; padding-left: 52px;">
                                    <asp:Button ID="Button3" runat="server" class="btn btn-default btn-mo30" Height="30px" Text="p-Life Near Miss" Width="200px" />
                                </div>
                                <div style="display: block; float: left; padding-top: 9px; padding-left: 140px;">
                                    <asp:Label ID="Label4" runat="server" ForeColor="#0066FF"></asp:Label>
                                </div>
                                <div style="text-align: right; padding-right: 12px; padding-top: 9px;">
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" style="padding: 0px 6px 0px 0px;">
                        </div>
                    </div>
                    <div class="row LeelawadeeFont">
                        <div class="col-md-7" style="padding: 16px 0px 16px 0px;">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblRpPerformance.* FROM tblRpPerformance"></asp:SqlDataSource>
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
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
