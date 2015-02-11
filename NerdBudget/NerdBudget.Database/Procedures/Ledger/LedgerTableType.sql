
if type_id('dbo.LedgerTableType') is not null drop type  dbo.LedgerTableType
go

create type dbo.LedgerTableType as table
(
	account_id                     char(2),
	ledger_id                      char(8),
	ledger_date                    datetime,
	budget_id                      char(2),
	original_text                  varchar(500),
	description                    varchar(100),
	amount                         float,
	balance                        float,
	sequence                       int,
	created_at                     datetime,
	updated_at                     datetime
)

go
