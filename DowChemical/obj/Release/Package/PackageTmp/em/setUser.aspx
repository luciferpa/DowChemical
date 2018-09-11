<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setUser.aspx.vb" Inherits="DowChemical.setUser" %>

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
        function ClearEmployee() {
            var tbDowId = document.getElementById("<%=tbDowId.ClientID%>");
            tbDowId.value = "";
            var tbName = document.getElementById("<%=tbName.ClientID%>");
            tbName.value = "";
            var tbSurname = document.getElementById("<%=tbSurname.ClientID%>");
            tbSurname.value = "";
            var tbEmail = document.getElementById("<%=tbEmail.ClientID%>");
            tbEmail.value = "";

            var cbDepart = $find("<%= rcbDepartment.ClientID %>");
            cbDepart.get_items().getItem(0).select();
            cbDepart.commitChanges();
            var cbjoblevel = $find("<%= rcbJobLevel.ClientID %>");
            cbjoblevel.get_items().getItem(0).select();
            cbjoblevel.commitChanges();

            document.getElementById("<%=chkEnUser.ClientID%>").checked = false;
        }
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 8px 0 0 16px;">
            User Account / Employee Setting
        </div>
        <div id="f-leftsidebar">
            <div class="row">
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
                                <telerik:RadPanelItem runat="server" Text="USER / EMPLOYEE" NavigateUrl="~/em/setUser.aspx?sel=setuser" Selected="true">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="BlackMetroTouch" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" Width="100%" Font-Bold="False">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="ADD EMPLOYEE" Font-Bold="True" Font-Size="12px" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="EMPLOYEE LIST" Font-Bold="True" Font-Size="12px">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div class="row" style="padding: 32px 16px 3px 3px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Dow ID </div>
                                <div class="col-md-8">
                                    <div style="display: block; float: left; width: 274px;">
                                        <asp:TextBox ID="tbDowId" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 3px 16px 3px 3px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Name </div>
                                <div class="col-md-8">
                                    <div style="display: block; float: left; width: 274px;">
                                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Surname </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="tbSurname" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 3px 16px 3px 3px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Email </div>
                                <div class="col-md-8">
                                    <div style="display: block; float: left; width: 274px;">
                                        <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">&nbsp;&nbsp;</div>
                                    <div class="col-md-4">&nbsp;&nbsp;</div>
                                </div>
                            </div>
                            <div class="row" style="padding: 3px 16px 3px 3px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Department </div>
                                <div class="col-md-8">
                                    <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [departId], [departName] FROM [tblDepartment]"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="row" style="padding: 3px 16px 3px 3px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Job Level </div>
                                <div class="col-md-8">
                                    <div style="display: block; float: left; width: 172px;">
                                        <telerik:RadComboBox ID="rcbJobLevel" runat="server" Skin="Metro" Width="172px">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="fsfl" Value="fsfl" />
                                                <telerik:RadComboBoxItem runat="server" Text="tech" Value="tech" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-1" style="display: block; float: left; width: 24px; padding-top: 5px; margin-left: -8px;">
                                        <asp:ImageButton ID="imbtNewJobLevel" runat="server" ImageUrl="~/Images/add-circle-red-18.png" Visible="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding: 12px 16px 4px 2px">
                                <div style="display: block; float: left; width: 164px; text-align: right; margin-top: 6px;">&nbsp;&nbsp;</div>
                                <div class="col-md-8">
                                    <asp:CheckBox ID="chkEnUser" runat="server" Text="&nbsp;&nbsp;Enable Employee" CssClass="chkBT2m" />
                                </div>
                            </div>
                            <div style="padding: 0 0 0 0">
                                <div style="padding: 16px 0 0 0; color: #3366FF; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #E4E4E4;"></div>
                                <div style="padding: 16px 0 24px 178px;">
                                    <asp:Button ID="btAddUser" runat="server" class="btn btn-primary" Text="&nbsp;&nbsp;Add&nbsp;&nbsp;" ClientIDMode="Static" />&nbsp;&nbsp;
                                    <input id="btCancelUser" runat="server" type="button" value="&nbsp;&nbsp;Cancel&nbsp;&nbsp;" class="btn btn-primary" onclick="ClearEmployee();" />
                                </div>
                            </div>
                            <div>
                                <div style="padding: 0px 0px 2px 16px; color: #282828; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">Last Employeee </div>
                                <telerik:RadGrid ID="rgEmployeeLast" runat="server" Skin="Metro" PageSize="5" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%" DataSourceID="srcEmployeeLast">
                                    <MasterTableView DataKeyNames="empId" DataSourceID="srcEmployeeLast">
                                        <EditItemTemplate></EditItemTemplate>
                                        <NoRecordsTemplate>
                                            <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="actioncolumn">
                                                <HeaderTemplate>
                                                    <div style="height: 25px"></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="padding-right: 0; padding-left: 16px;">
                                                        <asp:Image ID="imgbEdit" runat="server" ImageUrl="~/Images/pen-checkbox-24-gray72-h.png" />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                                <ItemStyle Width="60px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="empId" DataType="System.Int32" HeaderText="empId" UniqueName="empId" Visible="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="empDowId" FilterControlAltText="" HeaderText="Dow ID" SortExpression="empDowId" UniqueName="empdowId">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="88px" HorizontalAlign="Left" />
                                                <ItemStyle Width="88px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Employee Name" SortExpression="empName, empSurname" UniqueName="fullname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEmployeeName" runat="server" Text='<%# Eval("empName")%>' ToolTip='<%# Eval("empDisplay")%>'></asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="lbEmployeeSurName" runat="server" Text='<%# Eval("empSurname")%>' ToolTip='<%# Eval("empDisplay")%>'></asp:Label>&nbsp;
                                                    <asp:Label ID="lbEmployeeLoginName" runat="server" Text='<%# Eval("userName") %>' Font-Bold="True" ForeColor="#0066CC"></asp:Label>&nbsp;
                                                    <asp:Image ID="imgAccountType" runat="server" ImageUrl="~/Images/user-16h24-blank.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="empEmail" FilterControlAltText="" HeaderText="Email" SortExpression="empEmail" UniqueName="empemail">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="220px" HorizontalAlign="Left" />
                                                <ItemStyle Width="220px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="departName" FilterControlAltText="" HeaderText="Department" SortExpression="departName" UniqueName="departname">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="112px" HorizontalAlign="Left" />
                                                <ItemStyle Width="112px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="joblvCode" FilterControlAltText="" HeaderText="Job level" SortExpression="joblvCode" UniqueName="joblvcode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="88px" HorizontalAlign="Left" />
                                                <ItemStyle Width="88px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="actionColumn2" HeaderText=" ">
                                                <ItemTemplate>
                                                    <div style="position: relative; top: 2px; padding-left: 8px;">
                                                        <asp:ImageButton ID="imbEditEmpEnable" runat="server" ImageUrl="~/Images/user-24.png" ToolTip="user enable" CommandName="empenable" /><asp:HiddenField ID="hfEditEmpEnable" runat="server" Value='<%# Eval("empEnable") %>' />
                                                        &#160;&#160;<asp:ImageButton ID="imbSetLogin" runat="server" ImageUrl="~/Images/IsKey-gray80.png" ToolTip="not create user login" CommandName="setlogin" /><asp:HiddenField ID="hfIsSetLogin" runat="server" Value='<%# Eval("IsSetLogin")%>' />
                                                        <asp:HiddenField ID="hfUserName" runat="server" Value='<%# Eval("userName")%>' />
                                                        <asp:HiddenField ID="hfAccType" runat="server" Value='<%# Eval("typeId") %>' />
                                                        <asp:HiddenField ID="hfAspNetUserId" runat="server" Value='<%# Eval("aspNetUserId") %>' />
                                                        <asp:HiddenField ID="hfDisplayName" runat="server" Value='<%# Eval("empDisplay") %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="180px" HorizontalAlign="Left" />
                                                <ItemStyle Width="180px" HorizontalAlign="Left" CssClass="gridoverflow" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate></FormTemplate>
                                        </EditFormSettings>
                                        <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                                    <ItemStyle Height="32px" BackColor="#D5DCE3" />
                                    <FooterStyle Height="32px" />
                                </telerik:RadGrid><asp:SqlDataSource ID="srcEmployeeLast" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                                    SelectCommand="SELECT TOP (5) tblEmployee.empId, tblEmployee.empDowId, tblEmployee.empName, tblEmployee.empSurname, tblEmployee.empEmail, tblEmployee.empContact, tblEmployee.empMobile, tblEmployee.joblvCode, tblEmployee.empEnable, tblEmployee.IsSetLogin, tblEmployee.userName, tblEmployee.typeId, tblEmployee.aspNetUserId, tblEmployee.empFullName, tblEmployee.empDisplay, tblEmployee.departId FROM tblEmployee INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId WHERE (tblEmployee.IsVisible = 'True') ORDER BY tblEmployee.empId DESC"></asp:SqlDataSource>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <div class="row" style="padding: 8px 0px 4px 16px;">
                                <div class="col-md-10">
                                    <div style="display: block; float: left; width: 76px; text-align: right; margin-top: 6px;">Search </div>
                                    <div style="display: block; float: left; width: 43px; margin-top: 0px; padding-left: 6px;">
                                        <img alt="" src="../Images/search-28-normal.png" />
                                    </div>
                                    <div style="display: block; float: left; width: 280px;">
                                        <asp:TextBox ID="tbSearchKeyword" runat="server" CssClass="form-control input-sm" Width="272px"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 80px;">
                                        <asp:Button ID="btSearchBox" runat="server" Height="30px" class="btn btn-default btn-mo30" Text="Search" />
                                    </div>
                                    <div style="display: block; float: left; width: 32px; margin-top: 3px; margin-left: 476px; position: absolute">
                                        <asp:ImageButton ID="imgbClearKeyword" runat="server" ImageUrl="~/Images/bt_close-24.png" ToolTip="clear" Visible="false" />
                                    </div>
                                </div>
                                <div class="col-md-2" style="padding-bottom: 4px; padding-right: 0px;">
                                    <div class="pull-right" style="display: block;">
                                        <span style="padding-right: 6px;">
                                            <telerik:RadComboBox ID="rcbDepartmentView" runat="server" Skin="Metro" Width="153px" DataTextField="departName" DataValueField="departId" AutoPostBack="True" DataSourceID="srcDepartment" ItemsPerRequest="0"></telerik:RadComboBox>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <telerik:RadGrid ID="rgEmployeeList" runat="server" Skin="Metro" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%" PageSize="50">
                                    <MasterTableView DataKeyNames="empId, IsSetLogin, userName">
                                        <EditItemTemplate></EditItemTemplate>
                                        <NoRecordsTemplate>
                                            <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="actioncolumn">
                                                <HeaderTemplate>
                                                    <div style="height: 25px"></div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="padding-right: 0; padding-left: 16px;">
                                                        <asp:ImageButton ID="imgbEdit" runat="server" CommandName="edit" ImageUrl="~/Images/pen-checkbox-24-gray36.png" ToolTip="edit" />
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="60px" />
                                                <ItemStyle Width="60px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="empId" DataType="System.Int32" HeaderText="empId" UniqueName="empId" Visible="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="empDowId" FilterControlAltText="" HeaderText="Dow ID" SortExpression="empDowId" UniqueName="empdowId">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="88px" HorizontalAlign="Left" />
                                                <ItemStyle Width="88px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Employee Name" SortExpression="empName, empSurname" UniqueName="fullname">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbEmployeeName" runat="server" Text='<%# Eval("empName")%>' ToolTip='<%# Eval("empDisplay")%>'></asp:Label>&nbsp;&nbsp;
                                                    <asp:Label ID="lbEmployeeSurName" runat="server" Text='<%# Eval("empSurname")%>' ToolTip='<%# Eval("empDisplay")%>'></asp:Label>&nbsp;
                                                    <asp:Label ID="lbEmployeeLoginName" runat="server" Text='<%# Eval("userName") %>' Font-Bold="True" ForeColor="#0066CC"></asp:Label>&nbsp;
                                                    <asp:Image ID="imgAccountType" runat="server" ImageUrl="~/Images/user-16h24-blank.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="empEmail" FilterControlAltText="" HeaderText="Email" SortExpression="empEmail" UniqueName="empemail">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="220px" HorizontalAlign="Left" />
                                                <ItemStyle Width="220px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="departName" FilterControlAltText="" HeaderText="Department" SortExpression="departName" UniqueName="departname">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="112px" HorizontalAlign="Left" />
                                                <ItemStyle Width="112px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="joblvCode" FilterControlAltText="" HeaderText="Job level" SortExpression="joblvCode" UniqueName="joblvcode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text="" />
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="88px" HorizontalAlign="Left" />
                                                <ItemStyle Width="88px" HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn UniqueName="actioncolumn2" HeaderText=" ">
                                                <ItemTemplate>
                                                    <div style="position: relative; top: 2px; padding-left: 8px;">
                                                        <asp:ImageButton ID="imbEditEmpEnable" runat="server" ImageUrl="~/Images/user-24.png" ToolTip="user enable" CommandName="empenable" />
                                                        <asp:HiddenField ID="hfEditEmpEnable" runat="server" Value='<%# Eval("empEnable") %>' />
                                                        &#160;&#160;<asp:ImageButton ID="imbSetLogin" runat="server" ImageUrl="~/Images/IsKey-gray80.png" ToolTip="not create user login" CommandName="setlogin" />
                                                        <asp:HiddenField ID="hfIsSetLogin" runat="server" Value='<%# Eval("IsSetLogin")%>' />
                                                        <asp:HiddenField ID="hfUserName" runat="server" Value='<%# Eval("userName")%>' />
                                                        <asp:HiddenField ID="hfAccType" runat="server" Value='<%# Eval("typeId") %>' />
                                                        <asp:HiddenField ID="hfAspNetUserId" runat="server" Value='<%# Eval("aspNetUserId") %>' />
                                                        <asp:HiddenField ID="hfDisplayName" runat="server" Value='<%# Eval("empDisplay") %>' />
                                                        &#160;&#160;<span style="padding-left: 48px"><asp:ImageButton ID="imgbDel" runat="server" ImageUrl="~/Images/bt_delete-24.png" OnClientClick="javascript:if(!confirm('&nbsp;&nbsp;&nbsp;&nbsp;Confirm Delete?')){return false;}" ToolTip="delete employee" CommandName="Delete" Visible="False" /></span>
                                                    </div>
                                                </ItemTemplate>
                                                <HeaderStyle Width="180px" HorizontalAlign="Left" />
                                                <ItemStyle Width="180px" HorizontalAlign="Left" CssClass="gridoverflow" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div style="width: 100%; border-top-style: solid; border-top-width: 1px; border-top-color: #999999;">
                                                    <asp:HiddenField ID="hfAspNetUserIdOri" runat="server" Value='<%# Eval("aspNetUserId") %>' />
                                                    <div class="row" style="padding: 18px 16px 3px 5px">
                                                        <div style="display: block; float: left; width: 140px; text-align: right; margin-top: 6px;">Dow ID </div>
                                                        <div class="col-md-8">
                                                            <div style="display: block; float: left; width: 258px;">
                                                                <asp:TextBox ID="tbEditDowId" runat="server" Text='<%# Bind("empDowId") %>' CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                                <asp:HiddenField ID="hfEditDowId" runat="server" Value='<%# Bind("empDowId") %>' />
                                                            </div>
                                                            <div class="col-md-1" style="width: 24px; margin: 4px 0px 0px 0px;">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/pen-checkbox-24-blue-h.png" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 3px 16px 3px 5px">
                                                        <div style="display: block; float: left; width: 140px; text-align: right; margin-top: 6px;">Name </div>
                                                        <div class="col-md-8">
                                                            <div style="display: block; float: left; width: 274px;">
                                                                <asp:TextBox ID="tbEditName" runat="server" Text='<%# Bind("empName") %>' CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                                <asp:HiddenField ID="hfEditName" runat="server" Value='<%# Bind("empName") %>' />
                                                            </div>
                                                            <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Surname </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="tbEditSurname" runat="server" Text='<%# Bind("empSurname") %>' CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                                <asp:HiddenField ID="hfEditSurname" runat="server" Value='<%# Bind("empSurname") %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 3px 16px 3px 5px">
                                                        <div style="display: block; float: left; width: 140px; text-align: right; margin-top: 6px;">Email </div>
                                                        <div class="col-md-8">
                                                            <div style="display: block; float: left; width: 274px;">
                                                                <asp:TextBox ID="tbEditEmail" runat="server" Text='<%# Bind("empEmail") %>' CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                                <asp:HiddenField ID="hfEditEmail" runat="server" Value='<%# Bind("empEmail") %>' />
                                                            </div>
                                                            <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Display name </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="tbEditDisplay" runat="server" Text='<%# Bind("empDisplay") %>' CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                                <asp:HiddenField ID="hfEditDisplay" runat="server" Value='<%# Bind("empDisplay") %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 3px 16px 3px 5px">
                                                        <div style="display: block; float: left; width: 140px; text-align: right; margin-top: 6px;">Department </div>
                                                        <div class="col-md-8">
                                                            <telerik:RadComboBox ID="rcbEditDepart" runat="server" SelectedValue='<%# Bind("departId") %>' DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId" Skin="Metro" Width="172px"></telerik:RadComboBox>
                                                            <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT departId, departName FROM tblDepartment"></asp:SqlDataSource>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="padding: 3px 16px 3px 5px">
                                                        <div style="display: block; float: left; width: 140px; text-align: right; margin-top: 6px;">Job Level </div>
                                                        <div class="col-md-8">
                                                            <div style="display: block; float: left; width: 172px;">
                                                                <telerik:RadComboBox ID="rcbEditJobLevel" runat="server" SelectedValue='<%# Bind("joblvCode") %>' Skin="Metro" Width="172px">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem runat="server" Text="fsfl" Value="fsfl" />
                                                                        <telerik:RadComboBoxItem runat="server" Text="tech" Value="tech" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="padding: 8px 0px 2px 161px;">
                                                    <div style="padding: 0px 0px 8px 0px; color: #FF3300; font-size: 1em; font-weight: normal;">
                                                        <asp:Label ID="lbErrorMsgEditMode" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <asp:Button ID="btUpdEmployee" runat="server" CssClass="btn btn-primary" Text="Update" CommandName="Update" />&#160;&#160;
                                                    <asp:Button ID="btCancel" runat="server" CssClass="btn btn-primary" Text="Close" CommandName="cancel" />&#160;&#160;
                                                </div>
                                                <div style="height: 24px; border-bottom-color: chartreuse;">
                                                    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecorationZoneID="Favorite" Skin="Metro" />
                                                </div>
                                            </FormTemplate>
                                        </EditFormSettings>
                                        <HeaderStyle BackColor="#F1F5FB" Font-Bold="True" Font-Size="12px" ForeColor="#506C8C" HorizontalAlign="Center" />
                                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <AlternatingItemStyle Height="32px" BackColor="#D5DCE3" />
                                    <ItemStyle Height="32px" BackColor="#D5DCE3" />
                                    <FooterStyle Height="32px" />
                                </telerik:RadGrid>
                                <asp:SqlDataSource ID="srcEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
                                    SelectCommand="SELECT tblEmployee.empId, tblEmployee.empDowId, tblEmployee.empName, tblEmployee.empSurname, tblEmployee.empEmail, tblEmployee.empContact, tblEmployee.empMobile, tblEmployee.joblvCode, tblEmployee.empEnable, tblEmployee.IsSetLogin, tblEmployee.userName, tblEmployee.typeId, tblEmployee.aspNetUserId, tblEmployee.empFullName, tblEmployee.empDisplay, tblDepartment.departName, tblEmployee.departId FROM tblEmployee INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId WHERE (tblEmployee.IsVisible = 'True') AND (tblEmployee.joblvCode &lt;&gt; 'admin') ORDER BY tblEmployee.empName, tblEmployee.empSurname"
                                    DeleteCommand="DELETE FROM tblEmployee WHERE (empId = @empId)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="empId" Type="String" />
                                    </DeleteParameters>
                                </asp:SqlDataSource>
                                <div style="text-align: right; padding-right: 48px; padding-top: 4px; font-size: 0.95em; height: 24px;">
                                    <asp:CheckBox ID="cbShow_Del" runat="server" Text="&nbsp;Show delete button"
                                        CssClass="chkBT2m" Font-Size="0.95em" AutoPostBack="True" Checked="False" />
                                </div>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <asp:HiddenField ID="hfEmployeeId" runat="server" Value="0" />
                            <div>
                                <div class="row" style="padding: 32px 16px 3px 3px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Dow ID </div>
                                    <div class="col-md-8">
                                        <div style="display: block; float: left; width: 274px;">
                                            <asp:TextBox ID="tbDowId_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding: 3px 16px 3px 3px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Name </div>
                                    <div class="col-md-8">
                                        <div style="display: block; float: left; width: 274px;">
                                            <asp:TextBox ID="tbName_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Surname </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tbSurname_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding: 3px 16px 3px 3px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Email </div>
                                    <div class="col-md-8">
                                        <div style="display: block; float: left; width: 274px;">
                                            <asp:TextBox ID="tbEmail_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Display name </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tbDiaplay_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding: 3px 16px 3px 3px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Department </div>
                                    <div class="col-md-8">
                                        <div style="display: block; float: left; width: 274px;">
                                            <asp:TextBox ID="tbDepart_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 100px; text-align: right; margin-top: 6px;">Job Level </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="tbJoblv_Acc" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfRoleName" runat="server" Value="USER" />
                            </div>
                            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0" Width="100%">
                                <telerik:RadPageView ID="RadMultiPage2_Create" runat="server">
                                    <div style="padding: 16px 0 2px 16px; color: #282828; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">Create a new account </div>
                                    <div style="padding: 16px 0px 0px 0px; color: #282828; background-color: #ffeded">
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Account Type </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <telerik:RadComboBox ID="rcbAccountType" runat="server" Skin="Metro" Width="172px" DataSourceID="srcAccountType" DataTextField="typeName" DataValueField="typeId"></telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="srcAccountType" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT typeId, typeName, sortIdx FROM tblAccountType ORDER BY sortIdx"></asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Username </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbUsername" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Password </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control input-sm" Width="266px" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Confirm Password </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbPasswordConfirm" runat="server" CssClass="form-control input-sm" Width="266px" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding: 0 0 0 0">
                                            <div style="padding: 16px 0 2px 178px; color: #FF3300; font-size: 1em; font-weight: normal; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #E4E4E4;">
                                                <asp:Label ID="lbErrorMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 16px 0 8px 178px;">
                                        <asp:Button ID="btCreateAccount" runat="server" class="btn btn-primary" Text="&nbsp;&nbsp;Create Account&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btBack" runat="server" class="btn btn-primary" Text="&nbsp;&nbsp;Cancel&nbsp;&nbsp;" />
                                    </div>
                                    <div style="padding: 16px 0 0 178px;"></div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadMultiPage2_Change" runat="server">
                                    <div style="padding: 16px 0 2px 16px; color: #282828; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">Change to your account </div>
                                    <div style="padding: 16px 0px 0px 0px; color: #282828; background-color: #ffeded">
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Account Type </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <telerik:RadComboBox ID="rcbAccountType_Change" runat="server" Skin="Metro" Width="172px" DataSourceID="srcAccountType" DataTextField="typeName" DataValueField="typeId"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Username </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbUsername_Change" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox><asp:HiddenField ID="hfUsername_Ori" runat="server" />
                                                    <asp:HiddenField ID="hfAspNetUserId_Ori" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Email </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbEmail_Change" runat="server" CssClass="form-control input-sm" Width="266px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding: 0 0 0 0">
                                            <div style="padding: 16px 0 2px 178px; color: #FF3300; font-size: 1em; font-weight: normal; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #E4E4E4;">
                                                <asp:Label ID="lbErrorMsg_Change" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 16px 0 8px 178px;">
                                        <asp:Button ID="btEditAccount" runat="server" class="btn btn-primary" Text="&nbsp;&nbsp;Update Account&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btBack_Change" runat="server" class="btn btn-primary" Text="&nbsp;&nbsp;Cancel&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btResetPassword_Change" runat="server" class="btn btn-danger" Text="&nbsp;&nbsp;Reset Password&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btDeleteAccount" runat="server" class="btn btn-danger" Text="&nbsp;&nbsp;Delete Account&nbsp;&nbsp;" />
                                    </div>
                                    <div style="padding: 16px 0 0 178px;"></div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadMultiPage2_Reset" runat="server">
                                    <div style="padding: 16px 0 2px 16px; color: #CC0000; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">Reset your account </div>
                                    <div style="padding: 16px 0px 0px 0px; color: #282828; background-color: #ffeded">
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Username </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbUsername_Reset" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Password </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbPassword_Reset" runat="server" CssClass="form-control input-sm" Width="266px" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 3px 16px 3px 3px">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Confirm Password </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbPasswordConfirm_Reset" runat="server" CssClass="form-control input-sm" Width="266px" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding: 0 0 0 0">
                                            <div style="padding: 16px 0 2px 178px; color: #FF3300; font-size: 1em; font-weight: normal; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #E4E4E4;">
                                                <asp:Label ID="lbErrorMsg_Reset" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding: 16px 0 8px 178px;">
                                        <asp:Button ID="btResetPassword_Reset" runat="server" class="btn btn-danger" Text="&nbsp;&nbsp;Reset Password&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btBack_Reset" runat="server" class="btn btn-danger" Text="&nbsp;&nbsp;Cancel&nbsp;&nbsp;" />
                                    </div>
                                    <div style="padding: 16px 0 0 178px;"></div>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadMultiPage2_Delete" runat="server">
                                    <div style="padding: 16px 0 2px 16px; color: #CC0000; font-size: 1.2em; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">Confirm to delete your account </div>
                                    <div style="padding: 16px 0px 0px 0px; color: #282828; background-color: #ffeded">
                                        <div class="row" style="padding: 3px 16px 3px 3px;">
                                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 6px;">Username </div>
                                            <div class="col-md-8">
                                                <div style="display: block; float: left; width: 274px;">
                                                    <asp:TextBox ID="tbUsername_Delete" runat="server" CssClass="form-control input-sm" Width="266px" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="height: 45px"></div>
                                        <div style="padding: 24px 0 2px 178px; font-size: 1.1em; font-weight: 600; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #E4E4E4;">Are you sure want to delete this account ? </div>
                                    </div>
                                    <div style="padding: 16px 0 8px 178px;">
                                        <asp:Button ID="btConfirmDelete" runat="server" class="btn btn-primary btn-danger" Text="&nbsp;&nbsp;Confirm Delete&nbsp;&nbsp;" />&nbsp;&nbsp;
                                        <asp:Button ID="btBack_Delete" runat="server" class="btn btn-danger" Text="&nbsp;&nbsp;Cancel&nbsp;&nbsp;" />
                                    </div>
                                    <div style="padding: 16px 0 0 178px;"></div>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div>
        </div>
    </div>
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
