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
        UnityEngine.UI.Text[] s_levelTxt;
        [SerializeField]
        UnityEngine.UI.Text[] s_NameTxt;
        [SerializeField]
        UnityEngine.UI.Text[] s_ExpTxt;
        [SerializeField]
        UnityEngine.UI.Text[] s_GoldTxt;
        [SerializeField]
        GameObject[] s_CreateBT;
        [SerializeField]
        GameObject[] s_RemoveBT;
        int[] charIdxes = new int[2];

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
                string[] arr = JsonFx.Json.JsonReader.Deserialize<string[]>(GM.ServerMng.base64decoded(www.downloadHandler.text));

                if (arr.Length > 1)
                {
                    charIdxes[0] = System.Convert.ToInt32(arr[0]);
                    s_NameTxt[0].text = arr[1];
                    s_levelTxt[0].text = "Lv." + arr[2];
                    s_ExpTxt[0].text = "Exp : " + arr[3];
                    s_GoldTxt[0].text = "G : " + arr[4];
                    s_CreateBT[0].SetActive(false);
                    s_RemoveBT[0].SetActive(true);
                }
                else
                {
                    s_NameTxt[0].text = "( 없음 )";
                    s_levelTxt[0].text = "";
                    s_ExpTxt[0].text = "";
                    s_GoldTxt[0].text = "";
                    s_CreateBT[0].SetActive(true);
                    s_RemoveBT[0].SetActive(false);
                }
                if (arr.Length > 5)
                {
                    charIdxes[1] = System.Convert.ToInt32(arr[5]);
                    s_NameTxt[1].text = arr[6];
                    s_levelTxt[1].text = "Lv." + arr[7];
                    s_ExpTxt[1].text = "Exp : " + arr[8];
                    s_GoldTxt[1].text = "G : " + arr[9];
                    s_CreateBT[1].SetActive(false);
                    s_RemoveBT[1].SetActive(true);
                }
                else
                {
                    s_NameTxt[1].text = "( 없음 )";
                    s_levelTxt[1].text = "";
                    s_ExpTxt[1].text = "";
                    s_GoldTxt[1].text = "";
                    s_CreateBT[1].SetActive(true);
                    s_RemoveBT[1].SetActive(false);
                }
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

        #region _REMOVE_CHAR_
        public void removeCharBT(int idx)
        {
            StartCoroutine(removeChar(idx));
        }
        IEnumerator removeChar(int idx)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("charIdx", charIdxes[idx] + "");

            UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/DB_Class/g_removeChar.php", GM.ServerMng.base64encoded(JsonFx.Json.JsonWriter.Serialize(dic)));
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