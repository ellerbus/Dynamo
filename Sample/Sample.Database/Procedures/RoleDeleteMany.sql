
if object_id('dbo.RoleDeleteMany', 'P') is not null drop procedure dbo.RoleDeleteMany
go

create procedure dbo.RoleDeleteMany
	@items                          RoleTableType readonly
as

delete	t
from	dbo.Role t,
		@items x
where	t.roleID                      = x.roleID

go
