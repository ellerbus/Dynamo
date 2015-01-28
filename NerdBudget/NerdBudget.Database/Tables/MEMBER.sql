use NERD_BUDGET;

if OBJECT_ID('dbo.MEMBER') IS NOT NULL DROP TABLE dbo.MEMBER

create table dbo.MEMBER
(
	member_name				varchar(30) not null,
	member_password			varchar(120) not null,

	logged_in_at			datetime not null,
	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(member_name)
)