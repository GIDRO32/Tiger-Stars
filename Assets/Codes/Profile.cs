using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Profile : MonoBehaviour
{
    #region SingleTon:Profile
    public static Profile Instance;
    void Awake()
    {
        if(Instance == null)
        Instance = this;
        else
        Destroy(gameObject);
    }
    #endregion
    public class Skin
    {
        public Sprite Image;
    }

    public List<Skin> SkinList;
    [SerializeField] GameObject SkinUITemplate;
    [SerializeField] Transform SkinScrolling;
    GameObject g;
    void Start()
    {
        GetAvailableSkins();
    }
    void GetAvailableSkins()
    {
        for(int i = 0; i < Shopping.Instance.ShopItemList.Count; i++)
        {
            if(Shopping.Instance.ShopItemList[i].isAvailable)
            {
                AddSkin(Shopping.Instance.ShopItemList[i].Image);
            }
        }
    }
    void AddSkin(Sprite picture)
    {
        if(SkinList == null)
        SkinList = new List<Skin>();

        Skin av = new Skin(){Image = picture};
        SkinList.Add(av);
        g = Instantiate(SkinUITemplate, SkinScrolling);
        g.transform.GetChild(0).GetComponent<Image>().sprite = av.Image;
    }
}
