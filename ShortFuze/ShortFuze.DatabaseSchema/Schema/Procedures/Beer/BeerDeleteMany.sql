
create procedure dbo.BeerDeleteMany
	@items                          dbo.BeerTableType readonly
as

delete	t
from	dbo.Beer t inner join @items x
			on	x.beer_id           = t.beer_id

go
