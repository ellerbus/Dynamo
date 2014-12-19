
if object_id('dbo.RoleDelete', 'P') is not null drop procedure dbo.RoleDelete
go

create procedure dbo.RoleDelete
	@roleID                         int
as

delete
from	dbo.Role
where	roleID                      = @roleID

go
