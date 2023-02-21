using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{



    //残り体力数
    public static int playerHP = 100, maxplayerHP = 100;

    public Text hpText;
    //
    float x, z;
    //移動速度調整変数
    float speed = 0.1f;

    //ここにmain cameraをアタッチ
    public GameObject cam;
    //quaternionは角度を調整する
    Quaternion cameraRot, characterRot;
    //マウス速度調整用変数
    float Xsensityvity = 3f, Ysensityvity = 3f;
    //カーソル表示に関する変数
    bool cursorLock = true;
    //角度制限に関する変数
    float minX = -90f, MaxX = 90f;
    //スタミナ変数
    float PlayerStamina = 100;
    //スタミナ減少量
    float decStamina = 0.45f, recStamina = 0.3f;
   
    [SerializeField]
    private GameObject StaminaGage;
    [SerializeField]
    private Slider StaminaBer;
    public GameObject mainCamera, subCamera;

    public GameState gameState;

   


     // Start is called before the first frame update
    void Start()
    {

      
        cameraRot = cam.transform.localRotation;  //カメラのローカル視点での角度  
        characterRot = transform.localRotation;    //プレイヤーのローカル視点での角度
        playerHP = 100;
        hpText.text = playerHP + "/" + maxplayerHP;

    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Xsensityvity;       
        float yRot = Input.GetAxis("Mouse Y") * Ysensityvity;     

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Rotation関数によって調整された角度を再代入
        cameraRot = Rotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;


        //カーソル表示の有無に関する関数
        UpdateCursorLock();     


        //走るための関数
        RelatedRun();

        //後ろ向きのカメラオン
        SubCamera();
       
      
        


        


       
    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;     //→、←、A,Dを押した際に-1～1を取得
        z = Input.GetAxisRaw("Vertical") * speed;   　  //↓、↑,w,sを押した際に、-1から1を取得
       

        //transform.position += new Vector3(x, 0, z);
        //これがないとカメラの方向とcharaの方向が一致しない、ゼルダ視点になる。
        transform.position += cam.transform.forward * z + cam.transform.right * x;  
    }
    
    //後ろ向きのカメラ設定
    public void SubCamera()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            subCamera.SetActive(true);
            //Setactive(false)にしてしまうと、オブジェクト事態も非表示になる。maincameraの配下にplayerがあるため
            mainCamera.GetComponent<Camera>().enabled = false;
        }
        else if(subCamera.activeSelf)
        {
            subCamera.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = true;
        }
    }

    public void TakeHit()
    {
        playerHP--;
       //playerHP = (int)Mathf.Clamp(playerHP - damage, 0, playerHP);
       hpText.text = playerHP + "/" + maxplayerHP;

       if(playerHP <= 0 && !GameState.GameOver)
       {
        GameState.GameOver = true;
        gameState.Retry();
       }

    }


    //走るについての関数
    public void RelatedRun()
    {
         if(z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            StaminaBer.value = PlayerStamina;
            PlayerStamina -= decStamina;
            StaminaGage.SetActive(true);
            speed = 0.2f;
         if(PlayerStamina <= 0)
         {
            speed = 0.1f;
         }
        }
        else
        {
            if(PlayerStamina <= 99)
            {
                StaminaBer.value = PlayerStamina;
                StaminaGage.SetActive(true);
                PlayerStamina += recStamina;
               
            }
            else 
            {

                PlayerStamina += 0;
                StaminaGage.SetActive(false);
           
            
            }
            
            speed = 0.1f;
            
           
        }

    }
    //カーソル表示の有無に関する関数
   public void UpdateCursorLock()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || GameState.GameOver)           //esc押したら
        {
            cursorLock = false;
        }
        else if(Input.GetMouseButton(1))
        {
            cursorLock = true;
        }
        
        if(cursorLock)　　　　　　　　                  //カーソルロックになってるなら
        {
            Cursor.lockState = CursorLockMode.Locked; 　//持続しよう
        }
        else if(!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;   
        }
       
    } 
    

    //角度制限についての関数
    public Quaternion Rotation(Quaternion q)
    {
        //q = x,y,z,w(x,y,zはベクトル量と方向)wは(座標とは無関係の量)
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        //tanAで角度を計り、Rad2Degで度に変換
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        //Mathf(x, y, z) xの値をy～zの範囲内に落とし込む
        angleX = Mathf.Clamp(angleX, minX, MaxX);
        //度数をラジアンに変換
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f); 
        return q;
    }


    //ButtonManagerに記述するための関数
    public void forButtonManager(){
        playerHP = 100;
        hpText.text = playerHP + "/" + maxplayerHP;
        

    }
    
   


}