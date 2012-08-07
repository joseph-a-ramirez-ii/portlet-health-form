<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Review_View.ascx.cs" Inherits="CUS.ICS.HealthForm.Review_View" TargetSchema=""%>
<%@ Register TagPrefix="common" Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" %>
<style type="text/css">
    .style1
    {
        width: 139px;
        
    }
    .left
    {
    	text-align: left;
    }
    .right
    {
    	text-align: right;
    }
    
    #pg0_V_mentaldesc
    {
        height: 25px;
        width: 644px;
    }
    
    </style>
<body style="font-weight: 700">

<br /><br />
   Thank you for completing the Health form.  You may review or update the form at any time. 
    <b>Click the button below to return to the form.</b><br />
<asp:label id="lblError1" Runat="server" ForeColor="Red" BackColor="Yellow" /><br />
				
<asp:Button ID="btnSubmit" OnClick="Button3_Click" runat="server" Text="Return" />

			

</body>