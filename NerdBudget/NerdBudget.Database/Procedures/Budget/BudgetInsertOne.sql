
if object_id('dbo.BudgetInsertOne', 'P') is not null drop procedure dbo.BudgetInsertOne
go

create procedure dbo.BudgetInsertOne
	@account_id                     char(2),
	@budget_id                      char(2),
	@category_id                    char(2),
	@budget_name                    varchar(30),
	@budget_frequency               char(2),
	@sequence                       int,
	@start_date                     datetime,
	@end_date                       datetime,
	@amount                         float,
	@created_at                     datetime,
	@updated_at                     datetime
as

insert into dbo.BUDGET
	(
		account_id,
		budget_id,
		category_id,
		budget_name,
		budget_frequency,
		sequence,
		start_date,
		end_date,
		amount,
		created_at,
		updated_at
	)
	values
	(
		@account_id,
		@budget_id,
		@category_id,
		@budget_name,
		@budget_frequency,
		@sequence,
		@start_date,
		@end_date,
		@amount,
		@created_at,
		@updated_at
	)
	




go
