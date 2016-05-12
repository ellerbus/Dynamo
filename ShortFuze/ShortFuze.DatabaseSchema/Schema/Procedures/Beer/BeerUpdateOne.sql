
create procedure dbo.BeerUpdateOne
	@beer_id                        int,
	@beer_name                      nvarchar(128),
	@flavor                         nvarchar(128),
	@hoppiness                      int,
	@yumminess                      int,
	@row_version                    timestamp
as

update	dbo.Beer
set		beer_name                   = @beer_name,
		flavor                      = @flavor,
		hoppiness                   = @hoppiness,
		yumminess                   = @yumminess,
		row_version                 = @row_version
where	beer_id                     = @beer_id

go
