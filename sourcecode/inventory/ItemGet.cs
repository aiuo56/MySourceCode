using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemGet : MonoBehaviour
{
    //左クリックをした際に、アクションを二つ起こしたいために、ButtonManagerとこのクラスでbool値を設定して、使い分けできるようにする。
    private bool itemGet = false;
    Collider coll;
    private float scroll;

    [SerializeField]
    private Camera targetCamera;

    void Update()
    {

         if(Input.GetMouseButton(1)){
            Inventory.GetInstance().MouseWheel();
        }

        if(Input.GetMouseButtonDown(0)){
            Inventory.GetInstance().MouseWheel();
        }

        ItemPick();


        
    }

    public void ItemPick(){
        RaycastHit hit;
        //指定したカメラ範囲内にrayが当たったら
        if(Physics.Raycast(targetCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hit, 100f))
        {


            itemGet = true;

            //もし、左クリックした際
    　　　　 if(Input.GetMouseButtonDown(0) && itemGet == true)
            {
               coll = hit.collider;
　　　　　　    var sc = coll.GetComponent<ITouchable>();
               if(sc != null)
               {
                //Vector3 mousePosition = Input.mousePosition;
                sc.Touch(gameObject);
               }
            }

            
        }

    }
}
