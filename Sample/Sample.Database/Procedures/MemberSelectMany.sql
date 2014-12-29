
if object_id('dbo.MemberSelectMany', 'P') is not null drop procedure dbo.MemberSelectMany
go

create procedure dbo.MemberSelectMany
as

select	*
from	dbo.Member

go
