DROP PROCEDURE IF EXISTS set_keyword_true$$
CREATE PROCEDURE set_keyword_true
(IN url_in VARCHAR(512),
 IN keyword VARCHAR(512),
 OUT retval INT)
MODIFIES SQL DATA

BEGIN
	IF (EXISTS (SELECT * FROM crawler_index WHERE url = url_in)
		AND EXISTS (SELECT * FROM information_schema.COLUMNS
					WHERE table_name = 'crawler_index'
					AND column_name = keyword)
	) THEN
		UPDATE crawler_index SET keyword = TRUE WHERE url = url_in;
		SELECT TRUE INTO retval;
	ELSE
		SELECT FALSE INTO retval;
	END IF;
END$$

-- kalo pake PHPMyAdmin, jangan lupa tambahin $$ di bagian delimiter
-- kalo pake shell biasa, tambahin DELIMITER $$ di atas baris 1