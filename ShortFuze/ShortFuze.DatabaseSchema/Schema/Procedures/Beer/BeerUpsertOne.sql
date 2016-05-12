
create procedure dbo.BeerUpsertOne
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

if	@@rowcount = 0
begin

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
end

select	@beer_id

go
