﻿use NERD_BUDGET;

if OBJECT_ID('dbo.MAP') IS NOT NULL DROP TABLE dbo.MAP

create table dbo.MAP
(
	account_id				char(2) not null,
	category_id				char(2) not null,
	budget_id				char(2) not null,
	map_id					int not null,
	
	regex_pattern			varchar(500) not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, category_id, budget_id, map_id),
	foreign key				(account_id, category_id, budget_id)
							references BUDGET (account_id, category_id, budget_id)
							on delete cascade
							on update cascade
)