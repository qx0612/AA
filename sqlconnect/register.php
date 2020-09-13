<?php

	$con = mysqli_connect('localhost', 'root', 'root', 'database_test');

	//check that connection happened
	if (mysqli_connect_errno())
	{
		echo "1: Connection Failed"; //error code #1 = connection failed
		exit();
	}

	$username = $_POST["name"];
	$password = $_POST["password"];

	//check if name exists
	$namecheckquerry = "SELECT username FROM players WHERE username='" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquerry) or die("2: Name check querry Failed"); //error code #2 = name check query failed

	if (mysqli_num_rows($namecheck) > 0)
	{
		echo "3: Name already exists"; //error code #3 = name already exists
		exit();
	}

	//add user to the table
	$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
	$hash = crypt($password, $salt);
	$insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";
	mysqli_query($con, $insertuserquery) or die("4: Insert player query failed"); //error code #4 = insert query failed

	echo("0");
?>