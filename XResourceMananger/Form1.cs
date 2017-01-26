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
            int currentFile = 0;
            XClientManager manager = new XClientManager();
            try
            {
                foreach(string file in Directory.GetFiles(Path.Combine(runPath, "Resource", "Resource_Patch"), "*.*", SearchOption.AllDirectories))
                {
                    if (!file.Contains("(ascii)"))
                    {
                        File.Move(file, Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "(ascii)" + Path.GetExtension(file)));
                    }
                }

                manager.Open(Path.Combine(runPath, "data.000"));

                var patchList = Directory.GetFiles(Path.Combine(runPath, "Resource", "Resource_Patch"), "*.*", SearchOption.AllDirectories);

                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Maximum = patchList.Length));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Step = 1));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Minimum = 0));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Value = 0));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Style = ProgressBarStyle.Blocks));

                foreach (string szFile in patchList)
                {
                    manager.AddFile(szFile, runPath);
                    updateProgressBar(ref currentFile);
                }

                manager.Save(Path.Combine(runPath, "data.000"));

                if (MessageBox.Show("Successfully patched your client!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    formMain.ActiveForm.Invoke((MethodInvoker)(() => formMain.ActiveForm.Dispose()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                formMain.ActiveForm.Invoke((MethodInvoker)(() => formMain.ActiveForm.Dispose()));
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
