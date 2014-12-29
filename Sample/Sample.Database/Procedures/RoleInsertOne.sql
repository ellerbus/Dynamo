
if object_id('dbo.RoleInsertOne', 'P') is not null drop procedure dbo.RoleInsertOne
go

create procedure dbo.RoleInsertOne
	@roleID                         int,
	@roleName                       varchar,
	@createdAt                      datetime,
	@updatedAt                      datetime
as

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

select	@roleID

go
