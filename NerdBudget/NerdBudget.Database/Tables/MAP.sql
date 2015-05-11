use NERD_BUDGET;

if OBJECT_ID('dbo.MAP') IS NOT NULL DROP TABLE dbo.MAP

create table dbo.MAP
(
	account_id				char(2) not null,
	map_id					char(8) not null,

	budget_id				char(2) not null,
	
	regex_pattern			varchar(750) not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, map_id),
	foreign key				(account_id, budget_id)
							references BUDGET (account_id, budget_id)
							on delete cascade
							on update cascade
)