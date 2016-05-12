create table dbo.Beer
(
	beer_id					int not null identity (100000, 1),
	beer_name				nvarchar(128) not null,
	flavor					nvarchar(128) not null,
	hoppiness				int default(6),
	yumminess				as hoppiness * 2,
	row_version				timestamp
)
go

alter table Beer
	add constraint PK_Beer
	primary key (beer_id)
go
