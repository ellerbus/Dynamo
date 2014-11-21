create table MemberVisitHistory
(
	memberID						int not null references Member (memberID),
	visitedAt						datetime not null,
	pageUrl							varchar(255) not null,
	primary key						(memberID, visitedAt)
)