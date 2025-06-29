using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsLocalDrivingLicenseApplications : clsApplication
    {
        enum enMode { New ,Update};

        enMode _Mode;
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int LicenseClassID { get; set; }

        static public DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            if (base.Save())
            {
                this.LocalDrivingLicenseApplicationID = -1;
                this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplication(this.LicenseClassID, this.ApplicationID);
            }
            return this.LocalDrivingLicenseApplicationID != -1;
        }

        public clsLocalDrivingLicenseApplications():base()
        {
            this.LicenseClassID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            _Mode=enMode.New;
        }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID, int ApplicantID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
         : base (ApplicationID,ApplicantID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
        {
            this.LicenseClassID = LicenseClassID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID ;

            _Mode = enMode.New;
        }

        new public bool Save()
        {
            switch (_Mode)
            {
                case enMode.New:
                    {
                        _Mode = enMode.Update;
                        return _AddNewLocalDrivingLicenseApplication();
                    }
            }
            return false;
        }
        // build the constructors both private and public then build save 
    }
}
