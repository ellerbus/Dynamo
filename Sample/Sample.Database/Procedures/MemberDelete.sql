
if object_id('dbo.MemberDelete', 'P') is not null drop procedure dbo.MemberDelete
go

create procedure dbo.MemberDelete
	@memberID                       int
as

delete
from	dbo.Member
where	memberID                    = @memberID

go
