
if object_id('dbo.BudgetUpdateMany', 'P') is not null drop procedure dbo.BudgetUpdateMany
go

create procedure dbo.BudgetUpdateMany
	@items                          dbo.BudgetTableType readonly
as

update	t
set		t.category_id                 = x.category_id,
		t.budget_name                 = x.budget_name,
		t.budget_frequency            = x.budget_frequency,
		t.sequence                    = x.sequence,
		t.start_date                  = x.start_date,
		t.end_date                    = x.end_date,
		t.amount                      = x.amount,
		t.created_at                  = x.created_at,
		t.updated_at                  = x.updated_at
from	dbo.BUDGET t inner join @items x
			on	x.account_id          = t.account_id
			and	x.budget_id           = t.budget_id

go
