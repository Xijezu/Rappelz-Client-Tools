using System;
using System.IO;
using System.Windows.Forms;

namespace XResourceManager
{
    public partial class formMain : Form
    {
        string runPath = @"F:\Games\DarknessFight";// Environment.CurrentDirectory;
        System.Threading.Thread thread;

        public formMain()
        {
            InitializeComponent();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
                runPath = args[1];

            thread = new System.Threading.Thread(new System.Threading.ThreadStart(RunManager));
            thread.IsBackground = true;
            thread.Start();
        }

        private void RunManager()
        {
            XClientManager manager = new XClientManager();
            foreach (string szFile in Directory.GetFiles(Path.Combine(Path.Combine(runPath, "Resource"), "Resource_Patch"), "*.*", SearchOption.AllDirectories))
            {
                if(szFile.ToLower().Contains("(ascii)"))
                {
                    File.Move(szFile, Path.Combine(Path.GetDirectoryName(szFile), manager.Encode(Path.GetFileName(szFile))));
                }
                else
                {
                    File.Move(szFile, Path.Combine(Path.GetDirectoryName(szFile), manager.Decode(Path.GetFileName(szFile))));
                }
            }
        }

        private void updateProgressBar(ref int index)
        {
            pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.PerformStep()));
            index++;
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread.IsAlive)
                e.Cancel = true;
        }
    }
}
