
if object_id('dbo.RoleSelect', 'P') is not null drop procedure dbo.RoleSelect
go

create procedure dbo.RoleSelect
	@roleID                         int
as

select	*
from	dbo.Role
where	roleID                      = @roleID

go
