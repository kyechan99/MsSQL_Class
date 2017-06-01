using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GM
{
    public class ServerMng : MonoBehaviour
    {        
        public static User user;

        public void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public static string base64encoded(string text)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(bytesToEncode);
        }

        public static string base64decoded(string text)
        {
            byte[] decoded = System.Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(decoded);
        }
    }
}