using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Dynamo.Core
{
    public class LinesOfCodeViewModel : INotifyPropertyChanged
    {
        #region Members

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        #endregion

        #region Constructors

        public LinesOfCodeViewModel()
        {
            LinesOfCodeMetrics = new BindingList<LinesOfCodeMetric>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void GetMetrics()
        {
            LinesOfCodeMetrics.Clear();

            if (Directory.Exists(CurrentPath))
            {
                LinesOfCodeCounter locc = new LinesOfCodeCounter();

                locc.ProgressChanged += (o, e) => OnProgressChanged(e.UserState as string);

                IEnumerable<LinesOfCodeMetric> metrics = locc.GetMetrics(CurrentPath);

                foreach (LinesOfCodeMetric m in metrics)
                {
                    LinesOfCodeMetrics.Add(m);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnProgressChanged(string message)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(0, message));
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string CurrentPath
        {
            get { return _currentPath; }
            set
            {
                if (_currentPath != value)
                {
                    _currentPath = value;

                    OnPropertyChanged("CurrentPath");

                    GetMetrics();
                }
            }
        }
        private string _currentPath;

        /// <summary>
        /// 
        /// </summary>
        public BindingList<LinesOfCodeMetric> LinesOfCodeMetrics { get; set; }

        #endregion
    }
}
