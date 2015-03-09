
if object_id('dbo.AccountUpsertOne', 'P') is not null drop procedure dbo.AccountUpsertOne
go

create procedure dbo.AccountUpsertOne
	@account_id                     char(2),
	@account_name                   varchar(30),
	@account_type                   char(1),
	@started_at                     datetime,
	@created_at                     datetime,
	@updated_at                     datetime
as

update	dbo.ACCOUNT
set		account_name                = @account_name,
		account_type                = @account_type,
		started_at                  = @started_at,
		created_at                  = @created_at,
		updated_at                  = @updated_at
where	account_id                  = @account_id

if	@@rowcount = 0
begin

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
		
		
end



go
