
if type_id('dbo.MemberRoleTableType') is not null drop type  dbo.MemberRoleTableType
go

create type dbo.MemberRoleTableType as table
(
	memberID                       int,
	roleID                         int
)

go
