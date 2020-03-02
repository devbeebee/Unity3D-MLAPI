<?php
/*
 *  Example of HOWTO: PHP TCP Server/Client with SSL Encryption using Streams
 *  Client side Script
 *
 *  Website : http://blog.leenix.co.uk/2011/05/howto-php-tcp-serverclient-with-ssl.html
 */

$ip="127.0.0.1";     //Set the TCP IP Address to connect too
$port="8099";        //Set the TCP PORT to connect too
$command="hi";       //Command to run


//Connect to Server
$socket = stream_socket_client("tcp://{$ip}:{$port}", $errno, $errstr, 30);

if($socket) {
 //Start SSL
 stream_set_blocking ($socket, true);
 stream_set_blocking ($socket, false);

 //Send a command
 fwrite($socket, $command);


 $buf = null;
 //Receive response from server. Loop until the response is finished
 while (!feof($socket)) {
  $buf .= fread($socket, 20240);
 }

 //close connection
 fclose($socket);

 //echo our command response
 echo $buf;
}
?>