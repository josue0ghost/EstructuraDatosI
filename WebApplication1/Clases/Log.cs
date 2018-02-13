using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebApplication1.Clases
{
    public class Log
    {
        static string filename = "Log.txt";
        public static void beginLog()
        {
            filename = "Log" + DateTime.Now.ToShortTimeString() + ".txt";
        }
        public static void SendToLog(string logMessage, TimeSpan time)
        {
            StreamWriter w = File.AppendText(@"C:\Users\Williams Monterroso\source\repos\EstructuraDatosI\" + filename);
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("Event:{0}     Duration:{1}", logMessage, Convert.ToString(time));
            w.WriteLine("-------------------------------");
            w.Close();
        }
        public static void CleanLog()
        {
            File.Delete(@"C:\Users\Williams Monterroso\source\repos\EstructuraDatosI\Log.txt");
        }
    }
}
