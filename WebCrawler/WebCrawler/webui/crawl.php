<!DOCTYPE html>
<html>
    <header>
	<title>Stima Tubes 2 Search Engine</title>
	<link href="style.css" type="text/css" rel="stylesheet"></link>
    </header>
    <body>
	<div class = "maincontainer">
	    <br />
	    <h1>HST: Hubble Search Telescope</h1>
	    <p>"Because binoculars for search icons are too mainstream"</p>
	    <br />
	    <p>
		<b>How we Index the crawl results</b><br/><br/>
		The crawl results is first screened of punctuation marks and such symbols, then converted to lowercase<br/>
		Then unique keywords were chosen such that every page will have no duplicate keywords<br/>
		Next, we look up the MySQL database to see each keyword, whether was it already stored. If not, store it to a new column.<br/>
		The database used CSV Storage Engine, since other sophisticated search engines have only ~1000 columns limit<br/>
	    </p>
	</div>
	<div class = "linkscontainer">
	    <div class = "link"><a href="index.php">Back to Search</a></div>
	</div>
    </body>
</html>