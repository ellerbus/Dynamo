
if object_id('dbo.MemberDeleteMany', 'P') is not null drop procedure dbo.MemberDeleteMany
go

create procedure dbo.MemberDeleteMany
	@items                          MemberTableType readonly
as

delete	t
from	dbo.Member t,
		@items x
where	t.memberID                    = x.memberID

go
