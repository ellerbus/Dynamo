using System.IO;
using System.Xml.Serialization;

namespace Dynamo.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class GeneratorProject
    {
        #region Constructor

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool IsDirty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public string Namespace
        {
            get
            {
                string ext = Path.GetExtension(SolutionFile);

                int pos = SolutionFile.LastIndexOf(@"\");

                return SolutionFile.Replace(ext, "").Substring(pos + 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SolutionFile
        {
            get { return _solutionFile; }
            set
            {
                IsDirty |= _solutionFile != value;

                _solutionFile = value;
            }
        }
        private string _solutionFile;

        /// <summary>
        /// 
        /// </summary>
        public string TemplateFolder
        {
            get { return _templateFolder; }
            set
            {
                IsDirty |= _templateFolder != value;
                _templateFolder = value;
            }
        }
        private string _templateFolder;

        /// <summary>
        /// 
        /// </summary>
        public string DataProvider
        {
            get { return _dataProvider; }
            set
            {
                IsDirty |= _dataProvider != value;

                _dataProvider = value;
            }
        }
        private string _dataProvider;

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                IsDirty |= _connectionString != value;

                _connectionString = value;
            }
        }
        private string _connectionString;

        #endregion
    }
}
