<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="exportToExcel.aspx.vb" Inherits="DowChemical.exportToExcel" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
        }

        .gridItemDesc {
            width: 200px !important;
            overflow: hidden !important;
        }
    </style>

    <script lang="javascript" type="text/javascript">        
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <div id="f-header-setting" style="font-size: 1.6em; padding: 8px 0 0 16px;">Export to Excel</div>
        <div id="f-leftsidebar">
            <div class="row">
                <div class="row">
                    <div class="col-md-4" style="margin: 14px 0 0 0px; height: 66px; text-align: right;">
                        <img alt="" src="../Images/avatar.png" />
                    </div>
                    <div class="col-md-8" style="margin: 10px 0 0 0;">
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
            <div style="padding: 2px -1px 0px 1px; margin-right: 0px;">
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
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Historical Data" NavigateUrl="~/Report/rpHistoricalData.aspx">
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Height="28px" Text="Export to Excel" Selected="true">
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
                    <div style="display: block; float: left; width: 100px; margin-top: 16px; text-align: right;">Department</div>
                    <div style="display: block; float: left; width: 154px; margin-top: 8px; margin-left: 15px;">
                        <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="153px" DataTextField="departName" DataValueField="departId" DataSourceID="srcDepartment" ItemsPerRequest="0"></telerik:RadComboBox>
                        <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                    </div>
                    <div style="display: block; float: left; width: 80px; margin-top: 16px; text-align: right;">Month</div>
                    <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 15px;">
                        <telerik:RadComboBox ID="rcbSelMonth" runat="server" Skin="Metro" Width="80px">
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
                    <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 8px;">
                        <telerik:RadComboBox ID="rcbSelYear" runat="server" Skin="Metro" Width="80px">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="2017" Value="2017" />
                                <telerik:RadComboBoxItem runat="server" Text="2018" Value="2018" />
                                <telerik:RadComboBoxItem runat="server" Text="2019" Value="2019" />
                                <telerik:RadComboBoxItem runat="server" Text="2020" Value="2020" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div style="display: block; float: left; width: 120px; padding-top: 8px; padding-left: 8px;">
                        <asp:Button ID="btSearchReport" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" Width="120px" />
                    </div>
                    <div style="display: block; float: left; width: 80px; padding-top: 8px; padding-left: 24px;">
                    </div>
                    <div style="text-align: right; padding-right: 12px; padding-top: 8px;">
                        <asp:Button ID="btExportExcel" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Export to Excel File" Width="150px" />
                    </div>
                </div>
                <div style="border-style: none none solid none; border-bottom-color: #DDDDDD; border-bottom-width: 1px; padding-top: 4px; padding-right: 0px; padding-left: 0px;">
                    <div class="row">
                        <div style="display: block; float: left; width: 44px; margin-top: 16px; text-align: right;"></div>
                        <div style="display: block; float: left; width: 151px; margin-top: 16px; text-align: right;">Export Option : </div>
                        <div style="display: block; float: left; width: 154px; margin-top: 16px; margin-left: 15px;">
                            No. of Other Observers
                        </div>
                        <div style="display: block; float: left; width: 240px; margin-top: 8px;">
                            <telerik:RadComboBox ID="rcbNoOthObserver" runat="server" Skin="Metro" Width="168px" AutoPostBack="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="3 Observer" Value="3" />
                                    <telerik:RadComboBoxItem runat="server" Text="5 Observer" Value="5" Selected="true" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                        <div style="display: block; float: left; width: 120px; margin-top: 16px; margin-left: 15px;">
                            <asp:CheckBox runat="server" ID="chkImgUrl" CssClass="chkBT2m" Text="&nbsp;&nbsp;Export Image URL" AutoPostBack="True" />
                        </div>
                    </div>
                    <div class="row">
                        <div style="display: block; float: left; width: 44px; margin-top: 16px; text-align: right;"></div>
                        <div style="display: block; float: left; width: 151px; margin-top: 16px; text-align: right;"></div>
                        <div style="display: block; float: left; width: 154px; margin-top: 16px; margin-left: 15px;">
                            No. of Propose Action
                        </div>
                        <div style="display: block; float: left; width: 240px; margin-top: 8px;">
                            <telerik:RadComboBox ID="rcbNoPropose" runat="server" Skin="Metro" Width="168px" AutoPostBack="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="1 Propose" Value="1" />
                                    <telerik:RadComboBoxItem runat="server" Text="3 Propose" Value="3" Selected="true" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                        <div style="display: block; float: left; width: 154px; margin-top: 16px; margin-left: 15px;">
                            <asp:CheckBox runat="server" ID="chkContractor" CssClass="chkBT2m" Text="&nbsp;&nbsp;Export Contractor" AutoPostBack="True" />
                        </div>
                    </div>
                    <div class="row">
                        <div style="display: block; float: left; width: 44px; margin-top: 16px; text-align: right;"></div>
                        <div style="display: block; float: left; width: 151px; margin-top: 16px; text-align: right;"></div>
                        <div style="display: block; float: left; width: 154px; margin-top: 16px; margin-left: 15px;">
                            Responsible Person
                        </div>
                        <div style="display: block; float: left; width: 80px; margin-top: 8px; text-align: right;">
                            <telerik:RadComboBox ID="rcbExpPerson" runat="server" Skin="Metro" Width="168px" AutoPostBack="True">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="Name/Surename" Value="0" />
                                    <telerik:RadComboBoxItem runat="server" Text="Dow Id" Value="1" Selected="true" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                </div>
                <div class="col-md-12" style="padding: 0px 6px 0px 0px;">
                    <telerik:RadGrid ID="RadGrid1" runat="server" GroupPanelPosition="Top" Skin="Metro" Height="760px">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False">
                            <CommandItemSettings ShowExportToExcelButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <NoRecordsTemplate>
                                <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="hfColumn">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfRecId" runat="server" Value='<%# Eval("recId") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="recActNo" HeaderText="ActionNumber" UniqueName="recActNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="recActDate" DataType="System.DateTime" HeaderText="ActionDate" UniqueName="recActDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="recActTime" DataType="System.TimeSpan" HeaderText="ActionTime" UniqueName="recActTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="durationValue" DataType="System.Int32" HeaderText="Duration" UniqueName="durationValue">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empFullName" HeaderText="Observer#1" UniqueName="empFullName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empDowId" HeaderText="DowId#1" UniqueName="empDowId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="departName" HeaderText="Depart#1" UniqueName="departName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othNameObs1" HeaderText="OthObsName1" UniqueName="othNameObs1" ReadOnly="True">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDowIdObs1" HeaderText="OthObsDowID1" UniqueName="othDowIdObs1" ReadOnly="True">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDepartObs1" HeaderText="OthObsDepart1" UniqueName="othDepartObs1" ReadOnly="True">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othNameObs2" HeaderText="OthObsName2" ReadOnly="True" UniqueName="othNameObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDowIdObs2" HeaderText="OthObsDowID2" ReadOnly="True" UniqueName="othDowIdObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDepartObs2" HeaderText="OthObsDepart2" ReadOnly="True" UniqueName="othDepartObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othNameObs3" HeaderText="OthObsName3" ReadOnly="True" UniqueName="othNameObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDowIdObs3" HeaderText="OthObsDowID3" ReadOnly="True" UniqueName="othDowIdObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDepartObs3" HeaderText="OthObsDepart3" ReadOnly="True" UniqueName="othDepartObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othNameObs4" HeaderText="OthObsName4" ReadOnly="True" UniqueName="othNameObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDowIdObs4" HeaderText="OthObsDowID4" ReadOnly="True" UniqueName="othDowIdObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDepartObs4" HeaderText="OthObsDepart4" ReadOnly="True" UniqueName="othDepartObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othNameObs5" HeaderText="OthObsName5" ReadOnly="True" UniqueName="othNameObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDowIdObs5" HeaderText="OthObsDowID5" ReadOnly="True" UniqueName="othDowIdObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="othDepartObs5" HeaderText="OthObsDepart5" ReadOnly="True" UniqueName="othDepartObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="noObserve" DataType="System.Int32" HeaderText="NoObserve" UniqueName="noObserve">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs1" UniqueName="titleObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs1" UniqueName="categoryObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs1" UniqueName="categorySubObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs1" UniqueName="FailPointObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs1" UniqueName="equipmentObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs1" UniqueName="HROObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs1" DataType="System.Boolean" UniqueName="2ndEyeObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs1" UniqueName="descriptionObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs1" UniqueName="proposeActionAObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs1" UniqueName="responAObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs1" UniqueName="statusAObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs1" UniqueName="proposeActionBObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs1" UniqueName="responBObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs1" UniqueName="statusBObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs1" UniqueName="proposeActionCObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs1" UniqueName="responCObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs1" UniqueName="statusCObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs2" UniqueName="titleObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs2" UniqueName="categoryObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs2" UniqueName="categorySubObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs2" UniqueName="FailPointObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs2" UniqueName="equipmentObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs2" UniqueName="HROObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs2" UniqueName="2ndEyeObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs2" UniqueName="descriptionObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs2" UniqueName="proposeActionAObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs2" UniqueName="responAObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs2" UniqueName="statusAObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs2" UniqueName="proposeActionBObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs2" UniqueName="responBObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs2" UniqueName="statusBObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs2" UniqueName="proposeActionCObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs2" UniqueName="responCObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs2" UniqueName="statusCObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs3" UniqueName="titleObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs3" UniqueName="categoryObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs3" UniqueName="categorySubObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs3" UniqueName="FailPointObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs3" UniqueName="equipmentObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs3" UniqueName="HROObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs3" UniqueName="2ndEyeObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs3" UniqueName="descriptionObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs3" UniqueName="proposeActionAObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs3" UniqueName="responAObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs3" UniqueName="statusAObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs3" UniqueName="proposeActionBObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs3" UniqueName="responBObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs3" UniqueName="statusBObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs3" UniqueName="proposeActionCObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs3" UniqueName="responCObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs3" UniqueName="statusCObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs4" UniqueName="titleObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs4" UniqueName="categoryObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs4" UniqueName="categorySubObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs4" UniqueName="FailPointObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs4" UniqueName="equipmentObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs4" UniqueName="HROObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs4" UniqueName="2ndEyeObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs4" UniqueName="descriptionObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs4" UniqueName="proposeActionAObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs4" UniqueName="responAObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs4" UniqueName="statusAObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs4" UniqueName="proposeActionBObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs4" UniqueName="responBObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs4" UniqueName="statusBObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs4" UniqueName="proposeActionCObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs4" UniqueName="responCObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs4" UniqueName="statusCObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs5" UniqueName="titleObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs5" UniqueName="categoryObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs5" UniqueName="categorySubObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs5" UniqueName="FailPointObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs5" UniqueName="equipmentObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs5" UniqueName="HROObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs5" UniqueName="2ndEyeObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs5" UniqueName="descriptionObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs5" UniqueName="proposeActionAObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs5" UniqueName="responAObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs5" UniqueName="statusAObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs5" UniqueName="proposeActionBObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs5" UniqueName="responBObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs5" UniqueName="statusBObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs5" UniqueName="proposeActionCObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs5" UniqueName="responCObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs5" UniqueName="statusCObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="TitleObs6" UniqueName="titleObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="CategoryObs6" UniqueName="categoryObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="SubCateObs6" UniqueName="categorySubObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="FailPointObs6" UniqueName="FailPointObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="EquipmentObs6" UniqueName="equipmentObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="HROObs6" UniqueName="HROObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="2ndEyeObs6" UniqueName="2ndEyeObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="DescriptionObs6" UniqueName="descriptionObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#1Obs6" UniqueName="proposeActionAObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#1Obs6" UniqueName="responAObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#1Obs6" UniqueName="statusAObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#2Obs6" UniqueName="proposeActionBObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#2Obs6" UniqueName="responBObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#2Obs6" UniqueName="statusBObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ProposeAction#3Obs6" UniqueName="proposeActionCObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Responsible#3Obs6" UniqueName="responCObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Status#3Obs6" UniqueName="statusCObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs1" UniqueName="observTypeObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs1" UniqueName="contractorObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs1" UniqueName="picCountObs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs1" UniqueName="picUrl1Obs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs1" UniqueName="picUrl2Obs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs1" UniqueName="picUrl3Obs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs1" UniqueName="picUrl4Obs1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs2" UniqueName="observTypeObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs2" UniqueName="contractorObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs2" UniqueName="picCountObs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs2" UniqueName="picUrl1Obs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs2" UniqueName="picUrl2Obs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs2" UniqueName="picUrl3Obs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs2" UniqueName="picUrl4Obs2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs3" UniqueName="observTypeObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs3" UniqueName="contractorObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs3" UniqueName="picCountObs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs3" UniqueName="picUrl1Obs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs3" UniqueName="picUrl2Obs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs3" UniqueName="picUrl3Obs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs3" UniqueName="picUrl4Obs3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs4" UniqueName="observTypeObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs4" UniqueName="contractorObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs4" UniqueName="picCountObs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs4" UniqueName="picUrl1Obs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs4" UniqueName="picUrl2Obs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs4" UniqueName="picUrl3Obs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs4" UniqueName="picUrl4Obs4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs5" UniqueName="observTypeObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs5" UniqueName="contractorObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs5" UniqueName="picCountObs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs5" UniqueName="picUrl1Obs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs5" UniqueName="picUrl2Obs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs5" UniqueName="picUrl3Obs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs5" UniqueName="picUrl4Obs5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="observTypeObs6" UniqueName="observTypeObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="contractorObs6" UniqueName="contractorObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picCountObs6" UniqueName="picCountObs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl1Obs6" UniqueName="picUrl1Obs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl2Obs6" UniqueName="picUrl2Obs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl3Obs6" UniqueName="picUrl3Obs6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="picUrl4Obs6" UniqueName="picUrl4Obs6">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle VerticalAlign="Top" />
                            <AlternatingItemStyle VerticalAlign="Top" />
                        </MasterTableView>
                        <HeaderStyle ForeColor="DarkSlateGray" />
                        <ItemStyle Font-Size="11px" />
                        <AlternatingItemStyle Font-Size="11px" />
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="srcGetData" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblRecord.recId, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObserver1.empFullName, tblObserver1.empDowId, tblDepartment.departName, tblRecord.noObserve, (SELECT TOP (1) tblEmployee1a.empFullName FROM tblRecordOthEmpO AS tblRecordOthEmpO1a INNER JOIN tblEmployee AS tblEmployee1a ON tblRecordOthEmpO1a.empIdOth = tblEmployee1a.empId WHERE (tblRecordOthEmpO1a.recId = tblRecord.recId) AND (tblRecordOthEmpO1a.recItem = '1')) AS othNameObs1, (SELECT TOP (1) tblEmployee1b.empDowId FROM tblRecordOthEmpO AS tblRecordOthEmpO1b INNER JOIN tblEmployee AS tblEmployee1b ON tblRecordOthEmpO1b.empIdOth = tblEmployee1b.empId WHERE (tblRecordOthEmpO1b.recId = tblRecord.recId) AND (tblRecordOthEmpO1b.recItem = '1')) AS othDowIdObs1, (SELECT TOP (1) tblDepartment1c.departName FROM tblRecordOthEmpO AS tblRecordOthEmpO1c INNER JOIN tblEmployee AS tblEmployee1c ON tblRecordOthEmpO1c.empIdOth = tblEmployee1c.empId INNER JOIN tblDepartment AS tblDepartment1c ON tblEmployee1c.departId = tblDepartment1c.departId WHERE (tblRecordOthEmpO1c.recId = tblRecord.recId) AND (tblRecordOthEmpO1c.recItem = '1')) AS othDepartObs1, (SELECT TOP (1) tblEmployee2a.empFullName FROM tblRecordOthEmpO AS tblRecordOthEmpO2a INNER JOIN tblEmployee AS tblEmployee2a ON tblRecordOthEmpO2a.empIdOth = tblEmployee2a.empId WHERE (tblRecordOthEmpO2a.recId = tblRecord.recId) AND (tblRecordOthEmpO2a.recItem = '2')) AS othNameObs2, (SELECT TOP (1) tblEmployee2b.empDowId FROM tblRecordOthEmpO AS tblRecordOthEmpO2b INNER JOIN tblEmployee AS tblEmployee2b ON tblRecordOthEmpO2b.empIdOth = tblEmployee2b.empId WHERE (tblRecordOthEmpO2b.recId = tblRecord.recId) AND (tblRecordOthEmpO2b.recItem = '2')) AS othDowIdObs2, (SELECT TOP (1) tblDepartment2c.departName FROM tblRecordOthEmpO AS tblRecordOthEmpO2c INNER JOIN tblEmployee AS tblEmployee2c ON tblRecordOthEmpO2c.empIdOth = tblEmployee2c.empId INNER JOIN tblDepartment AS tblDepartment2c ON tblEmployee2c.departId = tblDepartment2c.departId WHERE (tblRecordOthEmpO2c.recId = tblRecord.recId) AND (tblRecordOthEmpO2c.recItem = '2')) AS othDepartObs2, (SELECT TOP (1) tblEmployee3a.empFullName FROM tblRecordOthEmpO AS tblRecordOthEmpO3a INNER JOIN tblEmployee AS tblEmployee3a ON tblRecordOthEmpO3a.empIdOth = tblEmployee3a.empId WHERE (tblRecordOthEmpO3a.recId = tblRecord.recId) AND (tblRecordOthEmpO3a.recItem = '3')) AS othNameObs3, (SELECT TOP (1) tblEmployee3b.empDowId FROM tblRecordOthEmpO AS tblRecordOthEmpO3b INNER JOIN tblEmployee AS tblEmployee3b ON tblRecordOthEmpO3b.empIdOth = tblEmployee3b.empId WHERE (tblRecordOthEmpO3b.recId = tblRecord.recId) AND (tblRecordOthEmpO3b.recItem = '3')) AS othDowIdObs3, (SELECT TOP (1) tblDepartment3c.departName FROM tblRecordOthEmpO AS tblRecordOthEmpO3c INNER JOIN tblEmployee AS tblEmployee3c ON tblRecordOthEmpO3c.empIdOth = tblEmployee3c.empId INNER JOIN tblDepartment AS tblDepartment3c ON tblEmployee3c.departId = tblDepartment3c.departId WHERE (tblRecordOthEmpO3c.recId = tblRecord.recId) AND (tblRecordOthEmpO3c.recItem = '3')) AS othDepartObs3, (SELECT TOP (1) tblEmployee4a.empFullName FROM tblRecordOthEmpO AS tblRecordOthEmpO4a INNER JOIN tblEmployee AS tblEmployee4a ON tblRecordOthEmpO4a.empIdOth = tblEmployee4a.empId WHERE (tblRecordOthEmpO4a.recId = tblRecord.recId) AND (tblRecordOthEmpO4a.recItem = '4')) AS othNameObs4, (SELECT TOP (1) tblEmployee4b.empDowId FROM tblRecordOthEmpO AS tblRecordOthEmpO4b INNER JOIN tblEmployee AS tblEmployee4b ON tblRecordOthEmpO4b.empIdOth = tblEmployee4b.empId WHERE (tblRecordOthEmpO4b.recId = tblRecord.recId) AND (tblRecordOthEmpO4b.recItem = '4')) AS othDowIdObs4, (SELECT TOP (1) tblDepartment4c.departName FROM tblRecordOthEmpO AS tblRecordOthEmpO4c INNER JOIN tblEmployee AS tblEmployee4c ON tblRecordOthEmpO4c.empIdOth = tblEmployee4c.empId INNER JOIN tblDepartment AS tblDepartment4c ON tblEmployee4c.departId = tblDepartment4c.departId WHERE (tblRecordOthEmpO4c.recId = tblRecord.recId) AND (tblRecordOthEmpO4c.recItem = '4')) AS othDepartObs4, (SELECT TOP (1) tblEmployee5a.empFullName FROM tblRecordOthEmpO AS tblRecordOthEmpO5a INNER JOIN tblEmployee AS tblEmployee5a ON tblRecordOthEmpO5a.empIdOth = tblEmployee5a.empId WHERE (tblRecordOthEmpO5a.recId = tblRecord.recId) AND (tblRecordOthEmpO5a.recItem = '5')) AS othNameObs5, (SELECT TOP (1) tblEmployee5b.empDowId FROM tblRecordOthEmpO AS tblRecordOthEmpO5b INNER JOIN tblEmployee AS tblEmployee5b ON tblRecordOthEmpO5b.empIdOth = tblEmployee5b.empId WHERE (tblRecordOthEmpO5b.recId = tblRecord.recId) AND (tblRecordOthEmpO5b.recItem = '5')) AS othDowIdObs5, (SELECT TOP (1) tblDepartment5c.departName FROM tblRecordOthEmpO AS tblRecordOthEmpO5c INNER JOIN tblEmployee AS tblEmployee5c ON tblRecordOthEmpO5c.empIdOth = tblEmployee5c.empId INNER JOIN tblDepartment AS tblDepartment5c ON tblEmployee5c.departId = tblDepartment5c.departId WHERE (tblRecordOthEmpO5c.recId = tblRecord.recId) AND (tblRecordOthEmpO5c.recItem = '5')) AS othDepartObs5, tblRecordDetail.observItem, tblRecordDetail.title, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, tblRecordDetail.equipment, tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, tblRecordDetail.secondEye, tblRecordDetail.recognition, tblRecordDetail.observType, tblRecordDetail.contractor, tblRecordDetail.pictureCount, tblRecordDetail.description, tblRecordDetail.proposeAction_A, tblRecordDetail.proposeRespPerson_A, tblRecordDetail.proposeStatus_A, tblRecordDetail.proposeAction_B, tblRecordDetail.proposeRespPerson_B, tblRecordDetail.proposeStatus_B, tblRecordDetail.proposeAction_C, tblRecordDetail.proposeRespPerson_C, tblRecordDetail.proposeStatus_C, tblRecordDetail.observComplete FROM tblRecord INNER JOIN tblEmployee AS tblObserver1 ON tblRecord.empId = tblObserver1.empId INNER JOIN tblDepartment ON tblObserver1.departId = tblDepartment.departId INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId INNER JOIN tblObsvCate ON tblRecordDetail.category = tblObsvCate.cateId INNER JOIN tblObsvCateSub ON tblRecordDetail.categorySub = tblObsvCateSub.catesubId INNER JOIN tblObsvFailPoint ON tblRecordDetail.failurePoint = tblObsvFailPoint.failpointId WHERE (tblRecord.recActive = 'true') AND (tblRecord.recActMonth = '7') AND (tblRecord.recActYear = '2017') AND (tblRecordDetail.observItem = 1)">
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btSearchReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Transparency="50" MinDisplayTime="400" BackgroundPosition="Top"></telerik:RadAjaxLoadingPanel>
</asp:Content>
