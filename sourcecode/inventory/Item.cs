using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
[Serializable]
[CreateAssetMenu(fileName = "Item", menuName="CreateItem")]
public class Item : ScriptableObject {
 
    public enum KindOfItem {
        Axe,
        Pass,
        Capsule
    }
 
    //　アイテムの種類
    [SerializeField]
    private KindOfItem kindOfItem;
    //　アイテムのアイコン
    [SerializeField]
    private Sprite icon;
    //　アイテムの名前
    [SerializeField]
    string itemName;
    //　アイテムの情報
    [SerializeField]
    private int quantity;
 
    public KindOfItem GetKindOfItem() {
        return kindOfItem;
    }
 
    public Sprite GetIcon() {
        return icon;
    }
 
    public string GetItemName() {
        return itemName;
    }

    public int GetQuantity(){
        return quantity;
    }
}
