<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="srcSqlDataSource.aspx.vb" Inherits="DowChemical.srcSqlDataSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        table {
            background-color: transparent;
        }

        table {
            border-spacing: 0;
            border-collapse: collapse;
        }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        *, :after, :before {
            color: #000 !important;
            text-shadow: none !important;
            background: 0 0 !important;
            -webkit-box-shadow: none !important;
            box-shadow: none !important;
        }

        th, td {
            padding: 0;
            border-spacing: 0;
        }

        td, th {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="srcSumDurationObs1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRecord.empId, dbo.tblEmployee.empDisplay, dbo.tblEmployee.departId, dbo.tblEmployee.joblvCode, dbo.tblRecord.durationValue, dbo.tblRecord.recId, dbo.tblRecord.recActNo, dbo.tblRecord.noObserve, dbo.tblRecord.recActMonth FROM dbo.tblRecord INNER JOIN dbo.tblEmployee ON dbo.tblRecord.empId = dbo.tblEmployee.empId WHERE (dbo.tblRecord.recActYear = '2017') AND (dbo.tblRecord.empId = '100006')"></asp:SqlDataSource>
            <asp:SqlDataSource ID="srcSumDurationOth" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRecordOthEmpO.recId AS idxId, dbo.tblRecordOthEmpO.empIdOth AS empId, dbo.tblEmployee.empDisplay, dbo.tblEmployee.departId, dbo.tblEmployee.joblvCode, dbo.tblRecord.durationValue, dbo.tblRecord.recId, dbo.tblRecord.recActNo, dbo.tblRecord.noObserve, dbo.tblRecord.recActMonth FROM dbo.tblRecordOthEmpO INNER JOIN dbo.tblRecord ON dbo.tblRecordOthEmpO.recId = dbo.tblRecord.recId INNER JOIN dbo.tblEmployee ON dbo.tblRecordOthEmpO.empIdOth = dbo.tblEmployee.empId WHERE (dbo.tblRecord.recActYear = '2017') AND (dbo.tblRecordOthEmpO.empIdOth = '100006')"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT SUM(tblRpEmpHistorical_1.pLifeNearMiss) AS sumpLifeNearMiss, SUM(tblRpEmpHistorical_1.PSCE_ContainmentLoss) AS sumPSCE_ContainmentLoss, SUM(tblRpEmpHistorical_1.PSCE_PSNM) AS sumPSCE_PSNM, SUM(tblRpEmpHistorical_1.actionTotal) AS sumactionTotal, SUM(tblRpEmpHistorical_1.actionCompleted) AS sumactionCompleted, SUM(tblRpEmpHistorical_1.recognition) AS sumrecognition, SUM(tblRpEmpHistorical_1.leadershipVisibility) AS sumleadershipVisibility, SUM(tblRpEmpHistorical_1.proactiveCompliance) AS sumproactiveCompliance, SUM(tblRpEmpHistorical_1.secondEye) AS sumsecondEye, SUM(tblRpEmpHistorical_1.injuryNearMiss) AS suminjuryNearMiss, SUM(tblRpEmpHistorical_1.reliability_wHRO) AS sumreliability_wHRO, SUM(tblRpEmpHistorical_1.quality_wHRO) AS sumquality_wHRO, SUM(tblRpEmpHistorical_1.reliability) AS sumreliability, SUM(tblRpEmpHistorical_1.procedureUsed) AS sumprocedureUsed, SUM(tblRpEmpHistorical_1.safety) AS sumsafety, SUM(tblRpEmpHistorical_1.LCS) AS sumLCS, (SELECT SUM(dbo.tblRpEmpHistorical.leadershipVisibility) AS sumleadershipVisibility FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId WHERE (dbo.tblEmployee.joblvCode = 'fsfl')) AS sumleadershipVisibility_fsfl FROM dbo.tblRpEmpHistorical AS tblRpEmpHistorical_1 INNER JOIN dbo.tblEmployee AS tblEmployee_1 ON tblRpEmpHistorical_1.empId = tblEmployee_1.empId"></asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT SUM(dbo.tblRpEmpHistorical.leadershipVisibility) AS sumleadershipVisibility FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId WHERE (dbo.tblRpEmpHistorical.departId = @departId) AND (dbo.tblRpEmpHistorical.month = @month) AND (dbo.tblRpEmpHistorical.year = @year) AND (dbo.tblEmployee.joblvCode = 'fsfl')">
                <SelectParameters>
                    <asp:Parameter Name="departId" />
                    <asp:Parameter Name="month" />
                    <asp:Parameter Name="year" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRpEmpHistorical.year, dbo.tblRpEmpHistorical.month, dbo.tblDepartment.departName, dbo.tblEmployee.empDisplay, dbo.tblEmployee.empDowId, dbo.tblEmployee.joblvCode, dbo.tblRpEmpHistorical.pLifeNearMiss, dbo.tblRpEmpHistorical.PSCE_ContainmentLoss, dbo.tblRpEmpHistorical.PSCE_PSNM, dbo.tblRpEmpHistorical.leadershipVisibility, dbo.tblRpEmpHistorical.secondEye, dbo.tblRpEmpHistorical.injuryNearMiss, dbo.tblRpEmpHistorical.proactiveCompliance, dbo.tblRpEmpHistorical.actionTotal, dbo.tblRpEmpHistorical.actionCompleted, dbo.tblRpEmpHistorical.recognition, dbo.tblRpEmpHistorical.reliability_wHRO, dbo.tblRpEmpHistorical.quality_wHRO, dbo.tblRpEmpHistorical.reliability FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId INNER JOIN dbo.tblDepartment ON dbo.tblEmployee.departId = dbo.tblDepartment.departId WHERE (dbo.tblRpEmpHistorical.year = '2017') AND (dbo.tblRpEmpHistorical.month = '3') AND (dbo.tblRpEmpHistorical.departId = '1016') AND (dbo.tblEmployee.joblvCode = 'fsfl')"></asp:SqlDataSource>

            <br />
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRpEmpHistorical.year, dbo.tblRpEmpHistorical.month, dbo.tblDepartment.departName, dbo.tblEmployee.empDisplay, dbo.tblEmployee.empDowId, dbo.tblEmployee.joblvCode, dbo.tblRpEmpHistorical.pLifeNearMiss, dbo.tblRpEmpHistorical.PSCE_ContainmentLoss, dbo.tblRpEmpHistorical.PSCE_PSNM, dbo.tblRpEmpHistorical.leadershipVisibility, dbo.tblRpEmpHistorical.secondEye, dbo.tblRpEmpHistorical.injuryNearMiss, dbo.tblRpEmpHistorical.proactiveCompliance, dbo.tblRpEmpHistorical.actionTotal, dbo.tblRpEmpHistorical.actionCompleted, dbo.tblRpEmpHistorical.recognition, dbo.tblRpEmpHistorical.reliability_wHRO, dbo.tblRpEmpHistorical.quality_wHRO, dbo.tblRpEmpHistorical.reliability FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId INNER JOIN dbo.tblDepartment ON dbo.tblEmployee.departId = dbo.tblDepartment.departId WHERE (dbo.tblRpEmpHistorical.year = '2017') AND (dbo.tblRpEmpHistorical.month = '3')"></asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MAX(lastUpdate) AS Expr1 FROM dbo.tblRpOverallOps"></asp:SqlDataSource>

            <asp:SqlDataSource ID="srcCountGoal" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT COUNT(*) AS selDepart, (SELECT COUNT(*) AS count FROM dbo.tblGoalSetting WHERE (month = @month) AND (year = @year) AND (goalType = '0')) AS allDepart FROM dbo.tblGoalSetting AS tblGoalSetting_1 WHERE (month = @month) AND (year = @year) AND (goalType = '0') AND (departId = @departId )">
                <SelectParameters>
                    <asp:Parameter Name="month" />
                    <asp:Parameter Name="year" />
                    <asp:Parameter Name="departId" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRecord.IsComplete, dbo.tblRecord.departId, dbo.tblDepartment.departName FROM dbo.tblRecord INNER JOIN dbo.tblDepartment ON dbo.tblRecord.departId = dbo.tblDepartment.departId WHERE (dbo.tblRecord.recActMonth = 3) AND (dbo.tblRecord.departId = 1002)"></asp:SqlDataSource>

            <asp:SqlDataSource ID="srcGetGoalByDepartId" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT month, PSCE_ContainmentLoss, PSCE_PSNM, percentActionCompleted, percentLeadershipFieldVisibility, complianceProactive, secondEyeSafety FROM dbo.tblGoalSetting WHERE (departId = 1016) AND (year = 2017)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="srcGetGoalByMonth" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblDepartment.departName, dbo.tblGoalSetting.PSCE_ContainmentLoss, dbo.tblGoalSetting.PSCE_PSNM, dbo.tblGoalSetting.percentActionCompleted, dbo.tblGoalSetting.percentLeadershipFieldVisibility, dbo.tblGoalSetting.complianceProactive, dbo.tblGoalSetting.secondEyeSafety, dbo.tblGoalSetting.departId FROM dbo.tblGoalSetting INNER JOIN dbo.tblDepartment ON dbo.tblGoalSetting.departId = dbo.tblDepartment.departId WHERE (dbo.tblGoalSetting.year = 2017) AND (dbo.tblGoalSetting.month = 3) ORDER BY dbo.tblGoalSetting.goalType, dbo.tblDepartment.departName"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRpOverallOps.month, dbo.tblDepartment.departName, dbo.tblRpOverallOps.PSCE_ContainmentLoss, dbo.tblRpOverallOps.PSCE_PSNM, dbo.tblRpOverallOps.percentActionComplete, dbo.tblRpOverallOps.percentLeadershipVisibility, dbo.tblRpOverallOps.proactiveCompliance, dbo.tblRpOverallOps.secondEye, dbo.tblRpOverallOps.injuryNearMiss, dbo.tblRpOverallOps.reliability_wHRO, dbo.tblRpOverallOps.quality_wHRO, dbo.tblRpOverallOps.reliability, dbo.tblRpOverallOps.percentOFIIden, dbo.tblRpOverallOps.percentPUPFieldAss, dbo.tblRpOverallOps.percentLCSFieldAss, dbo.tblRpOverallOps.percentOffHour FROM dbo.tblRpOverallOps INNER JOIN dbo.tblDepartment ON dbo.tblRpOverallOps.departId = dbo.tblDepartment.departId WHERE (dbo.tblRpOverallOps.rpType &lt;&gt; 'g') AND (dbo.tblRpOverallOps.year = 2017) AND (dbo.tblRpOverallOps.month = 3) ORDER BY dbo.tblDepartment.departName"></asp:SqlDataSource>
            <asp:SqlDataSource ID="srcLastUpdSelDepart" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT MAX(lastUpdate) AS last FROM dbo.tblRpOverallOps WHERE (departId = 0)"></asp:SqlDataSource>

            <asp:SqlDataSource ID="srcLastUpdSelYearMonth0" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT        MAX(lastUpdate) AS last
FROM            dbo.tblRpOverallOps
WHERE        month=3 AND year=2017"></asp:SqlDataSource>

            <asp:SqlDataSource ID="srcDataParticipation" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblEmployee.empDisplay, dbo.tblRpEmpHistorical.PSCE_ContainmentLoss, dbo.tblRpEmpHistorical.PSCE_PSNM, dbo.tblRpEmpHistorical.secondEye, dbo.tblRpEmpHistorical.injuryNearMiss, dbo.tblRpEmpHistorical.proactiveCompliance, dbo.tblRpEmpHistorical.reliability_wHRO, dbo.tblRpEmpHistorical.quality_wHRO, dbo.tblRpEmpHistorical.reliability FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId WHERE (dbo.tblRpEmpHistorical.year = @year) AND (dbo.tblRpEmpHistorical.month = @month) AND (dbo.tblEmployee.departId = @departId) ORDER BY dbo.tblEmployee.empDisplay">
                <SelectParameters>
                    <asp:Parameter Name="year" />
                    <asp:Parameter Name="month" />
                    <asp:Parameter Name="departId" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="srcDataParticipation0" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblEmployee.empDisplay, dbo.tblRpEmpHistorical.leadershipVisibility, dbo.tblEmployee.joblvCode FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId WHERE (dbo.tblRpEmpHistorical.year = @year) AND (dbo.tblRpEmpHistorical.month = @month) AND (dbo.tblEmployee.departId = @departId) AND (dbo.tblEmployee.joblvCode = 'fsfl') ORDER BY dbo.tblEmployee.empDisplay">
                <SelectParameters>
                    <asp:Parameter Name="year" />
                    <asp:Parameter Name="month" />
                    <asp:Parameter Name="departId" />
                </SelectParameters>
            </asp:SqlDataSource>

            <br />

            <asp:SqlDataSource ID="srcDataParticipation1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblEmployee.empDisplay, SUM(dbo.tblRpEmpHistorical.PSCE_ContainmentLoss) AS sumPSCE_ContainmentLoss, SUM(dbo.tblRpEmpHistorical.PSCE_PSNM) AS sumPSCE_PSNM FROM dbo.tblRpEmpHistorical INNER JOIN dbo.tblEmployee ON dbo.tblRpEmpHistorical.empId = dbo.tblEmployee.empId WHERE (dbo.tblRpEmpHistorical.year = 2017) AND (dbo.tblEmployee.departId = 1016) AND (dbo.tblRpEmpHistorical.month &lt;= 5) GROUP BY dbo.tblEmployee.empDisplay ORDER BY dbo.tblEmployee.empDisplay">
            </asp:SqlDataSource>

        </div>
    </form>
</body>
</html>
