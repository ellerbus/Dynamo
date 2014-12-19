
if type_id('dbo.RoleTableType') is not null drop type  dbo.RoleTableType
go

create type dbo.RoleTableType as table
(
	roleID                         int,
	roleName                       varchar(50),
	createdAt                      datetime,
	updatedAt                      datetime
)

go
