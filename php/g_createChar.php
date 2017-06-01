<?php
require_once 'g_config.php';
?>
<?php
$param = json_decode(base64_decode(urldecode(file_get_contents("php://input"))), true);

$id = $param['id'];
$nick = $param['nick'];

$conn = mssql_connect($DB['host'], $DB['id'], $DB['pw']);
mssql_select_db($DB['db'], $conn);

$stmt = mssql_init('dbo.USP_CREATE_CHAR', $conn);;

mssql_bind($stmt, "@id",        $id,    SQLVARCHAR,    FALSE,    FALSE,    50);
mssql_bind($stmt, "@nick",    $nick,    SQLVARCHAR,    FALSE,    FALSE,    50);

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
