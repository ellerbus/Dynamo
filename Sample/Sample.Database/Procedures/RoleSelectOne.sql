
if object_id('dbo.RoleSelectOne', 'P') is not null drop procedure dbo.RoleSelectOne
go

create procedure dbo.RoleSelectOne
	@roleID                         int
as

select	*
from	dbo.Role
where	roleID                      = @roleID

go
