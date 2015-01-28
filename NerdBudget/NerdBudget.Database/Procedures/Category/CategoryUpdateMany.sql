
if object_id('dbo.CategoryUpdateMany', 'P') is not null drop procedure dbo.CategoryUpdateMany
go

create procedure dbo.CategoryUpdateMany
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

go
