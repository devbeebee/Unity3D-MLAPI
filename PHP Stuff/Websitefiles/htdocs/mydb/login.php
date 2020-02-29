<?php

	$connection = @new mysqli('localhost','root','','mydb');
	
	if($connection->connect_errno == 0)
	{
		if(isset($_POST['nick']) && isset($_POST['pass']) )
		{
			$nick = $_POST['nick'];
			$pass = $_POST['pass'];
			
			$rezultat = mysqli_query($connection, "SELECT * FROM users WHERE nick='$nick' AND pass='$pass'");
			
			if($rezultat->num_rows != 0)
			{
				echo 'Zalogowano pomyślnie';
			}
			else
			{
				echo 'Teki użytkownik nie istnieje';
			}
		}
		
		$connection->close();		
	}
?>