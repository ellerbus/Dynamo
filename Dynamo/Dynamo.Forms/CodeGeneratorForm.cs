﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Augment;
using DatabaseSchemaReader.DataSchema;
using Dynamo.Core;

namespace Dynamo.Forms
{
    partial class CodeGeneratorForm : Form
    {
        #region Constructors

        public CodeGeneratorForm()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;

            viewModelBindingSource.DataSource = new GeneratorViewModel();

            GeneratorViewModel.ProgressChanged += GeneratorViewModel_ProgressChanged;

            DataTable dt = DbProviderFactories.GetFactoryClasses();

            string[] providers = dt.Rows.OfType<DataRow>().Select(x => x["InvariantName"] as string).OrderBy(x => x).ToArray();

            dataProviderComboBox.DataSource = providers;

            generateCodeFilesToolStripMenuItem.DataBindings.Add("Enabled", viewModelBindingSource, "CanGenerate");

            refreshToolStripMenuItem.DataBindings.Add("Enabled", viewModelBindingSource, "CanGenerate");

            RecentlyUsedFiles = new MostRecentlyUsedFileMenu(
                recentFilesToolStripMenuItem, MostRecentlyUsedFileMenu_ClickedHandler,
                "Dynamo.RecentFiles", true, 7
                );
        }

        #endregion

        #region Events

        private void MostRecentlyUsedFileMenu_ClickedHandler(int number, string filename)
        {
            bool? op = CheckForSave();

            if (op == null)
            {
                return;
            }

            if (File.Exists(filename))
            {
                OpenProject(filename);
            }
            else
            {
                MessageBox.Show(this, "Selected File does not exist", "Invalid File");

                RecentlyUsedFiles.RemoveFile(number);
            }
        }

        private void templateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FolderBrowserDialog f = new FolderBrowserDialog())
            {
                f.Description = "Select Folder...";

                if (Directory.Exists(GeneratorViewModel.SolutionFile))
                {
                    f.SelectedPath = GeneratorViewModel.SolutionFolder;
                }
                else
                {
                    f.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    GeneratorViewModel.TemplateFolder = f.SelectedPath;

                    solutionLinkLabel.DataBindings[0].ReadValue();
                    templateLinkLabel.DataBindings[0].ReadValue();
                }
            }
        }

        private void solutionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog f = new OpenFileDialog())
            {
                f.Title = "Open Solution File...";
                f.Filter = "Solution Files (*.sln)|*.sln";

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    GeneratorViewModel.SolutionFile = f.FileName;

                    solutionLinkLabel.DataBindings[0].ReadValue();
                    templateLinkLabel.DataBindings[0].ReadValue();
                }
            }
        }

        private void GeneratorViewModel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Application.DoEvents();

            if (InvokeRequired)
            {
                Action<string> a = new Action<string>(x => generatorStatusLabel.Text = x);

                Invoke(a, e.UserState as string);
            }
            else
            {
                generatorStatusLabel.Text = e.UserState as string;

                Application.DoEvents();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool? op = CheckForSave();

            if (op == null)
            {
                return;
            }

            GeneratorViewModel.NewProject();

            viewModelBindingSource.ResetCurrentItem();

            mainTabControl.SelectedTab = tableTabPage;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool? op = CheckForSave();

            if (op == null)
            {
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.AddExtension = true;
                ofd.CheckPathExists = true;
                ofd.CheckFileExists = true;
                ofd.DefaultExt = GeneratorViewModel.ProjectExtension;
                ofd.Filter = "Dynamo Project (*{0})|*{0}".FormatArgs(GeneratorViewModel.ProjectExtension);
                ofd.Multiselect = false;
                ofd.Title = "Open File...";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    OpenProject(ofd.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GeneratorViewModel.IsValidFileName)
            {
                GeneratorViewModel.SaveProject();
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string f = GetFileName();

            if (f != null)
            {
                GeneratorViewModel.SaveAsProject(f);

                UpdateRecentlyUsedFiles(f);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool? op = CheckForSave();

            if (op == null)
            {
                return;
            }

            Close();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            generateCodeFilesToolStripMenuItem_Click(sender, e);
        }

        private void generateCodeFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            IList<GeneratorTable> tables = tablesDataGridView.SelectedRows
                .Cast<DataGridViewRow>().Select(x => new GeneratorTable(x.DataBoundItem as DatabaseTable))
                .ToList()
                ;

            IList<string> files = templatesDataGridView.SelectedRows
                .Cast<DataGridViewRow>().Select(x => (x.DataBoundItem as FileDisplay).FileInfo.FullName)
                .ToList()
                ;

            StringBuilder sb = new StringBuilder();

            if (tables.Count > 0)
            {
                GeneratorViewModel.Generate(tables, files, sb);
            }

            resultsTextBox.Text = sb.ToString();

            GeneratorViewModel.Current = 0;

            Cursor = Cursors.Default;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GeneratorViewModel.IsConnectionStringValid)
            {
                GeneratorViewModel.RefreshSchema();
                GeneratorViewModel.RefreshTemplates();
            }
        }

        private void connectionStringButton_Click(object sender, EventArgs e)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderComboBox.SelectedValue as string);

            DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

            if (connectionStringTextBox.Text != "")
            {
                csb.ConnectionString = connectionStringTextBox.Text;
            }

            using (ConnectionStringBuilderForm f = new ConnectionStringBuilderForm(csb))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    connectionStringTextBox.Text = csb.ConnectionString;
                }
            }
        }

        private void shortFuzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ShortFuzeForm sf = new ShortFuzeForm())
            {
                sf.ShowDialog(this);
            }
        }

        #endregion

        #region Methods

        private void OpenProject(string fileName)
        {
            GeneratorViewModel.OpenProject(fileName);

            viewModelBindingSource.ResetCurrentItem();

            Action schema = new Action(() =>
            {
                Cursor = Cursors.WaitCursor;

                Application.DoEvents();

                GeneratorViewModel.RefreshSchema();

                mainTabControl.SelectedTab = tableTabPage;

                UpdateRecentlyUsedFiles(fileName);

                Cursor = Cursors.Default;

                Application.DoEvents();
            });

            Application.DoEvents();

            Invoke(schema);

            Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if saved, False if not saved,
        /// NULL if the operation should be cancelled</returns>
        private bool? CheckForSave()
        {
            if (GeneratorViewModel.Project.IsDirty)
            {
                DialogResult mbr = MessageBox.Show(this,
                    "Would you like to save the current Project?",
                    "Save?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                    );

                switch (mbr)
                {
                    case DialogResult.Yes:
                        if (GeneratorViewModel.IsValidFileName)
                        {
                            GeneratorViewModel.SaveProject();
                        }
                        else
                        {
                            string f = GetFileName();

                            if (f == null)
                            {
                                //  cancel operation
                                return null;
                            }

                            GeneratorViewModel.SaveAsProject(f);
                        }
                        return true;

                    case DialogResult.No:
                        return false;

                    case DialogResult.Cancel:
                        return null;
                }
            }

            return true;
        }

        private string GetFileName()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.AddExtension = true;
                sfd.CheckPathExists = true;
                sfd.DefaultExt = GeneratorViewModel.ProjectExtension;
                sfd.Filter = "Dynamo Project (*{0})|*{0}".FormatArgs(GeneratorViewModel.ProjectExtension);
                sfd.OverwritePrompt = true;
                sfd.Title = "Save File...";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    return sfd.FileName;
                }

                return null;
            }
        }

        private void UpdateRecentlyUsedFiles(string fileName)
        {
            RecentlyUsedFiles.AddFile(fileName);

            RecentlyUsedFiles.SaveToRegistry();
        }

        #endregion

        #region Properties

        private MostRecentlyUsedFileMenu RecentlyUsedFiles { get; set; }

        private GeneratorViewModel GeneratorViewModel
        {
            get { return viewModelBindingSource.DataSource as GeneratorViewModel; }
        }

        #endregion
    }
}