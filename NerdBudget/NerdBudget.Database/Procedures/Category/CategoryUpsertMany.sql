
if object_id('dbo.CategoryUpsertMany', 'P') is not null drop procedure dbo.CategoryUpsertMany
go

create procedure dbo.CategoryUpsertMany
	@items                          dbo.CategoryTableType readonly
as

update	t
set		t.category_name               = x.category_name,
		t.multiplier                  = x.multiplier,
		t.sequence                    = x.sequence,
		t.created_at                  = x.created_at,
		t.updated_at                  = x.updated_at
from	dbo.CATEGORY t inner join @items x
			on	x.account_id          = t.account_id
			and	x.category_id         = t.category_id

insert into dbo.CATEGORY
	(
		account_id,
		category_id,
		category_name,
		multiplier,
		sequence,
		created_at,
		updated_at
	)
	select	x.account_id,
			x.category_id,
			x.category_name,
			x.multiplier,
			x.sequence,
			x.created_at,
			x.updated_at
	from	@items x left join dbo.CATEGORY t
				on	x.account_id          = t.account_id
				and	x.category_id         = t.category_id
	where	t.account_id              is null
go
