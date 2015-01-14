using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using EnsureThat;
using Microsoft.Win32;

namespace Dynamo.Forms
{
    /// <summary>
    /// Represents a most recently used (MRU) menu. (http://www.codeproject.com/Articles/11852/)
    /// </summary>
    /// <remarks>This class shows the MRU list in a popup menu. To display
    /// the MRU list "inline" use <see labelName="MruMenuInline" />.
    /// <para>The class will optionally load the last set of files from the registry
    /// on construction and store them when instructed by the main program.</para>
    /// <para>Internally, this class uses zero-based numbering for the items.
    /// The displayed numbers, however, will start with one.</para></remarks>
    class MostRecentlyUsedFileMenu
    {
        #region Members

        private ClickedHandler _clickedHandler;
        protected ToolStripMenuItem _recentFileMenuItem;
        protected string _registryKeyName;
        protected int _maxEntries = 4;
        protected int _maxShortenPathLength = 96;
        protected Mutex _menuStripMutex;

        #endregion

        #region MostRecentlyUsedFileItem

        /// <summary>
        /// The menu item which will contain the MRU entry.
        /// </summary>
        /// <remarks>The menu may display a shortened or otherwise invalid pathname.
        /// This class stores the actual filename, preferably as a fully
        /// resolved labelName, that will be returned in the event handler.</remarks>
        class MostRecentlyUsedFileItem : ToolStripMenuItem
        {
            /// <summary>
            /// Initializes a new instance of the MruMenuItem class.
            /// </summary>
            public MostRecentlyUsedFileItem()
            {
                Tag = "";
            }

            /// <summary>
            /// Initializes an MruMenuItem object.
            /// </summary>
            /// <param labelName="filename">The string to actually return in the <paramref labelName="eventHandler">eventHandler</paramref>.</param>
            /// <param labelName="entryname">The string that will be displayed in the menu.</param>
            /// <param labelName="eventHandler">The <see cref="EventHandler">EventHandler</see> that 
            /// handles the <see cref="MenuItem.Click">Click</see> event for this menu item.</param>
            public MostRecentlyUsedFileItem(string filename, string entryname, EventHandler eventHandler)
            {
                Tag = filename;
                Text = entryname;
                Click += eventHandler;
            }

            /// <summary>
            /// Gets the filename.
            /// </summary>
            /// <value>Gets the filename.</value>
            public string Filename
            {
                get { return (string)Tag; }
                set { Tag = value; }
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler)
            : this(recentFileMenuItem, clickedHandler, null, false, 4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        /// <param labelName="maxEntries"></param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, int maxEntries)
            : this(recentFileMenuItem, clickedHandler, null, false, maxEntries)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        /// <param labelName="registryKeyName"></param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName)
            : this(recentFileMenuItem, clickedHandler, registryKeyName, true, 4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        /// <param labelName="registryKeyName">The name or path of the registry key to use to store the MRU list and settings.</param>
        /// <param labelName="maxEntries">The maximum number of items on the MRU list.</param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, int maxEntries)
            : this(recentFileMenuItem, clickedHandler, registryKeyName, true, maxEntries)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        /// <param labelName="registryKeyName">The name or path of the registry key to use to store the MRU list and settings.</param>
        /// <param labelName="loadFromRegistry">Loads the MRU settings from the registry immediately.</param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry)
            : this(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, 4)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MruMenu class.
        /// </summary>
        /// <param labelName="recentFileMenuItem">The temporary menu item which will be replaced with the MRU list.</param>
        /// <param labelName="clickedHandler">The delegate to handle the item selection (click) event.</param>
        /// <param labelName="registryKeyName">The name or path of the registry key to use to store the MRU list and settings.</param>
        /// <param labelName="loadFromRegistry">Loads the MRU settings from the registry immediately.</param>
        /// <param labelName="maxEntries">The maximum number of items on the MRU list.</param>
        public MostRecentlyUsedFileMenu(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
        {
            Init(recentFileMenuItem, clickedHandler, registryKeyName, loadFromRegistry, maxEntries);
        }

        protected void Init(ToolStripMenuItem recentFileMenuItem, ClickedHandler clickedHandler, string registryKeyName, bool loadFromRegistry, int maxEntries)
        {
            Ensure.That(recentFileMenuItem).IsNotNull();

            MaxEntries = maxEntries;

            _recentFileMenuItem = recentFileMenuItem;

            _recentFileMenuItem.Checked = false;
            _recentFileMenuItem.Enabled = false;

            _clickedHandler = clickedHandler;

            if (registryKeyName != null)
            {
                RegistryKeyName = registryKeyName;

                if (loadFromRegistry)
                {
                    LoadFromRegistry();
                }
            }
        }

        #endregion

        #region Event Handling

        public delegate void ClickedHandler(int number, string filename);

        protected void OnClick(object sender, System.EventArgs e)
        {
            MostRecentlyUsedFileItem menuItem = (MostRecentlyUsedFileItem)sender;

            _clickedHandler(MenuItems.IndexOf(menuItem) - StartIndex, menuItem.Filename);
        }

        #endregion

        #region Properties

        public ToolStripItemCollection MenuItems { get { return _recentFileMenuItem.DropDownItems; } }

        public int StartIndex { get { return 0; } }

        public int EndIndex { get { return NumEntries; } }

        public int NumEntries { get; private set; }

        public int MaxEntries
        {
            get
            {
                return _maxEntries;
            }
            set
            {
                if (value > 16)
                {
                    _maxEntries = 16;
                }
                else
                {
                    _maxEntries = value < 4 ? 4 : value;

                    int index = StartIndex + _maxEntries;

                    while (NumEntries > _maxEntries)
                    {
                        MenuItems.RemoveAt(index);

                        NumEntries--;
                    }
                }
            }
        }

        public int MaxShortenPathLength
        {
            get { return _maxShortenPathLength; }
            set { _maxShortenPathLength = value < 16 ? 16 : value; }
        }

        #endregion

        #region Helper Methods

        protected void Enable()
        {
            _recentFileMenuItem.Enabled = true;
        }

        protected void Disable()
        {
            _recentFileMenuItem.Enabled = false;
        }

        public void SetFirstFile(int number)
        {
            if (number > 0 && NumEntries > 1 && number < NumEntries)
            {
                MostRecentlyUsedFileItem menuItem = (MostRecentlyUsedFileItem)MenuItems[StartIndex + number];

                MenuItems.RemoveAt(StartIndex + number);

                MenuItems.Insert(StartIndex, menuItem);

                FixupPrefixes(0);
            }
        }

        public static string FixupEntryname(int number, string entryname)
        {
            if (number < 9)
            {
                return "&" + (number + 1) + "  " + entryname;
            }

            if (number == 9)
            {
                return "1&0" + "  " + entryname;
            }

            return (number + 1) + "  " + entryname;
        }

        protected void FixupPrefixes(int startNumber)
        {
            if (startNumber < 0)
            {
                startNumber = 0;
            }

            if (startNumber < _maxEntries)
            {
                for (int i = StartIndex + startNumber; i < EndIndex; i++, startNumber++)
                {
                    int offset = MenuItems[i].Text.Substring(0, 3) == "1&0" ? 5 : 4;

                    MenuItems[i].Text = FixupEntryname(startNumber, MenuItems[i].Text.Substring(offset));
                }
            }
        }

        /// <summary>
        /// Shortens a pathname for display purposes.
        /// </summary>
        /// <param labelName="pathname">The pathname to shorten.</param>
        /// <param labelName="maxLength">The maximum number of characters to be displayed.</param>
        /// <remarks>Shortens a pathname by either removing consecutive components of a path
        /// and/or by removing characters from the end of the filename and replacing
        /// then with three elipses (...)
        /// <para>In all cases, the root of the passed path will be preserved in it's entirety.</para>
        /// <para>If a UNC path is used or the pathname and maxLength are particularly short,
        /// the resulting path may be longer than maxLength.</para>
        /// <para>This method expects fully resolved pathnames to be passed to it.
        /// (Use Path.GetFullPath() to obtain )</para>
        /// </remarks>
        /// <returns></returns>
        static public string ShortenPathname(string pathname, int maxLength)
        {
            if (pathname.Length <= maxLength)
            {
                return pathname;
            }

            string root = Path.GetPathRoot(pathname);

            if (root.Length > 3)
            {
                root += Path.DirectorySeparatorChar;
            }

            string[] elements = pathname.Substring(root.Length).Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int filenameIndex = elements.GetLength(0) - 1;

            if (elements.GetLength(0) == 1)
            {
                // pathname is just a root and filename

                if (elements[0].Length > 5)
                {
                    // long enough to shorten
                    // if path is a UNC path, root may be rather long
                    if (root.Length + 6 >= maxLength)
                    {
                        return root + elements[0].Substring(0, 3) + "...";
                    }
                    else
                    {
                        return pathname.Substring(0, maxLength - 3) + "...";
                    }
                }
            }
            else if ((root.Length + 4 + elements[filenameIndex].Length) > maxLength)
            {
                // pathname is just a root and filename

                root += "...\\";

                int len = elements[filenameIndex].Length;

                if (len < 6)
                {
                    return root + elements[filenameIndex];
                }

                if ((root.Length + 6) >= maxLength)
                {
                    len = 3;
                }
                else
                {
                    len = maxLength - root.Length - 3;
                }

                return root + elements[filenameIndex].Substring(0, len) + "...";
            }
            else if (elements.GetLength(0) == 2)
            {
                return root + "...\\" + elements[1];
            }
            else
            {
                int len = 0;
                int begin = 0;

                for (int i = 0; i < filenameIndex; i++)
                {
                    if (elements[i].Length > len)
                    {
                        begin = i;
                        len = elements[i].Length;
                    }
                }

                int totalLength = pathname.Length - len + 3;

                int end = begin + 1;

                while (totalLength > maxLength)
                {
                    if (begin > 0) totalLength -= elements[--begin].Length - 1;

                    if (totalLength <= maxLength) break;

                    if (end < filenameIndex) totalLength -= elements[++end].Length - 1;

                    if (begin == 0 && end == filenameIndex) break;
                }

                // assemble final string

                for (int i = 0; i < begin; i++)
                {
                    root += elements[i] + '\\';
                }

                root += "...\\";

                for (int i = end; i < filenameIndex; i++)
                {
                    root += elements[i] + '\\';
                }

                return root + elements[filenameIndex];
            }

            return pathname;
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Returns the entry number matching the passed filename.
        /// </summary>
        /// <param name="filename">The filename to search for.</param>
        /// <returns>The entry number of the matching filename or -1 if not found.</returns>
        public int FindFilenameNumber(string filename)
        {
            Ensure.That(filename).IsNotNullOrEmpty();

            if (NumEntries > 0)
            {
                int number = 0;

                for (int i = StartIndex; i < EndIndex; i++, number++)
                {
                    if (string.Compare(((MostRecentlyUsedFileItem)MenuItems[i]).Filename, filename, true) == 0)
                    {
                        return number;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the menu index of the passed filename.
        /// </summary>
        /// <param name="filename">The filename to search for.</param>
        /// <returns>The menu index of the matching filename or -1 if not found.</returns>
        public int FindFilenameMenuIndex(string filename)
        {
            int number = FindFilenameNumber(filename);
            return number < 0 ? -1 : StartIndex + number;
        }

        /// <summary>
        /// Returns the menu index for a specified MRU item number.
        /// </summary>
        /// <param name="number">The MRU item number.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <returns>The menu index of the passed MRU number.</returns>
        public int GetMenuIndex(int number)
        {
            if (number < 0 || number >= NumEntries)
                throw new ArgumentOutOfRangeException("number");

            return StartIndex + number;
        }

        public string GetFileAt(int number)
        {
            if (number < 0 || number >= NumEntries)
                throw new ArgumentOutOfRangeException("number");

            return ((MostRecentlyUsedFileItem)MenuItems[StartIndex + number]).Filename;
        }

        public string[] GetFiles()
        {
            string[] filenames = new string[NumEntries];

            int index = StartIndex;
            for (int i = 0; i < filenames.GetLength(0); i++, index++)
            {
                filenames[i] = ((MostRecentlyUsedFileItem)MenuItems[index]).Filename;
            }

            return filenames;
        }

        // This is used for testing
        public string[] GetFilesFullEntrystring()
        {
            string[] filenames = new string[NumEntries];

            int index = StartIndex;
            for (int i = 0; i < filenames.GetLength(0); i++, index++)
            {
                filenames[i] = MenuItems[index].Text;
            }

            return filenames;
        }
        #endregion

        #region Add Methods

        public void SetFiles(string[] filenames)
        {
            RemoveAll();

            for (int i = filenames.GetLength(0) - 1; i >= 0; i--)
            {
                AddFile(filenames[i]);
            }
        }

        public void AddFiles(string[] filenames)
        {
            for (int i = filenames.GetLength(0) - 1; i >= 0; i--)
            {
                AddFile(filenames[i]);
            }
        }

        public void AddFile(string filename)
        {
            string pathname = Path.GetFullPath(filename);

            AddFile(pathname, ShortenPathname(pathname, MaxShortenPathLength));
        }

        public void AddFile(string filename, string entryname)
        {
            Ensure.That(filename).IsNotNullOrEmpty();

            if (NumEntries > 0)
            {
                int index = FindFilenameMenuIndex(filename);

                if (index >= 0)
                {
                    SetFirstFile(index - StartIndex);

                    return;
                }
            }

            if (NumEntries < _maxEntries)
            {
                MostRecentlyUsedFileItem menuItem = new MostRecentlyUsedFileItem(filename, FixupEntryname(0, entryname), new System.EventHandler(OnClick));

                MenuItems.Insert(StartIndex, menuItem);

                if (NumEntries++ == 0)
                {
                    Enable();
                }
                else
                {
                    FixupPrefixes(1);
                }
            }
            else if (NumEntries > 1)
            {
                MostRecentlyUsedFileItem menuItem = (MostRecentlyUsedFileItem)MenuItems[StartIndex + NumEntries - 1];

                MenuItems.RemoveAt(StartIndex + NumEntries - 1);

                menuItem.Text = FixupEntryname(0, entryname);

                menuItem.Filename = filename;

                MenuItems.Insert(StartIndex, menuItem);

                FixupPrefixes(1);
            }
        }

        #endregion

        #region Remove Methods

        public void RemoveFile(int number)
        {
            if (number >= 0 && number < NumEntries)
            {
                if (--NumEntries == 0)
                {
                    Disable();
                }
                else
                {
                    int startIndex = StartIndex;

                    MenuItems.RemoveAt(startIndex + number);

                    if (number < NumEntries)
                    {
                        FixupPrefixes(number);
                    }
                }
            }
        }

        public void RemoveFile(string filename)
        {
            if (NumEntries > 0)
            {
                RemoveFile(FindFilenameNumber(filename));
            }
        }

        public void RemoveAll()
        {
            if (NumEntries > 0)
            {
                // remove all items in the sub menu
                MenuItems.Clear();

                Disable();

                NumEntries = 0;
            }
        }

        #endregion

        #region Rename Methods

        public void RenameFile(string oldFilename, string newFilename)
        {
            string newPathname = Path.GetFullPath(newFilename);

            RenameFile(Path.GetFullPath(oldFilename), newPathname, ShortenPathname(newPathname, MaxShortenPathLength));
        }

        public void RenameFile(string oldFilename, string newFilename, string newEntryname)
        {
            Ensure.That(oldFilename).IsNotNullOrEmpty();
            Ensure.That(newFilename).IsNotNullOrEmpty();

            if (NumEntries > 0)
            {
                int index = FindFilenameMenuIndex(oldFilename);

                if (index >= 0)
                {
                    MostRecentlyUsedFileItem menuItem = (MostRecentlyUsedFileItem)MenuItems[index];

                    menuItem.Text = FixupEntryname(0, newEntryname);

                    menuItem.Filename = newFilename;

                    return;
                }
            }

            AddFile(newFilename, newEntryname);
        }

        #endregion

        #region Registry Methods

        public string RegistryKeyName
        {
            get
            {
                return _registryKeyName;
            }
            set
            {
                if (_menuStripMutex != null)
                {
                    _menuStripMutex.Close();
                }

                _registryKeyName = value.Trim();

                if (_registryKeyName.Length == 0)
                {
                    _registryKeyName = null;

                    _menuStripMutex = null;
                }
                else
                {
                    string mutexName = _registryKeyName.Replace('\\', '_').Replace('/', '_') + "Mutex";

                    _menuStripMutex = new Mutex(false, mutexName);
                }
            }
        }

        public void LoadFromRegistry(string keyName)
        {
            RegistryKeyName = keyName;

            LoadFromRegistry();
        }

        public void LoadFromRegistry()
        {
            if (_registryKeyName != null)
            {
                _menuStripMutex.WaitOne();

                RemoveAll();

                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(_registryKeyName);

                if (regKey != null)
                {
                    _maxEntries = (int)regKey.GetValue("max", _maxEntries);

                    for (int number = _maxEntries; number > 0; number--)
                    {
                        string filename = (string)regKey.GetValue("File" + number.ToString());
                        if (filename != null)
                            AddFile(filename);
                    }

                    regKey.Close();
                }

                _menuStripMutex.ReleaseMutex();
            }
        }

        public void SaveToRegistry(string keyName)
        {
            RegistryKeyName = keyName;

            SaveToRegistry();
        }

        public void SaveToRegistry()
        {
            if (_registryKeyName != null)
            {
                _menuStripMutex.WaitOne();

                RegistryKey regKey = Registry.CurrentUser.CreateSubKey(_registryKeyName);

                if (regKey != null)
                {
                    regKey.SetValue("max", _maxEntries);

                    int number = 1;

                    int i = StartIndex;

                    for (; i < EndIndex; i++, number++)
                    {
                        regKey.SetValue("File" + number.ToString(), ((MostRecentlyUsedFileItem)MenuItems[i]).Filename);
                    }

                    for (; number <= 16; number++)
                    {
                        regKey.DeleteValue("File" + number.ToString(), false);
                    }

                    regKey.Close();
                }

                _menuStripMutex.ReleaseMutex();
            }
        }

        #endregion
    }
}
