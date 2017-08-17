using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
#if NETFX_CORE  //UWP下编译  
using Windows.Storage;  
#endif
/// <summary>  
/// 数据控制类  
/// </summary>  
public class DataBase : MonoBehaviour
{
    TextMesh debugger;
    // Use this for initialization  
    void Start()
    {
        debugger = GetComponentInChildren<TextMesh>();
        ReadData();
    }

#if NETFX_CORE   //UWP下  
    private void ReadData()  
    {  
        StorageFolder docLib =  KnownFolders.DocumentsLibrary;  
        var docFile = docLib.OpenStreamForReadAsync("Data\\data.bin");  
        docFile.Wait();  
        var fs = docFile.Result;  
        //成功取出fs，后续操作自己玩  
    
        byte[] bt = new byte[512];
        int q = fs.Read(bt,0, bt.Length);
        //将读取到的二进制转换成字符串
        string s = new UTF8Encoding().GetString(bt);
        debugger.text = s;

        fs.Dispose();  
    }  
#else   //Unity下  

    private void ReadData()
    {
        string strDataPath = @"C:\Users\spacewzl\Documents\Config.txt";
        Stream fs = new FileStream(strDataPath, FileMode.Open, FileAccess.Read);
        //成功取出fs，后续操作自己玩 

        byte[] bt = new byte[512];
        
        int q = fs.Read(bt,0, bt.Length);
        Debug.Log(q + "<><><><><>" + bt.Length);
        //将读取到的二进制转换成字符串
        string s = new UTF8Encoding().GetString(bt);
        debugger.text = s;
        fs.Dispose();
    }
#endif
}