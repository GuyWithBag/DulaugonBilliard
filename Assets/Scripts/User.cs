using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net; // for network IP address and DNS
using System; // for system date and time

public class User : MonoBehaviour
{
    public static string current_user;
    public static string hostName;
    public static string myIP;
    public static DateTime now;

    public Text txt;
    public Text gamenumber;

    int SStart = 0;
    int End = 0;
    string strStart;
    string strEnd;

    void Start()
    {
        strStart = "Users";
        strEnd = "Documents";
        string strSource = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        hostName = Dns.GetHostName(); // Retrive the Name of PC
        myIP = Dns.GetHostByName(hostName).AddressList[1].ToString();

        SStart = (strSource.IndexOf(strStart, 0) + strStart.Length) + 1;
        End = (strSource.IndexOf(strEnd, SStart)) - 1;
        current_user = strSource.Substring(SStart, End - SStart);
        txt.text = "Welcome " + current_user;
        //gamenumber.text =  "Game " + Math.game_num;
        gamenumber.text = "Game 1";
        now = DateTime.Now;
        //print(now);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
