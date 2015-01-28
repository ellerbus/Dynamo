
if object_id('dbo.AccountSelectOne', 'P') is not null drop procedure dbo.AccountSelectOne
go

create procedure dbo.AccountSelectOne
	@account_id                     char(2)
as

select	*
from	dbo.ACCOUNT
where	account_id                  = @account_id

select	*
from	dbo.CATEGORY
where	account_id                  = @account_id

go
