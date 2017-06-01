using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Response : MonoBehaviour
{
    public static void ReceiveEmp(string str)
    {
        string jsonData = JsonFx.Json.JsonReader.Deserialize<string>(base64decoded(str));
        Debug.Log(jsonData);

        System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
        xmldoc.LoadXml(jsonData.Trim());

        if (xmldoc != null)
        {
            System.Xml.XmlNodeList nodes0 = xmldoc.SelectNodes("server/emp");
            foreach(System.Xml.XmlNode node in nodes0)
            {
                Debug.Log("emp : " + node["empno"].InnerText);
                Debug.Log("emp : " + node["ename"].InnerText);
                Debug.Log("emp : " + node["job"].InnerText);
            }

            System.Xml.XmlNodeList nodes1 = xmldoc.SelectNodes("server/dept");
            foreach (System.Xml.XmlNode node in nodes1)
            {
                Debug.Log("dept : " + node["deptno"].InnerText);
                Debug.Log("dept : " + node["dname"].InnerText);
                Debug.Log("dept : " + node["loc"].InnerText);
            }

            System.Xml.XmlNodeList nodes2 = xmldoc.SelectNodes("server/salgrade");
            foreach (System.Xml.XmlNode node in nodes2)
            {
                Debug.Log("salgrade : " + node["grade"].InnerText);
                Debug.Log("salgrade : " + node["losal"].InnerText);
                Debug.Log("salgrade : " + node["hisal"].InnerText);
            }
        }
    }

    static string base64encoded(string text)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(text);
        return System.Convert.ToBase64String(bytesToEncode);
    }

    static string base64decoded(string text)
    {
        byte[] decoded = System.Convert.FromBase64String(text);
        return Encoding.UTF8.GetString(decoded);
    }
}
