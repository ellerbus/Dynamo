
if object_id('dbo.MemberSelectOne', 'P') is not null drop procedure dbo.MemberSelectOne
go

create procedure dbo.MemberSelectOne
	@memberID                       int
as

select	*
from	dbo.Member
where	memberID                    = @memberID

go
