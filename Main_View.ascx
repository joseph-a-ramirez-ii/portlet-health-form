<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Main_View.ascx.cs" Inherits="CUS.ICS.HealthForm.Main_View" TargetSchema=""%>
<%@ Register TagPrefix="jenzabar" Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" %>
<div id="divAdminLink" runat="server" visible="false">
	<table class="GrayBordered" width="50%" align="center" cellpadding="3" border="0">
		<tr>
			<td align="center"><IMG title="" alt="*" src="UI\Common\Images\PortletImages\Icons\portlet_admin_icon.gif">&nbsp;<jenzabar:globalizedlinkbutton OnClick="glnkAdmin_Click" id="glnkAdmin" runat="server" TextKey="TXT_CS_ADMIN_THIS_PORTLET"></jenzabar:globalizedlinkbutton></td>
		</tr>
	</table>
</div>
<div class="pSection">
   
</div>
<jenzabar:GlobalizedLabel TextKey="CUS_HEALTHFORM_WELCOME_MESSAGE" ID="mainMsg" runat="server" />
<asp:Button ID="btnHealthForm" OnClick="btnHealthForm_Click" runat="server" Text="Health Form" />
