<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin_View.ascx.cs" Inherits="CUS.ICS.HealthForm.Admin_View" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register TagPrefix="jenzabar" Namespace="Jenzabar.Common.Web.UI.Controls" Assembly="Jenzabar.Common" %>
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <jenzabar:subheader id="lblText" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Text_Key")%>' />
        <jenzabar:textboxeditor id="txtText" runat="server" MaxLength="2000000" InnerHTML='<%#DataBinder.Eval(Container.DataItem, "Text_Custom_Value")%>' />
    </ItemTemplate>
    <SeparatorTemplate>
    <br />
    <br />
    </SeparatorTemplate>
</asp:Repeater>
<br />
<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />