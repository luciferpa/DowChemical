<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="setPlant.aspx.vb" Inherits="DowChemical.setPlant" %>

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
                <div id="f-header" style="color: lightgray; font-size: 1.6em; padding: 8px 0 0 16px;">
                    Plant Setting
                </div>
                <div id="f-leftsidebar">
                    <div style="height: 160px"></div>
                    <dx:ASPxMenu ID="ASPxMenu2" runat="server" AllowSelectItem="True"
                        Orientation="Vertical" ShowPopOutImages="True"
                        Width="100%" ForeColor="#255991">
                        <Items>
                            <dx:MenuItem Text="USER / EMPLOYEE" Name="emp" ToolTip="User Account / Employee Setting"
                                NavigateUrl="~/setUser.aspx?sel=setuser">
                            </dx:MenuItem>
                            <dx:MenuItem Text="DEPARTMENT " Name="department" ToolTip=""
                                NavigateUrl="~/setDepartment.aspx?sel=setdepart">
                            </dx:MenuItem>
                            <dx:MenuItem Text="PLANT " Name="plant" ToolTip=""
                                Selected="True" NavigateUrl="~/setPlant.aspx?sel=setplant">
                            </dx:MenuItem>
                            <dx:MenuItem Text="CONTRACTOR " Name="contractor" ToolTip=""
                                NavigateUrl="~/setContractors.aspx?sel=setcontractor">
                            </dx:MenuItem>
                            <dx:MenuItem Text="GOAL SETTING" Name="setgoal" ToolTip="Goal Setting"
                                NavigateUrl="~/setGoal.aspx?sel=setgoal">
                                <ItemStyle VerticalAlign="Middle" />
                            </dx:MenuItem>
                            <dx:MenuItem Text="CATEGORY" Name="cate" ToolTip="Category / Sub Category Setting"
                                NavigateUrl="~/setCategory.aspx?sel=cate">
                            </dx:MenuItem>
                            <dx:MenuItem Text="SYSTEM" Name="sys" ToolTip="System Setting"
                                NavigateUrl="~/system/company_info.aspx?sel=sys" BeginGroup="True">
                            </dx:MenuItem>
                        </Items>
                        <LoadingPanelImage Url="~/App_Themes/Office2010Silver/Web/Loading.gif">
                        </LoadingPanelImage>
                        <ItemStyle DropDownButtonSpacing="10px" PopOutImageSpacing="10px"
                            CssClass="menunavleftfont" Height="38px" BackColor="#E8EEF4"
                            BackgroundImage-ImageUrl="Images/blank4x4.png">
                            <SelectedStyle BackColor="Gray" ForeColor="#FCFCFC">
                                <BackgroundImage ImageUrl="~/Images/blank4x4.gif" Repeat="NoRepeat"
                                    VerticalPosition="top" />
                                <BorderLeft BorderColor="#D33F3F" BorderStyle="Solid" BorderWidth="6px" />
                                <BorderRight BorderStyle="None" />
                                <BorderTop BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </SelectedStyle>
                            <HoverStyle BackColor="#D0D0D0" ForeColor="#255991">
                                <BackgroundImage ImageUrl="~/Images/blank4x4.gif" Repeat="NoRepeat"
                                    VerticalPosition="top" />
                                <BorderLeft BorderColor="#007ACC" BorderStyle="Solid" BorderWidth="6px" />
                                <BorderRight BorderStyle="None" />
                                <BorderTop BorderStyle="None" />
                                <BorderBottom BorderStyle="None" />
                            </HoverStyle>
                            <Paddings PaddingTop="10px" PaddingBottom="10px" PaddingLeft="14px" PaddingRight="4px" />
                            <BackgroundImage HorizontalPosition="right" Repeat="NoRepeat" />
                            <BorderLeft BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="6px" />
                            <BorderRight BorderStyle="None" />
                            <BorderTop BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </ItemStyle>
                        <LoadingPanelStyle ImageSpacing="5px">
                        </LoadingPanelStyle>
                        <ItemSubMenuOffset FirstItemX="1" LastItemX="1" X="1" FirstItemY="-1"
                            LastItemY="-1" Y="-1" />
                        <RootItemSubMenuOffset FirstItemX="-1" LastItemX="-1" X="-1" />
                        <SubMenuStyle GutterWidth="13px" GutterImageSpacing="9px" />
                        <BorderRight BorderStyle="None" />
                        <BorderLeft BorderStyle="None" />
                        <BorderTop BorderStyle="None" />
                        <BorderBottom BorderStyle="None" />
                    </dx:ASPxMenu>
                </div>
                <div id="content">
                    <div class="row LeelawadeeFont">
                        <div class="col-md-7" style="padding: 16px 0px 16px 0px;">
                            <div class="fieldset-box" style="border: 1px solid #CCCCCC;">
                                <div class="row" style="height: 32px; background-color: #f0f0f0;">
                                    <div class="col-md-6" style="font-size: small; font-weight: 600; margin-top: 4px; color: #666666;">Plant Dropdown</div>
                                    <div class="col-md-3" style="padding-top: 4px; padding-bottom: 4px;">
                                    </div>
                                    <div class="col-md-3" style="padding-top: 4px; padding-right: 20px; text-align: right;">
                                        <asp:CheckBox ID="chkShowDetail" runat="server" Text="&nbsp;&nbsp;Show detail" CssClass="chkBT2m" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row" style="height: 40px; background-color: #F0F0F0;">
                                    <div class="col-md-3">&nbsp;</div>
                                    <div class="col-md-8" style="padding-top: 4px; padding-bottom: 8px;">
                                        <telerik:RadComboBox ID="rcbPlant" runat="server" Skin="Metro" Width="180px" DataTextField="plantName" DataValueField="plantId" AutoPostBack="True" DataSourceID="srcPlant">
                                        </telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcPlant" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [plantId], [plantName], [plantDesc] FROM [tblPlant]"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-md-1">&nbsp;</div>
                                </div>
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        <div class="row" style="height: 40px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-8" style="padding-top: 16px; padding-bottom: 4px;">
                                                <asp:Button ID="btNew" runat="server" CssClass="btn btn-xs btn-primary" Text="New" CommandName="newplant" />&nbsp;
                                                <asp:Button ID="btEdit" runat="server" CssClass="btn btn-xs btn-primary" Text="Edit" CommandName="editplant" />&nbsp;
                                                <asp:Button ID="btDel" runat="server" CssClass="btn btn-xs btn-danger" Text="Delete" CommandName="delplant" />
                                                <div style="display: block; float: left; width: 100px; margin-top: -20px; margin-left: 168px; position: absolute">
                                                    <asp:CheckBox ID="chkConfirmDel" runat="server" Text="&nbsp;&nbsp;Confirm Delete" CssClass="chkBT2m" Visible="False" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 32px;">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Name
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbNewPlant" runat="server" CssClass="form-control input-sm" placeholder="New Plant"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 32px; padding: 0px 0px 0px 0px">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Description
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbNewPlantDesc" runat="server" CssClass="form-control input-sm" placeholder="Plant Description"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 0px 0px 0px 0px">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Email #1
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbNewPlantEmail1" runat="server" CssClass="form-control input-sm" placeholder="Plant Email"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="emailValidator1" runat="server" Display="Dynamic"
                                                    ErrorMessage="&nbsp;&nbsp;Please enter valid e-mail address" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                    ControlToValidate="tbNewPlantEmail1" Font-Size="11px" ForeColor="#CC0000"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 0px 0px 0px 0px">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Email #2
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbNewPlantEmail2" runat="server" CssClass="form-control input-sm" placeholder="Plant Email"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="emailValidator2" runat="server" Display="Dynamic"
                                                    ErrorMessage="&nbsp;&nbsp;Please enter valid e-mail address" ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                    ControlToValidate="tbNewPlantEmail2" Font-Size="11px" ForeColor="#CC0000"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 48px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 8px;">
                                                <asp:Button ID="btSave" runat="server" CssClass="btn btn-xs btn-primary" Text="Save" CommandName="saveplant" Visible="False" />
                                            </div>
                                        </div>
                                        <div class="row" style="height: 20px; padding-left: 12px;">
                                            <asp:Label ID="lbFailureText" runat="server" Text="" ForeColor="#CC0000" Font-Size="1em"></asp:Label>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <div class="row" style="height: 32px; padding-top: 8px;">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Name
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbShowPlantName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 32px;">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Description
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbShowPlantDesc" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 32px; padding: 0px 0px 0px 0px">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Email #1
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbShowPlantEmail1" runat="server" CssClass="form-control input-sm" ForeColor="#337AB7"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 32px; padding: 0px 0px 0px 0px">
                                            <div class="col-md-3" style="text-align: right; padding-top: 9px">
                                                Plant Email #2
                                            </div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 3px;">
                                                <asp:TextBox ID="tbShowPlantEmail2" runat="server" CssClass="form-control input-sm" ForeColor="#337AB7"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 48px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-8" style="padding-top: 3px; padding-bottom: 8px;">
                                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-xs btn-primary" Text="Save" CommandName="saveplant" Visible="False" />
                                            </div>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                        <div class="col-md-5" style="padding: 16px 20px 16px 0px;">
                            <telerik:RadGrid ID="rgPlantList" runat="server" Skin="Metro" PageSize="15" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GroupPanelPosition="Top" Width="100%"
                                DataSourceID="srcPlant" HeaderStyle-Height="60px">
                                <MasterTableView DataKeyNames="plantId" DataSourceID="srcPlant">
                                    <EditItemTemplate></EditItemTemplate>
                                    <NoRecordsTemplate>
                                        <div style="padding: 13px 0 30px 24px">&nbsp;</div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="plantId" DataType="System.Int32" HeaderText="plantId" UniqueName="plantId" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="plantName" FilterControlAltText="" HeaderText="Plant Name" SortExpression="plantName" UniqueName="plantname">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="140px" HorizontalAlign="Left" />
                                            <ItemStyle Width="140px" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="plantDesc" FilterControlAltText="" HeaderText="Description" SortExpression="plantDesc" UniqueName="plantDesc">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text="" />
                                            </ColumnValidationSettings>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
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
