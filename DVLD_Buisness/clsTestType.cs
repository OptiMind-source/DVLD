using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsTestType
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string Title { set; get; }

        public string Description { set; get; }
        public float Fees { set; get; }

        public clsTestType()

        {
            this.ID = -1;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        public clsTestType(int ID, string ApplicationTypeTitel, string ApplicationTypeDescription, float ApplicationTypeFees)

        {
            this.ID = ID;
            this.Title = ApplicationTypeTitel;
            this.Description = ApplicationTypeDescription;
            this.Fees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddTestType()
        {
            //call DataAccess Layer 

            this.ID = clsTestTypeData.AddNewTestType(this.Title,this.Description, this.Fees);


            return (this.ID != -1);
        }

        private bool _UpdateTestType()
        {
            //call DataAccess Layer 

            return clsTestTypeData.UpdateTestType(this.ID,this.Title,this.Description, this.Fees);
        }

        public static clsTestType Find(int ID)
        {
            string Title = "",Description=""; float Fees = 0;

            if (clsTestTypeData.GetTestTypeInfoByID((int)ID, ref Title,ref Description, ref Fees))

                return new clsTestType(ID, Title,Description, Fees);
            else
                return null;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }

    }
}
