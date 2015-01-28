
if object_id('dbo.CategoryInsertOne', 'P') is not null drop procedure dbo.CategoryInsertOne
go

create procedure dbo.CategoryInsertOne
	@account_id                     char(2),
	@category_id                    char(2),
	@category_name                  varchar(30),
	@multiplier                     int,
	@sequence                       int,
	@created_at                     datetime,
	@updated_at                     datetime
as

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
	




go
