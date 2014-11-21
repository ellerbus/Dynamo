create table Member
(
	memberID						int	not null identity(100000, 1),
	memberName						varchar(50) not null,
	createdAt						datetime not null,
	updatedAt						datetime null,
	primary key						(memberID),
	unique							(memberName)
)