using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;
using DVLD_Buisness;
using System.Runtime.Remoting.Messaging;

namespace DVLD.GlobalClasses
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool GetSavedLoginData(ref string UserName , ref string Password)
        {
            try
            {

                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");

                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            UserName = result[0];
                            Password = result[1];
                            return true;
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error Occur " + ex.Message);
                return false;
            }
            return false;
        }
        public static bool SaveRememberMeData(string UserName, string Password)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");

                string savedData = UserName + "#//#" + Password;

                if (UserName == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                File.WriteAllText(filePath, savedData);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            return false;
        }

    }
}
