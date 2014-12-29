
if object_id('dbo.RoleUpsertMany', 'P') is not null drop procedure dbo.RoleUpsertMany
go

create procedure dbo.RoleUpsertMany
	@items                          dbo.RoleTableType readonly
as

update	t
set		t.roleName                    = x.roleName,
		t.createdAt                   = x.createdAt,
		t.updatedAt                   = x.updatedAt
from	dbo.Role t inner join @items x
			on	x.roleID              = t.roleID

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
