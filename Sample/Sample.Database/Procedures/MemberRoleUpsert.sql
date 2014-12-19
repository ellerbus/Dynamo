
if object_id('dbo.MemberRoleUpsert', 'P') is not null drop procedure dbo.MemberRoleUpsert
go

create procedure dbo.MemberRoleUpsert
	@memberID                       int,
	@roleID                         int
as

update	dbo.MemberRole
set		
where	memberID                    = @memberID
  and	roleID                      = @roleID

if	@@rowcount = 0
begin

	insert into dbo.MemberRole
		(
			memberID,
			roleID
		)
		values
		(
			@memberID,
			@roleID
		)
		
		
end



go
