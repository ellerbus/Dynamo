
if object_id('dbo.DeleteMembers', 'P') is not null drop procedure dbo.DeleteMembers
go

create procedure dbo.DeleteMembers
	@items                          MemberTableType readonly
as

delete	t
from	dbo.Member t,
		@items x
where	t.memberID                    = x.memberID

go
