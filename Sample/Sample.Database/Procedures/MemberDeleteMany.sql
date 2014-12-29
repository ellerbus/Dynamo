
if object_id('dbo.MemberDeleteMany', 'P') is not null drop procedure dbo.MemberDeleteMany
go

create procedure dbo.MemberDeleteMany
	@items                          dbo.MemberTableType readonly
as

delete	t
from	dbo.Member t inner join @items x
			on	x.memberID            = t.memberID

go
