<form action="Word.php" method="POST">
<p>Please enter a Word ?</p>
<p><input type="text" name="OriginalWord" /></p>
<input type="submit" value="Submit" />
</form>
<?php
$fp3 = fopen('./data3.txt', 'w');
fclose($fp3);
unlink('data3.txt');
echo '<body style="background-color:lightgreen">';
if(isset($_POST['OriginalWord']))
{
	$data1text = array();
	$data2text = array();
	$OriginalWord = $_POST['OriginalWord'];
	;$data1text = implode("','",file('./data1.txt'));
	$fp1 = fopen('./data1.txt', 'r');
	while(!feof($fp1))
	{
	$data1text=fgets($fp1);	
	}
	;$data2text = implode("','",file('./data2.txt'));
	$fp2 = fopen('./data2.txt', 'r');
	while(!feof($fp2))
	{
	$data2text=fgets($fp2);	
	}
	
	$fp3 = fopen('./data3.txt', 'w');
	$data3text = implode("','",file('./data3.txt'));
	echo"<br>Old Text File 1<br><br>";
	echo $data1text;
	echo"<br>";
	
	echo"<br>Old Text File 2<br><br>";
	echo $data2text;
	echo"<br>";
	
	echo"<br>Old Text File 3<br><br>";
	echo $data3text;
	echo"<br>";
	
    	//$data3text = str_replace($OriginalWord, $data1text, $data3text);
	$done=false;
	$i=0;
	$data1text = explode(' ',$data1text);
		//print_r($data1text);
		$data2text = explode(' ',$data2text);
		//print_r($data2text);
		$count1=count($data1text);
		$count2=count($data2text);
		$count = $count1+$count2;
		//echo $count;
	while($i<$count)
	{
		if($i<$count1)
		{
			$data3text .= " ";
			$data3text .= $data1text[$i];
		}
		if($i<$count2)
		{
			$data3text .= " ";
			$data3text .= $data2text[$i];
		}
		if($i==0)
		{
			$data3text .= " ";
			$data3text.=$OriginalWord;
		}
		++$i;
		
	}
	$i=0;
		while($done!=true) 
		{
			if(flock($fp3, LOCK_EX))
			{
				fwrite($fp3, $data3text);
				echo "<script>alert('Successfully updated word to text file!');
				</script>";
				$done = true;
			}
			$i++;
			if($i ==10) 
			{
				echo "Error: Could not update word because file is in use! <br> Please try again ";
				$done = true;
			} 
		}
    
		fclose($fp3);
		$str = implode(file('./data3.txt'));
		$fp = fopen('./data3.txt', 'r');
		echo"<br>New Updated Text File<br><br>";
    	echo $str;
}
	
?>
