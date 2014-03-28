CREATE FUNCTION check_url
(url_target varchar(512))
RETURNS INT
READS SQL DATA

begin
	DECLARE retval int;
	
	IF (EXISTS(SELECT *
        	FROM `stima2`.`crawler.index`
        	WHERE url = url_target)) 
	THEN
		SELECT TRUE INTO retval;
	ELSE
		SELECT FALSE INTO retval;
	end if;

	RETURN retval;
end$$

-- jangan lupa tambahin DELIMITER $$
-- kalo di PHPMyAdmin, di bagian bawah
-- kalo di shell, sebelum baris pertama