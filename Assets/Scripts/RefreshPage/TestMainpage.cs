using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainpage : MonoBehaviour {
    Dictionary<object, ArrayList> dic = new Dictionary<object, System.Collections.ArrayList>();

    /// <summary>
    /// 实例预设
    /// </summary>
    public GameObject prefab;

    public Text PageIndex { get; private set; }

    void Start ()
    {
        if (PageIndex == null)
        {
            if (GameObject.Find("Canvas/Command/Text"))
                PageIndex = GameObject.Find("Canvas/Command/Text").GetComponent<Text>();
        }
        //虚构数据
        for (int i = 0; i < 5; i++)
        {
            ArrayList temp = new ArrayList();
            for (int j = 0; j < 3; j++)
            {
                TTTTT t = new TTTTT();
                t.Name = (i + "<+>" + j).ToString();
                temp.Add(t);
            }
            dic.Add(i, temp);

        }
        //放到运算类中
        RefreshPage<TTTTT>.GetInstance().Init(dic , RefreshSelf);
    }


    #region Button回调
    /// <summary>
    /// 切换分类“《”按钮
    /// </summary>
    public void Left()
    {
        RefreshPage<TTTTT>.GetInstance().Refreshcurrent(ChoosePage.Minus);
    }

    /// <summary>
    /// 切换分类“》”按钮
    /// </summary>
    public void Right()
    {
        RefreshPage<TTTTT>.GetInstance().Refreshcurrent(ChoosePage.Add);
    }


    /// <summary>
    /// 当前分类的下一页
    /// </summary>
    public void Next()
    {
        RefreshPage<TTTTT>.GetInstance().GetPageCells(ChoosePage.Add);
    }

    /// <summary>
    /// 当前分类的上一页
    /// </summary>
    public void Previous()
    {
      RefreshPage<TTTTT>.GetInstance().GetPageCells(ChoosePage.Minus);
    }

    #endregion


    #region 事件回调
    void RefreshSelf(ArrayList arr,int currentpage,int allpages)
    {
        ObjectPool.Instance.UnSpawnAll();
        if (arr != null)
        {
            int length = arr.Count;
            for (int i = 0; i < length; i++)
            {
                float r = Random.Range(0, 1f);
                float g = Random.Range(0, 1f);
                float b = Random.Range(0, 1f);
                Color c = new Color(r, g, b);

                GameObject go = ObjectPool.Instance.Spawn("Image");
                go.SetActive(true);
                go.transform.SetParent(prefab.transform.parent);
                go.transform.localScale = Vector3.one;
                go.GetComponentInChildren<Image>().color = c;
                go.GetComponentInChildren<Text>().text = (arr[i] as TTTTT).Name;
            }
        }
        RefreshText(currentpage,allpages);
    }
    private void RefreshText(int current,int allpages)
    {
        if (PageIndex)
            PageIndex.text = "第" +( current + 1).ToString() + "页/共" + allpages.ToString() + "页";
    }

    #endregion

}


public class TTTTT
{
    public string Name;
}
