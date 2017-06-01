using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    public Text idTxt;
    public Text nameTxt;
    public Text joinTxt;
    public Text lateJoinTxt;
    public Text heartTxt;

    void Start()
    {
        if (!GM.ServerMng.user.id.Equals(""))
            StartCoroutine(getUserInfo());
    }

    IEnumerator getUserInfo()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("id", GM.ServerMng.user.id);
        dic.Add("pw", GM.ServerMng.user.pw);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_getUserInfo.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
        yield return www.Send();


        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string[] arr = JsonFx.Json.JsonReader.Deserialize<string[]>(GM.ServerMng.base64decoded(www.downloadHandler.text));

            idTxt.text = arr[0];
            GM.ServerMng.user.name = arr[1];
            nameTxt.text = GM.ServerMng.user.name;
            joinTxt.text = arr[2];
            lateJoinTxt.text = arr[3];
            heartTxt.text = arr[4];
            GM.ServerMng.user.heart = System.Convert.ToInt32(arr[4]);

            foreach (string ar in arr)
            {
                Debug.Log(ar);
            }
        }
    }

    public void getUserCharInfoBT()
    {

    }
}
