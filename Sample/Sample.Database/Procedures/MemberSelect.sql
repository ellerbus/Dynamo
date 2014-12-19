
if object_id('dbo.MemberSelect', 'P') is not null drop procedure dbo.MemberSelect
go

create procedure dbo.MemberSelect
	@memberID                       int
as

select	*
from	dbo.Member
where	memberID                    = @memberID

go
