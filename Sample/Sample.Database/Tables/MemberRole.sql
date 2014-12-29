create table MemberRole
(
	memberID						int not null references Member (memberID),
	roleID							int not null references Role (roleID),
	createdAt						datetime not null,
	primary key						(memberID, roleID)
)