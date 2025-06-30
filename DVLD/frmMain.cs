using DVLD.GlobalClasses;
using DVLD_System.Application.Local_Driving_Licesnce;
using DVLD_System.People;
using DVLD_System.User;
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
    public partial class frmMain : Form
    {
        private Form _LoginForm;
        public frmMain(Form frm)
        {
            InitializeComponent();
            _LoginForm = frm;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void drivingApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();  
            frm.ShowDialog();
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void sdsdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }
    }
}
