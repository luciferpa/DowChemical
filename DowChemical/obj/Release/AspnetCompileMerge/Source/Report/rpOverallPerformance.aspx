<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/SiteReport.Master" AutoEventWireup="true" CodeBehind="rpOverallPerformance.aspx.vb" Inherits="DowChemical.rpOverallPerformance" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .rpHeader {
            height: 28px;
            background-color: lightskyblue;
            padding-top: 6px;
            padding-left: 12px;
            color: #111111;
        }

        .RadListView_Bootstrap .rlvI, .RadListView_Bootstrap .rlvA {
            font-family: 'Leelawadee', 'Segoe UI', Arial, Verdana, Helvetica, sans-serif !important;
            font-size: 0.85em !important;
            padding: 0px !important;
        }

        .width13col {
            width: 7.68% !important;
        }

        .width14col {
            width: 7.13% !important;
        }

        .rlvItem {
            text-align: right;
            font-size: 0.9em;
            padding: 3px 6px 3px 0px;
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
                <div id="f-header-setting" style="font-size: 1.6em; padding: 8px 0 0 16px;">MTP Operations Overall Performance</div>
                <div id="f-leftsidebar">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-4" style="margin: 14px 0 0 0px; height: 66px;">
                                <img alt="" src="../Images/avatar.png" />
                            </div>
                            <div class="col-md-8" style="margin: 10px 0 0 0px;">
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
                                <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True" Selected="true">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx" Selected="true">
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
                    <div class="row LeelawadeeFont">
                        <div style="border-style: none none solid none; height: 63px; border-bottom-color: #DDDDDD; border-bottom-width: 1px; padding-top: 12px; padding-right: 0px; padding-left: 0px;">
                            <div class="row" style="padding: 0px 6px 0px 0px; margin-right: -15px">
                                <div class="col-md-8">
                                    <div style="display: block; float: left; width: 72px; margin-top: 15px; text-align: right;">Year</div>
                                    <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 8px;">
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
                                    <div style="display: block; float: left; width: 84px; margin-top: 15px; text-align: right;">Department</div>
                                    <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 8px;">
                                        <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId">
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                    </div>
                                    <div style="display: block; float: left; width: 188px; padding-top: 8px;">
                                        <asp:Button ID="btViewReport" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="View Report" Width="180px" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="pull-right" style="display: block; margin-top: 8px;">
                                        <div style="display: block; float: left; width: 32px; padding-right: 2px; text-align: right;">
                                            <asp:ImageButton ID="btGenReportSelMonth" runat="server" Height="30px" Text="Generate Report" ImageUrl="~/Images/select-month-32.png" />
                                        </div>
                                        <span style="padding-right: 8px;">
                                            <asp:Button ID="btGenReport" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Generate Report" Width="132px" Visible="False" />
                                        </span>
                                        <asp:Button ID="btViewChart" runat="server" Height="30px" class="btn btn-default btn-mo32" Text="Chart" Width="120px" PostBackUrl="~/Report/rpOverallPerformChart.aspx" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnSelMonth" runat="server" Visible="false">
                                    <div class="row" style="padding: 4px 8px 8px 8px; margin-top: 4px;">
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-9">
                                            <div class="pull-right" style="display: block; margin-right: -7px; margin-top: 0px; height: 34px;">
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen1" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="JAN" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen2" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="FEB" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen3" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="MAR" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen4" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="APR" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen5" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="MAY" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen6" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="JUN" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen7" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="JUL" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen8" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="AUG" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen9" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="SEP" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen10" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="OCT" Visible="true" />
                                                </span>
                                                <span style="padding-right: 0px; text-align: center">
                                                    <asp:Button ID="Gen11" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="NOV" Visible="true" />
                                                </span>
                                                <span style="text-align: center">
                                                    <asp:Button ID="Gen12" runat="server" Height="30px" class="btn btn-default btn-mo30" Width="56px" Text="DEC" Visible="true" />
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <asp:SqlDataSource ID="srcIndicator" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT rpId, month, monthDesc, year, goalId, totalActionNumber, totalObserve, PSCE_ContainmentLoss, PSCE_PSNM, actionComplete, actionRecognition, percentActionComplete, leadershipVisibility_fsfl, percentLeadershipVisibility, proactiveCompliance, secondEye, injuryNearMiss, reliability_wHRO, quality_wHRO, reliability, percentOFIIden, percentPUPFieldAss, percentLCSFieldAss, offHourCount, percentOffHour, lastUpdate FROM tblRpOverallOps WHERE (year = @year) AND (departId = @departId) AND (rpType &lt;&gt; 'g')">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="rcbYear" Name="year" PropertyName="SelectedValue" />
                                <asp:ControlParameter ControlID="rcbDepartment" Name="departId" PropertyName="SelectedValue" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div class="row" style="margin-top: 1px; padding-bottom: 1px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #DDDDDD;">
                            <div style="padding: 0px 0px 1px 1px;">
                                <div class="rpHeader">INDICATORS</div>
                            </div>
                            <div style="display: block; float: left; width: 20%; margin-top: 0px; text-align: right; padding-right: 2px;">
                                <div class="rlvItem">
                                    &nbsp;
                                </div>
                                <div class="rlvItem">
                                    Total Action Number
                                </div>
                                <div class="rlvItem">
                                    Total No. of Observe
                                </div>
                                <div class="rlvItem">
                                    Total Number of Action Completed
                                </div>
                                <div class="rlvItem">
                                    Total Number of recognitions
                                </div>
                                <div class="rlvItem">
                                    PSCE_Containment Loss (1st)
                                </div>
                                <div class="rlvItem">
                                    PSCE_PSNM (1st)
                                </div>
                                <div class="rlvItem">
                                    % Action Completed
                                </div>
                                <div class="rlvItem">
                                    % Leadership Visibility (FSFL) (all)
                                </div>
                                <div class="rlvItem">
                                    Proactive Compliance (1st)
                                </div>
                                <div class="rlvItem">
                                    2nd eye (all)
                                </div>
                                <div class="rlvItem">
                                    Injury/Illness Near Miss (1st)
                                </div>
                                <div class="rlvItem">
                                    Reliability (HRO) (1st)
                                </div>
                                <div class="rlvItem">
                                    Quality (HRO) (1st)
                                </div>
                                <div class="rlvItem">
                                    Reliability (1st)
                                </div>
                                <div class="rlvItem">
                                    % OFI Identified
                                </div>
                                <div class="rlvItem">
                                    % PUP Field Assessment
                                </div>
                                <div class="rlvItem">
                                    % LCS Field Assessment
                                </div>
                                <div class="rlvItem">
                                    Off Hour Participation (count/total)
                                </div>
                                <div class="rlvItem">
                                    % Off Hour Participation
                                </div>
                            </div>
                            <div style="display: block; float: left; width: 80%;">
                                <telerik:RadListView ID="rlvIndicator" runat="server" DataKeyNames="rpId" DataSourceID="srcIndicator" Skin="Bootstrap" CssClass="LeelawadeeFont" Width="100%" ConvertEmptyStringToNull="False">
                                    <LayoutTemplate>
                                        <div class="RadListView RadListView_Bootstrap">
                                            <table style="width: 99.4%;">
                                                <tr>
                                                    <td id="itemPlaceholder" runat="server"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <td class="rlvI width13col">
                                            <asp:HiddenField ID="hfMonth" runat="server" Value='<%# Eval("month") %>' />
                                            <asp:HiddenField ID="hfYear" runat="server" Value='<%# Eval("year") %>' />
                                            <div style="background-color: #c9c9c9; padding: 3px 4px 3px 4px; text-align: center;">
                                                <asp:Label ID="monthDescLabel" runat="server" Text='<%# Eval("monthDesc") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionLabel" runat="server" Text='<%# Eval("totalActionNumber") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalObserveLabel" runat="server" Text='<%# Eval("totalObserve") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionCompleteLabel" runat="server" Text='<%# Eval("actionComplete") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionRecogLabel" runat="server" Text='<%# Eval("actionRecognition") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="PSCE_ContainmentLossLabel" runat="server" Text='<%# Eval("PSCE_ContainmentLoss") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="PSCE_PSNMLabel" runat="server" Text='<%# Eval("PSCE_PSNM") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentActionCompleteLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentActionComplete")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLeadershipVisibilityLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentLeadershipVisibility")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="proactiveComplianceLabel" runat="server" Text='<%# Eval("proactiveCompliance") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="secondEyeLabel" runat="server" Text='<%# Eval("secondEye") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="injuryNearMissLabel" runat="server" Text='<%# Eval("injuryNearMiss") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="reliabilityHROLabel" runat="server" Text='<%# Eval("reliability_wHRO") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="qualityHROLabel" runat="server" Text='<%# Eval("quality_wHRO") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="reliabilityLabel" runat="server" Text='<%# Eval("reliability") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOFIIdenLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentOFIIden")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentPUPFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentPUPFieldAss")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLCSFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentLCSFieldAss")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="offHourLabel" runat="server" Text='<%# Eval("offHourCount") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOffHourLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentOffHour")) %>' />
                                            </div>
                                        </td>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <td class="rlvA width13col">
                                            <asp:HiddenField ID="hfMonth" runat="server" Value='<%# Eval("month") %>' />
                                            <asp:HiddenField ID="hfYear" runat="server" Value='<%# Eval("year") %>' />
                                            <div style="background-color: #c9c9c9; padding: 3px 4px 3px 4px; text-align: center;">
                                                <asp:Label ID="monthDescLabel" runat="server" Text='<%# Eval("monthDesc") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionLabel" runat="server" Text='<%# Eval("totalActionNumber") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalObserveLabel" runat="server" Text='<%# Eval("totalObserve") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionCompleteLabel" runat="server" Text='<%# Eval("actionComplete") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="TotalActionRecogLabel" runat="server" Text='<%# Eval("actionRecognition") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="PSCE_ContainmentLossLabel" runat="server" Text='<%# Eval("PSCE_ContainmentLoss") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="PSCE_PSNMLabel" runat="server" Text='<%# Eval("PSCE_PSNM") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentActionCompleteLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentActionComplete")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLeadershipVisibilityLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentLeadershipVisibility")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="proactiveComplianceLabel" runat="server" Text='<%# Eval("proactiveCompliance") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="secondEyeLabel" runat="server" Text='<%# Eval("secondEye") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="injuryNearMissLabel" runat="server" Text='<%# Eval("injuryNearMiss") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="reliabilityHROLabel" runat="server" Text='<%# Eval("reliability_wHRO") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="qualityHROLabel" runat="server" Text='<%# Eval("quality_wHRO") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="reliabilityLabel" runat="server" Text='<%# Eval("reliability") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOFIIdenLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentOFIIden")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentPUPFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentPUPFieldAss")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLCSFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentLCSFieldAss")) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="offHourLabel" runat="server" Text='<%# Eval("offHourCount") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOffHourLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("percentOffHour")) %>' />
                                            </div>
                                        </td>
                                    </AlternatingItemTemplate>
                                    <EmptyDataTemplate>
                                        <div class="RadListView RadListView_Bootstrap">
                                            <div class="rlvEmpty">
                                            </div>
                                        </div>
                                    </EmptyDataTemplate>
                                </telerik:RadListView>
                            </div>
                        </div>
                        <div class="row LeelawadeeFont">
                            <div class="col-md-7" style="padding: 16px 0px 16px 16px; font-size: smaller;">
                                <div>%Action Completed = 100 x Action Completed /(Action Total - Recognition)</div>
                                <div>Working hour per month = 9800 minute</div>
                            </div>
                            <div class="col-md-5" style="padding: 2px 12px 16px 16px; font-size: smaller; text-align: right;">
                                <asp:Label ID="lbLastUpdate" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
