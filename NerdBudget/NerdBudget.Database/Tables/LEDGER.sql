use NERD_BUDGET;

if OBJECT_ID('dbo.LEDGER') IS NOT NULL DROP TABLE dbo.LEDGER

create table dbo.LEDGER
(
	account_id				char(2) not null references ACCOUNT(account_id) on delete cascade,
	category_id				char(2) null,
	budget_id				char(2) null,

	ledger_id				uniqueidentifier not null,

	ledger_date				datetime not null,

	ledger_description		varchar(250) not null,

	ledger_amount			float not null,
	ledger_balance			float not null,

	sequence				int not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, ledger_id, ledger_date)
)

create index IX_LEDGER on LEDGER (account_id, ledger_date)