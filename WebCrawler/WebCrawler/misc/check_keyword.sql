DROP FUNCTION IF EXISTS check_keyword$$
CREATE FUNCTION check_keyword
(keyword VARCHAR(512))
RETURNS INT
READS SQL DATA

BEGIN
	IF (EXISTS (
        SELECT *
        FROM information_schema.COLUMNS
        WHERE table_name = 'crawler_index'
        AND column_name = keyword))
	THEN
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
END$$

-- jangan luma DELIMITER $$
-- kalo di shell, tambahin di atas baris 1
-- kalo di PHPMyAdmin, tambahin di bagian Delimiter