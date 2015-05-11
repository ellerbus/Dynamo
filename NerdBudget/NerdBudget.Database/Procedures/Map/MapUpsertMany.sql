
if object_id('dbo.MapUpsertMany', 'P') is not null drop procedure dbo.MapUpsertMany
go

create procedure dbo.MapUpsertMany
	@items                          dbo.MapTableType readonly
as

update	t
set		t.budget_id                   = x.budget_id,
		t.regex_pattern               = x.regex_pattern,
		t.created_at                  = x.created_at,
		t.updated_at                  = x.updated_at
from	dbo.MAP t inner join @items x
			on	x.account_id          = t.account_id
			and	x.map_id              = t.map_id

insert into dbo.MAP
	(
		account_id,
		map_id,
		budget_id,
		regex_pattern,
		created_at,
		updated_at
	)
	select	x.account_id,
			x.map_id,
			x.budget_id,
			x.regex_pattern,
			x.created_at,
			x.updated_at
	from	@items x left join dbo.MAP t
				on	x.account_id          = t.account_id
				and	x.map_id              = t.map_id
	where	t.account_id              is null
go
