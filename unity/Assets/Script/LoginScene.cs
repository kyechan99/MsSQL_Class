using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScene : MonoBehaviour
{
    string id;
    string pw;

    public void idFormChange(UnityEngine.UI.InputField ipf)
    {
        id = ipf.text;
    }
    public void pwFormChange(UnityEngine.UI.InputField ipf)
    {
        pw = ipf.text;
    }

    public void loginBT()
    {
        StartCoroutine(login());
    }
    IEnumerator login()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("id", id);
        dic.Add("pw", pw);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_checkUser.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
        yield return www.Send();
        
        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            if (www.downloadHandler.text.Equals("SUCCESS_LOGIN"))
            {
                GM.ServerMng.user.id = id;
                GM.ServerMng.user.pw = pw;
                Debug.Log("SUCCESS Login");
                SceneManager.LoadScene("LobbyScene");
            }
            else
            {
                Debug.LogError("WRONG ID or PW");
            }
        }
    }

    public void registerBT()
    {
        SceneManager.LoadScene("RegisterScene");
    }
}
