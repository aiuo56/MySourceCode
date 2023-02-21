using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
 using System.Linq;

public class ButtonManager : MonoBehaviour
{
 
    public void push()
    {
        var goods = GetComponent<Goods>();
        
            switch(goods.itemName){
            //回復薬だった場合。
            case "capsule":
            Inventory.capsuleCount--;
            Inventory.GetInstance().Count(Inventory.capsuleCount, "capsule");
            //プレイヤーのHPを回復させる関数を記述
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerScript>().forButtonManager();
            //個数を減らす
            break;

　　　　　　 //斧だった場合
            case "axe":
            Inventory.axeCount--;
            Inventory.GetInstance().Count(Inventory.axeCount, "axe");
            //斧を装備状態にさせる関数を記述
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hit, 100f)){
                if(hit.collider.CompareTag("Extra")){
                    Destroy(hit.collider.gameObject);
                }
            }
            break;
　　　　　　 //通行証だった場合
            case "pass":
            Inventory.passCount--;
            Inventory.GetInstance().Count(Inventory.passCount, "pass");
            //通行証を選択状態の場合、通行可能にする
            break;
        }
    }

    

    






     
}
