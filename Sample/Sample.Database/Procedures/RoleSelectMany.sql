
if object_id('dbo.RoleSelectMany', 'P') is not null drop procedure dbo.RoleSelectMany
go

create procedure dbo.RoleSelectMany
as

select	*
from	dbo.Role

go
