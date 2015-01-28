
if type_id('dbo.CategoryTableType') is not null drop type  dbo.CategoryTableType
go

create type dbo.CategoryTableType as table
(
	account_id                     char(2),
	category_id                    char(2),
	category_name                  varchar(30),
	multiplier                     int,
	sequence                       int,
	created_at                     datetime,
	updated_at                     datetime
)

go
