
if object_id('dbo.AdjustmentUpsertOne', 'P') is not null drop procedure dbo.AdjustmentUpsertOne
go

create procedure dbo.AdjustmentUpsertOne
	@account_id                     char(2),
	@adjustment_id                  char(2),
	@budget_id                      char(2),
	@adjustment_name                varchar(60),
	@adjustment_date                datetime,
	@amount                         float,
	@created_at                     datetime,
	@updated_at                     datetime
as

update	dbo.ADJUSTMENT
set		budget_id                   = @budget_id,
		adjustment_name             = @adjustment_name,
		adjustment_date             = @adjustment_date,
		amount                      = @amount,
		created_at                  = @created_at,
		updated_at                  = @updated_at
where	account_id                  = @account_id
  and	adjustment_id               = @adjustment_id

if	@@rowcount = 0
begin

	insert into dbo.ADJUSTMENT
		(
			account_id,
			adjustment_id,
			budget_id,
			adjustment_name,
			adjustment_date,
			amount,
			created_at,
			updated_at
		)
		values
		(
			@account_id,
			@adjustment_id,
			@budget_id,
			@adjustment_name,
			@adjustment_date,
			@amount,
			@created_at,
			@updated_at
		)
		
		
end



go
