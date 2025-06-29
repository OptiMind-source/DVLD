using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        static public  int AddNewApplication(int ApplicantID,DateTime ApplicationDate ,int ApplicationTypeID ,short ApplicationStatus ,DateTime LastStatusDate,float PaidFees, int CreatorUserID)
        {
            int ApplicationID = -1;
            using (SqlConnection connection =new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Applications 
                (ApplicantPersonID, ApplicationDate, ApplicationTypeID, 
                 ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) 
                VALUES 
                (@ApplicantID, @ApplicationDate, @ApplicationTypeID, 
                 @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatorUserID)
                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatorUserID", CreatorUserID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationID = insertedID;
                        }
                    }
                    catch(Exception ex)
                    {
                         
                    }
                    return ApplicationID;

                }
            }

        }

        static public bool UpdateApplication(int ApplicationID, int ApplicantID, DateTime ApplicationDate,
          int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatorUserID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"UPDATE Applications 
                         SET ApplicantPersonID = @ApplicantID,
                             ApplicationDate = @ApplicationDate,
                             ApplicationTypeID = @ApplicationTypeID,
                             ApplicationStatus = @ApplicationStatus,
                             LastStatusDate = @LastStatusDate,
                             PaidFees = @PaidFees,
                             CreatedByUserID = @CreatorUserID
                         WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    command.Parameters.AddWithValue("@ApplicantID", ApplicantID);
                    command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                    command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
                    command.Parameters.AddWithValue("@PaidFees", PaidFees);
                    command.Parameters.AddWithValue("@CreatorUserID", CreatorUserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle exception if needed
                    }

                    return rowsAffected > 0;
                }
            }
        }

        static public bool GetApplicationByApplicationID(
        int ApplicationID,
        ref int ApplicantID,
        ref DateTime ApplicationDate,
        ref int ApplicationTypeID,
        ref short ApplicationStatus,
        ref DateTime LastStatusDate,
        ref float PaidFees,
        ref int CreatorUserID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ApplicantID = Convert.ToInt32(reader["ApplicantPersonID"]);
                                ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                                ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                                ApplicationStatus = Convert.ToInt16(reader["ApplicationStatus"]);
                                LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                                PaidFees = Convert.ToSingle(reader["PaidFees"]);
                                CreatorUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log or handle error
                    }

                    return false;
                }
            }
        }

        static public bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Optionally log or handle exception
                    }

                    return rowsAffected > 0;
                }
            }
        }

    }
}
