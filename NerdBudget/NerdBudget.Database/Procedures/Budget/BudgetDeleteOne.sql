
if object_id('dbo.BudgetDeleteOne', 'P') is not null drop procedure dbo.BudgetDeleteOne
go

create procedure dbo.BudgetDeleteOne
	@account_id                     char(2),
	@budget_id                      char(2)
as

delete
from	dbo.BUDGET
where	account_id                  = @account_id
  and	budget_id                   = @budget_id

go
