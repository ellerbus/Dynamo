
create procedure dbo.BeerDeleteOne
	@beer_id                        int
as

delete
from	dbo.Beer
where	beer_id                     = @beer_id

go
