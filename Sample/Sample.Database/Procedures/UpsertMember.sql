
if object_id('dbo.UpsertMember', 'P') is not null drop procedure dbo.UpsertMember
go

create procedure dbo.UpsertMember
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

if	@@rowcount = 0
begin

	insert into dbo.Member
		(
			memberName,
			createdAt,
			updatedAt
		)
		values
		(
			@memberName,
			@createdAt,
			@updatedAt
		)
		
		set @memberID = ident_current('dbo.Member')
end

select	@memberID

go
