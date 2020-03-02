<?php
function CLogger ($output)
 {
    $js_code = 'console.log(' . json_encode($output, JSON_HEX_TAG) . 
');';
    echo $js_code;
}
function GetDataBase ($host ,$user ,$pass ,$db_name ,$table_name)
{

//create connection
$connection = mysqli_connect($host, $user, $pass, $db_name,);

//test if connection failed
if(mysqli_connect_errno()){
    die("connection failed: "
        . mysqli_connect_error()
        . " (" . mysqli_connect_errno()
        . ")");
}
//get results from database
$result = mysqli_query($connection,"SELECT * FROM ".$table_name."");
$all_property = array();  //declare an array for saving property

echo '<div class="propertynames">{';
while ($property = mysqli_fetch_field($result)) {
     echo '[' . $property->name . ']';
    array_push($all_property, $property->name);  //save those to array
}
echo "}<br>";
//showing all data
$count =0;
while ($row = mysqli_fetch_array($result)) {
echo '{' ;
    foreach ($all_property as $item) 
    {
        echo '['.$row[$item].']' ;
        $count++;
 //get items using property value
    }  
    echo '}'."<br>";

    $count =0;
}
echo "</div>";
}
?>