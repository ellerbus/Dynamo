use NERD_BUDGET;

if OBJECT_ID('dbo.CATEGORY') IS NOT NULL DROP TABLE dbo.CATEGORY

create table dbo.CATEGORY
(
	account_id				char(2) not null references ACCOUNT(account_id) on delete cascade,
	category_id				char(2) not null,

	category_name			varchar(30) not null,
	
	multiplier				int not null,
	sequence				int not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, category_id),
	unique					(account_id, category_name)
)