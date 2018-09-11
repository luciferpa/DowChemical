<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rpOverallPerformChart.aspx.vb" Inherits="DowChemical.rpOverallPerformChart" %>

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
                                <telerik:RadPanelItem runat="server" Text="CREATE NEW OBSERVATION" Height="36px" NavigateUrl="~/observer/observer.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="OBSERVER" Height="36px" NavigateUrl="~/observer/observationList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="FOLLOW UP" Height="36px" NavigateUrl="~/followup/followupList.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="REPORT" Height="36px" Expanded="True" PreventCollapse="True" Selected="true">
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
                                        <asp:Button ID="btViewReport" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="View Chart/Refresh" Width="180px" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="pull-right" style="display: block; margin-top: 8px;">
                                        <span style="padding-right: 8px;">
                                            <asp:Image ID="imgMenu" runat="server" ImageUrl="~/Images/chart-bar-1a.png" />
                                            <telerik:RadToolTip ID="RadToolTip1" runat="server" RelativeTo="Element" ManualClose="True" TargetControlID="imgMenu" ShowEvent="OnClick" Position="TopRight" OffsetX="0" OffsetY="-4">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="form-horizontal" style="padding: 8px 12px 0 12px; width: 280px">
                                                            <h4>Show Goal line</h4>
                                                            <div style="border-top-style: solid; border-top-width: 2px; border-top-color: #cd5c5c; padding: 4px 0 0 0;"></div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowPsceContainment" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;PSCE_Containment Loss Near Miss" Checked="True" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowPscePsnm" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;PSCE_PSNM Near Miss" Checked="True" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowActionCompleted" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;% Action Completed" Checked="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowFieldVisibility" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;% Field Visibility" Checked="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowProactiveCompliance" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;Proactive Compliance" Checked="false" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:CheckBox ID="chkShowSecondEye" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;&nbsp;2nd EYE Participation" Checked="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-9">
                                                                    <asp:Button runat="server" Text="Close" CssClass="btn btn-default" ID="btShowGoalLine" />
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </telerik:RadToolTip>
                                        </span>
                                        <asp:Button ID="btViewChart" runat="server" Height="30px" class="btn btn-default btn-mo32" Text="Report" Width="120px" PostBackUrl="~/Report/rpOverallPerformance.aspx" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 1px; padding-bottom: 1px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #DDDDDD;">
                            <div style="padding: 0px 0px 1px 1px;">
                                <div class="rpHeader">SUMMARY INDICATORS CHART</div>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart1" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="PSCE_Containment Loss Near Miss Report"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="PSCE_ContainmentLoss" Name="PSCE_ContainmentLoss" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalPSCE_ContainmentLoss" Name="goalPSCE_ContainmentLoss" Visible="false">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White"></TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart2" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="PSCE_PSNM Near Miss Report"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="PSCE_PSNM" Name="PSCE_PSNM" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalPSCE_PSNM" Name="goalPSCE_PSNM" Visible="false">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White"></TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <script>
                                function formatLabel(number) {
                                    return parseFloat(number * 100).toFixed(2) + " %";
                                }
                                function formatLabelgoal(number) {
                                    return parseFloat(number * 100).toFixed(0) + " %";
                                }
                            </script>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart3" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% Action Completed"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentActionComplete" Name="percentActionComplete" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalPercentActionCompleted" Name="goalPercentActionCompleted">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White">
                                                    <ClientTemplate>
                                                        #= formatLabelgoal(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart4" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% Field Visibility"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentLeadershipVisibility" Name="percentLeadershipVisibility" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalPercentLeadershipFieldVisibility" Name="goalPercentLeadershipFieldVisibility">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White">
                                                    <ClientTemplate>
                                                        #= formatLabelgoal(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart5" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="Proactive Compliance Deviation Report"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="proactiveCompliance" Name="proactiveCompliance" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalComplianceProactive" Name="goalComplianceProactive" Visible="false">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White"></TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart6" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="2nd EYE Participation Report"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="secondEye" Name="secondEye" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                            <telerik:LineSeries DataFieldY="goalSecondEyeSafety" Name="goalSecondEyeSafety">
                                                <LineAppearance Width="2" />
                                                <MarkersAppearance MarkersType="Circle" BackgroundColor="White" Size="6" BorderColor="#ff0000" BorderWidth="2"></MarkersAppearance>
                                                <TooltipsAppearance BackgroundColor="Red" Color="White"></TooltipsAppearance>
                                                <Appearance>
                                                    <FillStyle BackgroundColor="#ff0000"></FillStyle>
                                                </Appearance>
                                                <LabelsAppearance Visible="false"></LabelsAppearance>
                                            </telerik:LineSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart7" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="Injury/Illness Near Miss (1st)"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="injuryNearMiss" Name="injuryNearMiss" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart8" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="Reliability (HRO) (1st)"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="reliability_wHRO" Name="reliability_wHRO" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart9" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="Quality (HRO) (1st)"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="quality_wHRO" Name="quality_wHRO" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart10" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="Reliability (1st)"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="reliability" Name="reliability" ColorField="colorBar">
                                                <LabelsAppearance>
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart11" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% Opportunity Identified (OFI)"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentOFIIden" Name="percentOFIIden" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart12" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% PUP Field Assessment"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentPUPFieldAss" Name="percentPUPFieldAss" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 40px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart13" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% LCS Field Assessment"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentLCSFieldAss" Name="percentLCSFieldAss" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                            <div class="col-md-6" style="padding: 8px 56px 32px 40px;">
                                <telerik:RadHtmlChart ID="RadHtmlChart14" runat="server" Skin="Bootstrap">
                                    <ChartTitle Text="% Off Hour Participation"></ChartTitle>
                                    <PlotArea>
                                        <Series>
                                            <telerik:ColumnSeries DataFieldY="percentOffHour" Name="percentOffHour" ColorField="colorBar">
                                                <LabelsAppearance DataFormatString="{0:0.## %}">
                                                    <TextStyle FontSize="11px" />
                                                </LabelsAppearance>
                                                <TooltipsAppearance>
                                                    <ClientTemplate>
                                                        #= formatLabel(value) #
                                                    </ClientTemplate>
                                                </TooltipsAppearance>
                                            </telerik:ColumnSeries>
                                        </Series>
                                        <XAxis DataLabelsField="monthDesc">
                                            <LabelsAppearance RotationAngle="75">
                                            </LabelsAppearance>
                                            <TitleAppearance Text="">
                                            </TitleAppearance>
                                        </XAxis>
                                        <YAxis>
                                            <LabelsAppearance DataFormatString="{0:0 %}"></LabelsAppearance>
                                        </YAxis>
                                    </PlotArea>
                                    <Legend>
                                        <Appearance Visible="false" />
                                    </Legend>
                                    <Zoom Enabled="False">
                                    </Zoom>
                                </telerik:RadHtmlChart>
                            </div>
                        </div>
                        <div class="row LeelawadeeFont">
                            <div class="col-md-7" style="padding: 16px 0px 16px 16px; font-size: smaller;">
                                <div>%Action Completed = 100 x Action Completed /(Action Total - Recognition)</div>
                                <div>Working hour per month = 9800 minute</div>
                            </div>
                            <div class="col-md-5" style="padding: 2px 12px 16px 16px; font-size: smaller; text-align: right;"></div>
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
