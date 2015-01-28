use NERD_BUDGET;

if OBJECT_ID('dbo.BUDGET') IS NOT NULL DROP TABLE dbo.BUDGET

create table dbo.BUDGET
(
	account_id				char(2) not null,
	budget_id				char(2) not null,

	category_id				char(2) not null,

	budget_name				varchar(30) not null,
	budget_frequency		char(2) not null,
	sequence				int not null,
	
	start_date				datetime null,
	end_date				datetime null,
	amount					float not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, budget_id),
	unique					(account_id, budget_name),
	foreign key				(account_id, category_id)
							references CATEGORY (account_id, category_id)
							on delete cascade
							on update cascade
)