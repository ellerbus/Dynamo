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
using EnsureThat;

namespace Dynamo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratorViewModel : INotifyPropertyChanged
    {
        #region Members

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

            FileName = filename;

            using (StreamReader sr = new StreamReader(FileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(GeneratorProject));

                Project = s.Deserialize(sr) as GeneratorProject;

                LoadTemplateFiles();

                Project.IsDirty = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveProject()
        {
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                XmlSerializer s = new XmlSerializer(typeof(GeneratorProject));

                s.Serialize(sw, Project);

                Project.IsDirty = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAsProject(string filename)
        {            
            FileName = filename;

            SaveProject();
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
                { "NAMESPACE", Project.Namespace }
            };

            Total = tables.Count + files.Count;

            foreach (GeneratorTable table in tables)
            {
                OnProgressChanged("Working {0}".FormatArgs(table.Name));

                foreach (string inPath in files)
                {
                    Current += 1;

                    string source = File.ReadAllText(inPath);

                    string contents = g.GetContents(table, variables, source);

                    string outPath = Path.Combine(Project.SolutionPath, g.FileName);

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

        private void LoadTemplateFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(Project.TemplateFolder);

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

            foreach (FileInfo f in dir.GetFiles("*.scgt"))
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
                if (!File.Exists(Project.SolutionFile)) return false;
                if (!Directory.Exists(Project.TemplateFolder)) return false;
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
        public string SolutionFile
        {
            get
            {
                if (Project.SolutionFile.IsNullOrEmpty())
                {
                    return "(none)";
                }

                return Utilities.ShortenPath(Project.SolutionFile, DisplayLength);
            }
            set
            {
                Project.SolutionFile = value;

                if (TemplateFolder.IsNullOrEmpty() || TemplateFolder == "(none)")
                {
                    string t = Path.Combine(Project.SolutionPath, "Templates");

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

                return Utilities.ShortenPath(Project.TemplateFolder, DisplayLength);
            }
            set
            {
                Project.TemplateFolder = value;

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
        public string Connect
        {
            get
            {
                if (Project.TemplateFolder.IsNullOrEmpty())
                {
                    return "(none)";
                }

                return Utilities.ShortenPath(Project.TemplateFolder, DisplayLength);
            }
            set
            {
                Project.TemplateFolder = value;

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
        public string FileName
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
        public bool IsValidFileName { get { return File.Exists(FileName); } }

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
                if (FileName.IsNullOrEmpty())
                {
                    return "New Project";
                }
                return FileName + (Project.IsDirty ? " *" : "");
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
