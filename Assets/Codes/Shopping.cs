using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    private int balance = 12000;
    public Text coins;
    private int productPrice;
    public string PriceTag;
    private int isAvailable = 0;
    public float timeCount = 30f;
    private int timePrice = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        productPrice = PlayerPrefs.GetInt(PriceTag,productPrice);
    }
    public void BuyThis()
    {
        if(balance >= productPrice)
        {
            balance -= productPrice;
            isAvailable = 1;
        }
    }
    public void ResetData()
    {
        balance = 0;
        timePrice = 200;
        timeCount = 30f;
    }
}
