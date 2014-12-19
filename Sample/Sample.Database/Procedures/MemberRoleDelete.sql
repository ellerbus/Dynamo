
if object_id('dbo.MemberRoleDelete', 'P') is not null drop procedure dbo.MemberRoleDelete
go

create procedure dbo.MemberRoleDelete
	@memberID                       int,
	@roleID                         int
as

delete
from	dbo.MemberRole
where	memberID                    = @memberID
  and	roleID                      = @roleID

go
