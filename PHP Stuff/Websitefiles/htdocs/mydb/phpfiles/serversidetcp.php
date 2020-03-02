<?php
/*
 *  Example of HOWTO: PHP TCP Server/Client with SSL Encryption using Streams
 *  Server side Script
 * 
 *  Website : http://blog.leenix.co.uk/2011/05/howto-php-tcp-serverclient-with-ssl.html
 */

$ip="127.0.0.1";               //Set the TCP IP Address to listen on
$port="8099";                  //Set the TCP Port to listen on
$pem_passphrase = "<your password>";   //Set a password here
$pem_file = "filename.pem";    //Set a path/filename for the PEM SSL Certificate which will be created.

//The following array of data is needed to generate the SSL Cert
$pem_dn = array(
 "countryName" => "UK",                 //Set your country name
 "stateOrProvinceName" => "Lancs",      //Set your state or province name
 "localityName" => "Rossendale",        //Ser your city name
 "organizationName" => "DevBeeBee",  //Set your company name
 "organizationalUnitName" => "CEO", //Set your department name
 "commonName" => "Your full hostname",  //Set your full hostname.
 "emailAddress" => "email@example.com"  //Set your email address
);

//create ssl cert for this scripts life.
echo "Creating SSL Cert\n";
createSSLCert($pem_file, $pem_passphrase, $pem_dn);

//setup and listen to a tcp IP/port, returning the socket stream
echo "Listening to {$ip}:{$port} for connections\n";
$socket = setupTcpStreamServer($pem_file, $pem_passphrase, $ip, $port);

//enter a loop until an exit command is received.
$exit=false;
$i=1;
while($exit==false) {

 //Accept any new connections
 $forkedSocket = stream_socket_accept($socket, "-1", $remoteIp);

 echo "New connection from $remoteIp\n";

 //start SSL on the connection
 stream_set_blocking ($forkedSocket, true); // block the connection until SSL is done.
 stream_socket_enable_crypto($forkedSocket, true, STREAM_CRYPTO_METHOD_SSLv3_SERVER);

 //Read the command from the client. This will read 8192 bytes of data, If you need to read more you may need to increase this. However some systems will fragment the command over 8192 anyway, so you would instead need to write a loop waiting for the command input to end before proceeding.
 $command = fread($forkedSocket, 8192);

 //unblock connection
 stream_set_blocking ($forkedSocket, false);

 //run a switch on the command to determine what we need to do
 switch($command) {
  //exit command will cause this script to quit out
  CASE "exit";
   $exit=true;
   echo "exit command received \n";
  break;

  //hi command
  CASE "hi";
   //write back to the client a response.
   fwrite($forkedSocket, "Hello {$remoteIp}. This is our $i command run!");
   $i++;

   echo "hi command received \n";
  break;
 }

 //close the connection to the client
 fclose($forkedSocket);
}
exit(0);



function createSSLCert($pem_file, $pem_passphrase, $pem_dn) {
//create ssl cert for this scripts life.

 //Create private key
 $privkey = openssl_pkey_new();

 //Create and sign CSR
 $cert    = openssl_csr_new($pem_dn, $privkey);
 $cert    = openssl_csr_sign($cert, null, $privkey, 365);

 //Generate PEM file
 $pem = array();
 openssl_x509_export($cert, $pem[0]);
 openssl_pkey_export($privkey, $pem[1], $pem_passphrase);
 $pem = implode($pem);

 //Save PEM file
 file_put_contents($pem_file, $pem);
 chmod($pem_file, 0600);
}

function setupTcpStreamServer($pem_file, $pem_passphrase, $ip, $port) {
//setup and listen to a tcp IP/port, returning the socket stream

 //create a stream context for our SSL settings
 $context = stream_context_create();

 //Setup the SSL Options
 stream_context_set_option($context, 'ssl', 'local_cert', $pem_file);  // Our SSL Cert in PEM format
 stream_context_set_option($context, 'ssl', 'passphrase', $pem_passphrase); // Private key Password
 stream_context_set_option($context, 'ssl', 'allow_self_signed', true);
 stream_context_set_option($context, 'ssl', 'verify_peer', false);

 //create a stream socket on IP:Port
 $socket = stream_socket_server("tcp://{$ip}:{$port}", $errno, $errstr, STREAM_SERVER_BIND|STREAM_SERVER_LISTEN, $context);
 stream_socket_enable_crypto($socket, false);

 return $socket;
}
?>