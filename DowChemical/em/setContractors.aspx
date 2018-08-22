<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setContractors.aspx.vb" Inherits="DowChemical.setContractors" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .header {
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
                    Contractor Setting
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
                                        <telerik:RadPanelItem runat="server" Text="CONTRACTOR" NavigateUrl="~/em/setContractors.aspx?sel=setcontractor" Selected="true">
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
                            <div class="fieldset-box" style="border: 1px solid #CCCCCC;">
                                <div class="row" style="height: 32px; background-color: #f0f0f0;">
                                    <div class="col-md-6" style="font-size: small; font-weight: 600; margin-top: 4px; color: #666666;">Contractor Dropdown setting</div>
                                    <div class="col-md-5" style="padding-top: 4px; padding-bottom: 4px;">
                                    </div>
                                    <div class="col-md-1">&nbsp;</div>
                                </div>
                                <div class="row" style="height: 40px; background-color: #F0F0F0;">
                                    <div class="col-md-3" style="text-align: right; margin-top: 11px;">Contractor</div>
                                    <div class="col-md-8" style="padding-top: 4px;">
                                        <telerik:RadComboBox ID="rcbContractor" runat="server" Skin="Metro" Width="391px" DataTextField="contractorName" DataValueField="contractorId" AutoPostBack="True" DataSourceID="srcContractor">
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcContractor" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT contractorId, contractorName FROM dbo.tblContractor ORDER BY contractorName"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-md-1">&nbsp;</div>
                                </div>
                                <div class="row" style="height: 40px;">
                                    <div class="col-md-3">&nbsp;</div>
                                    <div class="col-md-8" style="padding-top: 15px;">
                                        <asp:Button ID="btNew" runat="server" CssClass="btn btn-xs btn-primary" Text="New" CommandName="newcontractor" />&nbsp;
                                        <asp:Button ID="btEdit" runat="server" CssClass="btn btn-xs btn-primary" Text="Edit" CommandName="editcontractor" />&nbsp;
                                        <asp:Button ID="btDel" runat="server" CssClass="btn btn-xs btn-danger" Text="Delete" CommandName="delcontractor" />
                                        <div style="display: block; float: left; width: 100px; margin-top: -20px; margin-left: 168px; position: absolute">
                                            <asp:CheckBox ID="chkConfirmDel" runat="server" Text="&nbsp;&nbsp;Confirm Delete" CssClass="chkBT2m" Visible="False" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="height: 32px;">
                                    <div class="col-md-3" style="text-align: right; padding-top: 10px">Contractor Name</div>
                                    <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                        <asp:TextBox ID="tbNewContractor" runat="server" CssClass="form-control input-sm" placeholder="New Contractor"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="height: 48px;">
                                    <div class="col-md-3">&nbsp;</div>
                                    <div class="col-md-8" style="padding-top: 3px; padding-bottom: 8px;">
                                        <asp:Button ID="btSave" runat="server" CssClass="btn btn-xs btn-primary" Text="Save" CommandName="savecontractor" Visible="False" />
                                    </div>
                                </div>
                                <div class="row" style="height: 20px; padding-left: 12px;">
                                    <asp:Label ID="lbFailureText" runat="server" Text="" ForeColor="#CC0000" Font-Size="1em"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5" style="padding: 16px 20px 16px 0px;">
                            <telerik:RadGrid ID="rgContractorList" runat="server" Skin="Metro" PageSize="15" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%"
                                DataSourceID="srcContractor" HeaderStyle-Height="60px">
                                <MasterTableView DataKeyNames="contractorId" DataSourceID="srcContractor">
                                    <EditItemTemplate></EditItemTemplate>
                                    <NoRecordsTemplate>
                                        <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="contractorId" DataType="System.Int32" HeaderText="contractorId" UniqueName="contractorId" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="contractorName" FilterControlAltText="" HeaderText="Contractor Name" SortExpression="contractorName" UniqueName="contractorName">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="140px" HorizontalAlign="Left" />
                                            <ItemStyle Width="140px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <HeaderStyle Height="34px" BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                <FooterStyle Height="30px" />
                            </telerik:RadGrid>
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
