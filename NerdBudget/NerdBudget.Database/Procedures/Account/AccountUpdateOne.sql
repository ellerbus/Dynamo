
if object_id('dbo.AccountUpdateOne', 'P') is not null drop procedure dbo.AccountUpdateOne
go

create procedure dbo.AccountUpdateOne
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

go
