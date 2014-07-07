using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Augment;

namespace Dynamo.Core
{
    public class LinesOfCodeCounter
    {
        #region Members

        /// <summary>
        /// 
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        private Dictionary<string, LinesOfCodeMetric> _metrics;

        class ParserInfo
        {
            public bool IsMultiLineComment {get;set;}
            public bool IsCode { get; set; }
            public bool IsComment { get; set; }
        }

        #endregion

        #region Constructors

        public LinesOfCodeCounter()
        {
            _metrics = new Dictionary<string, LinesOfCodeMetric>();
        }

        #endregion

        #region Methods

        public List<LinesOfCodeMetric> GetMetrics(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            GetMetrics(dir);

            OnProgressChanged("Summarizing...");

            LinesOfCodeMetric total = new LinesOfCodeMetric() { Extension = ".*" };

            List<LinesOfCodeMetric> metrics = _metrics.Values.OrderBy(x => x.Extension).ToList();

            foreach (LinesOfCodeMetric m in metrics)
            {
                total.Blank += m.Blank;
                total.Code += m.Code;
                total.Comments += m.Comments;
                total.Total += m.Total;
            }

            metrics.Add(total);

            OnProgressChanged("");

            return metrics;
        }

        private void GetMetrics(DirectoryInfo dir)
        {
            OnProgressChanged("Searching {0}".FormatArgs(dir.FullName));

            foreach (DirectoryInfo sub in dir.GetDirectories())
            {
                GetMetrics(sub);
            }

            foreach (FileInfo fi in dir.GetFiles())
            {
                GetMetrics(fi);
            }
        }

        private void GetMetrics(FileInfo fi)
        {
            switch (fi.Extension)
            {
                case ".vb":
                    GetMetricsVbBased(fi);
                    break;

                case ".cs":
                case ".vj":
                case ".cpp":
                case ".cc":
                case ".cxx":
                case ".c":
                case ".hpp":
                case ".hh":
                case ".hxx":
                case ".h":
                case ".js":
                case ".css":
                    GetMetricsCBased(fi);
                    break;

                case ".cd":
                case ".resx":
                case ".res":
                case ".htm":
                case ".html":
                case ".xml":
                case ".xsl":
                case ".xslt":
                case ".xsd":
                case ".config":
                case ".asax":
                case ".ascx":
                case ".asmx":
                case ".aspx":
                case ".ashx":
                case ".cshtml":
                    GetMetricsXmlBased(fi);
                    break;

                case ".idl":
                case ".odl":
                case ".txt":
                case ".sql":
                case ".rc":
                    GetMetricsTextBased(fi);
                    break;
            }
        }

        private LinesOfCodeMetric GetMetricByExtension(string extension)
        {
            extension = extension.ToLower();

            LinesOfCodeMetric loc = null;

            if (!_metrics.TryGetValue(extension, out loc))
            {
                loc = new LinesOfCodeMetric() { Extension = extension };

                _metrics.Add(extension, loc);
            }

            return loc;
        }

        private void OnProgressChanged(string message)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(0, message));
            }
        }

        #endregion

        #region Parsers

        private void GetMetricsTextBased(FileInfo fi)
        {
            LinesOfCodeMetric loc = GetMetricByExtension(fi.Extension);

            foreach (string line in GetLines(fi))
            {
                loc.Total += 1;

                if (line.Trim() == string.Empty)
                {
                    loc.Blank += 1;
                }
            }
        }

        private void GetMetricsCBased(FileInfo fi)
        {
            LinesOfCodeMetric loc = GetMetricByExtension(fi.Extension);

            ParserInfo pi = new ParserInfo();

            foreach (string line in GetLines(fi))
            {
                ParseCBased(line, pi);

                if (pi.IsComment)
                {
                    loc.Comments += 1;
                }

                if (pi.IsCode)
                {
                    loc.Code += 1;
                }

                if (!pi.IsCode && !pi.IsComment)
                {
                    loc.Blank += 1;
                }

                loc.Total += 1;
            }
        }

        /// <summary>
        /// Parses a c-style code line for comments, code, and blanks.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pi.IsMultiLineComment"></param>
        /// <param name="pi.IsCode"></param>
        /// <param name="pi.IsComment"></param>
        /// <remarks>This algorithm was originally created by Oz Solomon,
        /// for his PLC line counter add-in for Visual Studio 2002/2003.</remarks>
        private void ParseCBased(string line, ParserInfo pi)
        {
            bool inString = false;

            pi.IsComment = pi.IsMultiLineComment;
            pi.IsCode = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                char chNext = (i < line.Length - 1 ? line[i + 1] : '\0');

                // Process a single-line comment
                if (IsPair('/', '/', ch, chNext) && !pi.IsMultiLineComment && !inString)
                {
                    pi.IsComment = true;
                    return;
                }

                // Process start of a multiline comment
                else if (IsPair('/', '*', ch, chNext) && !pi.IsMultiLineComment && !inString)
                {
                    pi.IsMultiLineComment = true;
                    pi.IsComment = true;
                    ++i;
                }

                // Process end of a multiline comment
                else if (IsPair('*', '/', ch, chNext) && !inString)
                {
                    pi.IsMultiLineComment = false;
                    ++i;
                }

                // Process escaped character
                else if (ch == '\\' && !pi.IsMultiLineComment)
                {
                    ++i;
                    pi.IsCode = true;
                }

                // Process string
                else if (ch == '"' && !pi.IsMultiLineComment)
                {
                    inString = !inString;
                    pi.IsCode = true;
                }

                else if (!pi.IsMultiLineComment)
                {
                    if (!char.IsWhiteSpace(ch))
                    {
                        pi.IsCode = true;
                    }
                }
            }
        }

        private void GetMetricsVbBased(FileInfo fi)
        {
            LinesOfCodeMetric loc = GetMetricByExtension(fi.Extension);

            ParserInfo pi = new ParserInfo();

            foreach (string line in GetLines(fi))
            {
                ParseVbBased(line, pi);

                if (pi.IsComment)
                {
                    loc.Comments += 1;
                }

                if (pi.IsCode)
                {
                    loc.Code += 1;
                }

                if (!pi.IsCode && !pi.IsComment)
                {
                    loc.Blank += 1;
                }

                loc.Total += 1;
            }
        }

        /// <summary>
        /// Parses a vb-style code line for comments, code and blanks.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pi.IsMultiLineComment"></param>
        /// <param name="pi.IsCode"></param>
        /// <param name="pi.IsComment"></param>
        /// <remarks>This algorithm was originally created by Oz Solomon,
        /// for his PLC line counter add-in for Visual Studio 2002/2003.</remarks>
        private void ParseVbBased(string line, ParserInfo pi)
        {
            pi.IsMultiLineComment = false;

            line = line.Trim();

            if (line.Length == 0)
            {
                pi.IsCode = false;
                pi.IsComment = false;
                return;
            }

            if (line[0] == '\'')
            {
                pi.IsCode = false;
                pi.IsComment = true;
                return;
            }

            if (line.IndexOf('\'') != -1)
            {
                pi.IsCode = true;
                pi.IsComment = true;
                return;
            }

            pi.IsCode = true;
            pi.IsComment = true;
        }

        /// <summary>
        /// Count each line in an xml source file, scanning
        /// for comments, code, and blank lines.
        /// </summary>
        /// <param name="info">The file information data to use.</param>
        /// <remarks>This algorithm is based on one created by Oz Solomon,
        /// for his PLC line counter add-in for Visual Studio 2002/2003.</remarks>
        private void GetMetricsXmlBased(FileInfo fi)
        {
            LinesOfCodeMetric loc = GetMetricByExtension(fi.Extension);

            ParserInfo pi = new ParserInfo();

            foreach (string line in GetLines(fi))
            {
                ParseXmlBased(line, pi);

                if (pi.IsComment)
                {
                    loc.Comments += 1;
                }

                if (pi.IsCode)
                {
                    loc.Code += 1;
                }

                if (!pi.IsCode && !pi.IsComment)
                {
                    loc.Blank += 1;
                }

                loc.Total += 1;
            }
        }

        private bool IsPair(char a, char b, char ch, char chNext)
        {
            return (ch == a && chNext == b);
        }

        /// <summary>
        /// Parses an xml-style code line for comments, markup, and blanks.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pi.IsMultiLineComment"></param>
        /// <param name="pi.IsCode"></param>
        /// <param name="pi.IsComment"></param>
        /// <remarks>This algorithm is based on one created by Oz Solomon,
        /// for his PLC line counter add-in for Visual Studio 2002/2003.</remarks>
        private void ParseXmlBased(string line, ParserInfo pi)
        {
            bool inString = false;

            pi.IsComment = pi.IsMultiLineComment;
            pi.IsCode = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch1 = line[i];
                char ch2 = (i < line.Length - 1 ? line[i + 1] : '\0');
                char ch3 = (i + 1 < line.Length - 1 ? line[i + 2] : '\0');
                char ch4 = (i + 2 < line.Length - 1 ? line[i + 3] : '\0');

                // Process start of XML comment
                if (IsPair('<', '!', ch1, ch2) && IsPair('-', '-', ch3, ch4) && !pi.IsMultiLineComment && !inString)
                {
                    pi.IsMultiLineComment = true;
                    pi.IsComment = true;
                    i += 3;
                }

                // Process end of XML comment
                else if (IsPair('-', '-', ch1, ch2) && ch3 == '>' && !inString)
                {
                    pi.IsMultiLineComment = false;
                    i += 2;
                }

                // Process string
                else if (ch3 == '"' && !pi.IsMultiLineComment)
                {
                    inString = !inString;
                    pi.IsCode = true;
                }

                else if (!pi.IsMultiLineComment)
                {
                    if (!char.IsWhiteSpace(ch1))
                    {
                        pi.IsCode = true;
                    }
                }
            }
        }

        private IEnumerable<string> GetLines(FileInfo fi)
        {
            return File.ReadAllLines(fi.FullName);
        }

        #endregion
    }
}