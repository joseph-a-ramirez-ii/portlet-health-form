using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Jenzabar.Common;
using Jenzabar.Portal.Framework;
using Jenzabar.Common.Web.UI.Controls;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Common.ApplicationBlocks.Data;

namespace CUS.ICS.HealthForm
{
    public partial class Review_View : PortletViewBase
    {
        String usernm = "";
        String idnum = "";
        String curtime = "";
        Int32 idnumInt = 0;
        SqlConnection sqlcon = null;
        String session = "HEALTH";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Maximized;
            lblError1.Text = "Lbl Error";

            try
            {
                sqlcon = new SqlConnection(
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["JenzabarConnectionString"]
                    .ConnectionString);

                sqlcon.Open();

                //****************************************
                // Try to populate 
                //****************************************
                try
                {

                    usernm = Jenzabar.Portal.Framework.PortalUser.Current.Username;
                    SqlCommand sqlcmdHealthPerson = new SqlCommand(
                     "SELECT a.ID_NUM, LAST_NAME, FIRST_NAME, MIDDLE_NAME, PHONE, ADDR_CDE, "
                    + "ADDR_LINE_1 "
                    + "FROM NAME_MASTER a LEFT JOIN ADDRESS_MASTER b "
                    + "ON a.ID_NUM = b.ID_NUM "
                    + "WHERE a.ID_NUM = '" + Jenzabar.Portal.Framework.PortalUser.Current.HostID + "'", sqlcon);

                    SqlDataReader reader = sqlcmdHealthPerson.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //          this.tluid.Text = reader["ID_NUM"].ToString();
                         /*   this.lname.Text = reader["LAST_NAME"].ToString();
                            this.fname.Text = reader["FIRST_NAME"].ToString();
                            this.mname.Text = reader["MIDDLE_NAME"].ToString();*/
                            //         this.usernm.Text = Jenzabar.Portal.Framework.PortalUser.Current.NameDetails.DisplayName.ToString();

                            String item = reader["ADDR_CDE"].ToString();

                            //          idnum = reader["ID_NUM"].ToString();
                            //          idnumInt = Convert.ToInt32(idnum);

                            //          if (item == "*LHP")
                            //          {
                            //              this.hphone.Text = reader["PHONE"].ToString();
                            //          }
                            //          if (item == "*EML")
                            //          {
                            //              this.email.Text = reader["ADDR_LINE_1"].ToString();
                            //          }

                        }

                    }
                    reader.Close();
                }
                catch (Exception Critical)
                {

               
                    lblError1.Text = "Critical Error reading [Last Name] from database. Please try again later. If the problem persists contact ishelp@tlu.edu : " + Critical.Message;
                    btnSubmit.Enabled = false;
                }
            }


            catch (Exception Critical)
            {
                btnSubmit.Enabled = false;
                lblError1.Text = "There was a critical error connecting to the database. Please try again later. If the problem persists contact ishelp@tlu.edu";
                sqlcon.Close();
            }

        }
        protected void Button3_Click(object sender, System.EventArgs e)
        {
            this.ParentPortlet.State = PortletState.Maximized;
            this.ParentPortlet.NextScreen("Main");

        }

    }
}