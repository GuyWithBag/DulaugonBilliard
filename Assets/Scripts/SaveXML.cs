using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.IO;

public class SaveXML : MonoBehaviour
{

    string path = Directory.GetCurrentDirectory();
    XmlDocument doc = new XmlDocument();


    public static bool save_now = false;

    void Start()
    {

        doc.Load(path + "\\Assets\\user_data.xml");
		Debug.Log("JL");
        XmlNode root = doc.FirstChild;

        XmlNodeList elemList = doc.GetElementsByTagName("name");

        for (int i = 0; i < elemList.Count; i++)
        {
            //print(elemList[i].InnerXml);
        }




    }

    void Update()
    {
        if (save_now == true)
        {
            add_new();
            save_now = false;
        }
    }

    public void add_new()
    {
        XmlElement roots = doc.DocumentElement;

        XmlElement e1 = doc.CreateElement("user");
        XmlElement e2 = doc.CreateElement("name");
        XmlElement e3 = doc.CreateElement("hostname");
        XmlElement e4 = doc.CreateElement("ipaddress");
        XmlElement e5 = doc.CreateElement("started");
        XmlElement e6 = doc.CreateElement("ended");
        XmlElement e7 = doc.CreateElement("life_left");

        if (roots != null)
        {
            roots.InsertAfter(e1, roots.LastChild);
        }

        if (e1 != null)
        {
            e1.InsertAfter(e2, e1.LastChild);
            e2.InnerText = User.current_user;

            e1.InsertAfter(e3, e1.LastChild);
            e3.InnerText = User.hostName;

            e1.InsertAfter(e4, e1.LastChild);
            e4.InnerText = User.myIP;

            e1.InsertAfter(e5, e1.LastChild);
            e5.InnerText = User.now.ToString();

            e1.InsertAfter(e6, e1.LastChild);
            //e6.InnerText = goodjob.end_time.ToString();
            e6.InnerText = "";

            e1.InsertAfter(e7, e1.LastChild);
            e7.InnerText = MyLife.numlife.ToString();
        }

        doc.Save(path + "\\Assets\\user_data.xml");
    }


}
