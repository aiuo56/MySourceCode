using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Goods :MonoBehaviour
{
    
    GameObject itemObject;
    public string itemName;
    Image image;
   
   
    
   
   

    static Goods instance;
    public static Goods GetInstance()
    {
        return instance;
    }
       
    private void Awake()
    {
        image = GetComponent<Image>();
        instance = this;
    }
 
    public void SetUp(Obtainable item)
    {
         
        image = GetComponent<Image>(); // Imageコンポーネント
 
        this.itemName = item.GetItemName(); // アイテム名を取得
             // 画像を取得してImageコンポーネントに入れる
            image.sprite = ItemManager.GetInstance().GetItem(this.itemName).GetIcon(); 
            this.itemObject = item.GetGameObject(); // オブジェクトを取得
            
        
    }

    public void addInSlot(){

    }

   
 
       
    
}
