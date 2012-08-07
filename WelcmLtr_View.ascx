<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WelcmLtr_View.ascx.cs" Inherits="CUS.ICS.HealthForm.WelcmLtr_View" TargetSchema=""%>
<%@ Register TagPrefix="jenzabar" Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" %>
    
    <jenzabar:GlobalizedLabel id="HealthLetter" runat="server" TextKey="CUS_HEALTHFORM_LETTER_MESSAGE" />
    <asp:Button ID="Button2" OnClick="Button2_Click" runat="server" Text="Health Form" />
