using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DatabaseSchemaReader.DataSchema;
using NOX;
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

            viewModelBindingSource.DataSource = new ProjectViewModel();

            DataTable dt = DbProviderFactories.GetFactoryClasses();

            string[] providers = dt.Rows.OfType<DataRow>().Select(x => x["InvariantName"] as string).OrderBy(x => x).ToArray();

            dataProviderComboBox.DataSource = providers;
        }

        #endregion

        #region Events

        private void templateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FolderBrowserDialog f = new FolderBrowserDialog())
            {
                f.Description = "Select Folder...";

                if (Directory.Exists(ViewModel.SolutionFile))
                {
                    f.SelectedPath = ViewModel.Project.SolutionPath;
                }
                else
                {
                    f.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
                }

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    ViewModel.TemplateFolder = f.SelectedPath;

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
                    ViewModel.SolutionFile = f.FileName;

                    solutionLinkLabel.DataBindings[0].ReadValue();
                    templateLinkLabel.DataBindings[0].ReadValue();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool? op = CheckForSave();

            if (op == null)
            {
                return;
            }

            ViewModel.NewProject();

            viewModelBindingSource.ResetCurrentItem();
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
                ofd.DefaultExt = Project.FileExtension;
                ofd.Filter = "SqlNOX Code Generator (*{0})|*{0}".FormatArgs(Project.FileExtension);
                ofd.Multiselect = false;
                ofd.Title = "Open File...";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    ViewModel.OpenProject(ofd.FileName);

                    viewModelBindingSource.ResetCurrentItem();

                    Action action = new Action(() => {
                        Cursor = Cursors.WaitCursor;

                        ViewModel.RefreshSchema();

                        mainTabControl.SelectedTab = tableTabPage;

                        Cursor = Cursors.Default;
                    });

                    Invoke(action);

                    Application.DoEvents();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ViewModel.IsValidFileName)
            {
                ViewModel.SaveProject();
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
                ViewModel.SaveAsProject(f);
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

            if (tables.Count > 0)
            {
                ViewModel.Generate(tables, files);
            }

            ViewModel.Current = 0;

            Cursor = Cursors.Default;
        }

        private void refreshTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ViewModel.IsConnectionStringValid)
            {
                ViewModel.RefreshSchema();
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

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if saved, False if not saved,
        /// NULL if the operation should be cancelled</returns>
        private bool? CheckForSave()
        {
            if (ViewModel.Project.IsDirty)
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
                        if (ViewModel.IsValidFileName)
                        {
                            ViewModel.SaveProject();
                        }
                        else
                        {
                            string f = GetFileName();

                            if (f == null)
                            {
                                //  cancel operation
                                return null;
                            }

                            ViewModel.SaveAsProject(f);
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
                sfd.DefaultExt = Project.FileExtension;
                sfd.Filter = "SqlNOX Code Generator (*{0})|*{0}".FormatArgs(Project.FileExtension);
                sfd.OverwritePrompt = true;
                sfd.Title = "Save File...";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    return sfd.FileName;
                }

                return null;
            }
        }

        #endregion

        #region Properties

        private ProjectViewModel ViewModel
        {
            get { return viewModelBindingSource.DataSource as ProjectViewModel; }
        }

        #endregion
    }
}