using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// 因为要用到 MonoBehaviour 里的方法所以才抽出来，不然直接写在PackView里
public class PackCompent : MonoBehaviour
{
    public static PackCompent Intance;

    public void Awake()
    {
        Intance = this;
    }

    public void ShowPack(List<PackModel> modelList)
    {
        while (this.transform.childCount != 0)
        {
            GameObject.DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        foreach (var item in modelList)
        {
            GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("PackItem"));
            obj.transform.parent = this.transform;
            obj.transform.localScale = Vector3.one;
            PackItem packItem = obj.GetComponent<PackItem>();
            packItem.Model = item;
        }
    }
}
