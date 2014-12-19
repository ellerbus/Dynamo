create table MemberRole
(
	memberID						int not null references Member (memberID),
	roleID							int not null references Role (roleID),
	primary key						(memberID, roleID)
)