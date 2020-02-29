<!DOCTYPE html>
<html lang="en">
<head>
<link rel="stylesheet" type="text/css" href="/mydb/styles/mainstyle.css">
<title>Page Title</title>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>

<div class="header">
  <h1>MLAPI + UNITY + PHP</h1>
  <h2>Total Players</h2>
</div>

<div class="navbar">
  <a href="/mydb/home.html">Home</a>
  <a href="/mydb/totalplayers.php">Player</a>
  <a href="/mydb/servers.php">Servers</a>
  <a href="/mydb/playerProfile.html" class="right">Profile</a>
</div>

<div class="phpdatabase">
<?php
require_once "getserverdatabase.php";
$host    = "localhost";
$user    = "root";
$pass    = "";
$db_name = "myDB";
$table_name = "myguests";
GetDataBase($host ,$user ,$pass ,$db_name ,$table_name);
?>
</div>


</body>
</html>