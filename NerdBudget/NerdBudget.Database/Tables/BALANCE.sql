use NERD_BUDGET;

if OBJECT_ID('dbo.BALANCE') IS NOT NULL DROP TABLE dbo.BALANCE

create table dbo.BALANCE
(
	account_id				char(2) not null references ACCOUNT(account_id) on delete cascade,
	as_of					datetime not null,

	amount					float not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, as_of)
)