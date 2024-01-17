using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    public Text ProductTitle;
    public string ProductTag;
    public string PriceTag;
    public int price;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt(PriceTag, price);
        ProductTitle.text = ProductTag + "\nPrice: " + price;
    }
}
