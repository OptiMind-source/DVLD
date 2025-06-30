using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness
{
    public  class clsLicenseClass
    {
        enum enMode {New = 0 ,Update = 1 };
        enMode _Mode=enMode.New;

        public int LicenseClassID { get; set; }
        public string LicenseClassName { get; set; }
        public string LicenseClassDescription { get; set; }
        public int MinimumAllowAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }
        public clsLicenseClass()
        {
            LicenseClassID = -1;
            LicenseClassName = string.Empty;
            LicenseClassDescription = string.Empty;
            MinimumAllowAge = 0;
            DefaultValidityLength = 0;
            ClassFees = 0.0f;

            _Mode = enMode.New;
        }
        private clsLicenseClass(int id, string name, string description, int minAge, int validityLength, float fees)
        {
            LicenseClassID = id;
            LicenseClassName = name;
            LicenseClassDescription = description;
            MinimumAllowAge = minAge;
            DefaultValidityLength = validityLength;
            ClassFees = fees;

            _Mode = enMode.Update;
        }
        static public DataTable GetAllLicenesClasses()
        {
            return clsLicenseClassData.GetAllLicenesClasses();
        }
        
      static public  clsLicenseClass Find(string LicenseClassName)
        {
            string  Description = "";
            int Id = -1, Age = -1, Length = -1; float Fees = 0;
            if (clsLicenseClassData.GetLicenseClassByName(ref Id,LicenseClassName ,ref Description,ref Age ,ref Length,ref Fees))
            {
                return new clsLicenseClass(Id,LicenseClassName,Description,Age,Length,Fees);
            }
            return null;
        }

    }
}
