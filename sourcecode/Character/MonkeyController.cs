using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyController : MonoBehaviour
{

   
    public AudioSource monkeyVoice;
    public AudioClip attack;

    public int attackDamage;
    GameObject target;

    Animator animator;
    NavMeshAgent agent;

    public float walkingSpeed;
    public float runSpeed;


    //
    enum STATE{IDLE, WANDER, CHASE};
    STATE state = STATE.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void TurnOffTrigger()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }

    float DistanceToPlayer()
    {
        if(GameState.GameOver)
        {
            return Mathf.Infinity;
        }
        //二者間の距離
        return Vector3.Distance(target.transform.position, transform.position);
    }

    bool CanSeePlayer()
    {
        if(DistanceToPlayer() < 15)
        {
           return true;
        }

        return false;
    }

    bool ForGetPlayer()
    {
         if(DistanceToPlayer() > 16)
        {
           return true;
        } 

       
    

        return false;
    }

    public void DamagePlayer()
    {
        if(DistanceToPlayer() < 6)
        {
            AttackSE();
            target.GetComponent<PlayerScript>().TakeHit();
        }
    }

    public void AttackSE()
    {
       monkeyVoice.clip = attack;
       monkeyVoice.Play();
    }

   

    
         

   
         
    
    // Update is called once per frame
    void Update()
    {

     

     





        switch(state)
        {
            case STATE.IDLE:
              TurnOffTrigger();

              if(CanSeePlayer())
              {
                state = STATE.CHASE;
              }
              else if(Random.Range(0, 5000) < 50)
              {
                state = STATE.WANDER;
              }

              if(Random.Range(0, 5000) < 50)
              {
                  state = STATE.WANDER;
              }
              break;



            case STATE.WANDER:
               if(!agent.hasPath)
               {
                 float newX = transform.position.x + Random.Range(-10, 10);
                 float newZ = transform.position.z + Random.Range(-10, 10);

                 Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);

                 agent.SetDestination(NextPos);
                 agent.stoppingDistance = 0;

                 TurnOffTrigger();

                 agent.speed = walkingSpeed;
                 animator.SetBool("Walk", true);
               }

               if(Random.Range(0, 5000) < 20)
               {
                state = STATE.IDLE;
                agent.ResetPath();
               }

               if(CanSeePlayer() )
               {
                state = STATE.CHASE;
               }
               break;



            case STATE.CHASE:


           

             if(GameState.GameOver)
                {
                  TurnOffTrigger();
                  agent.ResetPath();
                  state = STATE.WANDER;

                  return;
                }

            DamagePlayer();


               agent.SetDestination(target.transform.position);
               agent.stoppingDistance = 3;

               TurnOffTrigger();

               agent.speed = runSpeed;
               animator.SetBool("Run", true);

               if(ForGetPlayer())
               {
                agent.ResetPath();
                state = STATE.WANDER;
               }

               break;



        }


        
    
    }
}
