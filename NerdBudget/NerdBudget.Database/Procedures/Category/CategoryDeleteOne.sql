
if object_id('dbo.CategoryDeleteOne', 'P') is not null drop procedure dbo.CategoryDeleteOne
go

create procedure dbo.CategoryDeleteOne
	@account_id                     char(2),
	@category_id                    char(2)
as

delete
from	dbo.CATEGORY
where	account_id                  = @account_id
  and	category_id                 = @category_id

go
