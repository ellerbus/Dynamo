
if object_id('dbo.MemberUpdateMany', 'P') is not null drop procedure dbo.MemberUpdateMany
go

create procedure dbo.MemberUpdateMany
	@items                          dbo.MemberTableType readonly
as

update	t
set		t.memberName                  = x.memberName,
		t.createdAt                   = x.createdAt,
		t.updatedAt                   = x.updatedAt
from	dbo.Member t inner join @items x
			on	x.memberID            = t.memberID

go
