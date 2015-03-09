
if type_id('dbo.BudgetTableType') is not null drop type  dbo.BudgetTableType
go

create type dbo.BudgetTableType as table
(
	account_id                     char(2),
	budget_id                      char(2),
	category_id                    char(2),
	budget_name                    varchar(30),
	budget_frequency               char(2),
	sequence                       int,
	start_date                     datetime,
	end_date                       datetime,
	amount                         float,
	created_at                     datetime,
	updated_at                     datetime
)

go
