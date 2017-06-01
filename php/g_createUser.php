<?php
$param = json_decode(base64_decode(urldecode(file_get_contents("php://input"))), true);

$id = $param['id'];
$pw = $param['pw'];
$name = $param['name'];

$conn = mssql_connect("127.0.0.1", "kyechan99", "henry0814");
mssql_select_db("Game", $conn);

$stmt = mssql_init('dbo.USP_CREATE_USER', $conn);;

mssql_bind($stmt, "@id",    $id,    SQLVARCHAR,    FALSE,    FALSE,    50);
mssql_bind($stmt, "@pw",    $pw,    SQLVARCHAR,    FALSE,    FALSE,    50);
mssql_bind($stmt, "@name",  $name,  SQLVARCHAR,    FALSE,    FALSE,    50);

$result = mssql_execute($stmt);

if ($result==true)
{
  do {
    while($row = mssql_fetch_array($result)) {
      echo $row[0];
    }
  } while(mssql_next_result($result));
}

mssql_free_statement($stmt);


?>
