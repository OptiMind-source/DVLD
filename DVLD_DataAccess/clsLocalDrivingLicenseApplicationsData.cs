using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationsData
    {

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM LocalDrivingLicenseApplications_View";
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
     

                    }
                    catch(Exception ex)
                    {

                    
                    }
                   return dataTable; 
                }
            }
        }

        public static int AddNewLocalDrivingLicenseApplication(
            int LicenseClassID,int ApplicationID)
        {
            int LocalApplicationID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO LocalDrivingLicenseApplications
                         (ApplicationID, LicenseClassID)
                         VALUES (@ApplicationID, @LicenseClassID);
                         SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LocalApplicationID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: Log the exception or rethrow it
                    }
                }
            }

            return LocalApplicationID;
        }

    }
}
