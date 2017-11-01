using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


#if NETFX_CORE  //UWP下编译  
using Windows.Networking.Connectivity;
using Windows.Networking;
using Windows.Storage;  
using Windows.Storage.Streams;
using System.IO;
#endif

public class Test : MonoBehaviour
{
    string str = "";
    private List<string> localIPs;

    void Start()
    {
#if NETFX_CORE

        ReadData();
#endif
        ReadIp();
    }


    /// <summary>
    /// Hololens或PC读取本地IP
    /// </summary>
    void ReadIp()
    {
#if UNITY_EDITOR
        // Get the ip of the Hololens to see if this device is the Holographic Camera device.
        localIPs = new List<string>();
        IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
        foreach (IPAddress ip in ips)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIPs.Add(ip.ToString().Trim());
            }
        }

#endif

#if NETFX_CORE
            IReadOnlyList<HostName> hosts = NetworkInformation.GetHostNames();
            foreach (HostName aName in hosts)
            {
                if (aName.Type == HostNameType.Ipv4)
                {
                    localIPs.Add(aName.ToString().Trim());
                }
            }
#endif

        foreach (string ip in localIPs)
        {
            Debug.Log("Local IP: " + ip);
            GameObject.Find("Canvas/Text").GetComponent<Text>().text = "本地IP:" + ip;
        }
    }




#if NETFX_CORE

    /// <summary>
    ///Hololens读取浏览器上的文件
    /// </summary>
    private async void ReadData()
    {

        StorageFolder docLib = ApplicationData.Current.LocalFolder;
        // var docFile = docLib.OpenStreamForReadAsync("\\Test20170815.txt");
        // 获取应用程序数据存储文件夹

        Stream stream = await docLib.OpenStreamForReadAsync("\\Test20170815.txt");

        // 获取指定的文件的文本内容
        byte[] content = new byte[stream.Length];
        await stream.ReadAsync(content, 0, (int)stream.Length);

        GameObject.Find("Canvas/Text").GetComponent<Text>().text  = Encoding.UTF8.GetString(content, 0, content.Length);
       

    }

#endif

}
