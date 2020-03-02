<?php
        // Configuration
    $hostname = 'localhost';
    $username = 'root';
    $password = '';
    $database = 'serverDB';
 
        $secretKey = "mySecretKey"; // Change this value to match the value stored in the client javascript below 
 
        try {
            $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
        } catch(PDOException $e) {
            echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
        }
 echo 'Here<br>';
        $realHash = md5($_GET['server_name'] . $_GET['server_ip']. $_GET['server_password'] . $_GET['server_lastping'] . $secretKey); 
        
 echo $_GET['server_name'];
            $sth = $dbh->prepare('INSERT INTO gameservers VALUES (null, :server_name, :server_ip, :server_password, :server_lastping)');
            try {
                $sth->execute($_GET);
            } catch(Exception $e) {
                echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() ,'</pre>';
            }
        
?>