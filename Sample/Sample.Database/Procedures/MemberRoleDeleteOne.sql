
if object_id('dbo.MemberRoleDeleteOne', 'P') is not null drop procedure dbo.MemberRoleDeleteOne
go

create procedure dbo.MemberRoleDeleteOne
	@memberID                       int,
	@roleID                         int
as

delete
from	dbo.MemberRole
where	memberID                    = @memberID
  and	roleID                      = @roleID

go
