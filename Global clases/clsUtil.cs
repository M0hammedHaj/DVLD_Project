using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Global_clases
{
    public class clsUtil
    {
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static bool CreateFolderIfDosenotExist(string folderPath)
        {
            if(!Directory.Exists(folderPath)) 
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error creating folder : "+ex.Message,"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        public static string ReplaceFileNameWithGuid(string Path)
        {
            FileInfo fileInfo = new FileInfo(Path);
            string EX = fileInfo.Extension;
            return GenerateGuid() + EX;
        }
        public static bool CopyImageWithGuid(ref string SourceFile)
        {
            string DestintionFolder = @"E:\DVLD_Pictures\";
            if (!CreateFolderIfDosenotExist(DestintionFolder))
                return false;

            string GuidFileNameWithEx = ReplaceFileNameWithGuid(SourceFile);
            string DestintionFile = DestintionFolder + GuidFileNameWithEx;
            try
            {
                File.Copy(SourceFile, DestintionFile, true);

            }
            catch(IOException IOX)
            {
                MessageBox.Show(IOX.Message,"Error",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            SourceFile = DestintionFile;
            return true;
        }
    }
}
