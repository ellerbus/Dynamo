using System.Collections.Generic;
using System.Linq;
using DatabaseSchemaReader.DataSchema;

namespace Dynamo.Core
{
    public sealed class GeneratorTable
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GeneratorTable(DatabaseTable table)
            : this(table.Name)
        {
            foreach (DatabaseColumn c in table.Columns)
            {
                Columns.Add(new GeneratorColumn(c));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GeneratorTable(string name)
        {
            Name = name;

            Columns = new GeneratorColumnCollection();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public GeneratorColumnCollection Columns { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasIdentity
        {
            get
            {
                if (_hasIdentity == null)
                {
                    _hasIdentity = Columns.Any(x => x.IsIdentity && x.IsPrimaryKey);
                }
                return _hasIdentity.Value;
            }
        }
        private bool? _hasIdentity;

        /// <summary>
        /// An Identity in SqlNOX schema must be IsIdentity &amp; IsPrimaryKey
        /// </summary>
        public GeneratorColumn Identity
        {
            get
            {
                if (!HasIdentity)
                {
                    return null;
                }

                if (_identity == null)
                {
                    _identity = Columns.SingleOrDefault(x => x.IsIdentity && x.IsPrimaryKey);
                }

                return _identity;
            }
        }
        private GeneratorColumn _identity;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<GeneratorColumn> PrimaryKeys { get { return Columns.Where(x => x.IsPrimaryKey); } }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<GeneratorColumn> UniqueKeys { get { return Columns.Where(x => x.IsUniqueKey); } }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<GeneratorColumn> ForeignKeys { get { return Columns.Where(x => x.IsForeignKey); } }

        #endregion
    }
}
