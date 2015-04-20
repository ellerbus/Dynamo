
if object_id('dbo.AccountSelectMany', 'P') is not null drop procedure dbo.AccountSelectMany
go

create procedure dbo.AccountSelectMany
as
--	
--	CLEAN UP - WIPE OUT ANYTHING OLDER THAN 2 MONTHS AGO
--

declare @cutoff datetime

set	@cutoff = DATEADD(month, DATEDIFF(month, 0, dateadd(month, -2, getdate())), 0)


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

--
--	GRAB A LIST OF ACCOUNTS
--
select	*
from	dbo.ACCOUNT

go
