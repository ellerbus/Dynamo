
if object_id('dbo.MemberRoleDeleteMany', 'P') is not null drop procedure dbo.MemberRoleDeleteMany
go

create procedure dbo.MemberRoleDeleteMany
	@items                          MemberRoleTableType readonly
as

delete	t
from	dbo.MemberRole t,
		@items x
where	t.memberID                    = x.memberID
  and	t.roleID                      = x.roleID

go
