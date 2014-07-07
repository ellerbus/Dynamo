using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NOX;
using Dynamo.Core;

namespace Dynamo.Forms
{
    partial class ConnectionStringBuilderForm : Form
    {
        #region Constructors

        public ConnectionStringBuilderForm()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;
        }

        public ConnectionStringBuilderForm(DbConnectionStringBuilder builder)
            : this()
        {
            dictionaryPairBindingSource.DataSource = new DictionaryBindingList(builder);
        }

        #endregion
    }
}