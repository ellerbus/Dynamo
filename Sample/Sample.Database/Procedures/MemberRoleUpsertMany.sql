
if object_id('dbo.MemberRoleUpsertMany', 'P') is not null drop procedure dbo.MemberRoleUpsertMany
go

create procedure dbo.MemberRoleUpsertMany
	@items                          dbo.MemberRoleTableType readonly
as

update	t
set		t.createdAt                   = x.createdAt
from	dbo.MemberRole t inner join @items x
			on	x.memberID            = t.memberID
			and	x.roleID              = t.roleID

insert into dbo.MemberRole
	(
		memberID,
		roleID,
		createdAt
	)
	select	x.memberID,
			x.roleID,
			x.createdAt
	from	@items x left join dbo.MemberRole t
				on	x.memberID            = t.memberID
				and	x.roleID              = t.roleID
				and	t.memberID            is null
go
