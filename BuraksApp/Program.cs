using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace BuraksApp
{


    static class Program
    {
        const string fileName = "Commands.txt";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ReadCommands();
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = GetContext();
            notifyIcon.Icon = new System.Drawing.Icon("buraks.ico");
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new System.EventHandler(notifyIcon_DoubleClick);
            Application.Run();
        }
        public static void WriteCommands()
        {
            string output = JsonConvert.SerializeObject(Globals.myCommandObjects);

            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
            File.WriteAllText(fileName, output);
        }
        private static void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            showEditWindow();
        }

        public static void ReadCommands()
        {
            string input = "";
            try
            {
                StreamReader sr = new StreamReader(fileName);
                input = sr.ReadLine();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            Globals.myCommandObjects = new List<CommandObject>();
            var objects = JsonConvert.DeserializeObject<List<CommandObject>>(input); 
            if (objects != null)
            {
                Globals.myCommandObjects = objects;
            }
             
        }
        private static ContextMenuStrip GetContext()
        {
            ContextMenuStrip CMS = new ContextMenuStrip();
            addCustomMenuItems(CMS);
            addDefaultMenuItems(CMS);
            return CMS; 
        }
         
        private static ContextMenuStrip addDefaultMenuItems(ContextMenuStrip CMS)
        {
            CMS.Items.Add(new ToolStripSeparator());
            CMS.Items.Add("Edit Commands", null, new EventHandler(Edit_Commands));
            CMS.Items.Add(new ToolStripSeparator());
            CMS.Items.Add("IIS Start", null, new EventHandler(Iis_Start));
            CMS.Items.Add("IIS Stop", null, new EventHandler(Iis_Stop));
            CMS.Items.Add("IIS Reset", null, new EventHandler(Iis_Reset));
            CMS.Items.Add("IIS Recycle", null, new EventHandler(Iis_Recycle));
            CMS.Items.Add(new ToolStripSeparator());
            CMS.Items.Add("Exit", null, new EventHandler(Exit_Click));
            return CMS;
        }

        private static ContextMenuStrip addCustomMenuItems(ContextMenuStrip CMS)
        {
            foreach (CommandObject cObject in Globals.myCommandObjects)
            {
                CMS.Items.Add(cObject.Name, null, new EventHandler((s, e) => RunDynamicly(s, e, cObject.Command)));
            }
            return CMS;
        }

        private static void Edit_Commands(object sender, EventArgs e)
        {
            showEditWindow();
        }

        private static void showEditWindow()
        {
            EditCommands edit = new EditCommands();
            edit.Show();
        }

        private static void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private static void Iis_Reset(object sender, EventArgs e)
        {
            string command = "iisreset";
            Run(command);
        }
        private static void Iis_Stop(object sender, EventArgs e)
        {
            string command = "iisreset /stop";
            Run(command);
        }
        private static void Iis_Start(object sender, EventArgs e)
        {
            string command = "iisreset /start";
            Run(command);
        }
        private static void RunDynamicly(object sender, EventArgs e, string command)
        {
            Run(command);
        }
        private static void Run(string command)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        private static void Iis_Recycle(object sender, EventArgs e)
        {
            string command = "%systemroot%\\system32\\inetsrv\\appcmd recycle apppool /apppool.name:DefaultAppPool";
            Run(command);
        }
    }
}
