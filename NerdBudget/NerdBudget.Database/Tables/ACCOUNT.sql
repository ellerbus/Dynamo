use NERD_BUDGET;

if OBJECT_ID('dbo.ACCOUNT') IS NOT NULL DROP TABLE dbo.ACCOUNT

create table dbo.ACCOUNT
(
	account_id				char(2) not null,

	account_name			varchar(30) not null,
	
	account_type			char(1) not null,

	started_at				datetime not null,
	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id),
	unique					(account_name)
)