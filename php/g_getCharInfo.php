<?php
require_once 'g_config.php';
?>
<?php
$param = json_decode(base64_decode(urldecode(file_get_contents("php://input"))), true);

$id = $param['id'];

$conn = mssql_connect($DB['host'], $DB['id'], $DB['pw']);
mssql_select_db($DB['db'], $conn);

$stmt = mssql_init('dbo.USP_GET_CHAR', $conn);;

mssql_bind($stmt, "@id",    $id,    SQLVARCHAR,    FALSE,    FALSE,    50);

$result = mssql_execute($stmt);
$ret = array();

if ($result==true)
{
  while ($row = mssql_fetch_assoc($result)) {
    $JSONres = array (
       "nick" => $row['f_nick'],
       "level" => $row['f_level'],
       "exp" => $row['f_exp']
      );
      array_push($ret, $JSONres);
  }
  echo json_encode($ret);
  // do {
  //   while($row = mssql_fetch_array($result)) {
  //     echo "DDDDDD".$row[0];
  //     // echo base64_encode(json_encode($row[0]));
  //   }
  // } while(mssql_next_result($result));

}


mssql_free_statement($stmt);


?>
