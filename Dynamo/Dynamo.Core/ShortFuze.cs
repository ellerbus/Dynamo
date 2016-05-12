using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Augment;

namespace Dynamo.Core
{
    public sealed class ShortFuze
    {
        #region Private Member Variables

        private const string _shortFuzePackage = "ShortFuze.zip";
        private readonly Regex _regexPlaceholder = new Regex("ShortFuze", RegexOptions.IgnoreCase);
        private readonly Regex _regexSourceControl = new Regex(@"\t*GlobalSection\(TeamFoundationVersionControl\)[^\r\n]+\r\n((\t*Scc[^\r\n]*)\r\n)*\t*EndGlobalSection\r\n", RegexOptions.IgnoreCase);

        private string _solutionPath;
        private string _solutionName;

        private BackgroundWorker _backgroundWorker;

        #endregion

        #region Constructors

        public ShortFuze(string path, BackgroundWorker backgroundWorker)
        {
            _solutionPath = Path.GetDirectoryName(path);
            _solutionName = Path.GetFileNameWithoutExtension(path);

            _backgroundWorker = backgroundWorker;
        }

        #endregion

        #region Methods

        public void CreateSolution()
        {
            EnsurePath(_solutionPath);

            string thisPath = AppDomain.CurrentDomain.BaseDirectory;

            string package = Path.Combine(thisPath, _shortFuzePackage);

            Debug.WriteLine("ShortFuze Package=[{0}]".FormatArgs(package));

            using (ZipArchive archive = new ZipArchive(File.Open(package, FileMode.Open), ZipArchiveMode.Read))
            {
                int progress = 0;

                int total = archive.Entries.Count;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    _backgroundWorker.ReportProgress((++progress * 100) / total);

                    UnZip(entry);
                }
            }
        }

        private void UnZip(ZipArchiveEntry entry)
        {
            string filename = Translate(entry.FullName);

            Debug.WriteLine("Extracting Name=[{0}] To=[{1}]".FormatArgs(entry.FullName, filename));

            filename = Path.Combine(_solutionPath, filename);

            EnsurePath(Path.GetDirectoryName(filename));

            entry.ExtractToFile(filename);

            if (IsTextFile(filename))
            {
                UpdateContents(filename);
            }
        }

        private void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private bool IsTextFile(string path)
        {
            string binaryExtensions = @"(exe|dll|mdf|ldf|pdb|png|gif|jpg|jpeg)$";

            return !Regex.IsMatch(path, binaryExtensions);
        }

        private void UpdateContents(string path)
        {
            Debug.WriteLine("Updating Contents File=[{0}]".FormatArgs(path));

            string text = Translate(File.ReadAllText(path));

            if (path.EndsWith("sln"))
            {
                Debug.WriteLine("Removing Source Control File=[{0}]".FormatArgs(path));

                text = _regexSourceControl.Replace(text, string.Empty);
            }

            if (path.EndsWith("WebApp.csproj"))
            {
                Debug.WriteLine("Randomizing Website-Port File=[{0}]".FormatArgs(path));

                // Get a random number between 10000 - 65535
                int port = new Random().Next(10000, 65535);

                string[] patterns = new[]
                {
                    @"<DevelopmentServerPort>{0}</DevelopmentServerPort>",
                    @"<IISUrl>http://localhost:{0}/</IISUrl>"
                };

                foreach (string pattern in patterns)
                {
                    text = Regex.Replace(text, pattern.FormatArgs(@"\d+"), pattern.FormatArgs(port));
                }
            }

            File.WriteAllText(path, text);
        }

        private string Translate(string text)
        {
            //  ShortFuze to _solutionName
            text = _regexPlaceholder.Replace(text, _solutionName);

            return text;
        }

        #endregion
    }
}
