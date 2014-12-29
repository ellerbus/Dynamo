
if object_id('dbo.MemberRoleUpdateMany', 'P') is not null drop procedure dbo.MemberRoleUpdateMany
go

create procedure dbo.MemberRoleUpdateMany
	@items                          dbo.MemberRoleTableType readonly
as

update	t
set		t.createdAt                   = x.createdAt
from	dbo.MemberRole t inner join @items x
			on	x.memberID            = t.memberID
			and	x.roleID              = t.roleID

go
