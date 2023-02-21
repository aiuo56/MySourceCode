        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.UI;
        using System.Linq;
        using UnityEngine.EventSystems;
         
        public class Inventory : MonoBehaviour
        {
            [SerializeField] Transform content;
            [SerializeField] GameObject imagePrefab;
            public HashSet<string> inventory = new HashSet<string>();
            public static int passCount;
            public  static int capsuleCount;
            public  static int axeCount;
            private GameObject buttonComponent;


         

            
            static Inventory instance;

            public static Inventory GetInstance()
            {
                return instance;
            }

            private void Awake()
            {
                passCount = 1;
                capsuleCount = 1;
                axeCount = 1;
                instance = this;
                MouseWheel();
            }


            // アイテムを取得するメソッド
            public void Obtain(Obtainable item)
            {
                
                // アイテムの存在を確認したなら、itemはobtainbleのgameobject=itemのitem
                if (ItemManager.GetInstance().HasItem(item.GetItemName()))
                {
                    
                    if(!inventory.Contains(item.GetItemName())){
                        //スロットにイメージを貼る。
                       //Instantiate(生成したいもの、生成したものを置く位置)
                       GameObject goodsObj = Instantiate(imagePrefab, content);
                       Goods goods = goodsObj.GetComponent<Goods>(); // スクリプトを取得
                       goods.SetUp(item); // 画像など設定
                       inventory.Add(goods.itemName);//個数をカウントするためのリストにgoods加える。
                       if(goods.itemName == "pass"){
                        canPass();
                       }
                       if(capsuleCount == 0){
                        capsuleCount++;
                       }
                       else if(axeCount == 0){
                        axeCount++;
                       }
                       else if(passCount == 0){
                        passCount++;
                       }
                   }
                   else{

                    switch(item.GetItemName()){
                        case "capsule":
                        capsuleCount++;
                        Count(capsuleCount, "capsule");
                        
                        break;
                        case "axe":
                        axeCount++;
                        Count(axeCount, "axe");
                        
                        break;
                        case "pass":
                        passCount++;
                        Count(passCount, "pass");
                        
                        break;
                    }
                   }
                    MouseWheel();
                    item.GetGameObject().SetActive(false); // アイテムを非アクティブにする
                }
            }
            
            

            public void MouseWheel()
            {
               //gameobject.find ゲームオブジェクトを探す 
               buttonComponent = GameObject.Find("Canvas/Inventory/Slot/Button");
               //ボタンが選択された状態になる
               EventSystem.current.SetSelectedGameObject (buttonComponent);
           }

           public void canPass(){
               GameObject dontMess = GameObject.FindWithTag("Tell");
               GameObject dontPass = GameObject.FindWithTag("NoGo");
               dontMess.SetActive(false);
               dontPass.SetActive(false);
           

           }

           public void Count(int count, string itemName){
                Transform slot = GameObject.Find("Slot").transform;
                foreach(Transform slotChildren in slot){
                    if(slotChildren.GetComponent<Goods>()){
                        if(slotChildren.GetComponent<Goods>().itemName == itemName){
                            //goodsImageの子要素テキストを取得する。
                            GameObject textObject = slotChildren.transform.GetChild(0).gameObject;
                            textObject.GetComponent<Text>().text = "×" + count;
                            if(count <= 0){
                                Destroy(slotChildren.gameObject);
                                inventory.Remove(itemName);
                                MouseWheel();
                                break;
                            }
                        }
                    }

                }
            }
    }
