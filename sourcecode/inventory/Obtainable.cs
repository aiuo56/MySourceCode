// 取得可能クラス
using UnityEngine;
using System;
using System.Collections.Generic;

 
[Serializable]
public class Obtainable
{
    [SerializeField] public string itemName;
    GameObject gameObject;
   
 
    internal void Obtain(GameObject item)
    {
        //obtainableItemで例えば斧オブジェクトを引数として代入したので、それを新たに本クラスで定義したgameobjectに代入。
        gameObject = item;
         //thisはおそらくinventoryに記載されている引数を表しているかと
        Inventory.GetInstance().Obtain(this); 
    }
 
    public string GetItemName()
    {
        return itemName;
    }
 
    internal GameObject GetGameObject()
    {
        return gameObject;
    }
}
