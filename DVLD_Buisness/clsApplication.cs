using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public class clsApplication
    {
        enum enMode { Update = 0, New = 1  };

        enMode _Mode;

        public int ApplicationID {  get; set; }

        public int ApplicantID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }

        public short ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public float PaidFees { get; set;}
        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            this.ApplicantID = -1;
            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this.CreatedByUserID = -1;

            _Mode = enMode.New;

        }

        protected clsApplication (int ApplicationID ,int ApplicantID ,DateTime ApplicationDate ,int ApplicationTypeID ,short ApplicationStatus ,DateTime LastStatusDate ,float PaidFees ,int CreatedByUserID) {
            this.ApplicantID = ApplicantID;
            this.ApplicationID = ApplicationID;
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.CreatedByUserID = CreatedByUserID;


            _Mode=enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantID,this.ApplicationDate,this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees ,this.CreatedByUserID);
            return this.ApplicationID != -1;

        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID,this.ApplicantID,this.ApplicationDate,this.ApplicationTypeID,this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.New:
                    {
                        _Mode = enMode.Update;
                        return _AddNewApplication();
                    }
                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

    }
}
