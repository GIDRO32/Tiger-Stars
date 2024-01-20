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
    public int newSelectedIndex ,previousSelectedIndex;

    [SerializeField] Color ActiveSkinColor;
    [SerializeField] Color DefaultSkinColor;
    [SerializeField] Image CurrentSkin;

    void Start()
    {
        GetAvailableSkins();
        LoadPurchasedSkins();
        newSelectedIndex = previousSelectedIndex = 0;
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
        SelectSkin(newSelectedIndex);
    }
    public void AddSkin(Sprite picture)
    {
        if(SkinList == null)
        SkinList = new List<Skin>();

        Skin av = new Skin(){Image = picture};
        SkinList.Add(av);
        g = Instantiate(SkinUITemplate, SkinScrolling);
        g.transform.GetChild(0).GetComponent<Image>().sprite = av.Image;
        g.transform.GetComponent<Button>().AddEventListener(SkinList.Count-1, OnSkinClick);
        PlayerPrefs.SetInt("ItemAvailable_" + Shopping.Instance.ShopItemList.Count, 1);
        PlayerPrefs.Save();
    }
 void LoadPurchasedSkins()
    {
        SkinList.Clear(); // Clear existing skins
        ClearSkinUI(); // Clear existing skin UI elements

        // Always add the first skin
        AddSkin(Shopping.Instance.ShopItemList[0].Image);

        for (int i = 1; i < Shopping.Instance.ShopItemList.Count; i++)
        {
            if (PlayerPrefs.GetInt("ItemAvailable_" + i, 0) == 1)
            {
                AddSkin(Shopping.Instance.ShopItemList[i].Image);
            }
        }
        SelectSkin(newSelectedIndex);
    }
    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("ItemAvailable_" + skinIndex, 1);
        PlayerPrefs.Save();
    }
    void OnSkinClick(int SkinIndex)
    {
        SelectSkin(SkinIndex);
    }
    void SelectSkin(int SkinIndex)
    {
        previousSelectedIndex = newSelectedIndex;
        newSelectedIndex = SkinIndex;
        SkinScrolling.GetChild(newSelectedIndex).GetComponent<Image>().color = ActiveSkinColor;
        SkinScrolling.GetChild(previousSelectedIndex).GetComponent<Image>().color = DefaultSkinColor;
        CurrentSkin.sprite = SkinList[newSelectedIndex].Image;
    }
    void ClearSkinUI()
    {
        foreach (Transform child in SkinScrolling)
        {
            Destroy(child.gameObject);
        }
    }
    public void ResetData()
    {
        for (int i = 1; i < Shopping.Instance.ShopItemList.Count; i++)
        {
            PlayerPrefs.SetInt("ItemAvailable_" + i, 0);
            if (SkinScrolling.childCount > i) // Check if the child exists
            {
                Destroy(SkinScrolling.GetChild(i).gameObject);
            }
        }
        PlayerPrefs.Save();
        SelectSkin(0);
        LoadPurchasedSkins(); // Reload the skins to update the UI
    }
}
