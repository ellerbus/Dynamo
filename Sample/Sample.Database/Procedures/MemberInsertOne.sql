
if object_id('dbo.MemberInsertOne', 'P') is not null drop procedure dbo.MemberInsertOne
go

create procedure dbo.MemberInsertOne
	@memberID                       int,
	@memberName                     varchar,
	@createdAt                      datetime,
	@updatedAt                      datetime
as

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

select	@memberID

go
