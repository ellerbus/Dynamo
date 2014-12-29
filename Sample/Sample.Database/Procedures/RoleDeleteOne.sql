
if object_id('dbo.RoleDeleteOne', 'P') is not null drop procedure dbo.RoleDeleteOne
go

create procedure dbo.RoleDeleteOne
	@roleID                         int
as

delete
from	dbo.Role
where	roleID                      = @roleID

go
