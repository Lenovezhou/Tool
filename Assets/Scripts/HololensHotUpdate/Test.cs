using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


#if NETFX_CORE  //UWP下编译  
using Windows.Storage;  
using Windows.Storage.Streams;
using System.IO;
#endif

public class Test : MonoBehaviour
{
    string str = "Null";
    // Use this for initialization
    void Start()
    {
        ReadData();
        //GameObject.Find("Canvas/Text").GetComponent<Text>().text = ReadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

#if !NETFX_CORE   //非UWP下  
    private void ReadData()
    {
        GameObject.Find("Canvas/Text").GetComponent<Text>().text = "Test";
    }
#else
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
