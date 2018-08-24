<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DowChemical._Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
        }
    </style>
    <style type="text/css">
        .row {
            display: flex;
            text-align: center;
        }

        .column {
            /*flex: 50%;*/
            text-align: center;
            font-size: 1.2em;
        }

        .rpHeader {
            height: 28px;
            background-color: lightskyblue;
            padding-top: 6px;
            padding-left: 12px;
            color: #111111;
            font-size: 1.2em;
            font-weight: bold;
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
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="http://maptaphut.intranet.dow.com/ResponsibleCare/EZPath.htm">See Basics EZ Path Tool Guidelines CLICK</asp:HyperLink>
                </div>
                <br />
                <div class="row">
                    <div class="column" style="flex: 3%;"></div>
                    <div class="column" style="flex: 45%;">
                        <%--start table1--%>
                        <div style="padding: 0px 0px 1px 1px;">
                            <div class="rpHeader" style="background-color: #cccccc;text-align: left;">Observer-My Action Status</div>
                        </div>
                        <div class="row">
                            <div class="column" style="flex: 33%;">xxx</div>
                            <div class="column" style="flex: 33%;">xxx</div>
                            <div class="column" style="flex: 33%;">xxx</div>
                        </div>
                        <div class="row">
                            <div class="column" style="flex: 33%;">Total Action</div>
                            <div class="column" style="flex: 33%;">Completed</div>
                            <div class="column" style="flex: 33%;">In-Progress</div>
                        </div>
                        <br />
                        <div style="padding: 0px 0px 1px 1px;">
                            <div class="rpHeader" style="background-color: #cccccc;text-align: left;">Follow up-My Responsible Owner</div>
                        </div>
                        <div class="row">
                            <div class="column" style="flex: 33%;">xxx</div>
                            <div class="column" style="flex: 33%;">xxx</div>
                            <div class="column" style="flex: 33%;">xxx</div>
                        </div>
                        <div class="row">
                            <div class="column" style="flex: 33%;">Total Observation</div>
                            <div class="column" style="flex: 33%;">Validated</div>
                            <div class="column" style="flex: 33%;">Pending Validation</div>
                        </div>
                        <%--end table1--%>
                    </div>
                    <div class="column" style="flex: 3%;"></div>
                    <div class="column" style="flex: 46%;">
                        <%--start table2--%>
                        <asp:SqlDataSource ID="srcIndicator" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand = "select 1 as rpId, a.empId, a.departId ,MONTH(getdate()) as month, YEAR(getdate()) as year, FORMAT(getdate(), 'MMM') as monthDesc,
(SELECT COUNT(*) FROM tblRecord b WHERE b.recActMonth = MONTH(getdate()) and b.recActYear = YEAR(getdate()) and b.empId = a.empId) as totalActionNumber,
(SELECT COUNT(c2.recActNo) FROM tblRecordDetail c1 INNER JOIN tblRecord c2 ON c1.recId = c2.recId
WHERE c2.recActMonth =  MONTH(getdate()) AND c2.recActYear = YEAR(getdate()) AND c2.empId = a.empId) as totalObserve,
(SELECT SUM(PSCE_ContainmentLoss) FROM tblRpEmpHistorical d1 where d1.empId = a.empId and d1.month = MONTH(getdate()) and d1.year = YEAR(getdate())) as PSCE_ContainmentLoss,
(SELECT SUM(PSCE_PSNM) FROM tblRpEmpHistorical d2 where d2.empId = a.empId and d2.month = MONTH(getdate()) and d2.year = YEAR(getdate())) as PSCE_PSNM,
(SELECT SUM(actionCompleted) FROM tblRpEmpHistorical d3 where d3.empId = a.empId and d3.month = MONTH(getdate()) and d3.year = YEAR(getdate())) as actionComplete,
(SELECT SUM(recognition) FROM tblRpEmpHistorical d4 where d4.empId = a.empId and d4.month = MONTH(getdate()) and d4.year = YEAR(getdate())) as actionRecognition,
(SELECT case when count(leadershipVisibility) = 0 then 0 else SUM(leadershipVisibility) end FROM tblRpEmpHistorical d5 join tblEmployee d51 on d5.empId = d51.empId where d5.empId = a.empId and d5.month = MONTH(getdate()) and d5.year = YEAR(getdate()) and d51.joblvCode = 'fsfl') as leadershipVisibility_fsfl,
(SELECT SUM(proactiveCompliance) FROM tblRpEmpHistorical d6 where d6.empId = a.empId and d6.month = MONTH(getdate()) and d6.year = YEAR(getdate())) as proactiveCompliance,
(SELECT SUM(secondEye) FROM tblRpEmpHistorical d7 where d7.empId = a.empId and d7.month = MONTH(getdate()) and d7.year = YEAR(getdate())) as secondEye,
(SELECT SUM(injuryNearMiss) FROM tblRpEmpHistorical d8 where d8.empId = a.empId and d8.month = MONTH(getdate()) and d8.year = YEAR(getdate())) as injuryNearMiss,
(SELECT SUM(reliability_wHRO) FROM tblRpEmpHistorical d9 where d9.empId = a.empId and d9.month = MONTH(getdate()) and d9.year = YEAR(getdate())) as reliability_wHRO,
(SELECT SUM(quality_wHRO) FROM tblRpEmpHistorical d10 where d10.empId = a.empId and d10.month = MONTH(getdate()) and d10.year = YEAR(getdate())) as quality_wHRO,
(SELECT SUM(reliability) FROM tblRpEmpHistorical d11 where d11.empId = a.empId and d11.month = MONTH(getdate()) and d11.year = YEAR(getdate())) as reliability,
((SELECT COUNT(*) FROM tblEmployee d15 WHERE d15.joblvCode = 'fsfl' AND d15.empEnable = 'true' AND d15.empId &gt; 100000 AND d15.departId = a.departId) * 9800) as ManPowerWorkingHourPerMonth,
(SELECT SUM(procedureUsed) FROM tblRpEmpHistorical d14 where d14.empId = a.empId and d14.month = MONTH(getdate()) and d14.year = YEAR(getdate())) as procedureUsed,
(SELECT SUM(safety) FROM tblRpEmpHistorical d15 where d15.empId = a.empId and d15.month = MONTH(getdate()) and d15.year = YEAR(getdate())) as safety,
(SELECT SUM(LCS) FROM tblRpEmpHistorical d16 where d16.empId = a.empId and d16.month = MONTH(getdate()) and d16.year = YEAR(getdate())) as LCS,
getdate() as lastUpdate, @AllRecogMonth as AllRecog, @CountOffHourMonth as CountOffHour 
from tblEmployee a
where a.empId = @empId
union
select 1 as rpId, a.empId, a.departId ,13 as month, YEAR(getdate()) as year, 'YTD' as monthDesc,
(SELECT COUNT(*) FROM tblRecord b WHERE b.recActYear = YEAR(getdate()) and b.empId = a.empId) as totalActionNumber,
(SELECT COUNT(c2.recActNo) FROM tblRecordDetail c1 INNER JOIN tblRecord c2 ON c1.recId = c2.recId
WHERE c2.recActYear = YEAR(getdate()) AND c2.empId = a.empId) as totalObserve,
(SELECT SUM(PSCE_ContainmentLoss) FROM tblRpEmpHistorical d1 where d1.empId = a.empId and d1.year = YEAR(getdate())) as PSCE_ContainmentLoss,
(SELECT SUM(PSCE_PSNM) FROM tblRpEmpHistorical d2 where d2.empId = a.empId and d2.year = YEAR(getdate())) as PSCE_PSNM,
(SELECT SUM(actionCompleted) FROM tblRpEmpHistorical d3 where d3.empId = a.empId and d3.year = YEAR(getdate())) as actionComplete,
(SELECT SUM(recognition) FROM tblRpEmpHistorical d4 where d4.empId = a.empId and d4.year = YEAR(getdate())) as actionRecognition,
(SELECT case when count(leadershipVisibility) = 0 then 0 else SUM(leadershipVisibility) end FROM tblRpEmpHistorical d5 join tblEmployee d51 on d5.empId = d51.empId where d5.empId = a.empId and d5.year = YEAR(getdate()) and d51.joblvCode = 'fsfl') as leadershipVisibility_fsfl,
(SELECT SUM(proactiveCompliance) FROM tblRpEmpHistorical d6 where d6.empId = a.empId and d6.year = YEAR(getdate())) as proactiveCompliance,
(SELECT SUM(secondEye) FROM tblRpEmpHistorical d7 where d7.empId = a.empId and d7.year = YEAR(getdate())) as secondEye,
(SELECT SUM(injuryNearMiss) FROM tblRpEmpHistorical d8 where d8.empId = a.empId and d8.year = YEAR(getdate())) as injuryNearMiss,
(SELECT SUM(reliability_wHRO) FROM tblRpEmpHistorical d9 where d9.empId = a.empId and d9.year = YEAR(getdate())) as reliability_wHRO,
(SELECT SUM(quality_wHRO) FROM tblRpEmpHistorical d10 where d10.empId = a.empId and d10.year = YEAR(getdate())) as quality_wHRO,
(SELECT SUM(reliability) FROM tblRpEmpHistorical d11 where d11.empId = a.empId and d11.year = YEAR(getdate())) as reliability,
((SELECT COUNT(*) FROM tblEmployee d15 WHERE d15.joblvCode = 'fsfl' AND d15.empEnable = 'true' AND d15.empId &gt; 100000 AND d15.departId = a.departId) * 9800) as ManPowerWorkingHourPerMonth,
(SELECT SUM(procedureUsed) FROM tblRpEmpHistorical d14 where d14.empId = a.empId and d14.year = YEAR(getdate())) as procedureUsed,
(SELECT SUM(safety) FROM tblRpEmpHistorical d15 where d15.empId = a.empId and d15.year = YEAR(getdate())) as safety,
(SELECT SUM(LCS) FROM tblRpEmpHistorical d16 where d16.empId = a.empId and d16.year = YEAR(getdate())) as LCS,
getdate() as lastUpdate, @AllRecog as AllRecog, @CountOffHour as CountOffHour 
from tblEmployee a
where a.empId = @empId">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="HdactionRecogAllMonth" Name="AllRecogMonth" PropertyName="Value" />
                                <asp:ControlParameter ControlID="HdoffHourMonth" Name="CountOffHourMonth" PropertyName="Value" />
                                <asp:ControlParameter ControlID="HdEmpId" DefaultValue="" Name="empId" PropertyName="Value" />
                                <asp:ControlParameter ControlID="HdactionRecogAll" Name="AllRecog" PropertyName="Value" />
                                <asp:ControlParameter ControlID="HdoffHour" Name="CountOffHour" PropertyName="Value" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div style="padding: 0px 0px 1px 1px;">
                            <div class="rpHeader" style="text-align: left;">INDICATORS</div>
                            <div style="display: block; float: left; width: 30%; margin-top: 0px; text-align: right; padding-right: 2px;">
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
                        </div>
                        <div style="display: block; float: left; width: 50%;">
                                <telerik:RadListView ID="rlvIndicator" runat="server" DataKeyNames="empId" DataSourceID="srcIndicator" Skin="Bootstrap" CssClass="LeelawadeeFont" Width="100%" ConvertEmptyStringToNull="False">
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
                                                <asp:Label ID="percentActionCompleteLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("actionComplete") / (Eval("totalActionNumber") - Eval("actionRecognition"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLeadershipVisibilityLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("leadershipVisibility_fsfl") / Eval("ManPowerWorkingHourPerMonth")) %>' />
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
                                                <asp:Label ID="percentOFIIdenLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (1 - (Eval("AllRecog") / Eval("totalActionNumber")))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentPUPFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("procedureUsed") / Eval("safety"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLCSFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("LCS") / Eval("safety"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="offHourLabel" runat="server" Text='<%# Eval("CountOffHour") & "/" & Eval("totalActionNumber") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOffHourLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("CountOffHour") / Eval("totalActionNumber"))) %>' />
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
                                                <asp:Label ID="percentActionCompleteLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("actionComplete") / (Eval("totalActionNumber") - Eval("actionRecognition"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLeadershipVisibilityLabel" runat="server" Text='<%# String.Format("{0:0.### %}", Eval("leadershipVisibility_fsfl") / Eval("ManPowerWorkingHourPerMonth")) %>' />
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
                                                <asp:Label ID="percentOFIIdenLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (1 - (Eval("AllRecog") / Eval("totalActionNumber")))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentPUPFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("procedureUsed") / Eval("safety"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentLCSFieldAssLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("LCS") / Eval("safety"))) %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="offHourLabel" runat="server" Text='<%# Eval("CountOffHour") & "/" & Eval("totalActionNumber") %>' />
                                            </div>
                                            <div class="rlvItem">
                                                <asp:Label ID="percentOffHourLabel" runat="server" Text='<%# String.Format("{0:0.### %}", (Eval("CountOffHour") / Eval("totalActionNumber"))) %>' />
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
                        <%--end table2--%>
                    </div>
                    <div class="column" style="flex: 3%;"></div>
                </div>

                <div id="f-leftsidebar">
                    <div class="row">
                        <div class="row">
                            <div class="col-md-4" style="margin: 14px 0 0 0px; height: 66px;">
                                <img alt="" src="../Images/avatar.png" />
                            </div>
                            <asp:Panel ID="pnAvatar" runat="server">
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
                                    <asp:HiddenField ID="HdDepartId" runat="server" />
                                    <asp:HiddenField ID="HdEmpId" runat="server" />
                                    <asp:HiddenField ID="HdactionRecogAllMonth" runat="server" />
                                    <asp:HiddenField ID="HdoffHourMonth" runat="server" />
                                    <asp:HiddenField ID="HdactionRecogAll" runat="server" />
                                    <asp:HiddenField ID="HdoffHour" runat="server" />
                                    </div>
                                    <div>
                                        <asp:Label ID="lbAccountType" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="padding: 0 0 0 16px; height: 20px; top: 0px; left: 0px;">
                                <asp:Label ID="lbEmail" runat="server" ForeColor="#337AB7" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding: 2px -1px 0px 1px;">
                        <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont" ExpandMode="SingleExpandedItem">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx" Selected="true">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
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
</asp:Content>
