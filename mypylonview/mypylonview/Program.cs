using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PylonC.NET;
namespace mypylonview
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            /* This is a special debug setting needed only for GigE cameras.
                See 'Building Applications with pylon' in the Programmer's Guide. */
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);
#endif
            Pylon.Initialize();
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch
            {
                Pylon.Terminate();
                throw;
            }
            Pylon.Terminate();
        }
    }
}
