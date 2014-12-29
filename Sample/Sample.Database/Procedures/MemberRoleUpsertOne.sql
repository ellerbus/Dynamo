
if object_id('dbo.MemberRoleUpsertOne', 'P') is not null drop procedure dbo.MemberRoleUpsertOne
go

create procedure dbo.MemberRoleUpsertOne
	@memberID                       int,
	@roleID                         int,
	@createdAt                      datetime
as

update	dbo.MemberRole
set		createdAt                   = @createdAt
where	memberID                    = @memberID
  and	roleID                      = @roleID

if	@@rowcount = 0
begin

	insert into dbo.MemberRole
		(
			memberID,
			roleID,
			createdAt
		)
		values
		(
			@memberID,
			@roleID,
			@createdAt
		)
		
		
end



go
