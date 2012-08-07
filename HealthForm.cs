using System;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Jenzabar.Common.Globalization;
using Jenzabar.Portal.Framework.Web.UI; // PortletBase
using Jenzabar.Portal.Framework.Web.UI.Controls.MetaControls.Attributes; // Preferences and Settings
using Jenzabar.Portal.Framework; // NameValueDataSourceType
using Jenzabar.ICS.Web.Portlets.Common; // PortletUtilities
using Jenzabar.Portal.Framework.Security.Authorization;

#region "Settings - Optional"
	/// <summary>
	/// 
	
	/// </summary>
	/// 	


#endregion

namespace CUS.ICS.HealthForm
{
    [PortletOperation(
        "CanAccessPortlet",
        "Can Access Portlet",
        "Whether a user can access this portlet or not",
        PortletOperationScope.Global)]

    [PortletOperation(
        "CanAdminPortlet",
        "Can Admin Portlet",
        "Whether a user can admin this portlet or not",
        PortletOperationScope.Global)]

    public partial class HealthFormPortletTLU : SecuredPortletBase
    {
        protected override PortletViewBase GetCurrentScreen()
        {
            PortletViewBase screen = null;
            switch (this.CurrentPortletScreenName)
            {
                case "Admin":
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/Admin_View.ascx");
                    break;
                case "WelcmLtr":
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/WelcmLtr_View.ascx");
                    break;
                case "HlthFrm":
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/HlthFrm_View.ascx");
                    break;
                case "HlthFrmUpdt":
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/HlthFrmUpdt_View.ascx");
                    break;
                case "Review":
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/Review_view.ascx");
                    break;
                case "Main":                  
                default:
                    screen = this.LoadPortletView("ICS/HealthFormPortletTLU/Main_View.ascx");
                    break;
            }
            return screen;
        }
    }
}


		