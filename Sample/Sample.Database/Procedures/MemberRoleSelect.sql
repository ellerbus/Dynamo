
if object_id('dbo.MemberRoleSelect', 'P') is not null drop procedure dbo.MemberRoleSelect
go

create procedure dbo.MemberRoleSelect
	@memberID                       int,
	@roleID                         int
as

select	*
from	dbo.MemberRole
where	memberID                    = @memberID
  and	roleID                      = @roleID

go
