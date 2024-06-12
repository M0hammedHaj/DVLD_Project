using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD_Project.Global_clases
{
    internal class clsGlobal
    {
        public static clsUser LogedInUser;
        
        public static bool SaveCredentialInWindowsRegistry(string Username , string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDProjectCredential";
            string[] ValueNames = { "Username", "Password" };

            try
            {
                Registry.SetValue(KeyPath, ValueNames[0], Username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ValueNames[1], Password, RegistryValueKind.String);

                return true;
            }
            catch(Exception)
            { return false; }
        }
        public static bool SaveCredentialInTxtFile(string Username,string  Password)
        {
            try
            {
                string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = CurrentDirectory + "\\data.txt";

                if(Username=="" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string dataToSave = Username + "#//#" + Password;

                using (StreamWriter Writer = new StreamWriter(filePath))
                {
                    Writer.WriteLine(dataToSave);
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred:" + ex.Message,"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool GetStoredCredentialFromWindowsRegistry(ref string Username,ref string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLDProjectCredential";
            string[] ValueNames = { "Username", "Password" };

            try
            {
                Username = Registry.GetValue(KeyPath, ValueNames[0], null) as string;
                Password = Registry.GetValue(KeyPath, ValueNames[1], null) as string;

                return (Username != null);
            }
            catch(Exception)
            {
                return false;
            }
        }
        public static bool GetStoredCredential(ref string Username,ref string Password)
        {
            string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();

            string filePath = CurrentDirectory + "\\data.txt";
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader Reader = new StreamReader(filePath))
                    {
                        string Line;
                        while ((Line = Reader.ReadLine()) != null)
                        {
                            string[] Result = Line.Split(new string[] { "#//#" },
                                StringSplitOptions.None);
                            
                            Username = Result[0];
                            Password = Result[1];
                            return true;
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
