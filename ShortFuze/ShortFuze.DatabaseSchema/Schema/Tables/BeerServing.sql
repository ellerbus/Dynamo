create table dbo.BeerServing
(
	serving_id				int not null identity (100000, 1),
	beer_id					int not null,
	serving_descr			nvarchar(30) not null,
	row_version				timestamp
)
go

alter table BeerServing
	add constraint PK_BeerServing
	primary key (serving_id)
go

alter table BeerServing
	add constraint FK_BeerServing_To_Beer
	foreign key (beer_id)
	references Beer (beer_id)
	on delete cascade on update cascade
go