use NERD_BUDGET;

if OBJECT_ID('dbo.LEDGER') IS NOT NULL DROP TABLE dbo.LEDGER

create table dbo.LEDGER
(
	account_id				char(2) not null references ACCOUNT(account_id) on delete cascade,
	ledger_id				char(8) not null,
	ledger_date				datetime not null,

	budget_id				char(2) null,

	original_text			varchar(500) not null,
	description				varchar(100) not null,
	amount					float not null,
	balance					float not null,

	sequence				int not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, ledger_id, ledger_date)
)