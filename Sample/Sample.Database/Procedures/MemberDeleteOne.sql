
if object_id('dbo.MemberDeleteOne', 'P') is not null drop procedure dbo.MemberDeleteOne
go

create procedure dbo.MemberDeleteOne
	@memberID                       int
as

delete
from	dbo.Member
where	memberID                    = @memberID

go
