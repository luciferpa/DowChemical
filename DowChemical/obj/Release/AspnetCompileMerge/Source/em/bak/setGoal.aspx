<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setGoal.aspx.vb" Inherits="DowChemical.setGoal2" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
        }

        .editc1 {
            padding: 2px 16px 2px 16px;
        }

        .edittbnumber {
            height: 30px !important;
            padding: 5px 4px 5px 6px !important;
            line-height: 1.5 !important;
            border-radius: 3px !important;
            border: 1px solid #ccc !important;
        }

            .edittbnumber:focus {
                height: 30px !important;
                padding: 6px 4px 4px 6px !important;
                line-height: 1.5 !important;
                border-radius: 3px !important;
                border-color: #a0a0a0 !important;
                outline: 0 !important;
            }

        .edittbnumber_center {
            height: 30px !important;
            padding: 5px 4px 5px 6px !important;
            line-height: 1.5 !important;
            border-radius: 3px !important;
            border: 1px solid #ccc !important;
            text-align: center !important;
        }

            .edittbnumber_center:focus {
                height: 30px !important;
                padding: 6px 4px 4px 6px !important;
                line-height: 1.5 !important;
                border-radius: 3px !important;
                border-color: #a0a0a0 !important;
                outline: 0 !important;
                text-align: center !important;
            }

        .RadInput_Default .riError {
            height: 30px !important;
            padding: 6px 4px 4px 6px !important;
            line-height: 1.5 !important;
            border-radius: 3px !important;
            background-image: none !important;
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
                    Goal Setting
                </div>
                <div id="f-leftsidebar">
                    <div style="height: 100px">
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
                                        <telerik:RadPanelItem runat="server" Text="GOAL SETTING" NavigateUrl="~/em/setGoal.aspx?sel=setgoal" Selected="true">
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
                            <div class="row">
                                <div style="display: block; float: left; width: 100px; margin-top: 15px; text-align: right;">Year/Month</div>
                                <div style="display: block; float: left; width: 80px; margin-top: 8px; margin-left: 8px;">
                                    <telerik:RadComboBox ID="rcbYear" runat="server" Skin="Metro" Width="80px" AutoPostBack="True">
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
                                    <telerik:RadComboBox ID="rcbMonth" runat="server" Skin="Metro" Width="80px" AutoPostBack="True">
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
                                <div style="text-align: right; padding-right: 12px; padding-top: 8px;">&nbsp;</div>
                            </div>
                        </div>
                        <div>
                            <asp:SqlDataSource ID="srcDepartmentList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT departId, departName, departDesc FROM tblDepartment ORDER BY departName"></asp:SqlDataSource>
                            <telerik:RadGrid ID="rgGoalSetting" runat="server" Skin="Metro" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%"
                                PageSize="50" ShowFooter="True" DataSourceID="srcDepartmentList">
                                <MasterTableView DataKeyNames="departId" DataSourceID="srcDepartmentList">
                                    <EditItemTemplate></EditItemTemplate>
                                    <NoRecordsTemplate>
                                        <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="actioncolumn">
                                            <HeaderTemplate>
                                                <div style="height: 35px"></div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="padding-right: 0; padding-left: 16px;">
                                                    <asp:ImageButton ID="imgbEdit" runat="server" CommandName="edit" ImageUrl="~/Images/pen-checkbox-24-gray36.png" ToolTip="edit" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="68px" />
                                            <ItemStyle Width="68px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Department" SortExpression="departName" UniqueName="departName">
                                            <FooterTemplate>
                                                MTP Ops*
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbDepartName" runat="server" Text='<%# Eval("departName") %>' ToolTip='<%# Eval("departDesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <HeaderTemplate>
                                                <div style="display: block; float: left; width: 8%; text-align: center; color: brown; margin-top: 7px;">
                                                    FSFL Users<br />
                                                    (fsfl)
                                                </div>
                                                <div style="display: block; float: left; width: 8%; text-align: center; color: brown; margin-top: 7px;">
                                                    TECH User<br />
                                                    (tech)
                                                </div>
                                                <div style="display: block; float: left; width: 12%; text-align: center;">
                                                    2nd Eye<br />
                                                    Safety Review<br />
                                                    Participation
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%; text-align: center; margin-top: 7px;">
                                                    PSCE_Containment<br />
                                                    Loss Near miss (L5)
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%; height: 24px; vertical-align: middle; text-align: center; margin-top: 16px;">
                                                    PSCE_PSNM (L5)
                                                </div>
                                                <div style="display: block; float: left; width: 12%; text-align: center;">
                                                    % Leadership<br />
                                                    Field Visibility<br />
                                                    (fsfl)
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%; text-align: center; margin-top: 7px;">
                                                    % Action<br />
                                                    Complete
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%; text-align: center;">
                                                    Compliance<br />
                                                    Proactive<br />
                                                    Report
                                                </div>
                                                <div style="display: block; float: left; width: 14%; height: 24px; vertical-align: middle; text-align: center; margin-top: 16px;">
                                                    <div style="text-align: left;">Last Update</div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hfDepartId" runat="server" Value='<%# Eval("departId") %>' />
                                                <div style="display: block; float: left; width: 8%;">
                                                    <asp:Label ID="lb_fsflUser" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 8%;">
                                                    <asp:Label ID="lb_techUser" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 12%;">
                                                    <asp:Label ID="lb_seconfEyeSafety" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lb_PSCE_ContainmentLoss" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lb_PSCE_PSNM" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 12%;">
                                                    <asp:Label ID="lb_percentLeadershipFieldVisibility" runat="server" Text='<%#String.Format("{0:P1}", "0%") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lb_percentActionCompleted" runat="server" Text='<%#String.Format("{0:P1}", "0%") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lb_complianceProactiveReport" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 14%;">
                                                    <div style="text-align: left;">
                                                        <asp:Label ID="lbTimestamp" runat="server" Text='<%#String.Format("{0:MM/dd/yy H:mm:ss}", "") %>'></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="display: block; float: left; width: 8%;">
                                                    <asp:Label ID="lbFooter_fsflUser" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 8%;">
                                                    <asp:Label ID="lbFooter_techUser" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 12%;">
                                                    <asp:Label ID="lbFooter_seconfEyeSafety" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lbFooter_PSCE_ContainmentLoss" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lbFooter_PSCE_PSNM" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 12%;">
                                                    <asp:Label ID="lbFooter_percentLeadershipFieldVisibility" runat="server" Text='<%#String.Format("{0:P1}", "0%") %>'></asp:Label>
                                                    <span style="position: absolute; margin-top: -6px;">
                                                        <asp:ImageButton ID="imgEdit1" runat="server" ImageUrl="~/Images/pen-checkbox-24-gray36.png" OnClick="imgEdit1_Click" />
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lbFooter_percentActionCompleted" runat="server" Text='<%#String.Format("{0:P1}", "0%") %>'></asp:Label>
                                                    <span style="position: absolute; margin-top: -6px;">
                                                        <asp:ImageButton ID="imgEdit2" runat="server" ImageUrl="~/Images/pen-checkbox-24-gray36.png" OnClick="imgEdit1_Click" />
                                                </div>
                                                <div style="display: block; float: left; width: 11.5%;">
                                                    <asp:Label ID="lbFooter_complianceProactiveReport" runat="server" Text='<%#String.Format("{0:N0}", "0") %>'></asp:Label>
                                                </div>
                                                <div style="display: block; float: left; width: 14%;">&nbsp;</div>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div style="padding: 6px 0px 6px 0px; border-left-style: solid; border-left-width: 6px; border-left-color: #4AB0E1;">
                                                <asp:HiddenField ID="hfDepartId" runat="server" Value='<%# Eval("departId") %>' />
                                                <div class="row editc1">
                                                    <div style="display: block; float: left; width: 32px;">
                                                        <asp:ImageButton ID="imbBack" runat="server" ImageUrl="~/Images/back-1-32.png" ToolTip="Close" CommandName="Cancel" />
                                                    </div>
                                                    <div class="col-md-11" style="padding-top: 8px;">
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">2nd Eye Safety Review Participation : </div>
                                                            <div class="col-md-3">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_seconfEyeSafety" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Total FSFL Users (fsfl)</div>
                                                            <div style="display: block; float: left; width: 223px; padding-left: 15px;">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_total_fsfl" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                            <div style="display: block; float: left; width: 80px;">
                                                                <asp:Button ID="btGetData_fsfl" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Get Data" Width="80px" CommandName="getdatafsfl" OnClick="btGetData_fsfl_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">PSCE_Containment Loss Near miss (L5) : </div>
                                                            <div class="col-md-3">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_PSCE_ContainmentLoss" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Total TECH Users (tech)</div>
                                                            <div style="display: block; float: left; width: 223px; padding-left: 15px;">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_total_tech" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                            <div style="display: block; float: left; width: 80px;">
                                                                <asp:Button ID="btGetData_tech" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Get Data" Width="80px" CommandName="getdatatech" OnClick="btGetData_tech_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">PSCE_PSNM (L5) : </div>
                                                            <div class="col-md-4">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_PSCE_PSNM" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">% LeadershipField Visibility (fsfl) : </div>
                                                            <div class="col-md-4">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_percentLeadershipFieldVisibility" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" Culture="th-TH" DbValueFactor="1" LabelWidth="96px" MinValue="0" Type="Percent">
                                                                        <NegativeStyle Resize="None" />
                                                                        <NumberFormat DecimalDigits="1" ZeroPattern="n %" />
                                                                    </telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">% Action Complete : </div>
                                                            <div class="col-md-4">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_percentActionCompleted" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" Culture="th-TH" DbValueFactor="1" LabelWidth="96px" MinValue="0" Type="Percent">
                                                                        <NegativeStyle Resize="None" />
                                                                        <NumberFormat DecimalDigits="1" ZeroPattern="n %" />
                                                                    </telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row editc1">
                                                            <div style="display: block; float: left; width: 220px; text-align: right; margin-top: 6px;">Compliance Proactive Report : </div>
                                                            <div class="col-md-4">
                                                                <div style="display: block; float: left; width: 200px;">
                                                                    <telerik:RadNumericTextBox ID="tbn_complianceProactiveReport" runat="server" CssClass="form-control input-sm edittbnumber" Width="200px" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding: 6px 21px 16px 16px">
                                                            <div class="col-md-12">
                                                                <div style="display: block; float: left; width: 220px;">
                                                                    &nbsp;
                                                                </div>
                                                                <div style="display: block; float: left; width: 240px;">
                                                                    <asp:Button ID="btUpdate" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" />&nbsp;
                                                                <asp:Button ID="btClose" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Close" CommandName="Cancel" />&nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                                </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                                <ItemStyle Height="32px" BackColor="#D5DCE3" />
                                <FooterStyle Height="36px" BackColor="#E8EEF4" Font-Bold="true" />
                            </telerik:RadGrid>
                        </div>
                        <asp:Panel ID="pnEditMTP" runat="server" Visible="false">
                            <div class="row" style="padding-top: 8px; padding-bottom: 4px;">
                                <div style="padding-left: 144px;">
                                    <div style="display: block; float: left; width: 8%;">&nbsp;</div>
                                    <div style="display: block; float: left; width: 8%;">&nbsp;</div>
                                    <div style="display: block; float: left; width: 12%;">&nbsp;</div>
                                    <div style="display: block; float: left; width: 11.5%;">&nbsp;</div>
                                    <div style="display: block; float: left; width: 11.5%;">&nbsp;</div>
                                    <div style="display: block; float: left; width: 12%;">
                                        <div style="padding: 0px 8px 0px 8px;">
                                            <telerik:RadNumericTextBox ID="tbn_editpercentLeadershipFieldVisibility" runat="server" CssClass="form-control input-sm edittbnumber_center" Width="100%" Culture="th-TH" DbValueFactor="1" LabelWidth="96px" MinValue="0" Type="Percent" Value="12">
                                                <NegativeStyle Resize="None" />
                                                <NumberFormat DecimalDigits="1" ZeroPattern="n %" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 11.5%;">
                                        <div style="padding: 0px 8px 0px 8px;">
                                            <telerik:RadNumericTextBox ID="tbn_editpercentActionCompleted" runat="server" CssClass="form-control input-sm edittbnumber_center" Width="100%" Culture="th-TH" DbValueFactor="1" LabelWidth="96px" MinValue="0" Type="Percent">
                                                <NegativeStyle Resize="None" />
                                                <NumberFormat DecimalDigits="1" ZeroPattern="n %" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 25.5%; text-align: left;">
                                        <div style="padding: 0px 8px 0px 8px;">
                                            <asp:Button ID="btUpdateMTP" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" />&nbsp;
                                            <asp:Button ID="btCloseMTP" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Close" CommandName="Cancel" />&nbsp;
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="padding: 8px 0 0 16px;">MTP Ops = All Depatment data</div>
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
