
if object_id('dbo.MemberInsertMany', 'P') is not null drop procedure dbo.MemberInsertMany
go

create procedure dbo.MemberInsertMany
	@items                          dbo.MemberTableType readonly
as

insert into dbo.Member
	(
		memberName,
		createdAt,
		updatedAt
	)
	select	x.memberName,
			x.createdAt,
			x.updatedAt
	from	@items x left join dbo.Member t
				on	x.memberID            = t.memberID
				and	t.memberID            is null
go
