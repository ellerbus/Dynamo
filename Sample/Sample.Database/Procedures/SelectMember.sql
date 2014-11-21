
if object_id('dbo.SelectMember', 'P') is not null drop procedure dbo.SelectMember
go

create procedure dbo.SelectMember
	@memberID                       int
as

select	*
from	dbo.Member
where	memberID                    = @memberID

go
