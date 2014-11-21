using System;
using Insight.Database;

namespace Sample.Core.Models
{
	///	<summary>
	///
	///	</summary>
	public partial class MemberVisitHistory
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'memberID' (primary key)
		///	</summary>
		[Column("memberID")]
		public virtual int MemberId { get; set; }
		
		
		///	<summary>
		///	Gets / Sets database column 'visitedAt' (primary key)
		///	</summary>
		[Column("visitedAt")]
		public virtual DateTime VisitedAt { get; set; }
		
				
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'pageUrl'
		///	</summary>
		[Column("pageUrl")]
		public virtual string PageUrl { get; set; }

		
		#endregion
	}
}