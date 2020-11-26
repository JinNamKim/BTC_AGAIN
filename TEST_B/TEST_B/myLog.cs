using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_B
{
    public static class myLog
    {
        public static void init()
        {
            string folderPath = string.Format("{0}\\LOG", System.IO.Directory.GetCurrentDirectory()) ;
            DirectoryInfo di = new DirectoryInfo(folderPath);
            if(!di.Exists)
            {
                di.Create();
            }

            string filePath = string.Format("{0}\\{1:yyyyMMdd}.txt", folderPath, DateTime.Now);
        }

        public static void write()
        {

        }

    }
}
