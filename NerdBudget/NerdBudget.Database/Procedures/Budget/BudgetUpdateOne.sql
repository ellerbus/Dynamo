
if object_id('dbo.BudgetUpdateOne', 'P') is not null drop procedure dbo.BudgetUpdateOne
go

create procedure dbo.BudgetUpdateOne
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

update	dbo.BUDGET
set		category_id                 = @category_id,
		budget_name                 = @budget_name,
		budget_frequency            = @budget_frequency,
		sequence                    = @sequence,
		start_date                  = @start_date,
		end_date                    = @end_date,
		amount                      = @amount,
		created_at                  = @created_at,
		updated_at                  = @updated_at
where	account_id                  = @account_id
  and	budget_id                   = @budget_id

go
