create table categoria (
	id serial primary key,
	nombre varchar(30) not null
);
create table articulo (
	id serial primary key,
	nombre varchar(45) not null,
	categoria bigint(20) unsigned,
	precio decimal(10,2),
	foreign key (categoria) references categoria(id)	
)

