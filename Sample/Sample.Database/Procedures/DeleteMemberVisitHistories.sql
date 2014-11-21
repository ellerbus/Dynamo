
if object_id('dbo.DeleteMemberVisitHistories', 'P') is not null drop procedure dbo.DeleteMemberVisitHistories
go

create procedure dbo.DeleteMemberVisitHistories
	@items                          MemberVisitHistoryTableType readonly
as

delete	t
from	dbo.MemberVisitHistory t,
		@items x
where	t.memberID                    = x.memberID
  and	t.visitedAt                   = x.visitedAt

go
