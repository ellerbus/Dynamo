
if object_id('dbo.MemberRoleInsertMany', 'P') is not null drop procedure dbo.MemberRoleInsertMany
go

create procedure dbo.MemberRoleInsertMany
	@items                          dbo.MemberRoleTableType readonly
as

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
