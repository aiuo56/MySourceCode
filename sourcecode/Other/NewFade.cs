using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFade : MonoBehaviour
{
[SerializeField]
    private Camera downCamera;
[SerializeField]
    private GameObject player;
[SerializeField]
    private Image FadeImage;
[SerializeField]
    Text tellMessage;

    public GameState finish;

    public GameObject object1; 




   







    float fadeSpeed = 0.1f;　　　　　 //フェイドするスピード
    float red, green, blue, alfa;     //画像の透明度情報
    Image fadeImage;
    
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = FadeImage.GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        






        if(downCamera)
        {

            tellMessage.enabled = false;
            RaycastHit hit2;

            if (Physics.Raycast(downCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit2, 4f)) 
            {
                

                 if(hit2.collider.CompareTag("NoGo"))
                {
                    player.transform.position += new Vector3(10, 0, 0);


                    
                }
                else if(hit2.collider.CompareTag("Tell"))
                   {
                     tellMessage.enabled = true;
                   }

                else if(hit2.collider.CompareTag("Finish") )
                   {
                    GameState.GameOver = true;
                      fadeImage.enabled = true;  // パネルの表示をオンにする
                      alfa += fadeSpeed;         // 不透明度を徐々にあげる
                      SetAlpha ();               // 変更した透明度をパネルに反映する
                      if(alfa >= 1)
                      {
                        alfa += 0;
                        finish.RealFinish();
                       
                      }
                   }
                
                
             }
         }
 
    }



    
    
 
    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
