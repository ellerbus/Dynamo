
if object_id('dbo.MemberRoleUpsertMany', 'P') is not null drop procedure dbo.MemberRoleUpsertMany
go

create procedure dbo.MemberRoleUpsertMany
	@items                          MemberRoleTableType readonly
as

update	t
set		
from	dbo.MemberRole t,
		@items x
where	t.memberID                    = x.memberID
  and	t.roleID                      = x.roleID

insert into dbo.MemberRole
	(
		memberID,
		roleID
	)
	select	x.memberID,
			x.roleID
	from	@items x left join dbo.MemberRole t
				on	x.memberID                    = t.memberID
				and	x.roleID                      = t.roleID
				and	t.memberID                    is null
go
