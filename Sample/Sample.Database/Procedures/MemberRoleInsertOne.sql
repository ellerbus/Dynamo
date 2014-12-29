
if object_id('dbo.MemberRoleInsertOne', 'P') is not null drop procedure dbo.MemberRoleInsertOne
go

create procedure dbo.MemberRoleInsertOne
	@memberID                       int,
	@roleID                         int,
	@createdAt                      datetime
as

insert into dbo.MemberRole
	(
		memberID,
		roleID,
		createdAt
	)
	values
	(
		@memberID,
		@roleID,
		@createdAt
	)
	




go
