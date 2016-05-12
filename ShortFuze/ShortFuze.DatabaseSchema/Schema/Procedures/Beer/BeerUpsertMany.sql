
create procedure dbo.BeerUpsertMany
	@items                          dbo.BeerTableType readonly
as

update	t
set		t.beer_name                 = x.beer_name,
		t.flavor                    = x.flavor,
		t.hoppiness                 = x.hoppiness,
		t.yumminess                 = x.yumminess,
		t.row_version               = x.row_version
from	dbo.Beer t inner join @items x
			on	x.beer_id           = t.beer_id

insert into dbo.Beer
	(
		beer_name,
		flavor,
		hoppiness,
		yumminess,
		row_version
	)
	select	x.beer_name,
			x.flavor,
			x.hoppiness,
			x.yumminess,
			x.row_version
	from	@items x left join dbo.Beer t
				on	x.beer_id       = t.beer_id
	where	t.beer_id               is null
go
