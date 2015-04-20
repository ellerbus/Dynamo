
if object_id('dbo.AdjustmentDeleteOne', 'P') is not null drop procedure dbo.AdjustmentDeleteOne
go

create procedure dbo.AdjustmentDeleteOne
	@account_id                     char(2),
	@adjustment_id                  char(2)
as

delete
from	dbo.ADJUSTMENT
where	account_id                  = @account_id
  and	adjustment_id               = @adjustment_id

go
