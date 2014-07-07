using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo.Core
{
    public sealed class FileDisplay
    {
        public string Display
        {
            get
            {
                if (FileInfo.Exists)
                {
                    return FileInfo.Name;
                }

                return "n/a";
            }
        }

        public string FileName
        {
            get
            {
                return FileInfo.FullName;
            }
        }

        public FileInfo FileInfo { get; set; }
    }
}
