
if type_id('dbo.MapTableType') is not null drop type  dbo.MapTableType
go

create type dbo.MapTableType as table
(
	account_id                     char(2),
	map_id                         char(8),
	budget_id                      char(2),
	regex_pattern                  varchar(750),
	created_at                     datetime,
	updated_at                     datetime
)

go
