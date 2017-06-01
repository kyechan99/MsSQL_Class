using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Request : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("post"))
        {
            StartCoroutine(postText());
        }
        if (GUILayout.Button("get"))
        {
            StartCoroutine(getText());
        }
        if (GUILayout.Button("req"))
        {
            StartCoroutine(requestPostText());
        }
    }

    string sss;
    string sss2;
    public void inputFieldChange(UnityEngine.UI.InputField s)
    {
        sss = s.text;
    }
    public void inputFieldChange2(UnityEngine.UI.InputField s)
    {
        sss2 = s.text;
    }
    public void butt()
    {
        //Debug.Log(sss);
        StartCoroutine(postText());
    }

    IEnumerator postText()
    {

        //WWWForm form = new WWWForm();
        //form.AddField("id", "root");
        //form.AddField("pw", "1234");

        Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add("id", sss);
        //dic.Add("pw", "1234");
        dic.Add("empno", sss);
        dic.Add("ename", sss2);
        Debug.Log(sss);
        Debug.Log(sss2);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/postAccount.php", base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
        yield return www.Send();

        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            //Debug.Log(base64decoded(www.downloadHandler.text));
            string[] arr = JsonFx.Json.JsonReader.Deserialize<string[]>(base64decoded(www.downloadHandler.text));

            foreach (string ar in arr)
            {
                Debug.Log(ar);
            }
        }
    }

    IEnumerator getText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1/DB_Class/getAccount.php?id=root&pw=1234");
        yield return www.Send();

        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log(base64decoded(www.downloadHandler.text));
        }
    }

    IEnumerator requestPostText()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("empno", sss);
        dic.Add("ename", sss2);

        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/postAccount.php", base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
        yield return www.Send();

        if (www.isError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Response.ReceiveEmp(www.downloadHandler.text);
        }
    }

    string base64encoded(string text)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(text);
        return System.Convert.ToBase64String(bytesToEncode);
    }

    string base64decoded(string text)
    {
        byte[] decoded = System.Convert.FromBase64String(text);
        return Encoding.UTF8.GetString(decoded);
    }
}
