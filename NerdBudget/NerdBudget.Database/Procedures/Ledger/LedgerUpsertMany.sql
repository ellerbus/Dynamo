
if object_id('dbo.LedgerUpsertMany', 'P') is not null drop procedure dbo.LedgerUpsertMany
go

create procedure dbo.LedgerUpsertMany
	@items                          dbo.LedgerTableType readonly
as

update	t
set		t.budget_id                   = x.budget_id,
		t.original_text               = x.original_text,
		t.description                 = x.description,
		t.amount                      = x.amount,
		t.balance                     = x.balance,
		t.sequence                    = x.sequence,
		t.created_at                  = x.created_at,
		t.updated_at                  = x.updated_at
from	dbo.LEDGER t inner join @items x
			on	x.account_id          = t.account_id
			and	x.ledger_id           = t.ledger_id
			and	x.ledger_date         = t.ledger_date

insert into dbo.LEDGER
	(
		account_id,
		ledger_id,
		ledger_date,
		budget_id,
		original_text,
		description,
		amount,
		balance,
		sequence,
		created_at,
		updated_at
	)
	select	x.account_id,
			x.ledger_id,
			x.ledger_date,
			x.budget_id,
			x.original_text,
			x.description,
			x.amount,
			x.balance,
			x.sequence,
			x.created_at,
			x.updated_at
	from	@items x left join dbo.LEDGER t
				on	x.account_id          = t.account_id
				and	x.ledger_id           = t.ledger_id
				and	x.ledger_date         = t.ledger_date
	where	t.account_id              is null
go
