
create procedure dbo.BeerInsertOne
	@beer_id                        int,
	@beer_name                      nvarchar(128),
	@flavor                         nvarchar(128),
	@hoppiness                      int,
	@yumminess                      int,
	@row_version                    timestamp
as

insert into dbo.Beer
	(
		beer_name,
		flavor,
		hoppiness,
		yumminess,
		row_version
	)
	values
	(
		@beer_name,
		@flavor,
		@hoppiness,
		@yumminess,
		@row_version
	)
	
set @beer_id = ident_current('dbo.Beer')

select	@beer_id

go
