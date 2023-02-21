using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ItemManager : MonoBehaviour
{

    [SerializeField]
    private ItemDataBase itemDataBase;


    static ItemManager instance;
 
    public static ItemManager GetInstance()
    {
        return instance;
    }
 
    // ...
 
    // Use this for initialization
    void Start()
    {
        instance = this;
 
        // ...
    }
 
    // ...
 
    public bool HasItem(string searchName)
    {
        //itemDataBase内の要素はItem型でできており、その中にGetItemNameメソッドが含まれているから使用できるということか。
        return itemDataBase.GetItemLists().Exists(item => item.GetItemName() == searchName);
    }

    public Item GetItem(string searchName) {
        return itemDataBase.GetItemLists().Find(itemName => itemName.GetItemName() == searchName);
    }
}
