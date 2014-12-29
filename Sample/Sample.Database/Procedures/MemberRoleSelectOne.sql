
if object_id('dbo.MemberRoleSelectOne', 'P') is not null drop procedure dbo.MemberRoleSelectOne
go

create procedure dbo.MemberRoleSelectOne
	@memberID                       int,
	@roleID                         int
as

select	*
from	dbo.MemberRole
where	memberID                    = @memberID
  and	roleID                      = @roleID

go
