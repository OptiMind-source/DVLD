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

namespace DVLD_System.User.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID=-1;
        private clsUser _User;
        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public int UserID
        {
            get { return _UserID; }
        }
        private void _ResetPersonInfo()
        {
           
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            ctrlPersonCard1.ResetPersonInfo();
            lbUserID.Text = "[???]";
            lbUserName.Text = "[???]";
            lbIsActive.Text = "[???]";
        }
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
            if (_User == null) 
            { 
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
            

        }
        private void _FillUserInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lbUserID.Text = _User.UserID.ToString();
            lbUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

        }

    }
}
// [/]