<%@ Control Language="c#" AutoEventWireup="false" Codebehind="HlthFrm_View.ascx.cs" Inherits="CUS.ICS.HealthForm.HlthFrm_View" TargetSchema=""%>
<%@ Register TagPrefix="common" Assembly="Jenzabar.Common" Namespace="Jenzabar.Common.Web.UI.Controls" %>
<script language="javascript" type="text/javascript">
// <!CDATA[

function obsessive_onclick() {

}

// ]]>
</script>
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
    
    .style2
    {
        height: 118px;
    }
    
    .style3
    {
        text-align: left;
    }
        
</style>

<div class="pSection">
	<div class="pContent">
	


		<table id="table1" style="border: black 0px solid; width:600px; background-color:#eeeeee"
		cellspacing="1" cellpadding="3"> 
			<tr>
				<th colspan="3">
					<p class="left">TLU ID&nbsp;&nbsp;
						<asp:TextBox id="tluid" runat="server"></asp:TextBox></p>
						
				</th>
				<td colspan="3">
				<asp:ValidationSummary id="ValidationSummary1" runat="server"
                        HeaderText="<b>Errors were found and are identified in red<br /> on the form (scroll down to correct).</b>"
                        ShowSummary="True"
                        DisplayMode="List" /><br />
				<asp:label id="lblComplete" Runat="server" ForeColor="Red" BackColor="Yellow" />
				</td>
			</tr>
			<tr>
				<th class="style1">
					Last Name</th>
				<td>
					<ASP:TEXTBOX id="lname" ReadOnly="true" runat="server" Width="100"></ASP:TEXTBOX>
				</ br>	
				</td>
				<th>
					First Name</th>
				<td>
					<ASP:TEXTBOX id="fname" ReadOnly="True" runat="server" Width="100"></ASP:TEXTBOX></td>
				<th>
					Middle Name</th>
				<td>
					<ASP:TEXTBOX id="mname" ReadOnly="True" runat="server" Width="104px"></ASP:TEXTBOX></td>
			</tr>
			<tr>
				<th>
					Phone(H)</th>
				<td>
					<ASP:TEXTBOX id="hphone" ReadOnly="True" runat="server" Width="100"></ASP:TEXTBOX></td>
				<th>
					Phone(Cell)</th>
				<td>
					<ASP:TEXTBOX id="cphone" ReadOnly="True" runat="server" Width="100"></ASP:TEXTBOX></td>
				<th>
					Email</th>
				<td>
					<ASP:TEXTBOX id="email" ReadOnly="True" runat="server" Width="104px" MaxLength="2" 
                        Height="22px"></ASP:TEXTBOX></td>
			</tr>
			<tr>
				<td colspan="6">
					<hr/>
				</td>
			</tr>
			<tr>
				<th colspan="6">
					<ASP:CHECKBOX id="CheckBox4" runat="server"></ASP:CHECKBOX>(required if student 
					will be under 18 years old at time of enrollment)&nbsp; With the understanding 
					that every effort will be made to contact me in case of medical emergency, I 
					hereby give my permission to the physician selected by the University to 
					hospitalize, secure proper treatment for, and order injections, anesthesia, or 
					surgery for my daughter/son submitting this medical report.</th>
			</tr>
			<tr>
				<th>
					Parent/Guardian name</th>
				<td colspan="3"><ASP:TEXTBOX id="parentgd" runat="server" Width="300"></ASP:TEXTBOX><br />
                <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator13" runat="SERVER" 
                        ControlToValidate="parentgd" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:''-.'\s]+$">
                    </asp:RegularExpressionValidator>
                </td>
				<th>
					Phone<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="parentgdPh" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator15" runat="SERVER" 
                        ControlToValidate="parentgdPh" 
                        ErrorMessage="Ph # include Area Cd"
                        ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<th class="style1">
					City<br />&nbsp;</th>
				<td style="width:124px"><ASP:TEXTBOX id="parentgdCty" runat="server" width="100px"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator14" runat="SERVER" 
                        ControlToValidate="parentgdCty" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:'.\s]{1,40}$">
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					State<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="parentgdSt" runat="server"  width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator16" runat="SERVER" 
                        ControlToValidate="parentgdSt" 
                        ErrorMessage="2 letter code"
                        ValidationExpression="^[a-zA-Z?!;:'.\s]{1,2}$">
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					Zip<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="parentgdZip" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator17" runat="SERVER" 
                        ControlToValidate="parentgdZip" 
                        ErrorMessage="Invalid Zip"
                        ValidationExpression="^(\d{5}-\d{4}|\d{5}|\d{9})$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td colspan="6">
					<hr/>
				</td>
			</tr>
	
	
			<tr>
				<th colspan="6">
					<p><strong>Please check any of the following that have been experienced by 
							close relatives:</strong></p>
				</th>
			</tr>
			<tr>
				<td colspan="3">
					<ASP:CHECKBOX id="hiBlPressRel" runat="server"></ASP:CHECKBOX>&nbsp; Hi Blood Press<br />
					<ASP:CHECKBOX id="hrtdisRel" runat="server"></ASP:CHECKBOX>&nbsp; Heart Disease<br />
					<ASP:CHECKBOX id="strokeRel" runat="server"></ASP:CHECKBOX>&nbsp; Stroke<br />
					<ASP:CHECKBOX id="bldDisorderRel" runat="server"></ASP:CHECKBOX>&nbsp; Bleeding disorder<br />
					<ASP:CHECKBOX id="diabetesRel" runat="server"></ASP:CHECKBOX>&nbsp; Diabetes<br />
					<ASP:CHECKBOX id="ulcersRel" runat="server"></ASP:CHECKBOX>&nbsp; Ulcers<br />
					<ASP:CHECKBOX id="kidneyRel" runat="server"></ASP:CHECKBOX>&nbsp; Kidney Disease<br />
					<ASP:CHECKBOX id="epilepsyRel" runat="server"></ASP:CHECKBOX>&nbsp; Epilepsy<br />
					
				</td>
				<td colspan="3">
					<br />
					<ASP:CHECKBOX id="migraineRel" runat="server"></ASP:CHECKBOX>Migraine<br />
					<ASP:CHECKBOX id="arthritisRel" runat="server"></ASP:CHECKBOX>Arthritis<br />
					<ASP:CHECKBOX id="cancerRel" runat="server"></ASP:CHECKBOX>Cancer<br />
					<ASP:CHECKBOX id="tbRel" runat="server"></ASP:CHECKBOX>Tuberculosis<br />
					<ASP:CHECKBOX id="asthmaRel" runat="server"></ASP:CHECKBOX>Asthma<br />
					<ASP:CHECKBOX id="allergiesRel" runat="server"></ASP:CHECKBOX>Allergies<br />
					<ASP:CHECKBOX id="mentalRel" runat="server"></ASP:CHECKBOX>Mental Illness<br />
&nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                </td>
			</tr>
			
			
			<tr>
				<th colspan="6">
					<hr/>
				</th>
			</tr>
           <!--
			<tr>
				<th colspan="6">
					<p class="style3">
                        <b>All Students:</b>&nbsp; Record dates of 
                        immunizations in spaces provided.&nbsp; All students 
                        must fill this out or mail a copy of the shot record to TLU Office of Student 
                        Life and Learning, 1000 W. Court, Seguin, Tx 78155 or fax to 830-372-6412.&nbsp; <br />
					</p>
				</th>
			</tr>
            -->
			<tr>
				<th colspan="6">
					<p>
                        <b>ALL NEW STUDENTS, including transfers:</b>&nbsp; must submit proof of vaccination against 
                        bacterial meningitis.  Mail your shot record to TLU Office of Enrollment Services, 1000 W Court, 
                        Seguin, TX 78155 or fax it to 830-372-8096.  The vaccination must have been received less than 5 
                        years from the first day of the semester for which you are enrolling and at least 10 days before 
                        the first day of the semester.  This requirement is dictated by the Texas Education Code which 
                        regulates the requirement (and exceptions) for bacterial meningitis vaccination. </p>
				</th>
			</tr>
			<tr>
				<th colspan="6">
					<p>
						&nbsp;</p>
                    <p>
						If your are sending by mail or fax a complete record of 
                        immunizations, including the meningitis vaccination, indicate here: &nbsp;
                        <ASP:CHECKBOX id="MailImmunFrm" runat="server"></ASP:CHECKBOX>
					</p>
				</th>
			</tr>
			<tr>
				<th colspan="6">
					<hr/>
				</th>
			</tr>
            <!--
			<tr>
				<th colspan="2">
					Tetanus/Diptheria(within 10 yr)</th>
				<th colspan="2">
					Meningitis</th>
				<th colspan="2">
					Polio (if under 18)</th>
			</tr>
			<tr>
				<th class="style1">
					Date:<br />(mm/dd/yyyy)<br />&nbsp;
				</th>
				 
				<td style="width:124px">
					<ASP:TEXTBOX id="tetdipDt" runat="server" WIDth ="100"></ASP:TEXTBOX>
					<asp:RegularExpressionValidator         
                        id="tetdip_validation" runat="SERVER" 
                        ControlToValidate="tetdipDt" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>

					</td>
				<th>
					Date:<br />(mm/dd/yyyy)<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="meningitisDt" runat="server" width="100"></ASP:TEXTBOX>
				    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator1" runat="SERVER" 
                        ControlToValidate="meningitisDt" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					Date:<br />(mm/dd/yyyy)<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="polio" runat="server" width="100"></ASP:TEXTBOX>
				    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator2" runat="SERVER" 
                        ControlToValidate="polio" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<th colspan="6">
					<hr/>
				</th>
			</tr>
			<tr>
				<th colspan="6">
					Measles, Mumps, Rubella</th>
				<th colspan="2">
					&nbsp;</th>
			</tr>
			<tr>
				<th colspan="2" style="text-decoration:underline">
					If born before 01/01/1957</th>
				<th colspan="2" style="text-decoration:underline">
					If born after 01/01/1957</th>
				<th colspan="2" style="text-decoration:underline">
					Hepatitis B:</th>
			</tr>
			<tr>
				<th class="style1">
					Measles Date 1<br />&nbsp;
				</th>
				<td style="width:124px"><ASP:TEXTBOX id="measlesDt1" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator3" runat="SERVER" 
                        ControlToValidate="measlesDt1" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					MMR Date 1<br />&nbsp;
				</th>
				<td><ASP:TEXTBOX id="Imunmmr1" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator4" runat="SERVER" 
                        ControlToValidate="Imunmmr1" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					Hep Date 1<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="Imunhep1" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator5" runat="SERVER" 
                        ControlToValidate="Imunhep1" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<th class="style1">
					Measles Date 2<br />&nbsp;
				</th>
				<td style="width:124px"><ASP:TEXTBOX id="measlesDt2" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator6" runat="SERVER" 
                        ControlToValidate="measlesDt2" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					MMR Date 2<br />&nbsp;
				</th>
				<td><ASP:TEXTBOX id="Imunmmr2" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator7" runat="SERVER" 
                        ControlToValidate="Imunmmr2" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					Hep Date 2<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="Imunhep2" runat="server" width="100"></ASP:TEXTBOX></td>
			</tr>
			<tr>
				<th class="style1">
					Mumps<br />&nbsp;
				</th>
				<td style="width:124px"><ASP:TEXTBOX id="Imunmumps" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator8" runat="SERVER" 
                        ControlToValidate="Imunmumps" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					&nbsp;
				</th>
				<td>&nbsp;
				</td>
				<th>
					Hep Date 3<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="Imunhep3" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator9" runat="SERVER" 
                        ControlToValidate="Imunhep3" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<th class="style1">
					Rubella<br />&nbsp;
				</th>
				<td style="width:124px"><ASP:TEXTBOX id="Imunrubella" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator10" runat="SERVER" 
                        ControlToValidate="Imunrubella" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
				<th>
					
					&nbsp;
				    
				</th>
				<td>&nbsp;
				</td>
				<th>
					&nbsp;</th>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<th colspan="6">
					<hr/>
				</th>
			</tr>
			<tr>
				<th class="style1">
					Last TB Test: Date<br />&nbsp;</th>
				<td style="width:124px"><ASP:TEXTBOX id="tbtestdt" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator11" runat="SERVER" 
                        ControlToValidate="tbtestdt" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
                </td>
				<td>
				    <asp:RadioButtonList ID="TB" runat="server">
					    <ASP:listitem selected="True" value="Neg">TB Neg</ASP:listitem>
					    <ASP:listitem value="Pos">TB Pos</ASP:listitem>
				    </asp:RadioButtonList>
				</td>
				<th colspan="2">
					If positive, chest X-Ray date:<br />&nbsp;</th>
				<td><ASP:TEXTBOX id="chestxray" runat="server" width="100"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator12" runat="SERVER" 
                        ControlToValidate="chestxray" 
                        ErrorMessage="Invalid date"
                        ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$" >
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			
			<tr>
				<td colspan="6">
					<hr/>
				</td>
			</tr>
            -->

			<tr>
				<th colspan="6">
					<p class="left"><strong>MEDICAL HISTORY&nbsp; (check all that you&#39;ve experienced)</strong></p>
				</th>
			</tr>
			<tr>
				<td colspan="3">
				    <ASP:CHECKBOX id="rubeola" runat="server"></ASP:CHECKBOX>&nbsp; Measles<br />
				    <ASP:CHECKBOX id="mumps" runat="server"></ASP:CHECKBOX>&nbsp; Mumps<br />
				    <ASP:CHECKBOX id="rubella" runat="server"></ASP:CHECKBOX>&nbsp; Rubella<br />
				    <ASP:CHECKBOX id="chickenpox" runat="server"></ASP:CHECKBOX>&nbsp; Chicken Pox<br />
				    <ASP:CHECKBOX id="asthma" runat="server"></ASP:CHECKBOX>&nbsp; Asthma<br />    
				    <ASP:CHECKBOX id="acne" runat="server"></ASP:CHECKBOX>&nbsp; Acne/eczema<br />
				    <ASP:CHECKBOX id="allergyother" runat="server"></ASP:CHECKBOX>&nbsp; Allergic to food/insect<br />
				    <ASP:CHECKBOX id="kidney" runat="server"></ASP:CHECKBOX>&nbsp; Bladder/kidney disease<br />
				    <ASP:CHECKBOX id="bonejoint" runat="server"></ASP:CHECKBOX>&nbsp; Bone/joint/muscle problem<br />
                    <ASP:CHECKBOX id="cancer" runat="server"></ASP:CHECKBOX>&nbsp; Cancer<br />
                    <ASP:CHECKBOX id="concussion" runat="server"></ASP:CHECKBOX>&nbsp;Concussion<br />
  			
                </td>
                 <td colspan="3">
                    <ASP:CHECKBOX id="endocrine" runat="server"></ASP:CHECKBOX>&nbsp;Diabetes<br />
                    <ASP:CHECKBOX id="hospital" runat="server"></ASP:CHECKBOX>&nbsp; Hospitalization/surgery<br />
					<ASP:CHECKBOX id="bloodpress" runat="server"></ASP:CHECKBOX>&nbsp; High blood pressure/heart disease<br />
					<ASP:CHECKBOX id="abdominal" runat="server"></ASP:CHECKBOX>&nbsp; Hepatitis<br />
                    <ASP:CHECKBOX id="entdisease" runat="server"></ASP:CHECKBOX>&nbsp; Hearing aid/cochlear implant<br />
                    <ASP:CHECKBOX id="mono" runat="server"></ASP:CHECKBOX>&nbsp; Mono<br />
					<ASP:CHECKBOX id="seizure" runat="server"></ASP:CHECKBOX>&nbsp; Seizure disorder<br />
                    <ASP:CHECKBOX id="seasaller" runat="server"></ASP:CHECKBOX>&nbsp; Seasonal allergies<br />
                    <ASP:CHECKBOX id="thyroid" runat="server"></ASP:CHECKBOX>&nbsp; Thyroid disease<br />
					<ASP:CHECKBOX id="vision" runat="server"></ASP:CHECKBOX>&nbsp; Vision Problems/glasses or contacts<br />
					<ASP:CHECKBOX id="digestive" runat="server"></ASP:CHECKBOX>&nbsp; Disease of the digestive system<br />
					
					
                 </td>
			</tr>
			<tr>
				<td colspan="6">
				Other/or explanation of any of the above:&nbsp;
					<br /><ASP:TEXTBOX id="otherdisorder" runat="server" width="466"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator19" runat="SERVER" 
                        ControlToValidate="otherdisorder" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:'/.\s]{1,40}$">
                    </asp:RegularExpressionValidator>
					</td>
				
			</tr>
			<tr>
				<td colspan="6">
					Medication Allergies (please list)&nbsp;
                   <br />
                    <asp:TextBox ID="medallergies" runat="server" Width="466"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator21" runat="SERVER" 
                        ControlToValidate="medallergies" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:'/,.\s]{1,45}$">
                    </asp:RegularExpressionValidator>
                </td>
				
			</tr>
			<tr>
				<td colspan="6">
				Medications you regularly take (please list)<br />
				<ASP:TEXTBOX id="medications" runat="server" width="466"></ASP:TEXTBOX>
				<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator18" runat="SERVER" 
                        ControlToValidate="medications" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:'/.\s]{1,45}$">
                </asp:RegularExpressionValidator>
				
				</td>	
			</tr>
			<tr>
			<td colspan="6">
					 Blood Type (enter below):&nbsp;
					<br /><ASP:TEXTBOX id="bloodtype" runat="server" width="100"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator20" runat="SERVER" 
                        ControlToValidate="bloodtype" 
                        ErrorMessage="Blood Type"
                        ValidationExpression="^[a-zA-Z'+-.\s]{1,5}$">
                    </asp:RegularExpressionValidator>
					</td>
			
			</tr>
			<tr>
				<td colspan="6">
					<hr/>
				</td>
			</tr>
			<tr>
				<th colspan="6">
					<p class="left"><strong>Tuberculosis(TB) Screening Questions:&nbsp;</strong></p>
				</th>
			</tr>
			<tr>
			    <td colspan="3">
			        Have you ever had a positive TB skin test?<br />
			        Have you ever been vaccinated with BCG?<br />
			    </td>
			    <td colspan="3">
			        <ASP:CHECKBOX id="tuberculosis" runat="server"></ASP:CHECKBOX>&nbsp; Yes<br />
					<ASP:CHECKBOX id="bcgvaccine" runat="server"></ASP:CHECKBOX>&nbsp; Yes<br />
			    </td>
			</tr>
			<tr>
			    <td colspan="6">
			        Have you ever lived or visited outside of the US?
			    </td>
			</tr>
			<tr>
			    <td colspan="3">
			        Where?<br />
			        <asp:TextBox ID="foreignloc" runat="server" Width="260px"></asp:TextBox>
			    </td>
			    <td colspan="3">
			        For How Long?<br />
			        <asp:TextBox ID="foreigntime" runat="server" Width="246px"></asp:TextBox>
			    </td>
			</tr>
			<tr>
				<td colspan="6">
					<hr/>
				  
					
				</td>
			</tr>
			<tr>
				<td colspan="6">
					<b> MENTAL HEALTH HISTORY</b>
				</td>
			</tr>
			
			<tr>
				<th colspan="6">
					<p class="left"><strong>Please check any of the following that you have experienced 
							during the past four years:</strong></p>
				</th>
			</tr>
			<tr>
				<td colspan="3">
					<ASP:CHECKBOX id="depression" runat="server"></ASP:CHECKBOX>&nbsp; Depression<br />
					<ASP:CHECKBOX id="anxiety" runat="server"></ASP:CHECKBOX>&nbsp; An anxiety disorder<br />
					<ASP:CHECKBOX id="bipolar" runat="server"></ASP:CHECKBOX>&nbsp; Bipolar disorder
                    <br />
					<ASP:CHECKBOX id="obsessive" runat="server"></ASP:CHECKBOX>&nbsp; Obsessive-compulsive disorder<br />
					<ASP:CHECKBOX id="pstd" runat="server"></ASP:CHECKBOX>&nbsp; Post traumatic stress disorder(PStd)<br />
					<ASP:CHECKBOX id="adhd" runat="server"></ASP:CHECKBOX>&nbsp; ADD/ADHD<br />
					<ASP:CHECKBOX id="suicidal" runat="server"></ASP:CHECKBOX>&nbsp; Suicidal ideation</td>
				
				<td colspan="3">
					<ASP:CHECKBOX id="eating" runat="server"></ASP:CHECKBOX>&nbsp; An eating disorder<br />
					<ASP:CHECKBOX id="anger" runat="server"></ASP:CHECKBOX>&nbsp; An anger management disorder<br />
					<ASP:CHECKBOX id="suicideAtt" runat="server"></ASP:CHECKBOX>&nbsp; A suicide attempt<br />
					<ASP:CHECKBOX id="panic" runat="server"></ASP:CHECKBOX>&nbsp; Panic disorder<br />
					<ASP:CHECKBOX id="sleep" runat="server"></ASP:CHECKBOX>&nbsp; A sleep disorder<br />
					<ASP:CHECKBOX id="conduct" runat="server"></ASP:CHECKBOX>&nbsp; An anti-social or conduct disorder<br />
					<ASP:CHECKBOX id="alcohol" runat="server"></ASP:CHECKBOX>&nbsp; Alcohol or substance abuse or dependence</td>
			</tr>
			<tr>
				<td colspan="6">
					<ASP:CHECKBOX id="medication" runat="server"></ASP:CHECKBOX>Are you taking or have you ever taken medication for any of the 
					above?</td>
				
			</tr>
			<tr>
				<td colspan="6">
					
					
				    Describe any medical or mental health problems or conditions that have required 
                    psychological care.<br />
					(specify medication and dates for any above)<br />
					<ASP:TEXTBOX id="disordermeds" runat="server" Width="600"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator22" runat="SERVER" 
                        ControlToValidate="disordermeds" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:',.\s]{1,90}$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td colspan="6">
					<hr/>
				  
					
				</td>
			</tr>
			<tr>
				<td colspan="2">Do you intend to begin or continue psychotherapy during college?<br />
					<asp:RadioButtonList ID="psyther" runat="server">
					    <ASP:listitem selected="True" value="No">No</ASP:listitem>
					    <ASP:listitem value="Yes">Yes</ASP:listitem>
				    </asp:RadioButtonList>
				</td>
				<td colspan="4">
					If yes, please explain:<br />
					<ASP:TEXTBOX id="psytherTxt" runat="server" Width="400"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator23" runat="SERVER" 
                        ControlToValidate="psytherTxt" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z?!;:',.\s]{1,45}$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td colspan="2">Have you been hospitalized for a psychiatric disorder?<br />
					<asp:RadioButtonList ID="psyhosp" runat="server">
					    <ASP:listitem selected="True" value="No">No</ASP:listitem>
					    <ASP:listitem value="Yes">Yes</ASP:listitem>
				    </asp:RadioButtonList>
				</td>
				<td colspan="4">
					If yes, please specify dates and details:<br />
					<ASP:TEXTBOX id="psyhospdtl" runat="server" Width="400"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator24" runat="SERVER" 
                        ControlToValidate="psyhospdtl" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9/?!;:',.\s]{1,45}$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td colspan="2">Have you been treated for alcohol and/or drug addiction?
				</td>
				
			</tr>
			<tr>
				<td colspan="2">
				    <asp:RadioButtonList ID="addict" runat="server">
					    <ASP:listitem selected="True" value="No">No</ASP:listitem>
					    <ASP:listitem value="Yes">Yes</ASP:listitem>
					</asp:RadioButtonList>
				</td>
				<td colspan="5">
					If yes, please specify dates and details:<br />
					<ASP:TEXTBOX id="addictdates" runat="server" Width="400"></ASP:TEXTBOX><br />
					<asp:RegularExpressionValidator         
                        id="RegularExpressionValidator25" runat="SERVER" 
                        ControlToValidate="addictdates" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9/?!;,:'.\s]{1,45}$">
                    </asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td colspan="6">
					<hr />
					<hr />
				
				</td>
			</tr>
			<tr>
				<td colspan="6" class="style2">
				
					<p><b> DISABILITY INQUIRY</b></p>
				    <p>Texas Lutheran University provides reasonable accommodations for students with
				    documented physical/medical, learning, and or psychological disabilities and are
				    initiated in the Office of the TLU ADA Coordinator located in the Meadows Center in the Alumni Student Center.  
				    Accommodations are available upon request to those students entitled to them under
				    Section 504 of the Rehabilitation Act of 1973 and the Americans with Disabilities Act of 1990.
				    <br /><br />
				    If you have a disability, please answer the following questions so that we will have an idea of the services you may
				    need.  By filling out and submitting this form, you will be sent a copy of the University's policy
				    for students with disabilities which will outline the required documentation, process of requesting
				    documentation, and other pertinent information.  For more information, please check our Web site at http://www.tlu.edu.
				        <i>(Please note that filling out this form does not automatically qualify you for accommodations)
                        </i>
				    </p>
				</td>
			</tr>
			<tr>
			    <td colspan="6">
			        What is the nature of you disability?<br />
                    <ASP:TEXTBOX id="disability1" runat="server" width="623"></ASP:TEXTBOX>
                    <br />
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator26" runat="SERVER" 
                        ControlToValidate="disability1" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9?!;:,'/.\s]{1,90}$">
                    </asp:RegularExpressionValidator>   
			    </td>
			</tr>
			<tr>
			    <td colspan="6">
			        How and when was your disability diagnosed and documented?<br />
                    <ASP:TEXTBOX id="disability2" runat="server" width="623"></ASP:TEXTBOX>
                    <br />
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator27" runat="SERVER" 
                        ControlToValidate="disability2" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9?!;,:'/.\s]{1,90}$">
                    </asp:RegularExpressionValidator>   
			    </td>
			</tr>
			<tr>
			    <td colspan="6">
			        What types of accommodations have you used?<br />
                    <ASP:TEXTBOX id="disability3" runat="server" width="623"></ASP:TEXTBOX>
                    <br /> 
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator28" runat="SERVER" 
                        ControlToValidate="disability3" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9?!;,:'/.\s]{1,90}$">
                    </asp:RegularExpressionValidator>  
			    </td>
			</tr>
			<tr>
			    <td colspan="6">
			        What accommodations are you requesting at Texas Lutheran?<br />
                    <ASP:TEXTBOX id="disability4" runat="server" width="623"></ASP:TEXTBOX>
                    <br />
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator29" runat="SERVER" 
                        ControlToValidate="disability4" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9?,!;:'/.\s]{1,90}$">
                    </asp:RegularExpressionValidator>   
			    </td>
			</tr>
			<tr>
			    <td colspan="6">
			        Are there any new accommodations you anticipate requesting?&nbsp; If so, please 
                    specify.<br />
                    <ASP:TEXTBOX id="disability5" runat="server" width="623"></ASP:TEXTBOX>
                    <br />
                    <asp:RegularExpressionValidator         
                        id="RegularExpressionValidator30" runat="SERVER" 
                        ControlToValidate="disability5" 
                        ErrorMessage="No Punctuation"
                        ValidationExpression="^[a-zA-Z0-9?,!;:'/.\s]{1,90}$">
                    </asp:RegularExpressionValidator>   
			    </td>
			</tr>
			<tr>
				
			    <th style="BORDER-BOTTOM: gray 1px solid" align="center" colspan="6">
				<asp:label id="lblError" Runat="server" ForeColor="Red" BackColor="Yellow" />
					<br />
					<ASP:BUTTON OnClick="btnSubmit_Click" id="btnSubmit" runat="server" text="Submit">
					
					</ASP:BUTTON><br />
					<asp:ValidationSummary id="valSummary" runat="server"
                        HeaderText="<b>The following errors were found:</b>"
                        ShowSummary="true"
                        DisplayMode="List" />
                        
					
				</th>
				
			</tr>
		</table>
	</div>
</div>
