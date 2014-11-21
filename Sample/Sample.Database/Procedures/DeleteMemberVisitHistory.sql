
if object_id('dbo.DeleteMemberVisitHistory', 'P') is not null drop procedure dbo.DeleteMemberVisitHistory
go

create procedure dbo.DeleteMemberVisitHistory
	@memberID                       int,
	@visitedAt                      datetime
as

delete
from	dbo.MemberVisitHistory
where	memberID                    = @memberID
  and	visitedAt                   = @visitedAt

go
