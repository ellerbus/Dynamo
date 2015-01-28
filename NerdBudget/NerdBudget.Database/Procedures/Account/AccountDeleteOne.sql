
if object_id('dbo.AccountDeleteOne', 'P') is not null drop procedure dbo.AccountDeleteOne
go

create procedure dbo.AccountDeleteOne
	@account_id                     char(2)
as

delete
from	dbo.ACCOUNT
where	account_id                  = @account_id

go
