using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GM
{
    public class CharMng : MonoBehaviour
    {
        string nick;
        
        [SerializeField]
        UnityEngine.UI.Text charInfoTxt;

        public void nickFormChange(UnityEngine.UI.InputField ipf)
        {
            nick = ipf.text;
        }

        #region _VIEW_CHAR_
        public void viewCharBT()
        {
            StartCoroutine(viewChar());
        }
        IEnumerator viewChar()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", GM.ServerMng.user.id);

            UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_getCharInfo.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
            yield return www.Send();

            if (www.isError)
            {
                Debug.LogError( www.error);
            }
            else
            {
                charInfoTxt.text = www.downloadHandler.text;
                //Debug.Log("DDS : " + GM.ServerMng.base64decoded(www.downloadHandler.text));
                Debug.Log(www.downloadHandler.text);
            }
        }
        #endregion

        #region _CREATE_CHAR_
        public void createCharBT()
        {
            StartCoroutine(createChar());
        }
        IEnumerator createChar()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", GM.ServerMng.user.id);
            dic.Add("nick", nick);

            UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_createChar.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
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
        #endregion
    }
}