CREATE TABLE Users 
(
	id int primary key identity(1,1),
	username varchar(max) null,
	password varchar(max) null,
	date_register date null
)

select * from Users