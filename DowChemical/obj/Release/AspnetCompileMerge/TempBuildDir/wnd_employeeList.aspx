<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="wnd_employeeList.aspx.vb" Inherits="DowChemical.wnd_employeeList" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <link href="~/Styles/siteUI.css" rel="stylesheet" />
    <link href="~/Styles/site_devx.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        html {
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfWhoCall" runat="server" Value="" />
                <asp:HiddenField ID="hfEmpFilter" runat="server" Value="0" />
                <asp:HiddenField ID="hfEmpFilterStr" runat="server" />
                <div style="padding-top: 12px;">
                    <table style="font-size: 0.85ex;">
                        <tr>
                            <td style="width: 200px;">
                                <asp:TextBox ID="tbSearchKeyword" runat="server" class="form-control input-sm"></asp:TextBox>
                            </td>
                            <td style="width: 40px; clear: both; padding-top: 6px; padding-left: 8px;">
                                <div class="bg_search_gray">
                                    <asp:ImageButton ID="imbSearch" runat="server"
                                        ImageUrl="~/Images/bt_search_normal.png" ToolTip="ค้นหาคำ" />
                                </div>
                            </td>
                            <td style="width: 20px; clear: both; vertical-align: bottom; padding-bottom: 0; padding-left: 4px;">
                                <asp:ImageButton ID="imbSearch_Cancel" runat="server"
                                    ImageUrl="~/Images/bt_close.png" ToolTip="ยกเลิกการค้นหา"
                                    Visible="False" />
                            </td>
                            <td style="width: 218px; clear: both; vertical-align: bottom; padding-bottom: 0; padding-left: 4px;">&nbsp;</td>
                            <td style="width: 60px; clear: both; vertical-align: bottom; padding-bottom: 2px; padding-left: 4px;">
                                <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId" AutoPostBack="True">
                                </telerik:RadComboBox>
                                <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding: 0 0 12px 0; font-size: 0.85ex;">
                    <div style="padding-top: 2px; padding-bottom: 2px; height: 319px;">
                        <asp:SqlDataSource ID="srcEmployeeList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblEmployee.empId, tblEmployee.empDowId, tblEmployee.empName, tblEmployee.empSurname, tblEmployee.empFullName, tblEmployee.empEmail, tblEmployee.empContact, tblEmployee.empMobile, tblEmployee.departId, tblEmployee.joblvCode, tblDepartment.departName FROM tblEmployee INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId WHERE (tblEmployee.empEnable = 'true') AND (tblEmployee.IsVisible = 'true')"></asp:SqlDataSource>
                        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                            DataSourceID="srcEmployeeList" KeyFieldName="empId"
                            AutoGenerateColumns="False" EnableCallBacks="False"
                            CssClass="leelawadeefont" Width="100%" Theme="Mulberry">
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption=" "
                                    ShowSelectCheckbox="False" VisibleIndex="0" Width="45px">
                                    <SelectButton Visible="True">
                                        <Image Url="~/Images/tick_1-18.png"></Image>
                                    </SelectButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="empId" VisibleIndex="1" Visible="False" ReadOnly="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="empName" VisibleIndex="2" Caption="Name" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="empSurname" VisibleIndex="3" Caption="Surname">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="empDowId" VisibleIndex="4" Caption="Dow Id" Width="120px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="departName" VisibleIndex="5" Caption="Department" Width="100px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowSelectSingleRowOnly="True"
                                AllowSelectByRowClick="True"
                                ProcessSelectionChangedOnServer="True" />
                            <SettingsPager CurrentPageNumberFormat="{0}">
                                <Summary Text="Page {0} / {1} ({2})" />
                            </SettingsPager>
                            <ImagesFilterControl>
                                <LoadingPanel Url="~/App_Themes/DevEx/GridView/Loading.gif">
                                </LoadingPanel>
                            </ImagesFilterControl>
                            <Styles>
                                <Header ImageSpacing="5px" SortingImageSpacing="5px" BackColor="#EEEEEE" ForeColor="#001844" CssClass="ASPxGrid_Head">
                                </Header>
                                <LoadingPanel ImageSpacing="5px">
                                </LoadingPanel>
                                <Row CssClass="ASPxGrid_RowHeight30"></Row>
                                <AlternatingRow CssClass="ASPxGrid_RowHeight30"></AlternatingRow>
                                <SelectedRow BackColor="#FFEFCE" ForeColor="#003366"></SelectedRow>
                            </Styles>
                            <StylesPager>
                                <CurrentPageNumber Font-Names="CenturyGothicEOT" Font-Size="1.2em" BackColor="#C0DCE8" ForeColor="#003468">
                                </CurrentPageNumber>
                                <PageNumber Font-Names="CenturyGothicEOT" Font-Size="1.2em" Paddings-PaddingTop="6px">
                                    <Paddings PaddingTop="6px" />
                                </PageNumber>
                                <Pager Font-Names="CenturyGothicEOT" Font-Size="0.80em" ForeColor="#333333">
                                </Pager>
                            </StylesPager>
                            <StylesEditors ButtonEditCellSpacing="0">
                                <ProgressBar Height="21px">
                                </ProgressBar>
                            </StylesEditors>
                            <Settings GridLines="Horizontal" />
                        </dx:ASPxGridView>
                    </div>
                    <asp:HiddenField ID="hfSelEmpId" runat="server" Value="0" />
                    <asp:HiddenField ID="hfSelEmpFullname" runat="server" Value="" />
                </div>
                <div style="padding: 8px 2px 12px 8px; width: 100%; text-align: right;">
                    <asp:Button ID="btSelect" runat="server" class="btn btn-primary " Text="Select"
                        OnClientClick="SelectCloseWindow();return false;" />                    
                    &nbsp;
                    <asp:Button ID="btCancel" runat="server" class="btn btn-primary " Text="Cancel"
                        OnClientClick="CloseWindow();return false;" />
                </div>
                <div style="font-size: 8px; padding-left: 8px; padding-top: 4px; width: 100%; background-color: #EEEEEE; padding-bottom: 4px;">1.01.50518</div>
                <script type="text/javascript">
                    function GetRadWindow() {
                        var oWindow = null;
                        if (window.radWindow)
                            oWindow = window.radWindow;
                        else
                            if (window.frameElement.radWindow)
                                oWindow = window.frameElement.radWindow;
                        return oWindow;
                    }
                    function SelectCloseWindow() {
                        var arg = new Object();
                        var empid = document.getElementById("<%=hfSelEmpId.ClientID%>");
                        arg.selEmpId = empid.value;
                        var empfullname = document.getElementById("<%=hfSelEmpFullname.ClientID%>");
                        arg.selEmpFullname = empfullname.value;

                        var wnd = GetRadWindow();
                        wnd.close(arg);
                    }
                    function CloseWindow() {
                        var wnd = GetRadWindow();
                        wnd.close();
                    }
                    function controlEnter(obj, event) {
                        var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
                        if (keyCode == 13) {
                            document.getElementById(obj).focus();
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
