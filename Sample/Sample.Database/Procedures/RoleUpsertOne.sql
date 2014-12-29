
if object_id('dbo.RoleUpsertOne', 'P') is not null drop procedure dbo.RoleUpsertOne
go

create procedure dbo.RoleUpsertOne
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

if	@@rowcount = 0
begin

	insert into dbo.Role
		(
			roleName,
			createdAt,
			updatedAt
		)
		values
		(
			@roleName,
			@createdAt,
			@updatedAt
		)
		
		set @roleID = ident_current('dbo.Role')
end

select	@roleID

go
