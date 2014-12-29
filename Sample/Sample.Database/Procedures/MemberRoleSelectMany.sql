
if object_id('dbo.MemberRoleSelectMany', 'P') is not null drop procedure dbo.MemberRoleSelectMany
go

create procedure dbo.MemberRoleSelectMany
as

select	*
from	dbo.MemberRole

go
