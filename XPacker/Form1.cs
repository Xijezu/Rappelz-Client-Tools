using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace XPacker
{
    public partial class formMain : Form
    {
        string runPath = Environment.CurrentDirectory;
        System.Threading.Thread thread;

        public formMain()
        {
            InitializeComponent();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
                runPath = args[1];

            thread = new System.Threading.Thread(new System.Threading.ThreadStart(RunCleaner));
            thread.IsBackground = true;
            thread.Start();
        }

        private void RunCleaner()
        {
            int currentFile = 0;
            XClientManager manager = new XClientManager();
            try
            {
                manager.Open(Path.Combine(runPath, "data.000"));
                int maxFile = manager.m_lClient.Count;
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Maximum = maxFile + 6));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Step = 1));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Minimum = 0));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Value = 0));
                pbPatcher.Invoke((MethodInvoker)(() => pbPatcher.Style = ProgressBarStyle.Blocks));

                foreach (CSDATA_INDEX index in manager.m_lClient.ToList())
                {
                    manager.Patch(runPath, index);
                    updateProgressBar(ref currentFile);
                }

                for (int i = 1; i < 9; i++)
                {
                    string dataFile = Path.Combine(runPath, "data.00" + i);
                    if (File.Exists(dataFile))
                        File.Delete(dataFile);
                    File.Move(manager.tempDataName[i], dataFile);
                    updateProgressBar(ref currentFile);
                }
                if (File.Exists(Path.Combine(runPath, "data.000")))
                    File.Delete(Path.Combine(runPath, "data.000"));
                manager.Save(Path.Combine(runPath, "data.000"));

                if (MessageBox.Show("Successfully cleaned your client!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    formMain.ActiveForm.Invoke((MethodInvoker)(() => formMain.ActiveForm.Dispose()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
