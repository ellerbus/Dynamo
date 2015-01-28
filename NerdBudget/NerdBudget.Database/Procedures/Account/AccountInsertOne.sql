
if object_id('dbo.AccountInsertOne', 'P') is not null drop procedure dbo.AccountInsertOne
go

create procedure dbo.AccountInsertOne
	@account_id                     char(2),
	@account_name                   varchar(30),
	@account_type                   char(1),
	@started_at                     datetime,
	@created_at                     datetime,
	@updated_at                     datetime
as

insert into dbo.ACCOUNT
	(
		account_id,
		account_name,
		account_type,
		started_at,
		created_at,
		updated_at
	)
	values
	(
		@account_id,
		@account_name,
		@account_type,
		@started_at,
		@created_at,
		@updated_at
	)
	




go
