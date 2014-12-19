
if object_id('dbo.MemberUpsertMany', 'P') is not null drop procedure dbo.MemberUpsertMany
go

create procedure dbo.MemberUpsertMany
	@items                          MemberTableType readonly
as

update	t
set		t.memberName                  = x.memberName,
		t.createdAt                   = x.createdAt,
		t.updatedAt                   = x.updatedAt
from	dbo.Member t,
		@items x
where	t.memberID                    = x.memberID

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
				on	x.memberID                    = t.memberID
				and	t.memberID                    is null
go
