<?php
require_once 'g_config.php';
?>
<?php
$param = json_decode(base64_decode(urldecode(file_get_contents("php://input"))), true);

$id = $param['id'];
$pw = $param['pw'];

$conn = mssql_connect($DB['host'], $DB['id'], $DB['pw']);
mssql_select_db($DB['db'], $conn);

$stmt = mssql_init('dbo.USP_GET_USER', $conn);;

mssql_bind($stmt, "@id",    $id,    SQLVARCHAR,    FALSE,    FALSE,    50);
mssql_bind($stmt, "@pw",    $pw,    SQLVARCHAR,    FALSE,    FALSE,    50);

$result = mssql_execute($stmt);
$ret = array();

if ($result==true)
{
  do {
    while($row = mssql_fetch_array($result)) {
      array_push($ret, $row[0]);
      array_push($ret, $row[1]);
      array_push($ret, $row[2]);
      array_push($ret, $row[3]);
      array_push($ret, $row[4]);
    }
  } while(mssql_next_result($result));

  echo base64_encode(json_encode($ret));
}


mssql_free_statement($stmt);


?>
