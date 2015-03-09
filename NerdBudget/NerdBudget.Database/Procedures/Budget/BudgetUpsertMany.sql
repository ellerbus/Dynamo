
if object_id('dbo.BudgetUpsertMany', 'P') is not null drop procedure dbo.BudgetUpsertMany
go

create procedure dbo.BudgetUpsertMany
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
	select	x.account_id,
			x.budget_id,
			x.category_id,
			x.budget_name,
			x.budget_frequency,
			x.sequence,
			x.start_date,
			x.end_date,
			x.amount,
			x.created_at,
			x.updated_at
	from	@items x left join dbo.BUDGET t
				on	x.account_id          = t.account_id
				and	x.budget_id           = t.budget_id
	where	t.account_id              is null
go
