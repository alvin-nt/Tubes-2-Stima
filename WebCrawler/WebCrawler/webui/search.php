<!DOCTYPE html>
<html>
	<header>
	<title>Stima Tubes 2 Search Engine</title>
	<link href="style.css" type="text/css" rel="stylesheet"></link>
	</header>
	<body>
	<div class = "maincontainer">
		<h2>HST Search Results for:</h2>
		<h2><?php echo $_GET["q"]; ?></h2>
		<div class="results">
		<?php
		try {
			//just for escaping. I KNOW, its dirty, but does the job juust fine
			//$mysqli = new mysqli("localhost", "stima2", "stima2", "stima2");
			$mysqli = new mysqli("localhost", "dalvacom_labhst", "Stima2hst!", "dalvacom_labs_hubblesearch");
			$sanitizedq = $mysqli->real_escape_string($_GET["q"]);
			$mysqli->close();
			
			$pieces = explode(" ", strtolower($sanitizedq));
			$sqlquery = "SELECT URL,Title FROM `index` WHERE";
			foreach ($pieces as $piece) {
			$sqlquery = $sqlquery . " " . $piece . "=1 AND";
			}
			$sqlquery = substr($sqlquery, 0, -4);
			
			//$dbh = new PDO("mysql:dbname=stima2;host=localhost", "stima2", "stima2", array( PDO::ATTR_PERSISTENT => false));
			$dbh = new PDO("mysql:dbname=dalvacom_labs_hubblesearch;host=localhost", "dalvacom_labhst", "Stima2hst!", array( PDO::ATTR_PERSISTENT => false));
			$sth = $dbh->prepare($sqlquery);
			$sth->execute();
			
			$resultcount = 0;
			while ($row = $sth->fetch(PDO::FETCH_OBJ)) {
				echo "<h3><a class=\"nofx\" href=\"" . $row->URL . "\">" . $row->URL . "</a></h3>";
				echo "<h4>" . $row->Title . "</h4>";
			$resultcount++;
			}
			echo "<br /><h4>Found " . $resultcount . " results.</h4>";
			
			/*if ( ! $sth->execute() ) // debug purposes. Uncomment to show
			{
			echo "PDO Error 1.1:\n";
			print_r($sth->errorInfo());
			exit;
			}*/
		} catch (PDOException $e) {
			print "Error!: " . $e->getMessage() . "<br/>";
		}
		?>
		</div>
	</div>
	<div class = "linkscontainer">
		<div class = "link"><a href="index.php">Back to Search</a></div>
	</div>
	</body>
</html>