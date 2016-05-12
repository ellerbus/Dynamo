
create procedure dbo.BeerInsertMany
	@items                          dbo.BeerTableType readonly
as

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
