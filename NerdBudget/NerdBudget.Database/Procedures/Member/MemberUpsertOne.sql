
if object_id('dbo.MemberUpsertOne', 'P') is not null drop procedure dbo.MemberUpsertOne
go

create procedure dbo.MemberUpsertOne
	@member_name                    varchar(30),
	@member_password                varchar(120),
	@logged_in_at                   datetime,
	@created_at                     datetime,
	@updated_at                     datetime
as

update	dbo.MEMBER
set		member_password             = @member_password,
		logged_in_at                = @logged_in_at,
		created_at                  = @created_at,
		updated_at                  = @updated_at
where	member_name                 = @member_name

if	@@rowcount = 0
begin

	insert into dbo.MEMBER
		(
			member_name,
			member_password,
			logged_in_at,
			created_at,
			updated_at
		)
		values
		(
			@member_name,
			@member_password,
			@logged_in_at,
			@created_at,
			@updated_at
		)
		
		
end



go
