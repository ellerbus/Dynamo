using System;
using Insight.Database;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	public partial class Role
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'roleID' (primary key)
		///	</summary>
		[Column("roleID")]
		public virtual int Id { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'roleName'
		///	</summary>
		[Column("roleName")]
		public virtual string Name { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'createdAt'
		///	</summary>
		[Column("createdAt")]
		public virtual DateTime CreatedAt { get; set; }

		
		///	<summary>
		///	Gets / Sets database column 'updatedAt'
		///	</summary>
		[Column("updatedAt")]
		public virtual DateTime? UpdatedAt { get; set; }

		
		#endregion
	}
}