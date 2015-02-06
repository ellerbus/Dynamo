
if object_id('dbo.BalanceUpsertMany', 'P') is not null drop procedure dbo.BalanceUpsertMany
go

create procedure dbo.BalanceUpsertMany
	@items                          dbo.BalanceTableType readonly
as

update	t
set		t.amount                      = x.amount,
		t.created_at                  = x.created_at,
		t.updated_at                  = x.updated_at
from	dbo.BALANCE t inner join @items x
			on	x.account_id          = t.account_id
			and	x.as_of               = t.as_of

insert into dbo.BALANCE
	(
		account_id,
		as_of,
		amount,
		created_at,
		updated_at
	)
	select	x.account_id,
			x.as_of,
			x.amount,
			x.created_at,
			x.updated_at
	from	@items x left join dbo.BALANCE t
				on	x.account_id          = t.account_id
				and	x.as_of               = t.as_of
				and	t.account_id          is null
go
