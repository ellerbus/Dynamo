
if object_id('dbo.SelectMemberVisitHistory', 'P') is not null drop procedure dbo.SelectMemberVisitHistory
go

create procedure dbo.SelectMemberVisitHistory
	@memberID                       int,
	@visitedAt                      datetime
as

select	*
from	dbo.MemberVisitHistory
where	memberID                    = @memberID
  and	visitedAt                   = @visitedAt

go
