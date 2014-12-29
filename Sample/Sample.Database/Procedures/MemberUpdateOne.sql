
if object_id('dbo.MemberUpdateOne', 'P') is not null drop procedure dbo.MemberUpdateOne
go

create procedure dbo.MemberUpdateOne
	@memberID                       int,
	@memberName                     varchar,
	@createdAt                      datetime,
	@updatedAt                      datetime
as

update	dbo.Member
set		memberName                  = @memberName,
		createdAt                   = @createdAt,
		updatedAt                   = @updatedAt
where	memberID                    = @memberID

go
