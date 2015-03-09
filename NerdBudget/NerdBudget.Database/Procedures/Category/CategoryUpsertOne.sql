
if object_id('dbo.CategoryUpsertOne', 'P') is not null drop procedure dbo.CategoryUpsertOne
go

create procedure dbo.CategoryUpsertOne
	@account_id                     char(2),
	@category_id                    char(2),
	@category_name                  varchar(30),
	@multiplier                     int,
	@sequence                       int,
	@created_at                     datetime,
	@updated_at                     datetime
as

update	dbo.CATEGORY
set		category_name               = @category_name,
		multiplier                  = @multiplier,
		sequence                    = @sequence,
		created_at                  = @created_at,
		updated_at                  = @updated_at
where	account_id                  = @account_id
  and	category_id                 = @category_id

if	@@rowcount = 0
begin

	insert into dbo.CATEGORY
		(
			account_id,
			category_id,
			category_name,
			multiplier,
			sequence,
			created_at,
			updated_at
		)
		values
		(
			@account_id,
			@category_id,
			@category_name,
			@multiplier,
			@sequence,
			@created_at,
			@updated_at
		)
		
		
end



go
