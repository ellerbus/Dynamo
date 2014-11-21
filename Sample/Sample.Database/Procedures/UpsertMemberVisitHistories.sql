
if object_id('dbo.UpsertMemberVisitHistories', 'P') is not null drop procedure dbo.UpsertMemberVisitHistories
go

create procedure dbo.UpsertMemberVisitHistories
	@items                          MemberVisitHistoryTableType readonly
as

update	t
set		t.pageUrl                     = x.pageUrl
from	dbo.MemberVisitHistory t,
		@items x
where	t.memberID                    = x.memberID
  and	t.visitedAt                   = x.visitedAt

insert into dbo.MemberVisitHistory
	(
		memberID,
		visitedAt,
		pageUrl
	)
	select	x.memberID,
			x.visitedAt,
			x.pageUrl
	from	@items x left join dbo.MemberVisitHistory t
				on	x.memberID                    = t.memberID
				and	x.visitedAt                   = t.visitedAt
				and	t.memberID                    is null
go
