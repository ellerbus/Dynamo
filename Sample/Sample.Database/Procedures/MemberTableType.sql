
if type_id('dbo.MemberTableType') is not null drop type  dbo.MemberTableType
go

create type dbo.MemberTableType as table
(
	memberID                       int,
	memberName                     varchar(50),
	createdAt                      datetime,
	updatedAt                      datetime
)

go
