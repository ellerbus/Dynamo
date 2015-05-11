use TBM;

begin tran

declare @sql nvarchar(max)

set		@sql = N''

select	@sql +=
		case
		when 1 in (i.is_unique_constraint, i.is_primary_key) then 'alter table ' + t.name + ' drop constraint ' + i.name
		else 'drop index ' + i.name + ' on ' + t.name
		end + '; '		
from	sys.indexes i
		inner join sys.tables t
			on	i.object_id = t.object_id
where	t.name = 'MAP'

select	@sql += 'alter table ' + t.name + ' drop constraint ' + f.name + '; '
from	sys.foreign_keys f
		inner join sys.tables t
			on	f.parent_object_id = t.object_id
where	t.name = 'MAP'

select	@sql += 'drop proc ' + p.name + '; '
from	sys.procedures p
where	p.name like 'Map%'

print @sql

exec sp_executesql @sql

exec sp_rename 'MAP', 'MAP_V1'

create table dbo.MAP
(
	account_id				char(2) not null,
	map_id					char(8) not null,

	budget_id				char(2) not null,
	
	regex_pattern			varchar(750) not null,

	created_at				datetime not null,
	updated_at				datetime null,

	primary key				(account_id, map_id),
	foreign key				(account_id, budget_id)
							references BUDGET (account_id, budget_id)
							on delete cascade
							on update cascade
)

select	distinct account_id, map_id into #maps
from	MAP_V1

insert into MAP 
select	x.account_id, x.map_id, MAX(budget_id), MAX(regex_pattern), MIN(created_at), MAX(updated_at)
from	MAP_V1 a inner join #maps x
		on	a.account_id = x.account_id
		and	a.map_id = x.map_id
group by x.account_id, x.map_id

drop table MAP_V1

commit