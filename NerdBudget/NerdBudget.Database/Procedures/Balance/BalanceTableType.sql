
if type_id('dbo.BalanceTableType') is not null drop type  dbo.BalanceTableType
go

create type dbo.BalanceTableType as table
(
	account_id                     char(2),
	as_of                          datetime,
	amount                         float,
	created_at                     datetime,
	updated_at                     datetime
)

go
