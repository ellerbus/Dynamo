
if object_id('dbo.RolesUpsert', 'P') is not null drop procedure dbo.RolesUpsert
go

create procedure dbo.RolesUpsert
	@items                          RoleTableType readonly
as

update	t
set		t.roleName                    = x.roleName,
		t.createdAt                   = x.createdAt,
		t.updatedAt                   = x.updatedAt
from	dbo.Role t,
		@items x
where	t.roleID                      = x.roleID

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
				on	x.roleID                      = t.roleID
				and	t.roleID                      is null
go
