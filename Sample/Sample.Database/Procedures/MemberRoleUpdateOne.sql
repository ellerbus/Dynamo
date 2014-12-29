
if object_id('dbo.MemberRoleUpdateOne', 'P') is not null drop procedure dbo.MemberRoleUpdateOne
go

create procedure dbo.MemberRoleUpdateOne
	@memberID                       int,
	@roleID                         int,
	@createdAt                      datetime
as

update	dbo.MemberRole
set		createdAt                   = @createdAt
where	memberID                    = @memberID
  and	roleID                      = @roleID

go
