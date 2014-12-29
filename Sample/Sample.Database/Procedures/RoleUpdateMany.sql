
if object_id('dbo.RoleUpdateMany', 'P') is not null drop procedure dbo.RoleUpdateMany
go

create procedure dbo.RoleUpdateMany
	@items                          dbo.RoleTableType readonly
as

update	t
set		t.roleName                    = x.roleName,
		t.createdAt                   = x.createdAt,
		t.updatedAt                   = x.updatedAt
from	dbo.Role t inner join @items x
			on	x.roleID              = t.roleID

go
