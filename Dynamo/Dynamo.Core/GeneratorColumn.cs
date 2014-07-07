using DatabaseSchemaReader.DataSchema;

namespace Dynamo.Core
{
    public sealed class GeneratorColumn
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public GeneratorColumn(DatabaseColumn column)
            : this(column.Name)
        {
            IsIdentity = column.IsAutoNumber;

            IsPrimaryKey = column.IsPrimaryKey;

            IsUniqueKey = column.IsUniqueKey;

            IsForeignKey = column.IsForeignKey;

            IsNullable = column.Nullable;

            Ordinal = column.Ordinal;

            Length = column.Length ?? 0;

            Precision = column.Precision ?? 0;

            Scale = column.Scale ?? 0;

            SqlType = column.DbDataType;

            ClrType = column.DataType.NetDataTypeCSharpName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public GeneratorColumn(string name)
        {
            Name = name;
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
        public string ShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUniqueKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SqlType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClrType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Scale { get; set; }

        #endregion
    }
}
