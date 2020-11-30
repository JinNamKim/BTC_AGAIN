using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TEST_B
{
    public static class myLog
    {
        private static Thread LOG_THREAD;
        private static Queue<string> STR_QUEUE = new Queue<string>();

        public static void init()
        {
            try
            {
                string folderPath = string.Format("{0}\\LOG", System.IO.Directory.GetCurrentDirectory());
                DirectoryInfo di = new DirectoryInfo(folderPath);
                if (!di.Exists)
                {
                    di.Create();
                }

                

                LOG_THREAD = new Thread(new ThreadStart(LOG_thread));
                LOG_THREAD.Start();
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            
        }

        public static void write(string txt)
        {
            try
            {
                STR_QUEUE.Enqueue(txt);
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        public static void LOG_CLOSE()
        {
            try
            {
                if(LOG_THREAD.IsAlive)
                {
                    LOG_THREAD.Abort();
                    //LOG_THREAD.Join();
                }
                
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private static void LOG_thread()
        {
            try
            {
                while(true)
                {
                    Thread.Sleep(500);
                    GC.Collect();

                    if(STR_QUEUE.Count > 0)
                    {
                        string msg = STR_QUEUE.Dequeue();

                        string folderPath = string.Format("{0}\\LOG", System.IO.Directory.GetCurrentDirectory());
                        string filePath = string.Format("{0}\\{1:yyyyMMdd}.txt", folderPath, DateTime.Now);

                        FileInfo fi = new FileInfo(filePath);
                        StreamWriter sw;
                        if (fi.Exists)
                        {
                            sw = File.AppendText(filePath);
                        }
                        else
                        {
                            sw = File.CreateText(filePath);
                        }

                        sw.WriteLine(string.Format("{0:HH시 mm분 ss초} : {1}", DateTime.Now, msg));
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LOG_THREAD.Join();
                LOG_THREAD.Abort();
            }
            finally
            {

            }
        }

    }
}
