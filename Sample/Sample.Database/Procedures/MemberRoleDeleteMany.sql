
if object_id('dbo.MemberRoleDeleteMany', 'P') is not null drop procedure dbo.MemberRoleDeleteMany
go

create procedure dbo.MemberRoleDeleteMany
	@items                          dbo.MemberRoleTableType readonly
as

delete	t
from	dbo.MemberRole t inner join @items x
			on	x.memberID            = t.memberID
			and	x.roleID              = t.roleID

go
