
if object_id('dbo.AccountSelectOne', 'P') is not null drop procedure dbo.AccountSelectOne
go

create procedure dbo.AccountSelectOne
	@account_id                     char(2)
as

select	*
from	dbo.ACCOUNT
where	account_id                  = @account_id

select	*
from	dbo.BALANCE
where	account_id                  = @account_id

select	*
from	dbo.CATEGORY
where	account_id                  = @account_id

select	*
from	dbo.BUDGET
where	account_id                  = @account_id

select	*
from	dbo.ADJUSTMENT
where	account_id                  = @account_id

select	*
from	dbo.MAP
where	account_id                  = @account_id

select	*
from	dbo.LEDGER
where	account_id                  = @account_id

go
