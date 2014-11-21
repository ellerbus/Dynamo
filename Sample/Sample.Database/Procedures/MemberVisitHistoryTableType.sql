
if type_id('dbo.MemberVisitHistoryTableType') is not null drop type  dbo.MemberVisitHistoryTableType
go

create type dbo.MemberVisitHistoryTableType as table
(
	memberID                       int,
	visitedAt                      datetime,
	pageUrl                        varchar(255)
)

go
