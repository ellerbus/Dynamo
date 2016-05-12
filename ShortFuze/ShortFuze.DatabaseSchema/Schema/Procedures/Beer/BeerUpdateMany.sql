
create procedure dbo.BeerUpdateMany
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

go
