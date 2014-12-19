create table Role
(
	roleID							int	not null identity(100000, 1),
	roleName						varchar(50) not null,
	createdAt						datetime not null,
	updatedAt						datetime null,
	primary key						(roleID),
	unique							(roleName)
)