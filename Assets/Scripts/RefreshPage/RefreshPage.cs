using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChoosePage
{
    Init,Add,Minus
}

public class RefreshPage<T>
{

    #region 字段
    private static RefreshPage<T> Instance;
    public static RefreshPage<T> GetInstance()
    {
        if (Instance == null)
        {
            Instance = new RefreshPage<T>();
            ProCellCounts = 2;
        }
        return Instance;
    }

    List<T> list = new List<T>();
    ArrayList arr = new ArrayList();

    Dictionary<object, ArrayList> Allmodels = new Dictionary<object, ArrayList>();

    //每页数量
    public static int ProCellCounts { get; private set; }
    //当前页数
    private int currentpage = 0;
    //当前类别
    private int currentmode = 0;

    //回调刷新
    Action<ArrayList,int,int> call; 

    #endregion



    #region 帮助方法
    /// <summary>
    /// 传枚举对应不同字典
    /// </summary>
    /// <param name="dic"></param>
    public void Init(Dictionary<object,ArrayList> dic ,Action<ArrayList,int,int> callback)
    {
        this.call = callback;
        Allmodels = dic;


        //启动时刷新
        Refreshcurrent(ChoosePage.Init);
        GetPageCells(ChoosePage.Init);

       
    }

    /// <summary>
    /// 获取当前list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lis"></param>
    public void Refreshcurrent(ArrayList lis)
    {
        currentpage = 0;
        arr.Clear();
        arr.AddRange(lis);
        GetPageCells(ChoosePage.Init);
    }

    public void Refreshcurrent(ChoosePage cp)
    {
        bool isfind = true;
        switch (cp)
        {
            case ChoosePage.Add:
                if (currentmode < GetAllpages() - 1)
                {
                    currentmode++;
                }
                else
                {
                    isfind = false;
                }
                break;
            case ChoosePage.Minus:
                if (currentmode > 0)
                {
                    currentmode--;
                }
                else {
                    isfind = false;
                }
                break;
            case ChoosePage.Init:
                break;
            default:
                break;
        }
        if (isfind)
        {
            Refreshcurrent(Allmodels[currentmode]);
        }

    }




    /// <summary>
    /// 获取当前list能够分多少页
    /// </summary>
    /// <returns></returns>
    int GetPageCounts()
    {
        return Mathf.CeilToInt(arr.Count / (float)ProCellCounts);
    }

    /// <summary>
    /// 总分类数
    /// </summary>
    /// <returns></returns>
    int GetAllpages()
    {
        return Allmodels.Count;
    }

    public void GetPageCells(ChoosePage cp) 
    {
        switch (cp)
        {
            case ChoosePage.Add:
                Debug.Log(GetPageCounts());
                if (currentpage < GetPageCounts())
                {
                    currentpage++;
                }
                break;
            case ChoosePage.Minus:
                if (currentpage > 0)
                {
                    currentpage--;
                }
                break;
            case ChoosePage.Init:
                break;
            default:
                break;
        }
        ArrayList arrtemp = new ArrayList();
        for (int i = (currentpage) * ProCellCounts; i < (currentpage + 1) * ProCellCounts; i++)
        {
            if (arr.Count > i)
            {
                object o = arr[i];
                arrtemp.Add(o);
            }
        }
        //回调
        if (call != null && arrtemp.Count != 0)
        {
            call(arrtemp,currentpage, GetPageCounts());
        }
    }



    #endregion




}
