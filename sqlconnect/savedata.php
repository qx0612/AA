<?php
	
	$con = mysqli_connect('localhost', 'root', 'root', 'database_test');

	if (mysqli_connect_errno())
	{
		echo "1: Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$username = $_POST["name"];
	$newscore = $_POST["score"];

	$namecheckquerry = "SELECT username FROM players WHERE username='" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquerry) or die("2: Name check querry Failed");
	if (mysqli_num_rows($namecheck) != 1)
	{
		echo "5: Either no user with name, or more than one"; //error code #5 = number of names matching != 1
		exit();
	}

	$updatequery = "UPDATE players SET score = " . $newscore . " WHERE username = '" . $username . "';";
	mysqli_query($con, $updatequery) or die("7: Save query failed"); //error code #7 = Update query failed

	echo "0";
?>
