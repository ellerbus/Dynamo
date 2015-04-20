use NERD_BUDGET;

if OBJECT_ID('dbo.ADJUSTMENT') IS NOT NULL DROP TABLE dbo.ADJUSTMENT

create table dbo.ADJUSTMENT
(
	account_id				char(2) not null,
	adjustment_id			char(2) not null,

	budget_id				char(2) not null,

	adjustment_name			varchar(60) not null,
	adjustment_date			datetime null,
	amount					float not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, adjustment_id),
	foreign key				(account_id, budget_id)
							references BUDGET (account_id, budget_id)
							on delete cascade
							on update cascade
)