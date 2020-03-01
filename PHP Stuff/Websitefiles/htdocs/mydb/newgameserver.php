<?php
$host    = "localhost";
$user    = "root";
$pass    = "";
$db_name = "serverDB";
$table_name = "gameservers";

$id = 0;
$server_name ="";
$server_ip="";
$server_password="";
$server_lastping="";
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "INSERT INTO $table_name (id, server_name, server_ip,server_password,server_lastping)
VALUES ($id,$server_name,$server_ip, $server_password, $server_lastping)";

if ($conn->query($sql) === TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>
