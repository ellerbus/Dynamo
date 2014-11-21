
if object_id('dbo.DeleteMember', 'P') is not null drop procedure dbo.DeleteMember
go

create procedure dbo.DeleteMember
	@memberID                       int
as

delete
from	dbo.Member
where	memberID                    = @memberID

go
