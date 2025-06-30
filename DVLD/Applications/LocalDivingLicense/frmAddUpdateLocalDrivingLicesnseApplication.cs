using DVLD_Buisness;
using DVLD.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_System.Application.Local_Driving_Licesnce
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {

        DataTable _dtLicenseClsses = new DataTable();

        clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            _FillComboBoxWithLicenseClasses();
        }
        private void _ResetDefaultValues()
        {
            ctrlPersonCardWithFilter1.FilterFocus();
            tcApplicationInfo.TabPages[1].Enabled = false;
            btnSave.Enabled = false;
        }

        // [/]
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.SelectedPersonInfo != null)
            {
                // enable and move to next page 
                tcApplicationInfo.TabPages[1].Enabled = true;
                tcApplicationInfo.SelectedIndex = 1;
               
                lblApplicationDate.Text = DateTime.Now.ToString();
                lblApplicationID.Text = "???";
                lblCreatedBy.Text=clsGlobal.CurrentUser.UserName;
                btnSave.Enabled = true;

            }
            else
            {
                MessageBox.Show("Missing Info");
            }
        }

        private void _FillComboBoxWithLicenseClasses()
        {

            // fill the combobox with LicenseClasses
            _dtLicenseClsses = clsLicenseClass.GetAllLicenesClasses();
            for (short i = 0; i < _dtLicenseClsses.Rows.Count; i++)
            {
                cbLicenseClass.Items.Add(_dtLicenseClsses.Rows[i]["ClassName"]);

            }
            cbLicenseClass.SelectedIndex = 0;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {

                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                LocalDrivingLicenseApplications=new clsLocalDrivingLicenseApplications();

                LocalDrivingLicenseApplications.ApplicantID = ctrlPersonCardWithFilter1.PersonID;
                LocalDrivingLicenseApplications.ApplicationDate = DateTime.Now;
                LocalDrivingLicenseApplications.ApplicationTypeID = 1;
                LocalDrivingLicenseApplications.ApplicationStatus = 1;
                LocalDrivingLicenseApplications.LastStatusDate= DateTime.Now;
                LocalDrivingLicenseApplications.PaidFees = 15;
                LocalDrivingLicenseApplications.CreatedByUserID=clsGlobal.CurrentUser.UserID;
                //LocalDrivingLicenseApplications.LicenseClassID =
                MessageBox.Show("Saved Successfully");
            }
        }
        
    }
}
