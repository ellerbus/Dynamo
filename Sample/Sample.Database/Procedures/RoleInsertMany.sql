
if object_id('dbo.RoleInsertMany', 'P') is not null drop procedure dbo.RoleInsertMany
go

create procedure dbo.RoleInsertMany
	@items                          dbo.RoleTableType readonly
as

insert into dbo.Role
	(
		roleName,
		createdAt,
		updatedAt
	)
	select	x.roleName,
			x.createdAt,
			x.updatedAt
	from	@items x left join dbo.Role t
				on	x.roleID              = t.roleID
				and	t.roleID              is null
go
