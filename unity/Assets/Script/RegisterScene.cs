using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterScene : MonoBehaviour
{
    string id;
    string pw;
    string name;

    public void idFormChange(UnityEngine.UI.InputField ipf)
    {
        id = ipf.text;
    }
    public void pwFormChange(UnityEngine.UI.InputField ipf)
    {
        pw = ipf.text;
    }
    public void nameFormChange(UnityEngine.UI.InputField ipf)
    {
        name = ipf.text;
    }

    public void backBT()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public void registerBT()
    {
        StartCoroutine(register());
    }
    IEnumerator register()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("id", id);
        dic.Add("pw", pw);
        dic.Add("name", name);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_createUser.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
        yield return www.Send();
        
        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
