<?php
$externalIp  = file_get_contents("http://ipecho.net/plain");

    // Return unreliable IP address since all else failed
    echo "Remote IP : ". $externalIp;

echo var_export(unserialize(file_get_contents('http://www.geoplugin.net/php.gp?ip='.$externalIp)));
?>


