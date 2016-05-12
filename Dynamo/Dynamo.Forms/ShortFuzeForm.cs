using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Augment;
using Dynamo.Core;

namespace Dynamo.Forms
{
    public partial class ShortFuzeForm : Form
    {
        private ShortFuze _shortFuze;

        public ShortFuzeForm()
        {
            InitializeComponent();
        }

        private void solutionTextBox_Click(object sender, EventArgs e)
        {
            if (solutionTextBox.Text.IsNullOrEmpty())
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Select Destination Folder...";
                    fbd.SelectedPath = @"C:\";
                    fbd.ShowNewFolderButton = false;

                    if (fbd.ShowDialog(this) == DialogResult.OK)
                    {
                        solutionTextBox.Text = fbd.SelectedPath;

                        solutionTextBox.SelectionLength = 0;
                        solutionTextBox.SelectionStart = solutionTextBox.Text.Length;
                    }
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _shortFuze = new ShortFuze(solutionTextBox.Text, backgroundWorker);

            Action action = () =>
              {
                  progressBar.Minimum = 0;
                  progressBar.Value = 0;
                  progressBar.Maximum = 100;
                  progressBar.Visible = true;
              };

            Invoke(action);

            _shortFuze.CreateSolution();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Action action = () =>
            {
                progressBar.Value = Math.Min(e.ProgressPercentage, 100);
            };

            Invoke(action);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Action action = () =>
            {
                progressBar.Visible = false;

                Process.Start(Path.GetDirectoryName(solutionTextBox.Text));

                Close();
            };

            Invoke(action);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (solutionTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show(this, "Solution FileName is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!solutionTextBox.Text.EndsWith(".sln"))
            {
                MessageBox.Show(this, "Solution FileName does not end in .sln", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (File.Exists(solutionTextBox.Text))
            {
                DialogResult r = MessageBox.Show(this,
                    "Solution file already exists. Overwrite ALL files?",
                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (r != DialogResult.Yes)
                {
                    return;
                }
            }

            backgroundWorker.RunWorkerAsync();
        }
    }
}
