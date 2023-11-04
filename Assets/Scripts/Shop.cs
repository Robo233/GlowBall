using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Linq;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject Content;
    [SerializeField] GameObject CoinCounter;
    [SerializeField] GameObject CoinAmountText;
    [SerializeField] GameObject BuyBallWindow;
    [SerializeField] GameObject GlowBall;
    [SerializeField] GameObject ContentsContent;
    [SerializeField] GameObject BallNameText;
    GameObject CurrentPressedButton;

    [SerializeField] GameObject[] ItemIcons;

    [SerializeField] Material[] AllItems;
    [SerializeField] List<Material> BoughtItems = new List<Material>();

    public int CoinAmount;

    [SerializeField] Sprite GreenSprite; 
    [SerializeField] Sprite BlueSprite;
    [SerializeField] Sprite[] IconSprites;

    Material UsedMaterial;

    string nameOfButton;
    string CurrentUsedMaterialName;

    [SerializeField] string BoughtItemToSaveString;
    [SerializeField] List<string> BoughtItemsSave = new List<string>();

    void Start(){
        if(PlayerPrefs.GetString("BoughtItems")==String.Empty){
        PlayerPrefs.SetString("BoughtItems","Orange ball");
        }
         if(PlayerPrefs.GetString("CurrentUsedMaterialName")==String.Empty){
        PlayerPrefs.SetString("CurrentUsedMaterialName","Orange ball");
        }
        CurrentUsedMaterialName = PlayerPrefs.GetString("CurrentUsedMaterialName");
        BoughtItemToSaveString = PlayerPrefs.GetString("BoughtItems");
        BoughtItemsSave = PlayerPrefs.GetString("BoughtItems").Split(',').ToList();
        for(int i=0;i<BoughtItemsSave.Count;i++){
            GameObject CurrentItem = ItemIcons.SingleOrDefault(item => item.name == BoughtItemsSave[i]);
            CurrentItem.transform.Find("ItemBorder").GetComponent<Image>().sprite = BlueSprite;
            CurrentItem.transform.Find("ItemPrice").gameObject.SetActive(false);
            CurrentItem.transform.Find("ItemImage").gameObject.SetActive(false);
            BoughtItems.Add(AllItems.SingleOrDefault(item => item.name == BoughtItemsSave[i]));
            if(CurrentUsedMaterialName == CurrentItem.name){
                CurrentItem.transform.Find("ItemBorder").GetComponent<Image>().sprite = GreenSprite;
                GlowBall.GetComponent<MeshRenderer>().material = AllItems.SingleOrDefault(item => item.name == BoughtItemsSave[i]);
            }
        }

        CoinAmount = PlayerPrefs.GetInt("CoinAmount");
        PositionCoin();
        
    }

    void Update() {
        CoinCounter.GetComponent<TextMeshProUGUI>().text =  CoinAmount.ToString();
    }



   public void ShopMenuOn(){
       Content.transform.localPosition = new Vector3(Content.transform.localPosition.x, -1250, Content.transform.localPosition.z);

   }

   public void SelectIcon(){
       CurrentPressedButton = EventSystem.current.currentSelectedGameObject;
       nameOfButton = CurrentPressedButton.name;
       UsedMaterial = AllItems.SingleOrDefault(item => item.name == nameOfButton);
     
           if(!BoughtItems.Contains(AllItems.SingleOrDefault(item => item.name == nameOfButton))){
               if(Int32.Parse(CurrentPressedButton.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text)<=CoinAmount){
               OpenBuyBallWindow();
            
               }
               
           }
           else{
               GlowBall.GetComponent<MeshRenderer>().material = UsedMaterial;
               for(int i=0;i<BoughtItems.Count;i++){
          
            ItemIcons.SingleOrDefault(item => item.name == BoughtItems[i].name).transform.Find("ItemBorder").GetComponent<Image>().sprite = BlueSprite;
            }
        CurrentPressedButton.transform.Find("ItemBorder").GetComponent<Image>().sprite = GreenSprite;
        PlayerPrefs.SetString("CurrentUsedMaterialName",CurrentPressedButton.name);
        
           }
          
      
   }

   public void OpenBuyBallWindow(){
       CurrentPressedButton = EventSystem.current.currentSelectedGameObject;
       nameOfButton = CurrentPressedButton.name;
       BuyBallWindow.SetActive(true);
       BuyBallWindow.transform.Find("Ballicon").GetComponent<Image>().sprite = IconSprites.SingleOrDefault(item => item.name == nameOfButton);
       BallNameText.GetComponent<TextMeshProUGUI>().text = nameOfButton;
   }

    public void BuyItem(){
        UsedMaterial = AllItems.SingleOrDefault(item => item.name == nameOfButton);
        GlowBall.GetComponent<MeshRenderer>().material = UsedMaterial;
        BoughtItems.Add(UsedMaterial);
        BuyBallWindow.SetActive(false);
        CoinAmount -= Int32.Parse(CurrentPressedButton.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text);
        PlayerPrefs.SetInt("CoinAmount",CoinAmount);

         if(CoinAmount<10){
            CoinAmountText.transform.localPosition = new Vector3(168.37f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount<100 && CoinAmount>=10){
            CoinAmountText.transform.localPosition = new Vector3(109.05f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount<1000 && CoinAmount>=100){
            CoinAmountText.transform.localPosition = new Vector3(40.9f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount>=1000){
            CoinAmountText.transform.localPosition = new Vector3(-15.69f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }

        
        for(int i=0;i<BoughtItems.Count;i++){
            ItemIcons.SingleOrDefault(item => item.name == BoughtItems[i].name).transform.Find("ItemBorder").GetComponent<Image>().sprite = BlueSprite;
        }
        CurrentPressedButton.transform.Find("ItemBorder").GetComponent<Image>().sprite = GreenSprite;
        CurrentPressedButton.transform.Find("ItemPrice").gameObject.SetActive(false);
        CurrentPressedButton.transform.Find("ItemImage").gameObject.SetActive(false);
        BoughtItemToSaveString += "," + CurrentPressedButton.name;
        PlayerPrefs.SetString("BoughtItems",BoughtItemToSaveString);
        PlayerPrefs.SetString("CurrentUsedMaterialName",CurrentPressedButton.name);
    }

    string FirstWord(string str){
        string[] words = str.Split(new[] { " " }, StringSplitOptions.None);
	    return words[0];
    }

    public void PositionCoin(){
        PlayerPrefs.SetInt("CoinAmount",CoinAmount);
        if(CoinAmount<10){
            CoinAmountText.transform.localPosition = new Vector3(168.37f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount<100 && CoinAmount>=10){
            CoinAmountText.transform.localPosition = new Vector3(109.05f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount<1000 && CoinAmount>=100){
            CoinAmountText.transform.localPosition = new Vector3(40.9f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
        else if(CoinAmount>=1000){
            CoinAmountText.transform.localPosition = new Vector3(-15.69f,CoinAmountText.transform.localPosition.y,CoinAmountText.transform.localPosition.z);
        }
    }

}
