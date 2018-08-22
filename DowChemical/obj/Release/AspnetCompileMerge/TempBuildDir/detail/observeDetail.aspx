<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="observeDetail.aspx.vb" Inherits="DowChemical.ObserveDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/siteContent.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/site_telerik.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/upload.js"></script>

    <script lang="javascript" type="text/javascript">
        function OnClientEntryAddingProposeAction(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
                alert("You can select only one entry.");
            }
        }
        function OnClientEntryAddingOtherObservers(sender, eventArgs) {
            if (sender.get_entries().get_count() > 4) {
                eventArgs.set_cancel(true);
                alert("You can select only five entry.");
            }
        }
    </script>

    <style type="text/css">
        .statusIcon {
            width: 28px;
            display: block;
            float: left;
            margin-top: 33px;
            margin-left: 1px;
        }

        .statusText {
            width: 160px;
            display: block;
            float: left;
            margin-top: 36px;
            margin-left: 1px;
            font-weight: bold;
        }

        .txtboxDisableHead {
            border-color: #d5d5d5 !important;
            background-color: ghostwhite !important;
            font-weight: 500 !important;
            color: darkblue !important;
        }

        .txtboxDisable {
            border-color: #e5e5e5 !important;
            background-color: white !important;
            color: darkblue !important;
        }

        .txtboxEdit {
            border-color: #e5e5e5 !important;
            background-color: white !important;
            color: darkblue !important;
        }

        .statusComplete {
            width: 160px;
            display: block;
            float: left;
            margin-top: 24px;
            margin-left: 2px;
            padding: 5px 20px 0px 20px;
            border-color: #e5e5e5 !important;
            border-style: solid !important;
            border-width: 1px;
            border-radius: 3px;
        }

        .statusCompleteEdit {
            width: 160px;
            display: block;
            float: left;
            margin-top: 24px;
            margin-left: 2px;
            padding: 5px 20px 0px 20px;
            border-color: #cccccc !important;
            border-style: solid !important;
            border-width: 1px;
            border-radius: 3px;
        }

            .statusCompleteEdit:hover {
                background-color: #cccccc;
            }

        .thisReadOnly {
            border-left-style: solid;
            border-left-width: 6px;
            border-left-color: #FFFFFF;
        }

        .thisEdit {
            border-left-style: solid;
            border-left-width: 6px;
            border-left-color: #4AB0E1;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
        <div>
            <div id="f-header" style="font-size: 1.7em;">
                <div style="display: block; float: left; margin-top: 4px; margin-left: 4px; color: #FFF">Observation Detail</div>
                <div style="float: right; margin-right: 308px; margin-top: 8px;">&nbsp;</div>
            </div>
            <div id="f-leftsidebar">
                <div class="row">
                    <div class="row">
                        <div style="display: block; float: left; width: 74px; margin: 14px 0 0 16px;">
                            <img alt="" src="../Images/avatar.png" />
                        </div>
                        <asp:Panel ID="pnAvatar" runat="server">
                            <div style="display: block; float: left; width: 120px; margin: 10px 0 0 0; padding-right: 8px;">
                                <div style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #cd5c5c; padding: 0 0 2px 0;">
                                    <asp:Label ID="lbName" Font-Bold="true" ForeColor="#333333" Font-Size="1.2em" runat="server" Text=""></asp:Label>
                                </div>
                                <div style="padding-top: 4px;">
                                    Dow ID :<asp:Label ID="lbDowId" Font-Bold="true" runat="server" Text=""></asp:Label>
                                </div>
                                <div>
                                    Department :<asp:Label ID="lbDepartName" Font-Bold="true" runat="server" Text=""></asp:Label>
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
                    <telerik:RadPanelBar ID="RadPanelBar1" runat="server" Skin="Bootstrap" Width="100%" CssClass="LeelawadeeFont" ExpandMode="SingleExpandedItem">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="HOME" Height="36px" NavigateUrl="~/Default.aspx">
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
                <asp:HiddenField ID="hfRecId" runat="server" />
                <asp:HiddenField ID="hfOwnerEmpId" runat="server" Value="0" />
                <asp:HiddenField ID="hfOwnerDepartName" runat="server" />
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
                    <telerik:RadPageView ID="RadPageView_Blank" runat="server">
                        <div class="row" style="padding: 24px 16px 8px 32px; height: 500px; font-size: medium; font-weight: 500;">
                            Please contact system admin to get correct link.
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView_Detail" runat="server">
                        <asp:HiddenField ID="hfDetailId" runat="server" />
                        <div class="row" style="padding: 24px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                Observed Department
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="lbObDepartName" runat="server" CssClass="form-control input-sm txtboxDisableHead" Width="172px" Enabled="False" BackColor="#66FF99"></asp:TextBox>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                Action Number
                            </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="lbActionNum" runat="server" CssClass="form-control input-sm txtboxDisableHead" Width="172px" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                Date
                            </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <asp:TextBox ID="lbDocDate" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                                <div style="display: block; float: left; width: 180px;">
                                    <div style="display: block; float: left; width: 36px; text-align: right; margin-top: 7px; margin-right: 15px;">
                                        Time
                                    </div>
                                    <div style="display: block; float: left; width: 124px;">
                                        <asp:TextBox ID="lbDocTime" runat="server" CssClass="form-control input-sm txtboxDisable" Width="120px" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="display: block; float: left; width: 352px;">
                                    <div style="display: block; float: left; width: 166px; text-align: right; margin-top: 7px; margin-right: 14px;">
                                        Duration
                                    </div>
                                    <div style="display: block; float: left; width: 172px;">
                                        <asp:TextBox ID="lbDuration" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                Observer #1
                            </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 360px;">
                                    <asp:TextBox ID="lbOwnerFullname" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                </div>
                                <div style="display: block; float: left; width: 180px;">
                                    <asp:TextBox ID="lbOwnerDowId" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                                <div style="display: block; float: left; width: 172px;">
                                    <asp:TextBox ID="lbOwnerDepart" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row" style="padding: 3px 16px 3px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                    Other Observers
                                </div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 712px; color: darkblue !important;">
                                        <telerik:RadGrid ID="rgObserverList" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" ShowHeader="False" GroupPanelPosition="Top" DataSourceID="srcObserverList">
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                            <MasterTableView DataSourceID="srcObserverList">
                                                <NoRecordsTemplate>
                                                    <div style="padding: 6px 0 6px 7px; color: darkblue !important;">
                                                        No Other Observers.
                                                    </div>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" Visible="False">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="empFullName" UniqueName="empFullName">
                                                        <ItemStyle Width="238px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="empDowId" UniqueName="empDowId">
                                                        <ItemStyle Width="120px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="departName" UniqueName="departName">
                                                        <ItemStyle Width="100px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="empEmail" UniqueName="empEmail">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" ForeColor="darkblue" />
                                            <ItemStyle Height="30px" BackColor="#D5DCE3" ForeColor="darkblue" />
                                        </telerik:RadGrid>
                                        <asp:SqlDataSource ID="srcObserverList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT dbo.tblRecordOthEmpO.Id, dbo.tblRecordOthEmpO.recId, dbo.tblRecordOthEmpO.recItem, dbo.tblRecordOthEmpO.empIdOth, dbo.tblEmployee.empDowId, dbo.tblDepartment.departName, dbo.tblEmployee.empFullName, dbo.tblEmployee.empDisplay, dbo.tblEmployee.empEmail FROM dbo.tblRecordOthEmpO INNER JOIN dbo.tblEmployee ON dbo.tblRecordOthEmpO.empIdOth = dbo.tblEmployee.empId INNER JOIN dbo.tblDepartment ON dbo.tblEmployee.departId = dbo.tblDepartment.departId WHERE (dbo.tblRecordOthEmpO.recId = @recId)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="recId" SessionField="recId" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row" style="padding: 16px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                                Observe No 
                            </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <asp:TextBox ID="lbObserveNo" runat="server" CssClass="form-control input-sm txtboxDisableHead" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control input-sm txtboxDisable" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <asp:TextBox ID="tbCategory" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbCategorySub" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 532px;">
                                    <asp:TextBox ID="tbFailurePoint" runat="server" CssClass="form-control input-sm txtboxDisable" Width="712px" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="tbEquipment" runat="server" CssClass="form-control input-sm txtboxDisable" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" Enabled="False" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" Enabled="False" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" Enabled="False" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnHRO" runat="server" Visible="false">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 6px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHROop1" runat="server" CssClass="chkBT2m" Enabled="false" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHROop2" runat="server" CssClass="chkBT2m" Enabled="false" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHROop3" runat="server" CssClass="chkBT2m" Enabled="false" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHROop4" runat="server" CssClass="chkBT2m" Enabled="false" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHROop5" runat="server" CssClass="chkBT2m" Enabled="false" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <asp:TextBox ID="tbObserveType" runat="server" CssClass="form-control input-sm txtboxDisable" Width="172px" Enabled="False"></asp:TextBox>
                                </div>
                                <div style="display: block; float: left; width: 525px;">
                                    <asp:TextBox ID="tbContractor" runat="server" CssClass="form-control input-sm txtboxDisable" Width="532px" Enabled="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnShowImage" runat="server" Visible="false">
                            <div class="row" style="padding: 3px 16px 3px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                                <div class="col-md-9">
                                    <div class="col-md-12" style="margin: 0px 0px 0px -14px;">
                                        <asp:DataList ID="PictureList" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture">
                                            <ItemTemplate>
                                                <div style="display: block; float: left; width: 178px;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                    <telerik:RadToolTip ID="RadToolTip" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image1">
                                                        <asp:Image ID="ImageView1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="520px" />
                                                    </telerik:RadToolTip>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:SqlDataSource ID="srcPicture" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM dbo.tblRecordPictureO WHERE (recId = @recId) AND (observeItem = @observItem)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="recId" SessionField="recId" />
                                                <asp:SessionParameter Name="observItem" SessionField="observItem" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row" style="padding: 3px 16px 3px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Description : </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control input-sm txtboxDisable" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row thisReadOnly" id="pnActionA" runat="server" style="padding: 3px 16px 3px 16px;">
                                <div style="display: block; float: left; width: 154px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbActionA" runat="server" CssClass="form-control input-sm txtboxDisable" Height="88px" Width="352px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <asp:TextBox ID="tbResponA" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                        </div>
                                        <asp:HiddenField ID="hfStatusA" runat="server" />
                                        <div class="statusIcon">
                                            <asp:Image ID="imgStatusA" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                        </div>
                                        <div class="statusText">
                                            <asp:Label ID="lbStatusA" runat="server" Text="STATUS"></asp:Label>
                                        </div>
                                        <div id="chkBorderA" class="statusComplete" runat="server">
                                            <asp:CheckBox ID="chkCompleteA" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnResponB" runat="server" Visible="false">
                                <div class="row thisReadOnly" id="pnActionB" runat="server" style="padding: 3px 16px 3px 16px">
                                    <div style="display: block; float: left; width: 154px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbActionB" runat="server" CssClass="form-control input-sm txtboxDisable" Height="88px" Width="352px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <asp:TextBox ID="tbResponB" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                            </div>
                                            <asp:HiddenField ID="hfStatusB" runat="server" />
                                            <div class="statusIcon">
                                                <asp:Image ID="imgStatusB" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                            </div>
                                            <div class="statusText">
                                                <asp:Label ID="lbStatusB" runat="server" Text="STATUS"></asp:Label>
                                            </div>
                                            <div id="chkBorderB" class="statusComplete" runat="server">
                                                <asp:CheckBox ID="chkCompleteB" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnResponC" runat="server" Visible="false">
                                <div class="row thisReadOnly" id="pnActionC" runat="server" style="padding: 3px 16px 3px 16px">
                                    <div style="display: block; float: left; width: 154px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbActionC" runat="server" CssClass="form-control input-sm txtboxDisable" Height="88px" Width="352px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <asp:TextBox ID="tbResponC" runat="server" CssClass="form-control input-sm txtboxDisable" Width="352px" Enabled="False"></asp:TextBox>
                                            </div>
                                            <asp:HiddenField ID="hfStatusC" runat="server" />
                                            <div class="statusIcon">
                                                <asp:Image ID="imgStatusC" runat="server" ImageUrl="~/Images/status-blank-20.png" />
                                            </div>
                                            <div class="statusText">
                                                <asp:Label ID="lbStatusC" runat="server" Text="STATUS"></asp:Label>
                                            </div>
                                            <div id="chkBorderC" class="statusComplete" runat="server">
                                                <asp:CheckBox ID="chkCompleteC" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Validated" Enabled="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnUpdateButton" runat="server" Visible="false">
                                <div class="row" style="padding: 12px 16px 0px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">&nbsp;</div>
                                    <div class="col-md-9">
                                        <asp:Button ID="btUpdate" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Update" CommandName="Update" />&nbsp;
                                    <asp:Button ID="btBack" runat="server" Height="30px" class="btn btn-warning btn-mo30" Text="Other Follow Up" PostBackUrl="~/followup/followupList.aspx" />&nbsp;                                                            
                                    <span style="padding-left: 24px;">
                                        <asp:Label ID="lbUpdateInfo" runat="server" Text="" ForeColor="OrangeRed"></asp:Label></span>
                                    </div>
                                </div>
                            </asp:Panel>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <div class="row" style="padding: 8px 16px 8px 32px"></div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <br />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnResponB" />
                    <telerik:AjaxUpdatedControl ControlID="pnResponC" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap"></telerik:RadAjaxLoadingPanel>
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
