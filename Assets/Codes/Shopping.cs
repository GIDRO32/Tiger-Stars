using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    #region SingleTon:Shopping
    public static Shopping Instance;
    void Awake()
    {
        if(Instance == null)
        Instance = this;
        else
        Destroy(gameObject);
    }
    #endregion
    [System.Serializable] public class ShopItem
    {
        public Sprite Image;
        public string Title;
        public int price;
        public bool isAvailable = false;
    }
    public List<ShopItem> ShopItemList;
    [SerializeField]GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrolling;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] Text Balance;
    Button buyBtn;
    public GameObject NotEnough_MSG;
    void Start()
    { 
        NotEnough_MSG.SetActive(false);
        int len = ShopItemList.Count;
        for(int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrolling);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].Image;
            g.transform.GetChild(1).GetComponent<Text>().text = ShopItemList[i].Title;
            g.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = ShopItemList[i].price.ToString();
            buyBtn = g.transform.GetChild(3).GetComponent<Button>();
            buyBtn.interactable = !ShopItemList[i].isAvailable;
            buyBtn.AddEventListener(i, OnBuyBtnClicked);
        }
    }
    IEnumerator MessageHang()
    {
            for(float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                yield return new WaitForSeconds(0.1f);
            }
            NotEnough_MSG.SetActive(false);
    }
    void OnBuyBtnClicked(int itemIndex)
    {
        if(Product.Instance.HasEnoughCoins(ShopItemList[itemIndex].price))
        {
        Product.Instance.UseCoins(ShopItemList[itemIndex].price);
        ShopItemList[itemIndex].isAvailable = true;
        buyBtn = ShopScrolling.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Owned";
        }
        else
        {
            Debug.Log("Not Enough!");
            NotEnough_MSG.SetActive(true);
            StartCoroutine("MessageHang");
        }
    }
    void Update()
    {
        Balance.text = Product.Instance.Coins.ToString();
    }
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }
}
