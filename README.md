# Dynamo

Dynamo is a friendly generator that uses [DB Schema Reader](http://dbschemareader.codeplex.com/)
and [DotLiquid Syntax](https://github.com/formosatek/dotliquid) to generate code files.

#### Quick Start

For those that simply what to jump in and check things out download and view the [Sample
Solution](https://github.com/ellerbus/Dynamo/tree/master/Sample).

#### Helpful Pages

- [Dynamo's API](https://github.com/ellerbus/Dynamo/wiki/Dynamo-API)
- [Dynamo's Filters](https://github.com/ellerbus/Dynamo/wiki/Dynamo-Filters)
- [Provided Templates](https://github.com/ellerbus/Dynamo/tree/master/Templates)

#### SQL Databases Currently Supported
via [DB Schema Reader](http://dbschemareader.codeplex.com/)
- SqlServer
- SqlServer CE 4
- MySQL
- SQLite
- ODP
- Devar
- PostgreSql
- Db2
- System.Data.OracleClient


#### Template Sample (SQL Schema, Template, & Final Code File)

``` sql
CREATE TABLE MEMBER
(
	member_id		int not null,
	member_name		varchar(50) not null,
	primary key		(member_id),
	unique			(member_name)
)
```

```
{% capture BASECLASS %}{{ table.name | pascal }}{% endcapture -%}
{% capture PROJECT %}{{ SOLUTION }}.WebProject{% endcapture -%}
{% capture FILENAME %}{{ PROJECT }}\Models\Generated\{{ BASECLASS }}.cs{% endcapture -%}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace {{ PROJECT }}.Models
{
	///	<summary>
	///	</summary>
	[Table("{{ table.name }}")]
	public partial class {{ BASECLASS }}
	{
		#region Primary Key Properties
		{% for column in table.primary_keys %}
		///	<summary>
		///	Gets / Sets database column '{{ column.name }}' (primary key)
		///	</summary>
		[Column("{{ column.name }}"), Key]
		public {{ column.clr_type }} {{ column.name | pascal | remove:BASECLASS }} { get; set; }
		
		{% endfor -%}
		
		#endregion
		
		#region Properties
		{% for column in table.columns %}{% if column.is_primary_key == false %}
		///	<summary>
		///	Gets / Sets database column '{{ column.name }}'
		///	</summary>
		[Column("{{ column.name }}")]
		public {{ column.clr_type }} {{ column.name | pascal }} { get; set; }

		{% endif %}{% endfor -%}

		#endregion
	}
}
```


``` csharp
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySolution.WebProject.Models
{
	///	<summary>
	///	</summary>
	[Table("MEMBER")]
	public partial class Member
	{
		#region Primary Key Properties
		
		///	<summary>
		///	Gets / Sets database column 'member_id' (primary key)
		///	</summary>
		[Column("member_id"), Key]
		public int Id { get; set; }
		
		#endregion
		
		#region Properties
		
		///	<summary>
		///	Gets / Sets database column 'member_name'
		///	</summary>
		[Column("member_name")]
		public string MemberName { get; set; }  // notice we didn't use remove:BASECLASS here

		#endregion
	}
}
```

#### Screen Shots

Code Generator

![Code Generator](/docs/CodeGenerator.png)

Connection String Builder

![Connection String Builder](/docs/ConnectionStringBuilder.png)


