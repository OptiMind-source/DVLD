using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {
        public static DataTable GetAllLicenesClasses()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "select * from LicenseClasses";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }


                    }
                    catch (Exception ex)
                    {


                    }
                    return dataTable;
                }
            }
        }

        public static bool GetLicenseClassByName(ref int LicenseClassID, string LicenseClassName,
            ref string LicenseClassDescription, ref int MinimumAllowAge,
            ref int DefaultValidityLength, ref float ClassFees)
        {
            bool recordFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT LicenseClassID, LicenseClassDescription, MinimumAllowAge, 
                                DefaultValidityLength, ClassFees 
                         FROM LicenseClasses 
                         WHERE LicenseClassName = @LicenseClassName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseClassName", LicenseClassName);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LicenseClassID = (int)reader["LicenseClassID"];
                        LicenseClassDescription = reader["LicenseClassDescription"].ToString();
                        MinimumAllowAge = (int)reader["MinimumAllowAge"];
                        DefaultValidityLength = (int)reader["DefaultValidityLength"];
                        ClassFees = Convert.ToSingle(reader["ClassFees"]);
                        recordFound = true;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle/log exception as needed
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return recordFound;
        }
    }
}
