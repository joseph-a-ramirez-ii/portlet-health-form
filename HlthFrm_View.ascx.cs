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
    public partial class HlthFrm_View : PortletViewBase
    {
        String usernm = "";
        String idnum = "";
        String curtime = "";
        Int32 idnumInt = 0;
        SqlConnection sqlcon = null;
        String session = "HEALTH";

        protected void Page_Load(object sender, System.EventArgs e)
        {

            // Get and populate known user information
            this.ParentPortlet.State = PortletState.Maximized;
            lblError.Text = "";

            try
            {
                sqlcon = new SqlConnection(
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["JenzabarConnectionString"]
                    .ConnectionString);

                sqlcon.Open();
                String sqlRead;
                sqlRead = "SELECT * FROM [dbo].[STUD_HLTH_PROFILE] "
                 + "where ID_NUM = '" + Jenzabar.Portal.Framework.PortalUser.Current.HostID + "' and HEALTH_CDE = 'HFORM' and SESS_CDE = 'HEALTH'";
                SqlCommand sqlRead1 = new SqlCommand(sqlRead, sqlcon);

                SqlDataReader reader1 = sqlRead1.ExecuteReader();

                if (reader1.HasRows)
                {
                    this.ParentPortlet.NextScreen("HlthFrmUpdt");
                    reader1.Close();

                }
                else
                {
                    reader1.Close();
                    //****************************************
                    // Try to populate 
                    //****************************************
                    try
                    {

                        usernm = Jenzabar.Portal.Framework.PortalUser.Current.Username;
                        if (usernm.Length > 15)
                        {
                            usernm = usernm.Substring(0, 14);
                        }
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
                                this.tluid.Text = reader["ID_NUM"].ToString();
                                this.lname.Text = reader["LAST_NAME"].ToString();
                                this.fname.Text = reader["FIRST_NAME"].ToString();
                                this.mname.Text = reader["MIDDLE_NAME"].ToString();
                                //         this.usernm.Text = Jenzabar.Portal.Framework.PortalUser.Current.NameDetails.DisplayName.ToString();

                                String item = reader["ADDR_CDE"].ToString();

                                idnum = reader["ID_NUM"].ToString();
                                idnumInt = Convert.ToInt32(idnum);

                                if (item == "*LHP")
                                {
                                    this.hphone.Text = reader["PHONE"].ToString();
                                }
                                if (item == "*EML")
                                {
                                    this.email.Text = reader["ADDR_LINE_1"].ToString();
                                }

                            }

                        }
                        reader.Close();
                    }
                    catch (Exception Critical)
                    {

                        lname.Enabled = false;
                        lname.Enabled = false;
                        lblError.Text = "Critical Error reading [Last Name] from database. Please try again later. If the problem persists contact ishelp@tlu.edu : " + Critical.Message;
                        btnSubmit.Enabled = false;
                    }

                }
            }
            catch (Exception Critical)
            {
                btnSubmit.Enabled = false;
                lblError.Text = "There was a critical error connecting to the database. Please try again later. If the problem persists contact ishelp@tlu.edu";
                sqlcon.Close();
            }


        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (Page.IsValid)
                {
                    SqlConnection sqlconUp = new SqlConnection(
                    System.Configuration.ConfigurationManager
                           .ConnectionStrings["JenzabarConnectionString"]
                           .ConnectionString);
                    sqlconUp.Open();

                    String pageItem;
                    String cmtText;
                    String sqlInsert;
                    DateTime Now = DateTime.Now;
                    pageItem = Now.ToString();
                    curtime = Now.ToString();
                    idnum = this.tluid.Text;
                    // Write immunization records
                    cmtText = "Form submitted On Line";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HFORM','" + pageItem + "','" + cmtText + "','"  + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmtInit = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmtInit.ExecuteNonQuery();


                    if (this.CheckBox4.Checked == true)
                    {

                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC, COMMENTS1,COMMENTS2,COMMENTS3,COMMENTS4,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CONSENT','" + this.parentgd.Text + "','" + this.parentgdPh.Text + "','" + this.parentgdCty.Text + "','" + this.parentgdSt.Text + "','" + this.parentgdZip.Text + "','0','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";

                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                        cmtText = this.parentgd.Text;
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CONSENT','" + curtime + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt1 = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt1.ExecuteNonQuery();
                    }

                    if (this.MailImmunFrm.Checked == true)
                    {
                        cmtText = "Immunization record mailed or faxed";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','IMREC','" + cmtText + "','0','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.tetdipDt.Text == ""))
                    {
                        pageItem = this.tetdipDt.Text;
                        cmtText = "Tetanus/Diptheria";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','TETANUS','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.meningitisDt.Text == ""))
                    {
                        pageItem = this.meningitisDt.Text;
                        cmtText = "Meningitis";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MENING','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.polio.Text == ""))
                    {
                        pageItem = this.polio.Text;
                        cmtText = "Polio";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','POLIO','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.measlesDt1.Text == ""))
                    {
                        pageItem = this.measlesDt1.Text;
                        cmtText = "Measles";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MEASLES1','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunmmr1.Text == ""))
                    {
                        pageItem = this.Imunmmr1.Text;
                        cmtText = "Mumps/Measle/Rubella";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MMR1','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunhep1.Text == ""))
                    {
                        pageItem = this.Imunhep1.Text;
                        cmtText = "Hepatitis 1";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HEPB1','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.measlesDt2.Text == ""))
                    {
                        pageItem = this.measlesDt2.Text;
                        cmtText = "Measles 2";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MEASLES2','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunmmr2.Text == ""))
                    {
                        pageItem = this.Imunmmr2.Text;
                        cmtText = "Measles/Mumps/Rubella 2";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MMR2','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunhep2.Text == ""))
                    {
                        pageItem = this.Imunhep2.Text;
                        cmtText = "Hepatitis 2";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HEPB2','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunmumps.Text == ""))
                    {
                        pageItem = this.Imunmmr2.Text;
                        cmtText = "Mumps";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MUMPS','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunhep3.Text == ""))
                    {
                        pageItem = this.Imunhep3.Text;
                        cmtText = "Hepatitis 3";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HEPB3','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.Imunrubella.Text == ""))
                    {
                        pageItem = this.Imunrubella.Text;
                        cmtText = "Rubella";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','RUBELLA','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (!(this.tbtestdt.Text == ""))
                    {
                        pageItem = this.tbtestdt.Text;
                        cmtText = "TB Result : " + this.TB.Text;
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','TBTEST','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }

                    if (this.TB.Text == "Yes")
                    {
                        pageItem = this.chestxray.Text;
                        cmtText = "Chest Xray Date";
                        sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','XRAY','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    
                    //  Write healthe history records (Custom table)
                    //  Relatives' history
                    if (this.hiBlPressRel.Checked == true)
                    {
                        cmtText = "Hi Blood Press, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HIBLPREL','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.strokeRel.Checked == true)
                    {
                        cmtText = "Stroke, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','STROKREL','" + cmtText + "','3','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.hrtdisRel.Checked == true)
                    {
                        cmtText = "Heart Disease, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HTDISREL','" + cmtText + "','2','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.bldDisorderRel.Checked == true)
                    {
                        cmtText = "Bleeding disorder, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BLEEDREL','" + cmtText + "','4','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.diabetesRel.Checked == true)
                    {
                        cmtText = "Diabetes, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DIABREL','" + cmtText + "','5','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.ulcersRel.Checked == true)
                    {
                        cmtText = "Ulcers, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ULCERREL','" + cmtText + "','6','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.kidneyRel.Checked == true)
                    {
                        cmtText = "Kidney, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','KIDNREL','" + cmtText + "','7','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.epilepsyRel.Checked == true)
                    {
                        cmtText = "Epilepsy, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','EPILEREL','" + cmtText + "','8','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.migraineRel.Checked == true)
                    {
                        cmtText = "Migraine, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MIGRAREL','" + cmtText + "','9','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.arthritisRel.Checked == true)
                    {
                        cmtText = "Arthritis, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ARTHREL','" + cmtText + "','10','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.cancerRel.Checked == true)
                    {
                        cmtText = "Cancer, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CANCREL','" + cmtText + "','11','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.tbRel.Checked == true)
                    {
                        cmtText = "Tuberculosis, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','TBREL','" + cmtText + "','12','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.asthmaRel.Checked == true)
                    {
                        cmtText = "Asthma, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ASTHMREL','" + cmtText + "','13','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.allergiesRel.Checked == true)
                    {
                        cmtText = "Allergies, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ALLERREL','" + cmtText + "','14','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    if (this.mentalRel.Checked == true)
                    {
                        cmtText = "Mental Illness, Relative";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MENTREL','" + cmtText + "','15','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }

                    //  Personal History

                    if (this.concussion.Checked == true)
                    {
                        cmtText = "Concussion";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CONCUSSI','" + cmtText + "','16','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.hospital.Checked == true)
                    {
                        cmtText = "Hospitalization";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','HOSPIT','" + cmtText + "','17','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.mumps.Checked == true)
                    {
                        cmtText = "Mumps";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MUMPS','" + cmtText + "','18','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.mono.Checked == true)
                    {
                        cmtText = "Mononucleosis";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MONO','" + cmtText + "','19','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.rubella.Checked == true)
                    {
                        cmtText = "German Measles (Rubella)";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','RUBELLA','" + cmtText + "','20','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.rubeola.Checked == true)
                    {
                        cmtText = "Hard/red Measles (Rubeola)";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','RUBEOLA','" + cmtText + "','21','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.chickenpox.Checked == true)
                    {
                        cmtText = "Chicken Pox";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CHICKPOX','" + cmtText + "','22','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.tuberculosis.Checked == true)
                    {
                        cmtText = "Tuberculosis";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','TUBERCUL','" + cmtText + "','23','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.bcgvaccine.Checked == true)
                    {
                        cmtText = "BCG Vaccine";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BCGVACC','" + cmtText + "','24','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.allergyother.Checked == true)
                    {
                        cmtText = "Allergic Reaction";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ALLEROTH','" + cmtText + "','25','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.digestive.Checked == true)
                    {
                        cmtText = "X-ray therapy";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','XRAYTHER','" + cmtText + "','26','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.entdisease.Checked == true)
                    {
                        cmtText = "Eye, ear, nose, throat disease";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ENTDIS','" + cmtText + "','27','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.vision.Checked == true)
                    {
                        cmtText = "Vision Correction";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','VISION  ','" + cmtText + "','28','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.seasaller.Checked == true)
                    {
                        cmtText = "Seasonal Allergy";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','SEASALLE','" + cmtText + "','29','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.bloodpress.Checked == true)
                    {
                        cmtText = "Blood Press, rheumatic, heart, vessel";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BLOODPRE','" + cmtText + "','30','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.abdominal.Checked == true)
                    {
                        cmtText = "Abdominal or intestinal problem";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ABDOMINA','" + cmtText + "','31','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.kidney.Checked == true)
                    {
                        cmtText = "Sugar, protein, kidney problem";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','KIDNEY','" + cmtText + "','32','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.seizure.Checked == true)
                    {
                        cmtText = "Seizure";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','SEIZURE ','" + cmtText + "','33','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.endocrine.Checked == true)
                    {
                        cmtText = "Diabetes";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ENDOCRIN','" + cmtText + "','34','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.thyroid.Checked == true)
                    {
                        cmtText = "Thyroid";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','THYROID ','" + cmtText + "','35','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.bonejoint.Checked == true)
                    {
                        cmtText = "Bone, joint, muscle problem";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BONEJOIN','" + cmtText + "','36','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.asthma.Checked == true)
                    {
                        cmtText = "Hay fever, asthma, other allergy";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ASTHMA','" + cmtText + "','37','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.acne.Checked == true)
                    {
                        cmtText = "Severe acne, other skin disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ACNE','" + cmtText + "','38''" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.cancer.Checked == true)
                    {
                        cmtText = "Cancer or other tumor";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CANCER','" + cmtText + "','39','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.depression.Checked == true)
                    {
                        cmtText = "Depression";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DEPRESS','" + cmtText + "','40','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.anxiety.Checked == true)
                    {
                        cmtText = "Anxiety disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ANXIETY','" + cmtText + "','41','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.bipolar.Checked == true)
                    {
                        cmtText = "Bipolar disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BIPOLAR','" + cmtText + "','42','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.obsessive.Checked == true)
                    {
                        cmtText = "Obsessive-compulsive disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','OBSESSIV','" + cmtText + "','43','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.pstd.Checked == true)
                    {
                        cmtText = "Post traumatic";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','PSTD','" + cmtText + "','44','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.adhd.Checked == true)
                    {
                        cmtText = "ADD/ADHD";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ADHD','" + cmtText + "','45','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.suicidal.Checked == true)
                    {
                        cmtText = "Suicidal ideation";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','SUICIDAL','" + cmtText + "','46','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.eating.Checked == true)
                    {
                        cmtText = "Eating disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','EATING','" + cmtText + "','47','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.anger.Checked == true)
                    {
                        cmtText = "Anger management disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ANGER','" + cmtText + "','48','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.suicideAtt.Checked == true)
                    {
                        cmtText = "Suicide attempt";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','SUICATT','" + cmtText + "','49','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.panic.Checked == true)
                    {
                        cmtText = "Panic disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','PANIC','" + cmtText + "','50','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.sleep.Checked == true)
                    {
                        cmtText = "Sleep disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','SLEEP','" + cmtText + "','51','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.conduct.Checked == true)
                    {
                        cmtText = "Anti-social or conduct disorder";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','CONDUCT','" + cmtText + "','52','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.alcohol.Checked == true)
                    {
                        cmtText = "Alcohol or substance abuse";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ALCOHOL','" + cmtText + "','53','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (this.medication.Checked == true)
                    {
                        cmtText = "Have taken medications for mental";
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MEDMENTA','" + cmtText + "','54','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (!(this.medications.Text == ""))
                    {
                        pageItem = "Current meds: ";
                        cmtText = this.medications.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MEDICATI','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.bloodtype.Text == ""))
                    {
                        pageItem = "Blood Type: ";
                        cmtText = this.bloodtype.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','BLOODTYP','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.medallergies.Text == ""))
                    {
                        pageItem = "Med Allergies : ";
                        cmtText = this.medallergies.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MEDALLER','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.otherdisorder.Text == ""))
                    {
                        pageItem = "Other Disorders : ";
                        cmtText = this.otherdisorder.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','OTHDISOR','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.disordermeds.Text == ""))
                    {
                        pageItem = "Mental meds: ";
                        cmtText = this.disordermeds.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','MENTMEDS','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }

                    if (!(this.foreignloc.Text == ""))
                    {
                        pageItem = "Foreign Local: ";
                        cmtText = this.foreignloc.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','FORLOCAL','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.foreigntime.Text == ""))
                    {
                        pageItem = "Foreign Time: ";
                        cmtText = this.foreigntime.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','FORTIME ','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }

                    if (this.psyther.Text == "Yes")
                    {
                        pageItem = "Will continue psychotherapy";
                        cmtText = this.psytherTxt.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','PSYTHER','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (this.psyhosp.Text == "Yes")
                    {
                        pageItem = "Have been hospitalized, Psychiatric";
                        cmtText = this.psyhospdtl.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','PSYHOSP','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (this.addict.Text == "Yes")
                    {
                        pageItem = "Treated for alcohol/substance ";
                        cmtText = this.addictdates.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','ADDICT','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();

                    }
                    if (!(this.disability1.Text == ""))
                    {
                        pageItem = "Nature of disability : ";
                        cmtText = this.disability1.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DISABIL1','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (!(this.disability2.Text == ""))
                    {
                        pageItem = "Disability Diagnosis : ";
                        cmtText = this.disability2.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DISABIL2','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (!(this.disability3.Text == ""))
                    {
                        pageItem = "Type accommodations : ";
                        cmtText = this.disability3.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DISABIL3','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (!(this.disability4.Text == ""))
                    {
                        pageItem = "Accommodations requested : ";
                        cmtText = this.disability4.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DISABIL4','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    if (!(this.disability5.Text == ""))
                    {
                        pageItem = "Anticipate New accommodations : ";
                        cmtText = this.disability5.Text;
                        sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                               + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                               + "VALUES('" + session + "', '" + idnum + "','DISABIL5','" + pageItem + "','" + cmtText + "','1','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                        SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                        sqlStmt.ExecuteNonQuery();
                    }
                    lblComplete.Text = "Thank you for completing the health form";
                    this.ParentPortlet.NextScreen("Review");

                }
                else
                {
                    lblComplete.Text = "Please correct any errors and resubmit form";
                    //       this.ParentPortlet.NextScreen("HlthFrm");
                }
            }
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
