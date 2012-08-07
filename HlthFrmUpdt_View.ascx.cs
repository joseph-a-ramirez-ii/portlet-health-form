using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

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
    public partial class HlthFrmUpdt_View : PortletViewBase
    {
        String usernm = "";
        String idnum = "";
        String curtime = "";
        Int32 idnumInt = 0;
        int postInd;
        SqlConnection sqlcon = null;
        String session = "HEALTH";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //  if (Jenzabar.Portal.Framework.PortalUser.Current.IsMemberOf(PortalGroup.Students))
            //  {


            if (IsFirstLoad)
            {

                idnum = this.tluid.Text;
                //  }
                //  else
                //  {
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

                    //****************************************
                    // Try to populate 
                    //****************************************
                    try
                    {

                        usernm = Jenzabar.Portal.Framework.PortalUser.Current.Username;
                        //    usernmDB = this.usernm.Text;
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

                        String inputCde;
                        String inputDte;
                        String inputTxt;
                        String inputCmt;

                        SqlCommand sqlcmdHealthForm = new SqlCommand(
                            "SELECT ID_NUM, HEALTH_CDE, BEGIN_DTE,COMMENT_TXT "
                            + "FROM STUD_HLTH_PROFILE "
                            + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + Jenzabar.Portal.Framework.PortalUser.Current.HostID + "'", sqlcon);
                        // Populate from from DB
                        SqlDataReader hlthreader = sqlcmdHealthForm.ExecuteReader();
                        if (hlthreader.HasRows)
                        {
                            while (hlthreader.Read())
                            {
                                inputCde = hlthreader["HEALTH_CDE"].ToString();
                                inputDte = hlthreader["BEGIN_DTE"].ToString();    //.Substring(0, 10);
                                inputCmt = hlthreader["COMMENT_TXT"].ToString();

                                DateTime inDte = Convert.ToDateTime(inputDte);
                                inputDte = inDte.ToShortDateString();

                                if (inputCde == "TETANUS ")
                                {
                                    this.tetdipDt.Text = inputDte;
                                }
                                if (inputCde == "MENING  ")
                                {
                                    this.meningitisDt.Text = inputDte;
                                }
                                if (inputCde == "POLIO   ")
                                {
                                    this.polio.Text = inputDte;
                                }
                                if (inputCde == "MEASLES1")
                                {
                                    this.measlesDt1.Text = inputDte;
                                }
                                if (inputCde == "MMR1    ")
                                {
                                    this.Imunmmr1.Text = inputDte;
                                }
                                if (inputCde == "HEPB1   ")
                                {
                                    this.Imunhep1.Text = inputDte;
                                }
                                if (inputCde == "MEASLES2")
                                {
                                    this.measlesDt2.Text = inputDte;
                                }
                                if (inputCde == "MMR2    ")
                                {
                                    this.Imunmmr2.Text = inputDte;
                                }
                                if (inputCde == "HEPB2   ")
                                {
                                    this.Imunhep2.Text = inputDte;
                                }
                                if (inputCde == "MUMPS   ")
                                {
                                    this.Imunmumps.Text = inputDte;
                                }
                                if (inputCde == "HEPB3   ")
                                {
                                    this.Imunhep3.Text = inputDte;
                                }
                                if (inputCde == "RUBELLA ")
                                {
                                    this.Imunrubella.Text = inputDte;
                                }
                                if (inputCde == "TBTEST  ")
                                {
                                    this.tbtestdt.Text = inputDte;
                                    if ((inputCmt.Substring(12, 3) == "Pos"))
                                    {
                                        this.TB.Text = "Pos";
                                        this.TB.SelectedIndex = 1;

                                    }
                                    //                 lblError.Text = usernm + inputCmt + (inputCmt.Substring(12, 3));
                                }
                                if (inputCde == "XRAY    ")
                                {
                                    this.chestxray.Text = inputDte;
                                }

                            }

                        }
                        hlthreader.Close();
                        SqlCommand sqlcmdCustForm = new SqlCommand(
                         "SELECT ID_NUM, HEALTH_CDE, HEALTH_CDE_DESC,COMMENTS1,COMMENTS2,COMMENTS3,COMMENTS4,COMMENTS5 "
                       + "FROM CUST_STUD_HLTH_PROFILE "
                       + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + Jenzabar.Portal.Framework.PortalUser.Current.HostID + "'", sqlcon);

                        SqlDataReader custreader = sqlcmdCustForm.ExecuteReader();
                        if (custreader.HasRows)
                        {
                            while (custreader.Read())
                            {
                                inputCde = custreader["HEALTH_CDE"].ToString();
                                inputDte = custreader["HEALTH_CDE_DESC"].ToString();
                                inputTxt = custreader["COMMENTS1"].ToString();
                                if (inputCde == "CONSENT ")
                                {
                                    this.CheckBox4.Checked = true;
                                    this.parentgd.Text = custreader["HEALTH_CDE_DESC"].ToString();
                                    this.parentgdPh.Text = custreader["COMMENTS1"].ToString();
                                    this.parentgdCty.Text = custreader["COMMENTS2"].ToString();
                                    this.parentgdSt.Text = custreader["COMMENTS3"].ToString();
                                    this.parentgdZip.Text = custreader["COMMENTS4"].ToString();
                                }
                                if (inputCde == "HIBLPREL")
                                {
                                    this.hiBlPressRel.Checked = true;
                                }
                                if (inputCde == "HTDISREL")
                                {
                                    this.hrtdisRel.Checked = true;
                                }
                                if (inputCde == "STROKREL")
                                {
                                    this.strokeRel.Checked = true;
                                }
                                if (inputCde == "BLEEDREL")
                                {
                                    this.bldDisorderRel.Checked = true;
                                }
                                if (inputCde == "DIABREL ")
                                {
                                    this.diabetesRel.Checked = true;
                                }
                                if (inputCde == "ULCERREL")
                                {
                                    this.ulcersRel.Checked = true;
                                }
                                if (inputCde == "KIDNREL ")
                                {
                                    this.kidneyRel.Checked = true;
                                }
                                if (inputCde == "EPILEREL")
                                {
                                    this.epilepsyRel.Checked = true;
                                }
                                if (inputCde == "MIGRAREL")
                                {
                                    this.migraineRel.Checked = true;
                                }
                                if (inputCde == "ARTHREL ")
                                {
                                    this.arthritisRel.Checked = true;
                                }
                                if (inputCde == "CANCREL ")
                                {
                                    this.cancerRel.Checked = true;
                                }
                                if (inputCde == "TBREL   ")
                                {
                                    this.tbRel.Checked = true;
                                }
                                if (inputCde == "ASTHMREL")
                                {
                                    this.asthmaRel.Checked = true;
                                }
                                if (inputCde == "ALLERREL")
                                {
                                    this.allergiesRel.Checked = true;
                                }
                                if (inputCde == "MENTREL ")
                                {
                                    this.mentalRel.Checked = true;
                                }
                                if (inputCde == "IMREC   ")
                                {
                                    this.MailImmunFrm.Checked = true;
                                }
                                if (inputCde == "MEDICATI")
                                {
                                    this.medications.Text = inputTxt;
                                }
                                if (inputCde == "CONCUSSI")
                                {
                                    this.concussion.Checked = true;
                                }
                                if (inputCde == "HOSPIT  ")
                                {
                                    this.hospital.Checked = true;
                                }
                                if (inputCde == "MUMPS   ")
                                {
                                    this.mumps.Checked = true;
                                }
                                if (inputCde == "MONO    ")
                                {
                                    this.mono.Checked = true;
                                }
                                if (inputCde == "RUBELLA ")
                                {
                                    this.rubella.Checked = true;
                                }
                                if (inputCde == "RUBEOLA ")
                                {
                                    this.rubeola.Checked = true;
                                }
                                if (inputCde == "CHICKPOX")
                                {
                                    this.chickenpox.Checked = true;
                                }
                                if (inputCde == "TUBERCUL")
                                {
                                    this.tuberculosis.Checked = true;
                                }
                                if (inputCde == "BCGVACC ")
                                {
                                    this.bcgvaccine.Checked = true;
                                }
                                if (inputCde == "ALLEROTH")
                                {
                                    this.allergyother.Checked = true;
                                }
                                if (inputCde == "DIGESTIV")
                                {
                                    this.digestive.Checked = true;
                                }
                                if (inputCde == "ENTDIS  ")
                                {
                                    this.entdisease.Checked = true;
                                }
                                if (inputCde == "VISION  ")
                                {
                                    this.vision.Checked = true;
                                }
                                if (inputCde == "SEASALLE")
                                {
                                    this.seasaller.Checked = true;
                                }
                                if (inputCde == "BLOODPRE")
                                {
                                    this.bloodpress.Checked = true;
                                }
                                if (inputCde == "ABDOMINA")
                                {
                                    this.abdominal.Checked = true;
                                }
                                if (inputCde == "KIDNEY  ")
                                {
                                    this.kidney.Checked = true;
                                }
                                if (inputCde == "STD     ")
                                {
                                    this.seizure.Checked = true;
                                }
                                if (inputCde == "ENDOCRIN")
                                {
                                    this.endocrine.Checked = true;
                                }
                                if (inputCde == "THYROID ")
                                {
                                    this.thyroid.Checked = true;
                                }
                                if (inputCde == "BONEJOIN")
                                {
                                    this.bonejoint.Checked = true;
                                }
                                if (inputCde == "ASTHMA  ")
                                {
                                    this.asthma.Checked = true;
                                }
                                if (inputCde == "ACNE    ")
                                {
                                    this.acne.Checked = true;
                                }
                                if (inputCde == "CANCER  ")
                                {
                                    this.cancer.Checked = true;
                                }
                                if (inputCde == "BLOODTYP")
                                {
                                    this.bloodtype.Text = inputTxt;
                                }
                                if (inputCde == "MEDALLER")
                                {
                                    this.medallergies.Text = inputTxt;
                                }
                                if (inputCde == "OTHDISOR")
                                {
                                    this.otherdisorder.Text = inputTxt;
                                }
                                if (inputCde == "FORLOCAL")
                                {
                                    this.foreignloc.Text = inputTxt;
                                }
                                if (inputCde == "FORTIME ")
                                {
                                    this.foreigntime.Text = inputTxt;
                                }
                                if (inputCde == "DEPRESS ")
                                {
                                    this.depression.Checked = true;
                                }
                                if (inputCde == "ANXIETY ")
                                {
                                    this.anxiety.Checked = true;
                                }
                                if (inputCde == "BIPOLAR ")
                                {
                                    this.bipolar.Checked = true;
                                }
                                if (inputCde == "OBSESSIV")
                                {
                                    this.obsessive.Checked = true;
                                }
                                if (inputCde == "PSTD    ")
                                {
                                    this.pstd.Checked = true;
                                }
                                if (inputCde == "ADHD    ")
                                {
                                    this.adhd.Checked = true;
                                }
                                if (inputCde == "SUICIDAL")
                                {
                                    this.suicidal.Checked = true;
                                }
                                if (inputCde == "EATING  ")
                                {
                                    this.eating.Checked = true;
                                }
                                if (inputCde == "ANGER   ")
                                {
                                    this.anger.Checked = true;
                                }
                                if (inputCde == "SUICATT ")
                                {
                                    this.suicideAtt.Checked = true;
                                }
                                if (inputCde == "PANIC   ")
                                {
                                    this.panic.Checked = true;
                                }
                                if (inputCde == "SLEEP   ")
                                {
                                    this.sleep.Checked = true;
                                }
                                if (inputCde == "CONDUCT ")
                                {
                                    this.conduct.Checked = true;
                                }
                                if (inputCde == "ALCOHOL ")
                                {
                                    this.alcohol.Checked = true;
                                }
                                if (inputCde == "MEDMENTA")
                                {
                                    this.medication.Checked = true;
                                }
                                if (inputCde == "MENTMEDS")
                                {
                                    this.disordermeds.Text = inputTxt;
                                }
                                if (inputCde == "PSYTHER ")
                                {
                                    this.psytherTxt.Text = inputTxt;
                                }
                                if (inputCde == "PSYHOSP ")
                                {
                                    this.psyhospdtl.Text = inputTxt;
                                }
                                if (inputCde == "ADDICT  ")
                                {
                                    this.addictdates.Text = inputTxt;
                                }
                                if (inputCde == "DISABIL1")
                                {
                                    this.disability1.Text = inputTxt;
                                }
                                if (inputCde == "DISABIL2")
                                {
                                    this.disability2.Text = inputTxt;
                                }
                                if (inputCde == "DISABIL3")
                                {
                                    this.disability3.Text = inputTxt;
                                }
                                if (inputCde == "DISABIL4")
                                {
                                    this.disability4.Text = inputTxt;
                                }
                                if (inputCde == "DISABIL5")
                                {
                                    this.disability5.Text = inputTxt;
                                }
                            }
                        }
                        custreader.Close();




                    }
                    catch (Exception Critical)
                    {
                        lname.Enabled = false;
                        lname.Enabled = false;
                        lblError.Text = "Critical Error reading [Last Name] from database. Please try again later. If the problem persists contact ishelp@tlu.edu : " + Critical.Message;
                        btnSubmit2.Enabled = false;
                    }

                }
                catch (Exception Critical)
                {
                    btnSubmit2.Enabled = false;
                    lblError.Text = "There was a critical error connecting to the database. Please try again later. If the problem persists contact ishelp@tlu.edu";
                    sqlcon.Close();
                }
            }
            //   }
            //   else
            //   {
            //        this.ParentPortlet.ShowFeedback(FeedbackType.Message, "Health Form for students only.");
            //   }
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            // if (!Page.IsPostBack)
            // {
            //     if (Page.IsValid)
            //     {
            SqlConnection sqlconUp = new SqlConnection(
                        System.Configuration.ConfigurationManager
                        .ConnectionStrings["JenzabarConnectionString"]
                        .ConnectionString);
            sqlconUp.Open();
            idnum = Jenzabar.Portal.Framework.PortalUser.Current.HostID;
            usernm = Jenzabar.Portal.Framework.PortalUser.Current.Username;

            String pageItem;
            String cmtText = "";
            String cmt5Text = "";
            String sqlUpdate;
            String sqlInsert;
            String sqlCheck;
            String sqlDelete;
            DateTime Now = DateTime.Now;
            DateTime imDt;
            pageItem = Now.ToString();
            curtime = Now.ToString();
            idnum = this.tluid.Text;
            idnumInt = Convert.ToInt32(idnum);
            // Write immunization records

            cmtText = "Form submitted On Line";
            sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                   + "SET BEGIN_DTE = '" + pageItem + "',USER_NAME = '" + usernm + "'"
                   + ", JOB_TIME = '" + pageItem + "' "
                   + "WHERE SESS_CDE = 'HEALTH' and ID_NUM = '" + idnumInt + "' and HEALTH_CDE = 'HFORM'";
            SqlCommand sqlStmtInit = new SqlCommand(sqlUpdate, sqlconUp);
            sqlStmtInit.ExecuteNonQuery();
            this.ParentPortlet.NextScreen("MAIN");

            if (this.CheckBox4.Checked == true)
            {
                try
                {
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                       + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC, COMMENTS1,COMMENTS2,COMMENTS3,COMMENTS4,USER_NAME,JOB_NAME,JOB_TIME) "
                       + "VALUES('" + session + "', '" + idnum + "','CONSENT','" + this.parentgd.Text + "','" + this.parentgdPh.Text + "','" + this.parentgdCty.Text + "','" + this.parentgdSt.Text + "','" + this.parentgdZip.Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // cmt5Text = ex.ToString();
                    //    sqlUpdate = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                    //      + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC, COMMENTS1,COMMENTS2,COMMENTS3,COMMENTS4,USER_NAME,JOB_NAME,JOB_TIME) "
                    //      + "VALUES('" + session + "', '" + idnum + "','error', '" + this.parentgd.Text + "','" + this.parentgdPh.Text + "','" + this.parentgdCty.Text + "','" + this.parentgdSt.Text + "','" + this.parentgdZip.Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    //    SqlCommand sqlUpdt = new SqlCommand(sqlUpdate, sqlconUp);
                    //    sqlUpdt.ExecuteNonQuery();

                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                       + "SET HEALTH_CDE_DESC = '" + this.parentgd.Text + "' "
                       + ",COMMENTS1 = '" + this.parentgdPh.Text + "' "
                       + ",COMMENTS2 = '" + this.parentgdCty.Text + "' "
                       + ",COMMENTS3 = '" + this.parentgdSt.Text + "' "
                       + ",COMMENTS4 = '" + this.parentgdZip.Text + "' "
                       + ",COMMENTS5 = '0'"
                       + ",USER_NAME = '" + usernm + "'"
                       + ", JOB_TIME = '" + pageItem + "' "
                       + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'CONSENT%'";
                    SqlCommand sqlUpdt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlUpdt.ExecuteNonQuery();
                }
            }



            if (!(this.tetdipDt.Text == ""))
            {

                try
                {
                    pageItem = this.tetdipDt.Text;
                    cmtText = "Tetanus/Diptheria";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','TETANUS','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.tetdipDt.Text;
                    imDt = Convert.ToDateTime(pageItem);

                    cmtText = "Tetanus/Diptheria";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnumInt + "' and HEALTH_CDE like 'TETANUS%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }


            }

            if (!(this.meningitisDt.Text == ""))
            {

                try
                {
                    pageItem = this.meningitisDt.Text;
                    cmtText = "Meningitis";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MENING','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.meningitisDt.Text;
                    imDt = Convert.ToDateTime(pageItem);

                    cmtText = "Meningitis";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE = 'HEALTH  ' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MENING%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }

            if (!(this.polio.Text == ""))
            {
                try
                {
                    pageItem = this.polio.Text;
                    cmtText = "Polio";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','POLIO','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.polio.Text;
                    cmtText = "Polio";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'POLIO%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.measlesDt1.Text == null))
            {

                try
                {
                    pageItem = this.measlesDt1.Text;
                    cmtText = "Measles";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MEASLES1','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.measlesDt1.Text;
                    cmtText = "Measles";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MEASLES1'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunmmr1.Text == ""))
            {
                try
                {
                    pageItem = this.Imunmmr1.Text;
                    cmtText = "Mumps/Measle/Rubella";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MMR1    ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunmmr1.Text;
                    cmtText = "Mumps/Measle/Rubella";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MMR1%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunhep1.Text == ""))
            {
                try
                {
                    pageItem = this.Imunhep1.Text;
                    cmtText = "Hepatitis 1";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HEPB1  ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunhep1.Text;
                    cmtText = "Hepatitis 1";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'HEPB1%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.measlesDt2.Text == ""))
            {
                try
                {
                    pageItem = this.measlesDt2.Text;
                    cmtText = "Measles 2";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MEASLES2','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.measlesDt2.Text;
                    cmtText = "Measles 2";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MEASLES2'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunmmr2.Text == ""))
            {
                try
                {
                    pageItem = this.Imunmmr2.Text;
                    cmtText = "Measles/Mumps/Rubella 2";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MMR2    ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunmmr2.Text;
                    cmtText = "Measles/Mumps/Rubella 2";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MMR2%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunhep2.Text == ""))
            {
                try
                {
                    pageItem = this.Imunhep2.Text;
                    cmtText = "Hepatitis 2";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HEPB2   ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunhep2.Text;
                    cmtText = "Hepatitis 2";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'HEPB2%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunmumps.Text == ""))
            {
                try
                {
                    pageItem = this.Imunmumps.Text;
                    cmtText = "Mumps";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MUMPS   ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunmumps.Text;
                    cmtText = "Mumps";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MUMPS%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunhep3.Text == ""))
            {
                try
                {
                    pageItem = this.Imunhep3.Text;
                    cmtText = "Hepatitis 3";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HEPB3   ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunhep3.Text;
                    cmtText = "Hepatitis 3";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'HEPB3%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.Imunrubella.Text == ""))
            {
                try
                {
                    pageItem = this.Imunrubella.Text;
                    cmtText = "Rubella";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','RUBELLA','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.Imunrubella.Text;
                    cmtText = "Rubella";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'RUBELLA%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }

            if (!(this.tbtestdt.Text == ""))
            {
                try
                {
                    pageItem = this.tbtestdt.Text;
                    cmtText = "TB Result : " + this.TB.Text;
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','TBTEST','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.tbtestdt.Text;
                    cmtText = "TB Result : " + this.TB.Text;
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'TBTEST%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }

            if (this.TB.Text == "Pos")
            {
                try
                {
                    pageItem = this.chestxray.Text;
                    cmtText = "Chest Xray Date";
                    sqlInsert = "INSERT INTO [dbo].[STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,BEGIN_DTE, COMMENT_TXT,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','XRAY','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = this.chestxray.Text;
                    cmtText = "Chest Xray Date";
                    sqlUpdate = "UPDATE [dbo].[STUD_HLTH_PROFILE] "
                           + "SET BEGIN_DTE = '" + pageItem + "' "
                           + ", COMMENT_TXT = '" + cmtText + "' "
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'XRAY%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }

            //  Write health history records (Custom table)
            //                    
            //
            //


            if (this.hiBlPressRel.Checked)
            {
                try
                {
                    cmtText = "Hi Blood Press, Relative";
                    cmt5Text = "1";
                    sqlUpdate = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HIBLPREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'HIBLPREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }

            if (this.MailImmunFrm.Checked)
            {
                try
                {
                    cmtText = "Immunization record mailed or faxed";
                    cmt5Text = "0";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','IMREC','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'IMREC   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.strokeRel.Checked)
            {
                try
                {
                    cmtText = "Stroke, Relative";
                    cmt5Text = "3";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','STROKREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'STROKREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.hrtdisRel.Checked)
            {
                try
                {
                    cmtText = "Heart Disease, Relative";
                    cmt5Text = "2";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HTDISREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'HTDISREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.bldDisorderRel.Checked)
            {
                try
                {
                    cmtText = "Bleeding disorder, Relative";
                    cmt5Text = "4";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BLEEDREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'BLEEDREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.diabetesRel.Checked)
            {
                try
                {
                    cmtText = "Diabetes, Relative";
                    cmt5Text = "5";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DIABREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";

                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'DIABREL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.ulcersRel.Checked)
            {
                try
                {
                    cmtText = "Ulcers, Relative";
                    cmt5Text = "6";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ULCERREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ULCERREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.kidneyRel.Checked)
            {
                try
                {
                    cmtText = "Kidney, Relative";
                    cmt5Text = "7";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','KIDNREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'KIDNREL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.epilepsyRel.Checked)
            {
                try
                {
                    cmtText = "Epilepsy, Relative";
                    cmt5Text = "8";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','EPILEREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'EPILEREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.migraineRel.Checked)
            {
                try
                {
                    cmtText = "Migraine, Relative";
                    cmt5Text = "9";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MIGRAREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'MIGRAREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.arthritisRel.Checked)
            {
                try
                {
                    cmtText = "Arthritis, Relative";
                    cmt5Text = "10";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ARTHREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ARTHREL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.cancerRel.Checked)
            {
                try
                {
                    cmtText = "Cancer, Relative";
                    cmt5Text = "11";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','CANCREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'CANCREL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.tbRel.Checked)
            {
                try
                {
                    cmtText = "Tuberculosis, Relative";
                    cmt5Text = "12";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','TBREL   ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'TBREL   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.asthmaRel.Checked)
            {
                try
                {
                    cmtText = "Asthma, Relative";
                    cmt5Text = "13";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ASTHMREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ASTHMREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.allergiesRel.Checked)
            {
                try
                {
                    cmtText = "Allergies, Relative";
                    cmt5Text = "14";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ALLERREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ALLERREL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.mentalRel.Checked)
            {
                try
                {
                    cmtText = "Mental Illness, Relative";
                    cmt5Text = "15";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MENTREL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'MENTREL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            //  Personal History

            if (this.concussion.Checked)
            {
                try
                {
                    cmtText = "Concussion";
                    cmt5Text = "16";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','CONCUSSI','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'INJURY  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.hospital.Checked)
            {
                try
                {
                    cmtText = "Hospitalization";
                    cmt5Text = "17";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','HOSPIT  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'HOSPIT  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.mumps.Checked)
            {
                try
                {
                    cmtText = "Mumps";
                    cmt5Text = "18";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MUMPS   ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'MUMPS   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.mono.Checked)
            {
                try
                {
                    cmtText = "Mononucleosis";
                    cmt5Text = "19";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MONO    ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'MONO    '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.rubella.Checked)
            {
                try
                {
                    cmtText = "German Measles (Rubella)";
                    cmt5Text = "20";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','RUBELLA ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'RUBELLA '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.rubeola.Checked)
            {
                try
                {
                    cmtText = "Hard/red Measles (Rubeola)";
                    cmt5Text = "21";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','RUBEOLA ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'RUBEOLA '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.chickenpox.Checked)
            {
                try
                {
                    cmtText = "Chicken Pox";
                    cmt5Text = "22";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','CHICKPOX','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'CHICKPOX'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.tuberculosis.Checked)
            {
                try
                {
                    cmtText = "Tuberculosis";
                    cmt5Text = "23";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','TUBERCUL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'TUBERCUL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.bcgvaccine.Checked)
            {
                try
                {
                    cmtText = "BCG Vaccine";
                    cmt5Text = "24";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BCGVACC ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'BCGVACC '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.allergyother.Checked)
            {
                try
                {
                    cmtText = "Allergic Reaction";
                    cmt5Text = "25";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ALLEROTH','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ALLEROTH'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.digestive.Checked)
            {
                try
                {
                    cmtText = "Digestive";
                    cmt5Text = "26";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DIGESTIV','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'XRAYTHER'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.entdisease.Checked)
            {
                try
                {
                    cmtText = "Eye, ear, nose, throat disease";
                    cmt5Text = "27";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ENTDIS  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ENTDIS  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.vision.Checked)
            {
                try
                {
                    cmtText = "Vision Correction";
                    cmt5Text = "28";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','VISION  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'HEADACHE'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.seasaller.Checked)
            {
                try
                {
                    cmtText = "Seasonal Allergy";
                    cmt5Text = "29";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','SEASALLE','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'LUNGDIS '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.bloodpress.Checked)
            {
                try
                {
                    cmtText = "Blood Press, rheumatic, heart, vessel";
                    cmt5Text = "30";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BLOODPRE','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'BLOODPRE'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.abdominal.Checked)
            {
                try
                {
                    cmtText = "Abdominal or intestinal problem";
                    cmt5Text = "31";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ABDOMINA','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ABDOMINA'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.kidney.Checked)
            {
                try
                {
                    cmtText = "Sugar, protein, kidney problem";
                    cmt5Text = "32";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','KIDNEY  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'KIDNEY  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.seizure.Checked)
            {
                try
                {
                    cmtText = "Seizure";
                    cmt5Text = "33";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','SEIZURE ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'STD     '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.endocrine.Checked)
            {
                try
                {
                    cmtText = "Diabetes";
                    cmt5Text = "34";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ENDOCRIN','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ENDOCRIN'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.thyroid.Checked)
            {
                try
                {
                    cmtText = "Thyroid";
                    cmt5Text = "35";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC, COMMENTS5,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','THYROID ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ANEMIA  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.bonejoint.Checked)
            {
                try
                {
                    cmtText = "Bone, joint, muscle problem";
                    cmt5Text = "36";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BONEJOIN','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'BONEJOIN'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.asthma.Checked)
            {
                try
                {
                    cmtText = "Hay fever, asthma, other allergy";
                    cmt5Text = "37";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                       + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                       + "VALUES('" + session + "', '" + idnum + "','ASTHMA  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ASTHMA  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.acne.Checked)
            {
                try
                {
                    cmtText = "Severe acne, other skin disorder";
                    cmt5Text = "38";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ACNE    ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ACNE    '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.cancer.Checked)
            {
                try
                {
                    cmtText = "Cancer or other tumor";
                    cmt5Text = "39";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','CANCER  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'CANCER  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.depression.Checked)
            {
                try
                {
                    cmtText = "Depression";
                    cmt5Text = "40";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DEPRESS ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'DEPRESS '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.anxiety.Checked)
            {
                try
                {
                    cmtText = "Anxiety disorder";
                    cmt5Text = "41";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ANXIETY ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ANXIETY '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.bipolar.Checked)
            {
                try
                {
                    cmtText = "Bipolar disorder";
                    cmt5Text = "42";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BIPOLAR ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'BIPOLAR '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.obsessive.Checked)
            {
                try
                {
                    cmtText = "Obsessive-compulsive disorder";
                    cmt5Text = "43";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','OBSESSIV','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'OBSESSIV'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.pstd.Checked)
            {
                try
                {
                    cmtText = "Post traumatic";
                    cmt5Text = "44";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','PSTD    ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'PSTD    '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.adhd.Checked)
            {
                try
                {
                    cmtText = "ADD/ADHD";
                    cmt5Text = "45";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ADHD    ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ADHD    '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.suicidal.Checked)
            {
                try
                {
                    cmtText = "Suicidal ideation";
                    cmt5Text = "46";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','SUICIDAL','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'SUICIDAL'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.eating.Checked)
            {
                try
                {
                    cmtText = "Eating disorder";
                    cmt5Text = "47";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','EATING  ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'EATING  '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.anger.Checked)
            {
                try
                {
                    cmtText = "Anger management disorder";
                    cmt5Text = "48";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ANGER   ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ANGER   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.suicideAtt.Checked)
            {
                try
                {
                    cmtText = "Suicide attempt";
                    cmt5Text = "49";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','SUICATT ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'SUICATT '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.panic.Checked)
            {
                try
                {
                    cmtText = "Panic disorder";
                    cmt5Text = "50";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','PANIC   ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'PANIC   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.sleep.Checked)
            {
                try
                {
                    cmtText = "Sleep disorder";
                    cmt5Text = "51";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','SLEEP   ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'SLEEP   '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.conduct.Checked)
            {
                try
                {
                    cmtText = "Anti-social or conduct disorder";
                    cmt5Text = "52";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','CONDUCT ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'CONDUCT '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.alcohol.Checked)
            {
                try
                {
                    cmtText = "Alcohol or substance abuse";
                    cmt5Text = "53";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ALCOHOL ','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'ALCOHOL '";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            if (this.medication.Checked)
            {
                try
                {
                    cmtText = "Have taken medications for mental";
                    cmt5Text = "54";
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS5, USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MEDMENTA','" + cmtText + "','" + cmt5Text + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    sqlUpdate = "";
                }
            }
            else
            {
                sqlDelete = "delete from [dbo].[CUST_STUD_HLTH_PROFILE] "
                            + "where id_num = '" + idnum + "' and health_cde = 'MEDMENTA'";
                SqlCommand sqlStmt = new SqlCommand(sqlDelete, sqlconUp);
                sqlStmt.ExecuteNonQuery();
            }
            //   Text box entries
            if (!(this.medications.Text == ""))
            {
                try
                {
                    pageItem = "Current meds: ";
                    cmtText = this.medications.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MEDICATI','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Current meds: ";
                    cmtText = this.medications.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MEDICATI'";

                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (!(this.bloodtype.Text == ""))
            {
                try
                {
                    pageItem = "Blood Type: ";
                    cmtText = this.bloodtype.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','BLOODTYP','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Blood Type: ";
                    cmtText = this.bloodtype.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'BLOODTYP'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (!(this.medallergies.Text == ""))
            {
                try
                {
                    pageItem = "Med Allergies : ";
                    cmtText = this.medallergies.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MEDALLER','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Med Allergies : ";
                    cmtText = this.medallergies.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MEDALLER'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (!(this.otherdisorder.Text == ""))
            {
                try
                {
                    pageItem = "Other Disorders : ";
                    cmtText = this.otherdisorder.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','OTHDISOR','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Other Disorders : ";
                    cmtText = this.otherdisorder.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'OTHDISOR'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (!(this.disordermeds.Text == ""))
            {
                try
                {
                    pageItem = "Mental meds: ";
                    cmtText = this.disordermeds.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','MENTMEDS','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Mental meds: ";
                    cmtText = this.disordermeds.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'MENTMEDS'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }

            if (!(this.foreignloc.Text == ""))
            {
                try
                {
                    pageItem = "Foreign Local: ";
                    cmtText = this.foreignloc.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','FORLOCAL','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Foreign Local: ";
                    cmtText = this.foreignloc.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'FORLOCAL'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }


            }
            if (!(this.foreigntime.Text == ""))
            {
                try
                {
                    pageItem = "Foreign Time: ";
                    cmtText = this.foreigntime.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','FORTIME ','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Foreign Time: ";
                    cmtText = this.foreigntime.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'FORTIME '";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();

                }
            }



            if (this.psyther.Text == "Yes")
            {
                try
                {
                    pageItem = "Will continue psychotherapy";
                    cmtText = this.psytherTxt.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','PSYTHER','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Will continue psychotherapy";
                    cmtText = this.psytherTxt.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'PSYTHER%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (this.psyhosp.Text == "Yes")
            {
                try
                {
                    pageItem = "Have been hospitalized, Psychiatric";
                    cmtText = this.psyhospdtl.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','PSYHOSP','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Have been hospitalized, Psychiatric";
                    cmtText = this.psyhospdtl.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'PSYHOSP%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (this.addict.Text == "Yes")
            {
                try
                {
                    pageItem = "Treated for alcohol/substance ";
                    cmtText = this.addictdates.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','ADDICT','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Treated for alcohol/substance ";
                    cmtText = this.addictdates.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'ADDICT%'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }

            }
            if (!(this.disability1.Text == ""))
            {
                try
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability1.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DISABIL1','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability1.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'DISABIL1'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }
            if (!(this.disability2.Text == ""))
            {
                try
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability2.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DISABIL2','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Disability Diagnosis : ";
                    cmtText = this.disability2.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'DISABIL2'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }
            if (!(this.disability3.Text == ""))
            {
                try
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability3.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DISABIL3','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Type accommodations : ";
                    cmtText = this.disability3.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'DISABIL3'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }
            if (!(this.disability4.Text == ""))
            {
                try
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability4.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DISABIL4','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Accommodations requested : ";
                    cmtText = this.disability4.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'DISABIL4'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }
            if (!(this.disability5.Text == ""))
            {
                try
                {
                    pageItem = "Nature of disability : ";
                    cmtText = this.disability5.Text;
                    sqlInsert = "INSERT INTO [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "(SESS_CDE,ID_NUM, HEALTH_CDE,HEALTH_CDE_DESC,COMMENTS1,USER_NAME,JOB_NAME,JOB_TIME) "
                           + "VALUES('" + session + "', '" + idnum + "','DISABIL5','" + pageItem + "','" + cmtText + "','" + usernm + "'," + "'HltFrmPort'" + ",'" + curtime + "')";
                    SqlCommand sqlStmt = new SqlCommand(sqlInsert, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
                catch
                {
                    pageItem = "Anticipate New accommodations : ";
                    cmtText = this.disability5.Text;
                    sqlUpdate = "UPDATE [dbo].[CUST_STUD_HLTH_PROFILE] "
                           + "SET HEALTH_CDE_DESC = '" + pageItem + "' "
                           + ", COMMENTS1 = '" + cmtText + "' "
                           + ", COMMENTS5 = '1'"
                           + ", JOB_TIME = '" + curtime + "' "
                           + "WHERE SESS_CDE like 'HEALTH%' and ID_NUM = '" + idnum + "' and HEALTH_CDE like 'DISABIL5'";
                    SqlCommand sqlStmt = new SqlCommand(sqlUpdate, sqlconUp);
                    sqlStmt.ExecuteNonQuery();
                }
            }
            lblComplete.Text = "Thank you for completing the health form";
            this.ParentPortlet.NextScreen("Review");


            // end isvalid if
            //   }
            //   else
            //   {
            //       lblComplete.Text = "Please correct any errors and resubmit form";
            //       this.ParentPortlet.NextScreen("HlthFrmUpdt");
            //   }

            //  end ispostback if
            //}


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
