using DVLD.GlobalClasses;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void pbLoginClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
               
                errorProvider1.SetError(txtUserName,"UserName can not be empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName,"");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "UserName can not be empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim() ,txtPassword.Text.Trim());
            if (User != null)
            {
                if (chkRememberMe.Checked)
                { 
                    clsGlobal.SaveRememberMeData(User.UserName, User.Password);
                }
                else
                    clsGlobal.SaveRememberMeData("","");
                if (!User.IsActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Inactive Username.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = User;
                frmMain frm=new frmMain(this);
                this.Hide();
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            string UserName = "", Password = "";
            if(clsGlobal.GetSavedLoginData(ref UserName,ref Password))
            {
                txtPassword.Text=Password;
                txtUserName.Text=UserName;
                chkRememberMe.Checked=true;
            }
            else
                chkRememberMe.Checked = false;
        }
    }
}
