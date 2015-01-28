if OBJECT_ID('dbo.sp_PRUNE_ACCOUNTS', 'P') is not null drop procedure dbo.sp_PRUNE_ACCOUNTS
go

create procedure dbo.sp_PRUNE_ACCOUNTS
	@cutoff datetime
as

	delete
	from	dbo.LEDGER
	where	ledger_date < @cutoff

	delete
	from	dbo.BALANCE
	where	as_of < @cutoff

	delete
	from	dbo.MAP
	where	updated_at is null
	  and	created_at < @cutoff

	delete
	from	dbo.MAP
	where	updated_at is not null
	  and	updated_at < @cutoff

	delete
	from	dbo.ADJUSTMENT
	where	adjustment_date < @cutoff

go
