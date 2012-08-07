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

namespace CUS.ICS.HealthForm
{
    public partial class WelcmLtr_View : PortletViewBase
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Maximized;
        }

        protected void Button2_Click(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Maximized;
            this.ParentPortlet.NextScreen("HlthFrm");

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