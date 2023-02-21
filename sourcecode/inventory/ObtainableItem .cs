// アイテムのクラス
using UnityEngine;
using System.Collections.Generic;


public interface ITouchable
{
    void Touch(GameObject sender);
    void Release(GameObject sender);
}

 
public class ObtainableItem : MonoBehaviour, ITouchable
{
    [SerializeField] Obtainable obt; // 取得可能クラスのオブジェクト
    
 
    // アイテムに触った時
    public void Touch(GameObject sender)
    {
        //例えば斧をクリックしたときに発動
            obt.Obtain(gameObject); // 取得メソッドを呼ぶ 
        
       
    }
 
    public void Release(GameObject sender)
    {
 
    }
 
}
