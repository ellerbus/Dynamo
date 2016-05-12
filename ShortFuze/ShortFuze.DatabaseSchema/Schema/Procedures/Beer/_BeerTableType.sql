
create type dbo.BeerTableType as table
(
	beer_id                        int,
	beer_name                      nvarchar(128),
	flavor                         nvarchar(128),
	hoppiness                      int,
	yumminess                      int,
	row_version                    timestamp
)

go
