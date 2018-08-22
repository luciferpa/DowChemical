<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moEmployee.aspx.vb" Inherits="DowChemical.moEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="DisplayName" />
                    <asp:Button ID="Button2" runat="server" Text="DepartName" />
                    <asp:Button ID="Button3" runat="server" Text="AccountType" />
                    <asp:Button ID="Button4" runat="server" Text="UserAccount" />
                    <asp:Button ID="Button5" runat="server" Text="CreateAccount" />
                    <asp:Button ID="Button6" runat="server" Text="Check Repeat" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GroupPanelPosition="Top">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="empId" DataSourceID="SqlDataSource1">
                            <Columns>
                                <telerik:GridBoundColumn DataField="empId" DataType="System.Int32" FilterControlAltText="Filter empId column" HeaderText="empId" ReadOnly="True" SortExpression="empId" UniqueName="empId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empDowId" FilterControlAltText="Filter empDowId column" HeaderText="empDowId" SortExpression="empDowId" UniqueName="empDowId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empName" FilterControlAltText="Filter empName column" HeaderText="empName" SortExpression="empName" UniqueName="empName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empSurname" FilterControlAltText="Filter empSurname column" HeaderText="empSurname" SortExpression="empSurname" UniqueName="empSurname">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empFullName" FilterControlAltText="Filter empFullName column" HeaderText="empFullName" SortExpression="empFullName" UniqueName="empFullName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empDisplay" FilterControlAltText="Filter empDisplay column" HeaderText="empDisplay" SortExpression="empDisplay" UniqueName="empDisplay">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="empEmail" FilterControlAltText="Filter empEmail column" HeaderText="empEmail" SortExpression="empEmail" UniqueName="empEmail">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="plantId" DataType="System.Int32" FilterControlAltText="Filter plantId column" HeaderText="plantId" SortExpression="plantId" UniqueName="plantId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="departId" DataType="System.Int32" FilterControlAltText="Filter departId column" HeaderText="departId" SortExpression="departId" UniqueName="departId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="departName" FilterControlAltText="Filter departName column" HeaderText="departName" SortExpression="departName" UniqueName="departName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="joblvCode" FilterControlAltText="Filter joblvCode column" HeaderText="joblvCode" SortExpression="joblvCode" UniqueName="joblvCode">
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataField="empEnable" DataType="System.Boolean" FilterControlAltText="Filter empEnable column" HeaderText="empEnable" SortExpression="empEnable" UniqueName="empEnable">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridCheckBoxColumn DataField="IsSetLogin" DataType="System.Boolean" FilterControlAltText="Filter IsSetLogin column" HeaderText="IsSetLogin" SortExpression="IsSetLogin" UniqueName="IsSetLogin">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="typeId" DataType="System.Int32" FilterControlAltText="Filter typeId column" HeaderText="typeId" SortExpression="typeId" UniqueName="typeId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="typeName" FilterControlAltText="Filter typeName column" HeaderText="typeName" SortExpression="typeName" UniqueName="typeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="userName" FilterControlAltText="Filter userName column" HeaderText="userName" SortExpression="userName" UniqueName="userName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="aspNetUserId" FilterControlAltText="Filter aspNetUserId column" HeaderText="aspNetUserId" SortExpression="aspNetUserId" UniqueName="aspNetUserId">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblEmployee]"></asp:SqlDataSource>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
