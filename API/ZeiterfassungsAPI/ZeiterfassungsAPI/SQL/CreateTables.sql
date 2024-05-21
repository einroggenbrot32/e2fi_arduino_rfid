CREATE TABLE uuser(
	id SERIAL,
	name varchar(255)
);

CREATE TABLE session(
	id SERIAL,
	user_id integer not null,
	startzeit timestamp not null,
	endzeit timestamp
);

CREATE TABLE rfid_chip(
	id SERIAL,
	user_id integer not null,
	rfid varchar(20)
)
