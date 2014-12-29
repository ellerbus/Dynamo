
if object_id('dbo.RoleDeleteMany', 'P') is not null drop procedure dbo.RoleDeleteMany
go

create procedure dbo.RoleDeleteMany
	@items                          dbo.RoleTableType readonly
as

delete	t
from	dbo.Role t inner join @items x
			on	x.roleID              = t.roleID

go
