<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="observer.aspx.vb" Inherits="DowChemical.observer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--version revise 3, last update 2017/09/20--%>

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

        function pnHideHROStart() {
            var pn1 = $("#<%=pnHRO1.ClientID %>");
            var pn2 = $("#<%=pnHRO2.ClientID %>");
            var pn3 = $("#<%=pnHRO3.ClientID %>");
            var pn4 = $("#<%=pnHRO4.ClientID %>");
            var pn5 = $("#<%=pnHRO5.ClientID %>");
            var pn6 = $("#<%=pnHRO6.ClientID %>");            
            pn1.hide(); pn2.hide(); pn3.hide(); pn4.hide(); pn5.hide(); pn6.hide();

            var pnEye1 = $("#<%=pnEye1.ClientID %>");
            var pnEye2 = $("#<%=pnEye2.ClientID %>");
            var pnEye3 = $("#<%=pnEye3.ClientID %>");
            var pnEye4 = $("#<%=pnEye4.ClientID %>");
            var pnEye5 = $("#<%=pnEye5.ClientID %>");
            var pnEye6 = $("#<%=pnEye6.ClientID %>");
            pnEye1.hide(); pnEye2.hide(); pnEye3.hide(); pnEye4.hide(); pnEye5.hide(); pnEye6.hide();

            var pnNon1 = $("#<%=pnNon1.ClientID %>");
            var pnNon2 = $("#<%=pnNon2.ClientID %>");
            var pnNon3 = $("#<%=pnNon3.ClientID %>");
            var pnNon4 = $("#<%=pnNon4.ClientID %>");
            var pnNon5 = $("#<%=pnNon5.ClientID %>");
            var pnNon6 = $("#<%=pnNon6.ClientID %>");
            pnNon1.hide(); pnNon2.hide(); pnNon3.hide(); pnNon4.hide(); pnNon5.hide(); pnNon6.hide();

            var pnContract1 = $("#<%=pnContract1.ClientID %>");
            var pnContract2 = $("#<%=pnContract2.ClientID %>");
            var pnContract3 = $("#<%=pnContract3.ClientID %>");
            var pnContract4 = $("#<%=pnContract4.ClientID %>");
            var pnContract5 = $("#<%=pnContract5.ClientID %>");
            var pnContract6 = $("#<%=pnContract6.ClientID %>");
            pnContract1.hide(); pnContract2.hide(); pnContract3.hide(); pnContract4.hide(); pnContract5.hide(); pnContract6.hide();
        }

        function showPanelHRO1(show) {
            var pn = $("#<%=pnHRO1.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO1.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelHRO2(show) {
            var pn = $("#<%=pnHRO2.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO2.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelHRO3(show) {
            var pn = $("#<%=pnHRO3.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO3.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelHRO4(show) {
            var pn = $("#<%=pnHRO4.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO4.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelHRO5(show) {
            var pn = $("#<%=pnHRO5.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO5.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelHRO6(show) {
            var pn = $("#<%=pnHRO6.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO6.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }

        function showPanelEye1(show) {
            var pn = $("#<%=pnEye1.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye1.ClientID%>");
            var pn1 = $("#<%=pnContract1.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract1.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction1.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi1.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }
        function showPanelEye2(show) {
            var pn = $("#<%=pnEye2.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye2.ClientID%>");
            var pn1 = $("#<%=pnContract2.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract2.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction2.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi2.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }
        function showPanelEye3(show) {
            var pn = $("#<%=pnEye3.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye3.ClientID%>");
            var pn1 = $("#<%=pnContract3.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract3.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction3.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi3.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }
        function showPanelEye4(show) {
            var pn = $("#<%=pnEye4.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye4.ClientID%>");
            var pn1 = $("#<%=pnContract4.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract4.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction4.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi4.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }
        function showPanelEye5(show) {
            var pn = $("#<%=pnEye5.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye5.ClientID%>");
            var pn1 = $("#<%=pnContract5.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract5.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction5.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi5.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }
        function showPanelEye6(show) {
            var pn = $("#<%=pnEye6.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye6.ClientID%>");
            var pn1 = $("#<%=pnContract6.ClientID %>");
            var hf1 = document.getElementById("<%=hfPnContract6.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction6.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi6.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                pn.hide();
                hf.value = "0";
                pn1.hide();
                hf1.value = "0";
                chkEyeInteraction.checked = false;
                chkEyeOfi.checked = false;
            }
        }

        function showPanelNon1(show) {
            var pn = $("#<%=pnNon1.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon1.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelNon2(show) {
            var pn = $("#<%=pnNon2.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon2.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelNon3(show) {
            var pn = $("#<%=pnNon3.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon3.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelNon4(show) {
            var pn = $("#<%=pnNon4.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon4.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelNon5(show) {
            var pn = $("#<%=pnNon5.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon5.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }
        function showPanelNon6(show) {
            var pn = $("#<%=pnNon6.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon6.ClientID%>");
            if (show) { pn.show(); hf.value = "1"; } else { pn.hide(); hf.value = "0"; }
        }

        function showPanelContract1(show) {
            var pn = $("#<%=pnContract1.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract1.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction1.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi1.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }
        function showPanelContract2(show) {
            var pn = $("#<%=pnContract2.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract2.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction2.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi2.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }
        function showPanelContract3(show) {
            var pn = $("#<%=pnContract3.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract3.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction3.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi3.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }
        function showPanelContract4(show) {
            var pn = $("#<%=pnContract4.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract4.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction4.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi4.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }
        function showPanelContract5(show) {
            var pn = $("#<%=pnContract5.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract5.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction5.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi5.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }
        function showPanelContract6(show) {
            var pn = $("#<%=pnContract6.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract6.ClientID%>");
            var chkEyeInteraction = document.getElementById("<%=chkEyeInteraction6.ClientID%>");
            var chkEyeOfi = document.getElementById("<%=chkEyeOfi6.ClientID%>");
            if (show) {
                pn.show();
                hf.value = "1";
            } else {
                if (chkEyeInteraction.checked == false && chkEyeOfi.checked == false) {
                    pn.hide();
                    hf.value = "0";
                }                
            }
        }

        function pnHideCaseHRO1() {
            var pn = $("#<%=pnHRO1.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO1.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseHRO2() {
            var pn = $("#<%=pnHRO2.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO2.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseHRO3() {
            var pn = $("#<%=pnHRO3.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO3.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseHRO4() {
            var pn = $("#<%=pnHRO4.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO4.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseHRO5() {
            var pn = $("#<%=pnHRO5.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO5.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseHRO6() {
            var pn = $("#<%=pnHRO6.ClientID %>");
            var hf = document.getElementById("<%=hfPnHRO6.ClientID%>");
            pn.hide(); hf.value = "0"
        }

        function pnHideCaseEye1() {
            var pn = $("#<%=pnEye1.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye1.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseEye2() {
            var pn = $("#<%=pnEye2.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye2.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseEye3() {
            var pn = $("#<%=pnEye3.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye3.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseEye4() {
            var pn = $("#<%=pnEye4.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye4.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseEye5() {
            var pn = $("#<%=pnEye5.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye5.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseEye6() {
            var pn = $("#<%=pnEye6.ClientID %>");
            var hf = document.getElementById("<%=hfPnEye6.ClientID%>");
            pn.hide(); hf.value = "0"
        }

        function pnHideCaseNon1() {
            var pn = $("#<%=pnNon1.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon1.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseNon2() {
            var pn = $("#<%=pnNon2.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon2.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseNon3() {
            var pn = $("#<%=pnNon3.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon3.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseNon4() {
            var pn = $("#<%=pnNon4.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon4.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseNon5() {
            var pn = $("#<%=pnNon5.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon5.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseNon6() {
            var pn = $("#<%=pnNon6.ClientID %>");
            var hf = document.getElementById("<%=hfPnNon6.ClientID%>");
            pn.hide(); hf.value = "0"
        }

        function pnHideCaseContract1() {
            var pn = $("#<%=pnContract1.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract1.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseContract2() {
            var pn = $("#<%=pnContract2.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract2.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseContract3() {
            var pn = $("#<%=pnContract3.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract3.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseContract4() {
            var pn = $("#<%=pnContract4.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract4.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseContract5() {
            var pn = $("#<%=pnContract5.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract5.ClientID%>");
            pn.hide(); hf.value = "0"
        }
        function pnHideCaseContract6() {
            var pn = $("#<%=pnContract6.ClientID %>");
            var hf = document.getElementById("<%=hfPnContract6.ClientID%>");
            pn.hide(); hf.value = "0"
        }

        function recogChk1(show) {
            var bt1a = $("#<%=imbtOtherAction1a.ClientID %>");
            var pn1b = $("#<%=pnRespon1b.ClientID %>");
            var pn1c = $("#<%=pnRespon1c.ClientID %>");
            if (show) { bt1a.hide(); } else { bt1a.show(); }
            pn1b.hide(); pn1c.hide();
        }
        function recogChk2(show) {
            var bt2a = $("#<%=imbtOtherAction2a.ClientID %>");
            var pn2b = $("#<%=pnRespon2b.ClientID %>");
            var pn2c = $("#<%=pnRespon2c.ClientID %>");
            if (show) { bt2a.hide(); } else { bt2a.show(); }
            pn2b.hide(); pn2c.hide();
        }
        function recogChk3(show) {
            var bt3a = $("#<%=imbtOtherAction3a.ClientID %>");
            var pn3b = $("#<%=pnRespon3b.ClientID %>");
            var pn3c = $("#<%=pnRespon3c.ClientID %>");
            if (show) { bt3a.hide(); } else { bt3a.show(); }
            pn3b.hide(); pn3c.hide();
        }
        function recogChk4(show) {
            var bt4a = $("#<%=imbtOtherAction4a.ClientID %>");
            var pn4b = $("#<%=pnRespon4b.ClientID %>");
            var pn4c = $("#<%=pnRespon4c.ClientID %>");
            if (show) { bt4a.hide(); } else { bt4a.show(); }
            pn4b.hide(); pn4c.hide();
        }
        function recogChk5(show) {
            var bt5a = $("#<%=imbtOtherAction5a.ClientID %>");
            var pn5b = $("#<%=pnRespon5b.ClientID %>");
            var pn5c = $("#<%=pnRespon5c.ClientID %>");
            if (show) { bt5a.hide(); } else { bt5a.show(); }
            pn5b.hide(); pn5c.hide();
        }
        function recogChk6(show) {
            var bt6a = $("#<%=imbtOtherAction6a.ClientID %>");
            var pn6b = $("#<%=pnRespon6b.ClientID %>");
            var pn6c = $("#<%=pnRespon6c.ClientID %>");
            if (show) { bt6a.hide(); } else { bt6a.show(); }
            pn6b.hide(); pn6c.hide();
        }

        function disableButton(cmdId, txt) {
            var cmd = document.getElementById(cmdId);
            cmd.value = txt;
            cmd.disabled = true;
        }
        function lockControl() {
            <%--document.getElementById('<%=tbAction1a.ClientID %>').disabled = true;
            document.getElementById('<%=imbtOtherAction1a.ClientID %>').disabled = true;
            document.getElementById('<%=imbtFindRespon1a.ClientID %>').disabled = true;
            document.getElementById('<%=racRespon1a.ClientID %>').disabled = true;--%>
        }
    </script>

    <style type="text/css">
        .listbox-info {
            border-style: none;
        }

        .RadGrid_Metro .rgDataDiv {
            overflow-x: hidden !important;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div>
        <div id="f-header" style="color: #fff; font-size: 1.6em; padding: 4px 0 0 16px;">
            <div style="display: block; float: left; margin-top: 4px; margin-left: 4px;">New Observation</div>
            <div style="float: right; margin-top: 8px; margin-right: 308px; color: #000">&nbsp;</div>
        </div>
        <div id="f-leftsidebar">
            <div class="row">
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
                        <telerik:RadPanelItem runat="server" Text="CREATE NEW OBSERVATION" Height="36px" NavigateUrl="~/observer/observer.aspx" Selected="true">
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
            <asp:HiddenField ID="hfOwnerEmpId" runat="server" Value="0" />
            <div class="row" style="padding: 8px 7px 4px 16px; margin-right: -15px">
                <div class="col-md-9" style="padding: 12px 0 0 0;">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                        Observed Department
                    </div>
                    <div class="col-md-6">
                        <telerik:RadComboBox ID="rcbDepartment" runat="server" Skin="Metro" Width="172px" DataSourceID="srcDepartment" DataTextField="departName" DataValueField="departId" AutoPostBack="True">
                        </telerik:RadComboBox>
                        <asp:SqlDataSource ID="srcDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="pull-right" style="display: block;">
                        <asp:Button ID="btNewObserv" runat="server" Height="30px" Width="136px" class="btn btn-primary btn-mo30" Text="New Observation" PostBackUrl="~/observer/observer.aspx" />
                    </div>
                </div>
            </div>
            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%">
                <div class="row" style="padding: 4px 16px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                        Action Number
                    </div>
                    <div class="col-md-9">
                        <asp:TextBox ID="lbActionNum" runat="server" CssClass="form-control input-sm" Width="172px" Enabled="False"></asp:TextBox>
                        <asp:HiddenField ID="hfRecId" runat="server" />
                    </div>
                </div>
                <div class="row" style="padding: 2px 16px 4px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 9px;">
                        Date
                    </div>
                    <div class="col-md-9">
                        <div style="display: block; float: left; width: 180px;">
                            <telerik:RadDatePicker ID="rdpDocDate" runat="server" Skin="Bootstrap" Width="174px" Culture="en-US" AutoPostBack="True">
                                <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" Skin="Bootstrap"></Calendar>
                                <DateInput runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" LabelWidth="40%" CssClass="rcDateInputAdj" AutoPostBack="True">
                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                    <FocusedStyle Resize="None"></FocusedStyle>
                                    <DisabledStyle Resize="None"></DisabledStyle>
                                    <InvalidStyle Resize="None"></InvalidStyle>
                                    <HoveredStyle Resize="None"></HoveredStyle>
                                    <EnabledStyle Resize="None"></EnabledStyle>
                                </DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl="" CssClass="rcCalPopup rcCalPopupAdj"></DatePopupButton>
                            </telerik:RadDatePicker>
                        </div>
                        <div style="display: block; float: left; width: 180px;">
                            <div style="display: block; float: left; width: 36px; text-align: right; margin-top: 9px; margin-right: 15px;">
                                Time
                            </div>
                            <div style="display: block; float: left; width: 124px; padding-top: 2px;">
                                <div style="display: block; float: left; width: 64px;">
                                    <telerik:RadComboBox ID="rcbTimeHH" runat="server" Skin="Metro" Width="56px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="" Value="x" />
                                            <telerik:RadComboBoxItem runat="server" Text="07" Value="7" />
                                            <telerik:RadComboBoxItem runat="server" Text="08" Value="8" />
                                            <telerik:RadComboBoxItem runat="server" Text="09" Value="9" />
                                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                                            <telerik:RadComboBoxItem runat="server" Text="11" Value="11" />
                                            <telerik:RadComboBoxItem runat="server" Text="12" Value="12" />
                                            <telerik:RadComboBoxItem runat="server" Text="13" Value="13" />
                                            <telerik:RadComboBoxItem runat="server" Text="14" Value="14" />
                                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                                            <telerik:RadComboBoxItem runat="server" Text="16" Value="16" />
                                            <telerik:RadComboBoxItem runat="server" Text="17" Value="17" />
                                            <telerik:RadComboBoxItem runat="server" Text="18" Value="18" />
                                            <telerik:RadComboBoxItem runat="server" Text="19" Value="19" />
                                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                                            <telerik:RadComboBoxItem runat="server" Text="21" Value="21" />
                                            <telerik:RadComboBoxItem runat="server" Text="22" Value="22" />
                                            <telerik:RadComboBoxItem runat="server" Text="23" Value="23" />
                                            <telerik:RadComboBoxItem runat="server" Text="00" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="01" Value="1" />
                                            <telerik:RadComboBoxItem runat="server" Text="02" Value="2" />
                                            <telerik:RadComboBoxItem runat="server" Text="03" Value="3" />
                                            <telerik:RadComboBoxItem runat="server" Text="04" Value="4" />
                                            <telerik:RadComboBoxItem runat="server" Text="05" Value="5" />
                                            <telerik:RadComboBoxItem runat="server" Text="06" Value="6" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 56px;">
                                    <telerik:RadComboBox ID="rcbTimeMM" runat="server" Skin="Metro" Width="56px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="" Value="x" />
                                            <telerik:RadComboBoxItem runat="server" Text="00" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="05" Value="5" />
                                            <telerik:RadComboBoxItem runat="server" Text="10" Value="10" />
                                            <telerik:RadComboBoxItem runat="server" Text="15" Value="15" />
                                            <telerik:RadComboBoxItem runat="server" Text="20" Value="20" />
                                            <telerik:RadComboBoxItem runat="server" Text="25" Value="25" />
                                            <telerik:RadComboBoxItem runat="server" Text="30" Value="30" />
                                            <telerik:RadComboBoxItem runat="server" Text="35" Value="35" />
                                            <telerik:RadComboBoxItem runat="server" Text="40" Value="40" />
                                            <telerik:RadComboBoxItem runat="server" Text="45" Value="45" />
                                            <telerik:RadComboBoxItem runat="server" Text="50" Value="50" />
                                            <telerik:RadComboBoxItem runat="server" Text="55" Value="55" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div style="display: block; float: left; width: 348px;">
                            <div style="display: block; float: left; width: 166px; text-align: right; margin-top: 9px; margin-right: 14px;">
                                Duration
                            </div>
                            <div style="display: block; float: left; width: 124px; padding-top: 2px;">
                                <div style="display: block; float: left; width: 64px;">
                                    <telerik:RadComboBox ID="rcbDurationH" runat="server" Skin="Metro" Width="56px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="0h" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="1h" Value="1" />
                                            <telerik:RadComboBoxItem runat="server" Text="2h" Value="2" />
                                            <telerik:RadComboBoxItem runat="server" Text="3h" Value="3" />
                                            <telerik:RadComboBoxItem runat="server" Text="4h" Value="4" />
                                            <telerik:RadComboBoxItem runat="server" Text="5h" Value="5" />
                                            <telerik:RadComboBoxItem runat="server" Text="6h" Value="6" />
                                            <telerik:RadComboBoxItem runat="server" Text="7h" Value="7" />
                                            <telerik:RadComboBoxItem runat="server" Text="8h" Value="8" />
                                            <telerik:RadComboBoxItem runat="server" Text="9h" Value="9" />
                                            <telerik:RadComboBoxItem runat="server" Text="10h" Value="10" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 56px;">
                                    <telerik:RadComboBox ID="rcbDurationM" runat="server" Skin="Metro" Width="56px">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="0m" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="5m" Value="5" />
                                            <telerik:RadComboBoxItem runat="server" Text="10m" Value="10" />
                                            <telerik:RadComboBoxItem runat="server" Text="15m" Value="15" />
                                            <telerik:RadComboBoxItem runat="server" Text="20m" Value="20" />
                                            <telerik:RadComboBoxItem runat="server" Text="30m" Value="30" />
                                            <telerik:RadComboBoxItem runat="server" Text="40m" Value="40" />
                                            <telerik:RadComboBoxItem runat="server" Text="50m" Value="50" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div style="display: block; float: left; width: 12px;">
                            <asp:ImageButton ID="imgAddEntry" runat="server" ImageUrl="~/Images/blank2h2.png"></asp:ImageButton>
                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
            <div class="row" style="padding: 4px 16px 4px 16px">
                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                    Observer #1
                </div>
                <div class="col-md-9">
                    <div style="display: block; float: left; width: 360px;">
                        <asp:TextBox ID="tbMyFullname" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                    </div>
                    <div style="display: block; float: left; width: 180px;">
                        <asp:TextBox ID="tbMyDowId" runat="server" CssClass="form-control input-sm" Width="172px"></asp:TextBox>
                    </div>
                    <div style="display: block; float: left; width: 172px;">
                        <asp:TextBox ID="tbMyDepart" runat="server" CssClass="form-control input-sm" Width="172px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                <div class="row" style="padding: 4px 16px 0px 16px">
                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                        Other Observers
                    </div>
                    <div class="col-md-9">
                        <div style="display: block; float: left; width: 712px;">
                            <telerik:RadAutoCompleteBox ID="racObservBox" runat="server" Width="712px" RenderMode="Lightweight" Skin="Bootstrap"
                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Select Employee, Please type here"
                                MaxResultCount="5" OnClientEntryAdding="OnClientEntryAddingOtherObservers" AllowCustomEntry="True">
                                <TokensSettings AllowTokenEditing="true" />
                            </telerik:RadAutoCompleteBox>
                            <asp:SqlDataSource ID="srcAutoName" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT empId, empDowId, empName, empSurname, empFullName, empEmail, empContact, empMobile, empDisplay, departId, plantId, joblvCode FROM tblEmployee WHERE (empEnable = 'true') AND (IsVisible = 'true')"></asp:SqlDataSource>
                        </div>
                        <div style="display: block; float: left; width: 20px; margin-top: 4px; margin-left: 8px;">
                            <asp:ImageButton ID="imbtFindObserv" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;"></asp:ImageButton>
                            <asp:HiddenField ID="hfEmpIdSelect" runat="server" Value="0" />
                            <asp:HiddenField ID="hfFullNameSelect" runat="server" Value="" />
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnOtherObserver" runat="server" Visible="false">
                    <div style="padding: 0px 16px 0px 191px; margin-top: -3px;">
                        <telerik:RadGrid ID="rgObserverList" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" DataSourceID="srcObserverList" ShowHeader="False" GroupPanelPosition="Top">
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <MasterTableView DataSourceID="srcObserverList">
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
                            <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                            <ItemStyle Height="30px" BackColor="#D5DCE3" />
                        </telerik:RadGrid>
                        <asp:SqlDataSource ID="srcObserverList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblRecordOthEmp.Id, tblRecordOthEmp.recId, tblRecordOthEmp.recItem, tblRecordOthEmp.empIdOth, tblEmployee.empDowId, tblEmployee.empName, tblEmployee.empSurname, tblEmployee.empFullName, tblEmployee.empEmail, tblEmployee.departId, tblDepartment.departName FROM tblRecordOthEmp INNER JOIN tblEmployee ON tblRecordOthEmp.empIdOth = tblEmployee.empId INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId WHERE (tblRecordOthEmp.recId = @recId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </asp:Panel>
            </telerik:RadAjaxPanel>
            <div class="row" style="padding: 8px 16px 4px 16px">
                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">
                    No. of Observe
                </div>
                <div class="col-md-9">
                    <div style="display: block; float: left; width: 200px;">
                        <telerik:RadComboBox ID="rcbNoObserve" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="1" Value="1" />
                                <telerik:RadComboBoxItem runat="server" Text="2" Value="2" />
                                <telerik:RadComboBoxItem runat="server" Text="3" Value="3" />
                                <telerik:RadComboBoxItem runat="server" Text="4" Value="4" />
                                <telerik:RadComboBoxItem runat="server" Text="5" Value="5" />
                                <telerik:RadComboBoxItem runat="server" Text="6" Value="6" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div style="display: block; float: left; width: 360px;">
                        <asp:Label ID="tokenInfo" runat="server" Text="" ForeColor="Red" Font-Size="X-Small"></asp:Label>
                    </div>
                    <div style="display: block; float: left; width: 20px;">
                    </div>
                </div>
            </div>
            <div style="padding: 16px 0px 2px 0px">
                <div style="display: block; float: left; width: 191px; border-bottom-style: solid; border-bottom-width: 3px; border-bottom-color: #25a0da; position: absolute; height: 41px;"></div>
                <div style="padding: 0px 0px 0px 191px">
                    <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                        <telerik:RadTabStrip ID="rtabStrip1" runat="server" SelectedIndex="0" Skin="MetroTouch" Height="38px" TabIndex="1" MultiPageID="RadMultiPage1">
                            <Tabs>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 1" Selected="True" TabIndex="0">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 2" Visible="False" TabIndex="1">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 3" Visible="False" TabIndex="2">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 4" Visible="False" TabIndex="3">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 5" Visible="False" TabIndex="4">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Height="38px" Text="Observe 6" Visible="False" TabIndex="5">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                    </telerik:RadAjaxPanel>
                </div>
                <asp:SqlDataSource ID="srcCategory0" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT cateId, cateName FROM tblObsvCate"></asp:SqlDataSource>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0" TabIndex="1">
                    <telerik:RadPageView ID="RadPageView1" runat="server" TabIndex="0" Selected="True">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle1" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory1" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub1" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub1" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory1" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint1" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint1" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub1" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment1" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation1" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye1(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon1(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO1(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye1" runat="server" Value="0" />
                        <asp:Panel ID="pnEye1" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk1(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract1(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract1(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon1" runat="server" Value="0" />
                        <asp:Panel ID="pnNon1" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk1(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO1" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO1" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO1op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO1op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO1op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO1op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO1op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract1" runat="server" Value="0" />
                        <asp:Panel ID="pnContract1" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType1" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel11" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor1" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel12" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload1" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo1" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg1" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload1" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage1" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList1" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture1">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose1" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose1_Click" />
                                                            </div>
                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip1" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image1">
                                                                <asp:Image ID="ImageView1" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription1" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel13" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction1a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction1a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon1a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted1a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction1a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon1a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon1b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction1b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon1b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted1b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction1b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction1b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon1b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon1c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction1c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon1c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted1c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction1c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon1c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server" >
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle2" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory2" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub2" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub2" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory2" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint2" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint2" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub2" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment2" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation2" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye2(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon2(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO2(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye2" runat="server" Value="0" />
                        <asp:Panel ID="pnEye2" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk2(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract2(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract2(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon2" runat="server" Value="0" />
                        <asp:Panel ID="pnNon2" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk2(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO2" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO2" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract2" runat="server" Value="0" />
                        <asp:Panel ID="pnContract2" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType2" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel21" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor2" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel22" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload2" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo2" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg2" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload2" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage2" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList2" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture2">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose2" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose2_Click" />
                                                            </div>
                                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip2" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image2">
                                                                <asp:Image ID="ImageView2" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription2" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel23" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction2a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction2a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon2a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted2a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction2a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon2a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon2b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction2b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon2b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted2b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction2b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction2b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon2b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon2c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction2c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon2c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted2c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction2c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon2c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server" >
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle3" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory3" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub3" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub3" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory3" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint3" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint3" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub3" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment3" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation3" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye3(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon3(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO3(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye3" runat="server" Value="0" />
                        <asp:Panel ID="pnEye3" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk3(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract3(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract3(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon3" runat="server" Value="0" />
                        <asp:Panel ID="pnNon3" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk3(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO3" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO3" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract3" runat="server" Value="0" />
                        <asp:Panel ID="pnContract3" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType3" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel31" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor3" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel32" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload3" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo3" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg3" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload3" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage3" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList3" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture3">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose3" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose3_Click" />
                                                            </div>
                                                            <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip3" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image3">
                                                                <asp:Image ID="ImageView3" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription3" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel33" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction3a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction3a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon3a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted3a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction3a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon3a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon3b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction3b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon3b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted3b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction3b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction3b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon3b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon3c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction3c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon3c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted3c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction3c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon3c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server" >
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle4" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory4" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub4" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub4" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory4" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint4" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint4" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub4" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment4" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation4" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye4(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon4(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO4(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye4" runat="server" Value="0" />
                        <asp:Panel ID="pnEye4" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk4(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract4(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract4(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon4" runat="server" Value="0" />
                        <asp:Panel ID="pnNon4" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk4(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO4" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO4" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract4" runat="server" Value="0" />
                        <asp:Panel ID="pnContract4" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType4" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel41" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor4" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel42" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload4" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo4" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg4" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload4" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage4" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList4" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture4">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose4" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose4_Click" />
                                                            </div>
                                                            <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip4" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image4">
                                                                <asp:Image ID="ImageView4" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription4" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel43" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction4a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction4a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon4a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted4a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction4a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon4a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon4b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction4b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon4b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted4b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction4b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction4b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon4b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon4c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction4c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon4c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted4c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction4c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon4c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView5" runat="server" >
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle5" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory5" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub5" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub5" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory5" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint5" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint5" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub5" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment5" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation5" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye5(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon5(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO5(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye5" runat="server" Value="0" />
                        <asp:Panel ID="pnEye5" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk5(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract5(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract5(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon5" runat="server" Value="0" />
                        <asp:Panel ID="pnNon5" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk5(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO5" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO5" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract5" runat="server" Value="0" />
                        <asp:Panel ID="pnContract5" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType5" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel51" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor5" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel52" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload5" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo5" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg5" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload5" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage5" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList5" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture5">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose5" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose5_Click" />
                                                            </div>
                                                            <asp:Image ID="Image5" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip5" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image5">
                                                                <asp:Image ID="ImageView5" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription5" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel53" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction5a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction5a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon5a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted5a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction5a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon5a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon5b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction5b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon5b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted5b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction5b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction5b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon5b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon5c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction5c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon5c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted5c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction5c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon5c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView6" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle6" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory6" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub6" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub6" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory6" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--new design--%>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint6" runat="server" Skin="Metro" Width="172px" DataSourceID="srcFailurePoint6" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub6" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Equipment : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:TextBox ID="tbEquipment6" runat="server" CssClass="form-control input-sm" Width="352px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Location : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="RCBLocation6" runat="server" Skin="Metro" Width="172px" >
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="AIE" Value="AIE" />
                                            <telerik:RadComboBoxItem runat="server" Text="MTP" Value="MTP" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT1" Value="COT1" />
                                            <telerik:RadComboBoxItem runat="server" Text="COT2" Value="COT2" />
                                            <telerik:RadComboBoxItem runat="server" Text="ATC" Value="ATC" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;"></div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <asp:CheckBox ID="chkSendEmail6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>                                    
                                </div>
                            </div>                            
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Behavior Observation [2<small>nd</small> Eye]" OnClick="showPanelEye6(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkNonBehavior6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Field Inspection [Non-Behavior]" OnClick="showPanelNon6(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 230px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO6(this.checked);" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hfPnEye6" runat="server" Value="0" />
                        <asp:Panel ID="pnEye6" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkRecognition6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk6(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeOfi6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" OnClick="showPanelContract6(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkEyeInteraction6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Interaction" OnClick="showPanelContract6(this.checked);" />
                                    </div>                                    
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnNon6" runat="server" Value="0" />
                        <asp:Panel ID="pnNon6" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkNonRecognition6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk6(this.checked);" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkNonOfi6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Opportunity For Improvement [OFI]" />
                                    </div>                                 
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>

                        <asp:HiddenField ID="hfPnHRO6" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO6" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        
                        <%--Interaction Panel--%>
                        <asp:HiddenField ID="hfPnContract6" runat="server" Value="0" />
                        <asp:Panel ID="pnContract6" runat="server">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Group : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType6" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel61" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor6" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Attachment File : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel62" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload6" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp,xlsx,xls,pdf"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo6" Text="File Size limit 1000KB, (jpg, bmp, png, gif, xlsm, xls, pdf)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg6" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload File" CommandName="uploadimg" Width="100px" /><span id="asyncUpload6" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage6" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList6" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture6">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose6" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose6_Click" />
                                                            </div>
                                                            <asp:Image ID="Image6" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip6" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image6">
                                                                <asp:Image ID="ImageView6" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '0')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        </asp:Panel>
                        <%--Interaction Panel--%>

                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription6" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel63" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1" PostBackControls="tbAction6a">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction6a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon6a" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                            <asp:CheckBox ID="cbActionCompleted6a" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction6a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon6a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,1);return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon6b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction6b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon6b" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted6b" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction6b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction6b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon6b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1, 2);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon6c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction6c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon6c" runat="server" Width="352px" RenderMode="Lightweight" Skin="Bootstrap"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                                <asp:CheckBox ID="cbActionCompleted6c" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Action Completed" />
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction6c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon6c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWndPara(1,3);return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>

                    <%--<telerik:RadPageView ID="RadPageView2" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle2" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory2" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub2" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub2" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM dbo.tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory2" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint2" runat="server" Skin="Metro" Width="712px" DataSourceID="srcFailurePoint2" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub2" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbEquipment2" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO2(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk2(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 172px; padding: 8px 0px 4px 16px; background-color: #f6f6f6">
                                        <asp:CheckBox ID="chkSendEmail2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfPnHRO2" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO2" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO2op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType2" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel21" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor2" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel22" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload2" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo2" Text="File Size limit 1000KB, (jpg, bmp, png, gif)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg2" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload Image" CommandName="uploadimg" Width="100px" /><span id="asyncUpload2" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage2" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList2" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture2">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose2" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose2_Click" />
                                                            </div>
                                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" /><telerik:RadToolTip ID="RadToolTip2" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image2">
                                                                <asp:Image ID="ImageView2" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '1')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription2" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel23" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction2a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon2a" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction2a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon2a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon2b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction2b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon2b" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction2b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction2b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon2b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon2c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction2c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon2c" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction2c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon2c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle3" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory3" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub3" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub3" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory3" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint3" runat="server" Skin="Metro" Width="712px" DataSourceID="srcFailurePoint3" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub3" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbEquipment3" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO3(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk3(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 172px; padding: 8px 0px 4px 16px; background-color: #f6f6f6">
                                        <asp:CheckBox ID="chkSendEmail3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfPnHRO3" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO3" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO3op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType3" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel31" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor3" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel32" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload3" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo3" Text="File Size limit 1000KB, (jpg, bmp, png, gif)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg3" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload Image" CommandName="uploadimg" Width="100px" /><span id="asyncUpload3" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage3" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList3" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture3">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose3" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose3_Click" />
                                                            </div>
                                                            <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" /><telerik:RadToolTip ID="RadToolTip3" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image3">
                                                                <asp:Image ID="ImageView3" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '2')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription3" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel33" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction3a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon3a" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction3a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon3a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon3b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction3b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon3b" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction3b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction3b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon3b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon3c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction3c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon3c" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction3c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon3c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle4" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory4" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub4" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub4" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory4" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint4" runat="server" Skin="Metro" Width="712px" DataSourceID="srcFailurePoint4" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub4" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbEquipment4" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO4(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk4(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 172px; padding: 8px 0px 4px 16px; background-color: #f6f6f6">
                                        <asp:CheckBox ID="chkSendEmail4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfPnHRO4" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO4" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO4op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType4" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel41" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor4" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel42" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload4" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo4" Text="File Size limit 1000KB, (jpg, bmp, png, gif)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg4" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload Image" CommandName="uploadimg" Width="100px" /><span id="asyncUpload4" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage4" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList4" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture4">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose4" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose4_Click" />
                                                            </div>
                                                            <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" /><telerik:RadToolTip ID="RadToolTip4" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image4">
                                                                <asp:Image ID="ImageView4" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '3')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription4" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel43" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction4a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon4a" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction4a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon4a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon4b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction4b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon4b" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction4b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction4b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon4b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon4c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction4c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon4c" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction4c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon4c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView5" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle5" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory5" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub5" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub5" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory5" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint5" runat="server" Skin="Metro" Width="712px" DataSourceID="srcFailurePoint5" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub5" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbEquipment5" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO5(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk5(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 172px; padding: 8px 0px 4px 16px; background-color: #f6f6f6">
                                        <asp:CheckBox ID="chkSendEmail5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfPnHRO5" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO5" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO5op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType5" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel51" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor5" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel52" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload5" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo5" Text="File Size limit 1000KB, (jpg, bmp, png, gif)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg5" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload Image" CommandName="uploadimg" Width="100px" /><span id="asyncUpload5" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage5" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList5" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture5">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose5" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose5_Click" />
                                                            </div>
                                                            <asp:Image ID="Image5" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" />
                                                            <telerik:RadToolTip ID="RadToolTip5" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image5">
                                                                <asp:Image ID="ImageView5" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture5" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '4')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription5" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel53" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction5a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon5a" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction5a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon5a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon5b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction5b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon5b" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction5b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction5b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon5b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon5c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction5c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon5c" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction5c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon5c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </telerik:RadAjaxPanel>
                        <div class="row" style="padding: 12px 16px 20px 32px"></div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView6" runat="server">
                        <div class="row" style="padding: 16px 16px 4px 16px;">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Title : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbTitle6" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Category : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 172px;">
                                    <telerik:RadComboBox ID="rcbCategory6" runat="server" Skin="Metro" Width="172px" DataSourceID="srcCategory0" DataTextField="cateName" DataValueField="cateId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 540px;">
                                    <div style="display: block; float: left; width: 174px; text-align: right; margin-top: 7px; margin-right: 14px;">Sub Category : </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <telerik:RadComboBox ID="rcbCategorySub6" runat="server" Skin="Metro" Width="352px" DataSourceID="srcCategorySub6" DataTextField="catesubName" DataValueField="catesubId" AutoPostBack="True" EnableItemCaching="True"></telerik:RadComboBox>
                                        <asp:SqlDataSource ID="srcCategorySub6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT catesubId, catesubName FROM tblObsvCateSub WHERE (cateId = @CateId) OR (catesubId = 1000)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="rcbCategory6" Name="CateId" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Failure Point : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 712px;">
                                    <telerik:RadComboBox ID="rcbFailurePoint6" runat="server" Skin="Metro" Width="712px" DataSourceID="srcFailurePoint6" DataTextField="failpointName" DataValueField="failpointId" EnableItemCaching="True"></telerik:RadComboBox>
                                    <asp:SqlDataSource ID="srcFailurePoint6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT failpointId, failpointName FROM tblObsvFailPoint WHERE (catesubId = @catesubId) OR (failpointId = 1000)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="rcbCategorySub6" Name="catesubId" PropertyName="SelectedValue" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Equipment/Location : </div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbEquipment6" runat="server" CssClass="form-control input-sm" Width="712px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">&nbsp;&nbsp;</div>
                            <div class="col-md-9">
                                <div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkHRO6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;HRO" OnClick="showPanelHRO6(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chk2Eye6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;2<small>nd</small> Eye /Interaction" />
                                    </div>
                                    <div style="display: block; float: left; width: 180px; padding: 8px 0px 4px 16px;">
                                        <asp:CheckBox ID="chkRecognition6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Recognition" AutoPostBack="True" OnClick="recogChk6(this.checked);" />
                                    </div>
                                    <div style="display: block; float: left; width: 172px; padding: 8px 0px 4px 16px; background-color: #f6f6f6">
                                        <asp:CheckBox ID="chkSendEmail6" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Send email this observe" Font-Bold="True" Checked="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfPnHRO6" runat="server" Value="0" />
                        <asp:Panel ID="pnHRO6" runat="server">
                            <div class="row" style="padding: 0px 0px 8px 11px;">
                                <div style="display: block; float: left; width: 180px; text-align: right; margin-top: 7px;">&#160;&#160;</div>
                                <div class="col-md-9" style="padding: 8px 16px 8px 48px; width: 712px; background-color: #f9f9f9">
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op1" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op2" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op3" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op4" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา)" />
                                    </div>
                                    <div>
                                        <asp:CheckBox ID="chkHRO6op5" runat="server" CssClass="chkBT2m" Text="&nbsp;&nbsp;Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว)" />
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 2px 16px 2px 16px"></div>
                        </asp:Panel>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Observed Type : </div>
                            <div class="col-md-9">
                                <div style="display: block; float: left; width: 180px;">
                                    <telerik:RadComboBox ID="rcbObserveType6" runat="server" Skin="Metro" Width="172px" AutoPostBack="True">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="Employee" Value="0" />
                                            <telerik:RadComboBoxItem runat="server" Text="Contractor" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <div style="display: block; float: left; width: 532px;">
                                    <telerik:RadAjaxPanel ID="RadAjaxPanel61" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                        <telerik:RadComboBox ID="rcbContractor6" runat="server" Skin="Metro" Width="532px" Visible="False" DataSourceID="srcContractor" DataTextField="contractorName" DataValueField="contractorId"></telerik:RadComboBox>
                                    </telerik:RadAjaxPanel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 7px;">Picture : </div>
                            <div class="col-md-9">
                                <telerik:RadAjaxPanel ID="RadAjaxPanel62" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                                    <div class="row">
                                        <div style="display: block; float: left; width: 360px;">
                                            <telerik:RadAsyncUpload ID="RadUpload6" runat="server" Height="32px" MaxFileSize="20524288" AllowedFileExtensions="jpg,png,gif,bmp"
                                                MultipleFileSelection="Automatic" Skin="Bootstrap" TemporaryFolder="~/ImagesUpload" MaxFileInputsCount="1" UploadedFilesRendering="BelowFileInput">
                                            </telerik:RadAsyncUpload>
                                            <asp:Label ID="lbUploadInfo6" Text="File Size limit 1000KB, (jpg, bmp, png, gif)" runat="server" Font-Size="X-Small" />
                                        </div>
                                        <div style="display: block; float: left; width: 200px;">
                                            <asp:Button ID="btUploadImg6" runat="server" CssClass="btn btn-sm btn-primary" Text="Upload Image" CommandName="uploadimg" Width="100px" /><span id="asyncUpload6" style="color: Red; line-height: 15px; font-size: x-small;"></span>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnShowImage6" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:DataList ID="PictureList6" runat="server" RepeatDirection="Horizontal" DataSourceID="srcPicture6">
                                                    <ItemTemplate>
                                                        <div style="display: block; float: left; width: 178px; margin-top: 4px;">
                                                            <div style="position: absolute; margin-left: 160px;">
                                                                <asp:ImageButton ID="imbtClose6" runat="server" ImageUrl="~/Images/bt_close-dark.png" ToolTip="Cancel Upload" OnClick="imbtClose6_Click" />
                                                            </div>
                                                            <asp:Image ID="Image6" runat="server" ImageUrl='<%# Eval("picUrl") %>' Width="176px" /><telerik:RadToolTip ID="RadToolTip6" runat="server" ManualClose="True" Modal="True" Position="Center" RelativeTo="Element" ShowEvent="OnClick" TargetControlID="Image6">
                                                                <asp:Image ID="ImageView6" runat="server" ImageUrl='<%# Eval("picUrl") %>' Height="480px" />
                                                            </telerik:RadToolTip>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:SqlDataSource ID="srcPicture6" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = '5')">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                        <div class="row" style="padding: 8px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Description :</div>
                            <div class="col-md-9">
                                <asp:TextBox ID="tbDescription6" runat="server" CssClass="form-control input-sm" Height="68px" Width="712px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel63" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                            <div class="row" style="padding: 4px 16px 4px 16px">
                                <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #1</div>
                                <div class="col-md-9">
                                    <div style="display: block; float: left; width: 360px;">
                                        <asp:TextBox ID="tbAction6a" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div style="display: block; float: left; width: 352px;">
                                        <div style="display: block; float: left; width: 352px;">
                                            <telerik:RadAutoCompleteBox ID="racRespon6a" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                            </telerik:RadAutoCompleteBox>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                            <asp:Button ID="imbtOtherAction6a" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="97px" />
                                        </div>
                                    </div>
                                    <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                        <asp:ImageButton ID="imbtFindRespon6a" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnRespon6b" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #2</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction6b" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon6b" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 180px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction6b" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />&nbsp;
                                                    <asp:Button ID="imbtOtherAction6b" runat="server" CssClass="btn btn-sm btn-primary" Text="Add Action" Width="100px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon6b" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnRespon6c" runat="server" Visible="false">
                                <div class="row" style="padding: 4px 16px 4px 16px">
                                    <div style="display: block; float: left; width: 160px; text-align: right; margin-top: 8px;">Propose Action #3</div>
                                    <div class="col-md-9">
                                        <div style="display: block; float: left; width: 360px;">
                                            <asp:TextBox ID="tbAction6c" runat="server" CssClass="form-control input-sm" Height="88px" Width="352px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div style="display: block; float: left; width: 352px;">
                                            <div style="display: block; float: left; width: 352px;">
                                                <telerik:RadAutoCompleteBox ID="racRespon6c" runat="server" Width="352px" Skin="Bootstrap" RenderMode="Lightweight"
                                                    DataSourceID="srcAutoName" DataTextField="empFullName" DataValueField="empId" EmptyMessage="Responsible Person"
                                                    DropDownPosition="Static" MaxResultCount="10" OnClientEntryAdding="OnClientEntryAddingProposeAction">
                                                </telerik:RadAutoCompleteBox>
                                            </div>
                                            <div style="display: block; float: left; width: 60px; margin-top: 28px; margin-left: 0px;">
                                                <asp:Button ID="imbtCloseAction6c" runat="server" CssClass="btn btn-sm btn-warning" Text="Close" Width="66px" />
                                            </div>
                                        </div>
                                        <div style="display: block; float: left; width: 60px; margin-top: 4px; margin-left: 8px;">
                                            <asp:ImageButton ID="imbtFindRespon6c" runat="server" ImageUrl="~/Images/search-18h.png" OnClientClick="OpenEmployeeListWnd();return false;" Visible="false"></asp:ImageButton>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row" style="padding: 12px 16px 20px 32px"></div>
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    --%>

                </telerik:RadMultiPage>
            </div>
            <div style="border-style: solid none none none; border-width: 2px; border-color: #00AEDB; padding: 0px 16px 0px 16px;">
                <telerik:RadAjaxPanel ID="RadAjaxPanel4" runat="server" Width="100%" LoadingPanelID="RadAjaxLoadingPanel1">
                    <asp:Panel ID="pnSaveAndSend" runat="server">
                        <div class="row" style="padding: 28px 16px 4px 16px">
                            <div style="display: block; float: left; width: 160px;">
                                &nbsp;
                                <asp:SqlDataSource ID="srcContractor" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT tblContractor.* FROM tblContractor ORDER BY contractorName"></asp:SqlDataSource>
                            </div>
                            <div class="col-md-8" style="padding: 0px 0px 0px 0px;">
                                <div style="display: block; float: left; width: 216px;">
                                    <asp:Button ID="btSaveAndSend" runat="server" CssClass="btn btn-lg btn-primary" Width="200px" Text="Save" CommandName="saveandsend" UseSubmitBehavior="true" OnClientClick="disableButton(this.id, 'waiting...')" />
                                </div>
                                <div class="col-md-8" style="padding: 0px 0px 0px 0px;">
                                    <asp:ListBox ID="infobox" runat="server" CssClass="listbox-info" Width="352px" Height="106px"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnSendEmail" runat="server" Visible="False">
                        <div class="row" style="padding: 4px 16px 4px 16px">
                            <div style="display: block; float: left; width: 144px;">
                                &nbsp;
                            </div>
                            <div class="col-md-8">
                                <div class="row" style="width: 712px; padding: 12px 0px 0px 0px; font-weight: bold; font-size: medium;">
                                    Action Number
                                    <asp:Label ID="lbActionNumberComplete" runat="server" ForeColor="#2179A4"></asp:Label>
                                    &nbsp;save completed.
                                </div>
                                <asp:SqlDataSource ID="srcSendEmailList" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, recId, emailType, email, empId, empFullName, IsSuggest FROM dbo.tblSendEmail WHERE (recId = @recId) AND (emailType &lt; 1010) OR (recId = @recId) AND (emailType &gt; 2020) AND (emailType &lt; 2100)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <div style="width: 712px; padding: 12px 0px 0px 4px;">
                                    Send to :
                                </div>
                                <div style="padding: 0px 0px 0px 0px; margin-top: 1px;">
                                    <telerik:RadGrid ID="rgEmailList" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" Height="110px" DataSourceID="srcSendEmailList" ShowHeader="False" GroupPanelPosition="Top">
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                        <MasterTableView DataSourceID="srcSendEmailList">
                                            <Columns>
                                                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelectSend" runat="server" Checked='<%# Bind("IsSuggest") %>' Enabled='<%# IIf(Eval("emailType").ToString().Equals("2022"), "false", "true") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="48px" HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" Visible="False">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("empFullName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="280px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbObserveNo" runat="server" ForeColor="#0067B0" Text="&nbsp;"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="96px" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>
                                        <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                        <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                    </telerik:RadGrid>
                                </div>
                                <asp:SqlDataSource ID="srcSendListEachOb" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT Id, observItem, recId, emailType, email, empId, empFullName, IsSuggest FROM dbo.tblSendEmail WHERE (recId = @recId) AND (emailType &gt; 1100) AND (emailType &lt; 2000)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfRecId" Name="recId" PropertyName="Value" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <div style="width: 712px; padding: 10px 0px 0px 4px;">
                                    CC to action owner :<asp:CheckBox ID="chkSendEachObserve" runat="server" CssClass="chkBT2m" Font-Bold="true" Text="&nbsp;&nbsp;Send each observe" AutoPostBack="True" Visible="False" />
                                </div>
                                <asp:Panel ID="pnSendEachObserve" runat="server">
                                    <div style="padding: 0px 0px 0px 0px; margin-top: 1px;">
                                        <telerik:RadGrid ID="rgEmailListEachOb" runat="server" Skin="Metro" PageSize="15" AutoGenerateColumns="False" Width="712px" Height="200px" DataSourceID="srcSendListEachOb" ShowHeader="False" GroupPanelPosition="Top">
                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                            <MasterTableView DataSourceID="srcSendListEachOb">
                                                <Columns>
                                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelectSend" runat="server" Checked='<%# Bind("IsSuggest") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="48px" HorizontalAlign="Right" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbFullName" runat="server" Text='<%# Bind("empFullName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="280px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbObserveNo" runat="server" ForeColor="#0067B0" Text="OBSERVE 0"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="96px" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <AlternatingItemStyle Height="30px" BackColor="#D5DCE3" />
                                            <ItemStyle Height="30px" BackColor="#D5DCE3" />
                                        </telerik:RadGrid>
                                    </div>
                                </asp:Panel>
                                <div style="padding: 8px 0px 0px 0px;">
                                    <asp:Button ID="btSendEmail" runat="server" CssClass="btn btn-lg btn-primary" Width="200px" Text="Send Email" CommandName="confirmsend" UseSubmitBehavior="true" OnClientClick="disableButton(this.id, 'waiting...')" />&nbsp;&nbsp;
                                    <asp:Button ID="btObservationList" runat="server" CssClass="btn btn-lg btn-primary" Width="200px" Text="Observation List" PostBackUrl="~/observer/observationList.aspx" />&nbsp;&nbsp;
                                    <asp:Button ID="btNewObserve" runat="server" CssClass="btn btn-lg btn-primary" Width="200px" Text="New Observation" PostBackUrl="~/observer/observer.aspx" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </telerik:RadAjaxPanel>
            </div>
        </div>
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
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Bootstrap">
        <Windows>
            <telerik:RadWindow ID="rwEmployee" runat="server" Width="680px" Height="504px"
                Animation="FlyIn" VisibleStatusbar="False" Behaviors="Close, Move, Reload"
                IconUrl="../Images/blank4h4.gif" Title="Select Employee" EnableShadow="True"
                Modal="True" OnClientClose="EmployeeSelect" ShowContentDuringLoad="False"
                Style="display: inline; overflow: hidden; z-index: 80001;" RenderMode="Classic">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function OpenEmployeeListWnd() {
                var wnd = $find("<%=rwEmployee.ClientID%>");
                wnd.setUrl("../wnd_employeeList.aspx");
                wnd.set_modal(true);
                wnd.Center();
                wnd.show();
            }
            function OpenEmployeeListWndPara(ob, act) {
                var wnd = $find("<%=rwEmployee.ClientID%>");
                wnd.setUrl("../wnd_employeeList.aspx?ob=" + ob + "&act=" + act);
                wnd.set_modal(true);
                wnd.show();
            }
            function EmployeeSelect(sender, eventArgs) {
                var arg = eventArgs.get_argument();
                if (arg) {
                    var hfempid = document.getElementById("<%=hfEmpIdSelect.ClientID%>");
                    hfempid.value = arg.selEmpId;

                    var hffullname = document.getElementById("<%=hfFullNameSelect.ClientID%>");
                    hffullname.value = arg.selEmpFullname;

                    //var autoCompleteBox = $find("<%= racObservBox.ClientID %>");
                    //var empFullnameTxt = arg.selEmpFullname;

                    //if (!empFullnameTxt) return;
                    //var entry = new Telerik.Web.UI.AutoCompleteBoxEntry();
                    //entry.set_text(arg.selEmpFullname);
                    //autoCompleteBox.get_entries().add(entry);

                    var btAddEntry = document.getElementById("<%=imgAddEntry.ClientID%>");
                    btAddEntry.click();
                }
            }
            function SuccessFunction(result) {
                alert("SuccessFunction")
            }
            function FailedFunction(error) {
                alert("FailedFunction")
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadPanelBar1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbDepartment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbActionNum" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdpDocDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbActionNum" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
            <telerik:AjaxSetting AjaxControlID="racObservBox">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="racObservBox" />
                    <telerik:AjaxUpdatedControl ControlID="pnOtherObserver" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imgAddEntry">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="racObservBox" />
                    <telerik:AjaxUpdatedControl ControlID="pnOtherObserver" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbNoObserve">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rtabStrip1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction1a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction1b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction1c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon1a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon1a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction1a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction1b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction1c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon1a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon1a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload1" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction1a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction1b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction2a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction2b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction2c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon2a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon2a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction2a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction2b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction2c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon2a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon2a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload2" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction2a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction2b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition2" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction3a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction3b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction3c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon3a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon3a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction3a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction3b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction3c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon3a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon3a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg3">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload3" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction3a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction3b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition3" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction4a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction4b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction4c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon4a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon4a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction4a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction4b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction4c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon4a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon4a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg4">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload4" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction4a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition4" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction4b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition4" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction5a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction5b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction5c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon5a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon5a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction5a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction5b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction5c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon5a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon5a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg5">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload5" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction5a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition5" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction5b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition5" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="rcbCategory6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint6" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbCategorySub6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint6" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rcbObserveType6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor6" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkRecognition6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction6a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction6b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction6c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon6a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon6a" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkNonRecognition6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tbAction6a" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction6b" />
                    <telerik:AjaxUpdatedControl ControlID="tbAction6c" />
                    <telerik:AjaxUpdatedControl ControlID="racRespon6a" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindRespon6a" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6c" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btUploadImg6">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadUpload6" />
                    <telerik:AjaxUpdatedControl ControlID="PictureList6" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtOtherAction6a">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition6" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="imbtCloseAction6b">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition6" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="btSaveAndSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rcbDepartment" />
                    <telerik:AjaxUpdatedControl ControlID="rdpDocDate" />
                    <telerik:AjaxUpdatedControl ControlID="rcbTimeHH" />
                    <telerik:AjaxUpdatedControl ControlID="rcbTimeMM" />
                    <telerik:AjaxUpdatedControl ControlID="rcbDurationH" />
                    <telerik:AjaxUpdatedControl ControlID="rcbDurationM" />
                    <telerik:AjaxUpdatedControl ControlID="tbMyFullname" />
                    <telerik:AjaxUpdatedControl ControlID="racObservBox" />
                    <telerik:AjaxUpdatedControl ControlID="imbtFindObserv" />
                    <telerik:AjaxUpdatedControl ControlID="rcbNoObserve" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint1" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO1op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye1" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType1" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor1" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload1" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg1" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription1" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior1" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi1" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction1" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition1" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi1" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted1c" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint2" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO2op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye2" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType2" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor2" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload2" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg2" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription2" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior2" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi2" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction2" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition2" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi2" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted2c" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint3" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO3op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye3" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType3" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor3" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload3" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg3" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription3" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior3" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi3" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction3" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition3" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi3" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted3c" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint4" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO4op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye4" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType4" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor4" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload4" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg4" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription4" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior4" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi4" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction4" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition4" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi4" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted4c" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint5" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment5" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO5op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye5" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType5" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor5" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload5" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg5" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription5" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior5" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi5" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction5" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition5" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi5" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted5c" />

                    <telerik:AjaxUpdatedControl ControlID="tbTitle6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategory6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbCategorySub6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbFailurePoint6" />
                    <telerik:AjaxUpdatedControl ControlID="tbEquipment6" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6op1" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6op2" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6op3" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6op4" />
                    <telerik:AjaxUpdatedControl ControlID="chkHRO6op5" />
                    <telerik:AjaxUpdatedControl ControlID="chk2Eye6" />
                    <telerik:AjaxUpdatedControl ControlID="chkRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="chkSendEmail6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbObserveType6" />
                    <telerik:AjaxUpdatedControl ControlID="rcbContractor6" />
                    <telerik:AjaxUpdatedControl ControlID="RadUpload6" />
                    <telerik:AjaxUpdatedControl ControlID="btUploadImg6" />
                    <telerik:AjaxUpdatedControl ControlID="tbDescription6" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonBehavior6" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeOfi6" />
                    <telerik:AjaxUpdatedControl ControlID="chkEyeInteraction6" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonRecognition6" />
                    <telerik:AjaxUpdatedControl ControlID="chkNonOfi6" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6a" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6b" />
                    <telerik:AjaxUpdatedControl ControlID="cbActionCompleted6c" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap"></telerik:RadAjaxLoadingPanel>
</asp:Content>
