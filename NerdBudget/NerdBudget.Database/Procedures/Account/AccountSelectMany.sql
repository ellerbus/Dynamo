
if object_id('dbo.AccountSelectMany', 'P') is not null drop procedure dbo.AccountSelectMany
go

create procedure dbo.AccountSelectMany
as

select	*
from	dbo.ACCOUNT

go
