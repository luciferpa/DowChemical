<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpHistoricalData.aspx.vb" Inherits="DowChemical.rpHistoricalData" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .Header {
            height: 160px !important;
            width: 48px !important;
            text-align: right !important;
            vertical-align: bottom !important;
        }

        .rotate {
            /* FF3.5+ */
            -moz-transform: rotate(-90.0deg);
            /* Opera 10.5 */
            -o-transform: rotate(-90.0deg);
            /* Saf3.1+, Chrome */
            -webkit-transform: rotate(-90.0deg);
            /* IE6,IE7 */
            /* filter: progid: DXImageTransform.Microsoft.BasicImage(rotation=0.083);*/
            /* IE8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083)";
            /* Standard */
            transform: rotate(-90.0deg);
            color: #333333;
        }
    </style>

    <script lang="javascript" type="text/javascript">
        
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <div id="f-header-setting" style="font-size: 1.6em; padding: 8px 0 0 16px;">Historical Data</div>
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
                        <telerik:RadPanelItem runat="server" Text="CREATE NEW OBSERVATION" Height="36px" NavigateUrl="~/observer/observer.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                        </telerik:RadPanelItem>
                        <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True" Selected="true">
                            <Items>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Overall Performance" NavigateUrl="~/Report/rpOverallPerformance.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Data Participation" NavigateUrl="~/Report/rpDataParticipation.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Performance by Department" NavigateUrl="~/Report/rpDepartmentPerform.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Historical Data" NavigateUrl="~/Report/rpHistoricalData.aspx" Selected="true">
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
                        <div class="col-md-9">
                            <div style="display: block; float: left; width: 72px; margin-top: 15px; text-align: right;">Year/Month</div>
                            <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 8px;">
                                <telerik:RadComboBox ID="rcbSelectYear" runat="server" Skin="Metro" Width="80px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                        <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                        <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                        <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                                        <telerik:RadComboBoxItem runat="server" Text="2021" Value="2021" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                            <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 8px;">
                                <telerik:RadComboBox ID="rcbSelectMonth" runat="server" Skin="Metro" Width="80px">
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
                            <div style="display: block; float: left; width: 84px; margin-top: 15px; text-align: right;">Department</div>
                            <div style="display: block; float: left; width: 180px; margin-top: 8px; margin-left: 8px;">
                                <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [departId], [departName] FROM [tblDepartment]"></asp:SqlDataSource>
                            </div>
                            <div style="display: block; float: left; width: 158px; padding-top: 8px;">
                                <asp:Button ID="btSearchHistory" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="120px" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="pull-right" style="display: block; margin-top: 24px; padding-right: 16px;">
                                <asp:CheckBox ID="chkOnly_fsfl" runat="server" Text="&nbsp;&nbsp;Show only fsfl" CssClass="chkBT2m" Font-Size="1em" AutoPostBack="True" Checked="False" />
                            </div>
                        </div>
                    </div>
                </div>
                <div style="padding: 0px 6px 0px 0px;">
                    <asp:SqlDataSource ID="srcHistoricalData" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblRpEmpHistorical.year, tblRpEmpHistorical.month, tblDepartment.departName, tblEmployee.empDisplay, tblEmployee.empDowId, tblEmployee.joblvCode, tblRpEmpHistorical.pLifeNearMiss, tblRpEmpHistorical.PSCE_ContainmentLoss, tblRpEmpHistorical.PSCE_PSNM, tblRpEmpHistorical.leadershipVisibility, tblRpEmpHistorical.secondEye, tblRpEmpHistorical.injuryNearMiss, tblRpEmpHistorical.proactiveCompliance, tblRpEmpHistorical.actionTotal, tblRpEmpHistorical.actionCompleted, tblRpEmpHistorical.recognition, tblRpEmpHistorical.reliability_wHRO, tblRpEmpHistorical.quality_wHRO, tblRpEmpHistorical.reliability FROM tblRpEmpHistorical INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId WHERE (tblRpEmpHistorical.year = @year) AND (tblRpEmpHistorical.month = @month)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="rcbSelectYear" Name="year" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="rcbSelectMonth" Name="month" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div class="row" style="padding-bottom: 1px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #DDDDDD; padding-top: 1px;">
                        <telerik:RadGrid ID="rgRecordList" runat="server" Skin="Metro" PageSize="20" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%" ShowFooter="True">
                            <MasterTableView AllowMultiColumnSorting="False" Name="ParentGrid">
                                <NoRecordsTemplate>
                                    <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="8px" />
                                        <ItemStyle Width="8px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="departName" HeaderText="Department" SortExpression="departName" UniqueName="departName">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="empDisplay" HeaderText="Name" SortExpression="empDisplay" UniqueName="empDisplay">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="empDowId" HeaderText="DOWId" SortExpression="empDowId" UniqueName="empDowId">
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="joblvCode" HeaderText="Job Level" SortExpression="joblvCode" UniqueName="joblvCode">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Bottom" />
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowSorting="true">
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                pLife Near Miss
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="pLifeNearMissLabel" runat="server" Text='<%# Eval("pLifeNearMiss") %>' ToolTip="pLife Near Miss" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_pLifeNearMissLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="pLife Near Miss" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                PSCE Containment Loss
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="PSCE_ContainmentLossLabel" runat="server" Text='<%# Eval("PSCE_ContainmentLoss") %>' ToolTip="PSCE Containment Loss" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_PSCE_ContainmentLossLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="PSCE Containment Loss" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                PSCE_PSNM
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="PSCE_PSNMLabel" runat="server" Text='<%# Eval("PSCE_PSNM") %>' ToolTip="PSCE_PSNM" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_PSCE_PSNMLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="PSCE_PSNM" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 24px; margin-right: 0;">
                                                Leader Visibility
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="leadershipVisibilityLabel" runat="server" Text='<%# Eval("leadershipVisibility") %>' ToolTip="Leader Visibility" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_leadershipVisibilityLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Leader Visibility" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle Width="6%" HorizontalAlign="Right" />
                                        <FooterStyle Width="6%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                2nd Eye
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="secondEyeLabel" runat="server" Text='<%# Eval("secondEye") %>' ToolTip="2nd Eye" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_secondEyeLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="2nd Eye" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Injury Near Miss
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="injuryNearMissLabel" runat="server" Text='<%# Eval("injuryNearMiss") %>' ToolTip="Injury Near Miss" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_injuryNearMissLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Injury Near Miss" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Proactive Compliance
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="proactiveComplianceLabel" runat="server" Text='<%# Eval("proactiveCompliance") %>' ToolTip="Proactive Compliance" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_proactiveComplianceLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Proactive Compliance" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Action Total
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="actionTotalLabel" runat="server" Text='<%# Eval("actionTotal") %>' ToolTip="Action Total" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_actionTotalLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Action Total" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Action Completed
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="actionCompletedLabel" runat="server" Text='<%# Eval("actionCompleted") %>' ToolTip="Action Completed" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_actionCompletedLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Action Completed" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Recognition
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="recognitionLabel" runat="server" Text='<%# Eval("recognition") %>' ToolTip="Recognition" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_recognitionLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Recognition" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Reliability (HRO) (1st)
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="reliability_wHROLabel" runat="server" Text='<%# Eval("reliability_wHRO") %>' ToolTip="Reliability (HRO) (1st)" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_reliability_wHROLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Reliability (HRO) (1st)" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Quality (HRO) (1st)
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="quality_wHROLabel" runat="server" Text='<%# Eval("quality_wHRO") %>' ToolTip="Quality (HRO) (1st)" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_quality_wHROLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Quality (HRO) (1st)" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>
                                            <div class="rotate" style="position: absolute; width: 48px; white-space: nowrap; margin: 36px 0px 0px 8px;">
                                                Reliability (1st)
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="reliabilityLabel" runat="server" Text='<%# Eval("reliability") %>' ToolTip="Reliability (1st)" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="ft_reliabilityLabel" runat="server" Text='<%#String.Format("{0:N0}", "0") %>' ToolTip="Reliability (1st)" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="4.5%" />
                                        <ItemStyle Width="4.5%" HorizontalAlign="Right" />
                                        <FooterStyle Width="4.5%" HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn>
                                        <HeaderStyle Width="8px" />
                                        <ItemStyle Width="8px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" Height="150px" />
                                <FooterStyle BackColor="#F9F9F9" Font-Bold="True" Height="30px" />
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="640px" />
                            </ClientSettings>
                            <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                            <ItemStyle Height="32px" BackColor="#D5DCE3" />
                            <FooterStyle Height="32px" />
                        </telerik:RadGrid>
                    </div>
                    <div class="row LeelawadeeFont">
                        <div class="col-md-7" style="padding: 16px 0px 16px 16px; font-size: smaller;"></div>
                    </div>
                </div>
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
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbSelectYear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbSelectMonth">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rcbDepartment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="chkOnly_fsfl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRecordList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
