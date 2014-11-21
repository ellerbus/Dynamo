
if object_id('dbo.UpsertMemberVisitHistory', 'P') is not null drop procedure dbo.UpsertMemberVisitHistory
go

create procedure dbo.UpsertMemberVisitHistory
	@memberID                       int,
	@visitedAt                      datetime,
	@pageUrl                        varchar
as

update	dbo.MemberVisitHistory
set		pageUrl                     = @pageUrl
where	memberID                    = @memberID
  and	visitedAt                   = @visitedAt

if	@@rowcount = 0
begin

	insert into dbo.MemberVisitHistory
		(
			memberID,
			visitedAt,
			pageUrl
		)
		values
		(
			@memberID,
			@visitedAt,
			@pageUrl
		)
		
		
end



go
