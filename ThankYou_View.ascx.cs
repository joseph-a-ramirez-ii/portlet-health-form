using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Portal.Framework;
//	using CUS.Jenzabar.OdbcConnectionClass;

namespace CUS.ICS.HealthFormPortletTLU
{
    public partial class Main_View : PortletViewBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Default;
            //mainMsg.Text = "<b>All students must complete a health form.  Click the button below to access the HealthForm.</b><br /><br />";
            if (!ParentPortlet.AccessCheck("CanAccessPortlet"))
            {
                this.ParentPortlet.ShowFeedbackGlobalized(FeedbackType.Message, "CUS_HEALTHFORM_ACCESS_DENIED_MESSAGE");
              
          

            }
            if (ParentPortlet.AccessCheck("CanAdminPortlet"))
            {
             
            }
        }

        protected void btnHealthForm_Click(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Maximized;
            this.ParentPortlet.NextScreen("WelcmLtr");

        }

        protected void glnkAdmin_Click(object sender, EventArgs e)
        {
            this.ParentPortlet.NextScreen("Admin");
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

    }
}
