<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="moActionNumIsComplete.aspx.vb" Inherits="DowChemical.moActionNumIsComplete" %>

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
                    <asp:Button ID="Button2" runat="server" Text="Update summary Propose Action status" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Update summary Action Number status" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GroupPanelPosition="Top" CellSpacing="-1" GridLines="Both">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="recId" DataSourceID="SqlDataSource1">
                            <Columns>
                                <telerik:GridBoundColumn DataField="recId" DataType="System.Int32" FilterControlAltText="Filter recId column" HeaderText="recId" ReadOnly="True" SortExpression="recId" UniqueName="recId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="timestamp" FilterControlAltText="Filter timestamp column" HeaderText="timestamp" SortExpression="timestamp" UniqueName="timestamp" DataType="System.DateTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="departId" FilterControlAltText="Filter departId column" HeaderText="departId" SortExpression="departId" UniqueName="departId" DataType="System.Int32">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="recActNo" FilterControlAltText="Filter recActNo column" HeaderText="recActNo" SortExpression="recActNo" UniqueName="recActNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="noObserve" FilterControlAltText="Filter noObserve column" HeaderText="noObserve" SortExpression="noObserve" UniqueName="noObserve" DataType="System.Int32">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsComplete" FilterControlAltText="Filter IsComplete column" HeaderText="IsComplete" SortExpression="IsComplete" UniqueName="IsComplete" DataType="System.Int32">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT recId, timestamp, departId, recActNo, noObserve, IsComplete FROM dbo.tblRecord WHERE (recActMonth = @sMonth) ORDER BY recId DESC">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="sMonth" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <br />
                    <br />
                    <br />

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
