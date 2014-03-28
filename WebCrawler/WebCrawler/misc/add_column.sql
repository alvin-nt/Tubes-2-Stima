DROP PROCEDURE IF EXISTS add_column$$
CREATE PROCEDURE add_column
(IN keyword varchar(512),
 OUT retval INT)
MODIFIES SQL DATA

BEGIN
	IF( EXISTS(
		SELECT * FROM information_schema.COLUMNS
		WHERE table_name = 'crawler_index'
		AND column_name = keyword))
	THEN
		SELECT FALSE INTO retval;
	ELSE
		ALTER TABLE crawler_index
			ADD keyword BOOLEAN NOT NULL DEFAULT FALSE;
		SELECT TRUE INTO retval;
	END IF;
END$$

-- kalo pake PHPMyAdmin, jangan lupa tambahin $$ di bagian delimiter
-- kalo pake shell biasa, tambahin DELIMITER $$ di atas baris satu