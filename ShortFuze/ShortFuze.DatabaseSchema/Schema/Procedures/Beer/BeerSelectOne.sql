
create procedure dbo.BeerSelectOne
	@beer_id                        int
as

select	*
from	dbo.Beer
where	beer_id                     = @beer_id

go
