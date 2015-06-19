using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Augment;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using Dynamo.Core.Properties;
using EnsureThat;

namespace Dynamo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratorViewModel : INotifyPropertyChanged
    {
        #region Members

        /// <summary>
        /// 
        /// </summary>
        public const string ProjectExtension = ".dynp";

        /// <summary>
        /// 
        /// </summary>
        public const string TemplateExtension = ".dynt";

        private const int DisplayLength = 40;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        #endregion

        #region Constructor

        public GeneratorViewModel()
        {
            Project = new GeneratorProject();
        }

        #endregion

        #region Methods

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);

                PropertyChanged(this, e);
            }
        }

        private void OnProgressChanged(string message)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(0, message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void NewProject()
        {
            Project = new GeneratorProject();

            Templates = null;

            Tables = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenProject(string filename)
        {
            Ensure.That(File.Exists(filename))
                .WithExtraMessageOf(() => "File {0} does not exist".FormatArgs(filename))
                .IsTrue()
                ;

            DynamoFileName = filename;

            using (StreamReader sr = new StreamReader(DynamoFileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(GeneratorProject));

                Project = s.Deserialize(sr) as GeneratorProject;

                LoadTemplateFiles();

                Project.IsDirty = false;

                UpdateRecentFiles(DynamoFileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveProject()
        {
            using (StreamWriter sw = new StreamWriter(DynamoFileName))
            {
                //  in case of save-as or new project
                //  ensure relative paths
                Project.SolutionFile = Utilities.GetRelativePath(DynamoFileName, SolutionFile);

                Project.TemplateFolder = Utilities.GetRelativePath(DynamoFileName, TemplateFolder);

                XmlSerializer s = new XmlSerializer(typeof(GeneratorProject));

                s.Serialize(sw, Project);

                Project.IsDirty = false;

                UpdateRecentFiles(DynamoFileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAsProject(string filename)
        {
            DynamoFileName = filename;

            SaveProject();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latestFile"></param>
        public void UpdateRecentFiles(string latestFile)
        {
            if (Settings.Default.RecentFiles == null)
            {
                Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
            }

            Settings.Default.RecentFiles.Insert(0, latestFile);

            for (int i = 1; i < Settings.Default.RecentFiles.Count; i++)
            {
                if (Settings.Default.RecentFiles[i].IsSameAs(latestFile))
                {
                    Settings.Default.RecentFiles.RemoveAt(i);

                    i -= 1;
                }
            }

            while (Settings.Default.RecentFiles.Count > 9)
            {
                Settings.Default.RecentFiles.RemoveAt(9);
            }

            Settings.Default.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables"></param>
        public void Generate(IList<GeneratorTable> tables, IList<string> files)
        {
            Generator g = new Generator();

            IDictionary<string, string> variables = new Dictionary<string, string>()
            {
                { "SOLUTION", Project.Namespace }
            };

            Total = tables.Count + files.Count;

            foreach (GeneratorTable table in tables)
            {
                OnProgressChanged("Working {0}".FormatArgs(table.Name));

                foreach (string inPath in files)
                {
                    Current += 1;

                    string contents = g.GetContents(table, variables, inPath);

                    string outPath = Path.Combine(SolutionFolder, g.FileName);

                    string dir = Path.GetDirectoryName(outPath);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    File.WriteAllText(outPath, contents);
                }
            }

            OnProgressChanged("");
        }

        public void RefreshSchema()
        {
            OnProgressChanged("Refreshing Schema...");

            using (DatabaseReader dr = new DatabaseReader(ConnectionString, DataProvider))
            {
                Tables = dr.AllTables();

                dr.DataTypes();

                OnPropertyChanged("Tables");
            }

            OnProgressChanged("");
        }

        public void RefreshTemplates()
        {
            OnProgressChanged("Refreshing Templates...");

            LoadTemplateFiles();

            OnPropertyChanged("Templates");

            OnProgressChanged("");
        }

        private void LoadTemplateFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(TemplateFolder);

            if (dir.Exists)
            {
                Templates = GetTemplateFiles(dir).ToList();
            }
            else
            {
                Templates = new List<FileDisplay>();
            }

            OnPropertyChanged("Templates");
        }

        private IEnumerable<FileDisplay> GetTemplateFiles(DirectoryInfo dir)
        {
            if (dir != null)
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    foreach (FileDisplay fd in GetTemplateFiles(d))
                    {
                        yield return fd;
                    }
                }
            }

            foreach (FileInfo f in dir.GetFiles("*" + TemplateExtension))
            {
                yield return new FileDisplay { FileInfo = f };
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public bool CanGenerate
        {
            get
            {
                if (!File.Exists(SolutionFile)) return false;
                if (!Directory.Exists(TemplateFolder)) return false;
                if (!IsConnectionStringValid) return false;

                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GeneratorProject Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DataProvider
        {
            get
            {
                return Project.DataProvider;
            }
            set
            {
                Project.DataProvider = value;

                OnPropertyChanged("DataProvider");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return Project.ConnectionString;
            }
            set
            {
                Project.ConnectionString = value;

                OnPropertyChanged("ConnectionString");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsConnectionStringValid
        {
            get
            {
                try
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(DataProvider);

                    DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();

                    csb.ConnectionString = ConnectionString;
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SolutionFolder
        {
            get
            {
                FileInfo fi = new FileInfo(SolutionFile);

                return fi.DirectoryName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SolutionFile
        {
            get
            {
                if (Project.SolutionFile.IsNullOrEmpty())
                {
                    return "(none)";
                }

                if (DynamoFileName.IsNotEmpty())
                {
                    string dir = Path.GetDirectoryName(DynamoFileName);

                    string combine = Path.Combine(dir, Project.SolutionFile);

                    FileInfo fi = new FileInfo(Path.GetFullPath(combine));

                    return fi.FullName;
                }

                return Project.SolutionFile;
            }
            set
            {
                if (DynamoFileName.IsNotEmpty())
                {
                    Project.SolutionFile = Utilities.GetRelativePath(DynamoFileName, value);
                }
                else
                {
                    Project.SolutionFile = value;
                }

                if (TemplateFolder.IsNullOrEmpty() || TemplateFolder == "(none)")
                {
                    string t = Path.Combine(SolutionFolder, "Templates");

                    if (Directory.Exists(t))
                    {
                        TemplateFolder = t;
                    }
                }

                OnPropertyChanged("SolutionFile");
                OnPropertyChanged("CanGenerate");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateFolder
        {
            get
            {
                if (Project.TemplateFolder.IsNullOrEmpty())
                {
                    return "(none)";
                }

                if (DynamoFileName.IsNotEmpty())
                {
                    string dir = Path.GetDirectoryName(DynamoFileName);

                    string combine = Path.Combine(dir, Project.TemplateFolder);

                    DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(combine));

                    return di.FullName;
                }

                return Project.TemplateFolder;
            }
            set
            {
                if (DynamoFileName.IsNotEmpty())
                {
                    Project.TemplateFolder = Utilities.GetRelativePath(DynamoFileName, value);
                }
                else
                {
                    Project.TemplateFolder = value;
                }

                if (value == null)
                {
                    Templates = new List<FileDisplay>();
                }
                else
                {
                    LoadTemplateFiles();
                }

                OnPropertyChanged("TemplateFolder");
                OnPropertyChanged("CanGenerate");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DynamoFileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;

                OnPropertyChanged("FileName");
                OnPropertyChanged("IsValidFileName");
                OnPropertyChanged("Title");
            }
        }
        private string _fileName;

        /// <summary>
        /// 
        /// </summary>
        public bool IsValidFileName { get { return File.Exists(DynamoFileName); } }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> RecentFiles
        {
            get
            {
                if (Settings.Default.RecentFiles == null)
                {
                    return new string[0];
                }
                return Settings.Default.RecentFiles.Cast<string>().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<DatabaseTable> Tables { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<FileDisplay> Templates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get
            {
                if (DynamoFileName.IsNullOrEmpty())
                {
                    return "New Project";
                }
                return DynamoFileName + (Project.IsDirty ? " *" : "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Current
        {
            get { return _current; }
            set { _current = value; OnPropertyChanged("Current"); }
        }
        private int _current;

        /// <summary>
        /// 
        /// </summary>
        public int Total
        {
            get { return _total; }
            set { _total = value; OnPropertyChanged("Total"); }
        }
        private int _total;

        #endregion
    }
}