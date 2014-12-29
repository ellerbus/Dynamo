
if object_id('dbo.RoleUpdateOne', 'P') is not null drop procedure dbo.RoleUpdateOne
go

create procedure dbo.RoleUpdateOne
	@roleID                         int,
	@roleName                       varchar,
	@createdAt                      datetime,
	@updatedAt                      datetime
as

update	dbo.Role
set		roleName                    = @roleName,
		createdAt                   = @createdAt,
		updatedAt                   = @updatedAt
where	roleID                      = @roleID

go
