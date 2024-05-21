DELETE FROM uuser;

DELETE FROM rfid_chip;

DELETE FROM session;

INSERT INTO uuser(name) 
	values('narvik')
;

INSERT INTO rfid_chip(user_id, rfid)
	values((select id from uuser where name = 'narvik'), '12345678')
;

