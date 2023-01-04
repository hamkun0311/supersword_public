using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayerMove : MonoBehaviour 
{
    public float moveSpeed = 1.5f;

    public float arrowCooltime = 0.5f;
    public float ArrowFireTime = 0;

    public float hitcooltime = 1f;
    public float hitfiretime = 0;

    public GameManager GM;
    public GameObject FireArrow;
    public GameObject IceArrow;
    public GameObject StoneArrow;
    public GameObject LightningArrow;
    public GameObject WaveSwordObj;
    public GameObject ChaosSwordObj;

    public SpriteRenderer spr;

    public string User_ID;
  
    Animator anim;
    public AnimatorOverrideController Player_robe;
    public ParticleSystem BossDamage1;
    public ParticleSystem RestoreHP;
    public ParticleSystem PowerUP;
    public ParticleSystem MPowerUp;

    public ParticleSystem PowerUp_Leaf;
    public ParticleSystem MPowerUp_Leaf;
    public ParticleSystem HPUp_Leaf;
    public ParticleSystem SuperSword1;
    public ParticleSystem SuperSword2;

    bool attack_state = false;

    public VirtualJoystick virtualJoystick;
    public VirtualAttackBtn virtualAttackBtn;

    public Transform Demon;

    public GameObject hptxt;
    public GameObject atktxt;
    public GameObject matkhptxt;

    void Awake()
    {
        User_ID = LoginMenu.User_ID;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(7,8);
        Player_robe = new AnimatorOverrideController();
        BossDamage1.Stop();
        RestoreHP.Stop();
        PowerUP.Stop();
        MPowerUp.Stop();
        PowerUp_Leaf.Stop();
        MPowerUp_Leaf.Stop();
        HPUp_Leaf.Stop();
        SuperSword1.Stop();
        SuperSword2.Stop();
    } 

    // Update is called once per frame
    void Update()
    {
            ///*
            //Mobile
            float inputX = virtualJoystick.Horizontal();
            float inputY = virtualJoystick.Vertical();
            
            anim.SetFloat("inputx", inputX);
            anim.SetFloat("inputy", inputY);

            if(inputX != 0 || inputY != 0)
            {
                anim.SetFloat("lastMoveX", inputX);
                anim.SetFloat("lastMoveY", inputY);
                transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * moveSpeed);
            } 
            //*/

            //PC test
            /*
            float keyX = Input.GetAxisRaw("Horizontal");
            float keyY = Input.GetAxisRaw("Vertical");

            anim.SetFloat("inputx", keyX);
            anim.SetFloat("inputy", keyY);

            if(keyX != 0 || keyY != 0 )
            {
                anim.SetFloat("lastMoveX", keyX);
                anim.SetFloat("lastMoveY", keyY);
                transform.Translate(new Vector2(keyX, keyY) * Time.deltaTime * moveSpeed);
            } 
            //*/

            if((virtualAttackBtn.push_chk == true || Input.GetButtonDown("attack")) && attack_state == false)
            {

                StartCoroutine(AttackCo());
                
                if( Time.time > ArrowFireTime)
                {
                    if(GM.f_bow_cnt > 0)
                    {
                        for(int i = 0; i < GM.quiver_cnt + 1 ; i++)
                        {
                            Vector3 vector = transform.position;
                            float var = 0;
                            var = var + (0.1f * i);
                            vector.y = vector.y + var;
                            Instantiate(FireArrow, vector, Quaternion.identity);    
                        }
                        SoundManager.instance.PlaySE("arrow");
                    }

                    if(GM.i_bow_cnt > 0)
                    {
                        for(int i = 0; i < GM.quiver_cnt + 1 ; i++)
                        {
                            Vector3 vector = transform.position;
                            float var = 0;
                            var = var + (0.1f * i);
                            vector.y = vector.y + var;
                            Instantiate(IceArrow, vector, Quaternion.identity);    
                        }
                        SoundManager.instance.PlaySE("arrow");
                    }

                    if(GM.s_bow_cnt > 0)
                    {
                        for(int i = 0; i < GM.quiver_cnt + 1 ; i++)
                        {
                            Vector3 vector = transform.position;
                            float var = 0;
                            var = var + (0.1f * i);
                            vector.y = vector.y + var;
                            Instantiate(StoneArrow, vector, Quaternion.identity);    
                        }
                        SoundManager.instance.PlaySE("arrow");
                    }

                    if(GM.l_bow_cnt > 0)
                    {
                        for(int i = 0; i < GM.quiver_cnt + 1 ; i++)
                        {
                            Vector3 vector = transform.position;
                            float var = 0;
                            var = var + (0.1f * i);
                            vector.y = vector.y + var;
                            Instantiate(LightningArrow, vector, Quaternion.identity);    
                        }
                        SoundManager.instance.PlaySE("arrow");
                    }
                    ArrowFireTime = Time.time + arrowCooltime;

                    

                }
            }

    }

    private IEnumerator AttackCo()
    {

        if(GM.wave_sword_cnt > 0)
        {
            int rand_no = UnityEngine.Random.Range(1,101);

            if(rand_no <= GM.wave_sword_cnt * 10)
            {
                Instantiate(WaveSwordObj, transform.position, Quaternion.identity);    
            }
        }

        if(GM.c_sword_cnt > 0)
        {
            int rand_no = UnityEngine.Random.Range(1,101);

            if(rand_no <= GM.c_sword_cnt * 3)
            {
                Instantiate(ChaosSwordObj, transform.position, Quaternion.identity);    
            }
        }

        anim.SetBool("attack", true);
        attack_state = true;
        SoundManager.instance.PlaySE("sword");
        yield return null;
        anim.SetBool("attack", false);
        attack_state = false;
        yield return new WaitForSeconds(.33f);

    }

    public void Smash(string monster){

        if(Time.time > hitfiretime)
        {
            StartCoroutine(DamagedCo());

            if(monster == "Goblin")
            {
                GM.P_health_now = GM.P_health_now - 100;
            } else if(monster == "Slime")
            {
                GM.P_health_now = GM.P_health_now - 10;
            }
            else if(monster == "Beholder")
            {
                GM.P_health_now = GM.P_health_now - 150;
            }else if(monster == "Demon")
            {
                BossDamage1.Play();
                GM.P_health_now = GM.P_health_now - (GM.P_health_total * 30 / 100);
                float x = Demon.position.x - transform.position.x;
                float y =  Demon.position.y - transform.position.y;
                KnockbackCo(x,y);
            }else if(monster == "GoblinKing")
            {
                GM.P_health_now = GM.P_health_now - 300;
            }
            else if(monster == "Ooze")
            {
                GM.P_health_now = GM.P_health_now - 500;
            }
             else if(monster == "Wolf")
            {
                GM.P_health_now = GM.P_health_now - 50;
            }

            hitfiretime = Time.time + hitcooltime;
        }

    }
    private IEnumerator DamagedCo()
    {

        spr.color = UnityEngine.Color.red;
        yield return new WaitForSeconds(1f);
        spr.color = UnityEngine.Color.white;
        yield return null;

    }

    void KnockbackCo(float x, float y)
    {
        transform.Translate(new Vector2(x, y) * Time.deltaTime * 10f);
    }

    public void showHPtxt()
    {
        GameObject hpText = Instantiate(hptxt);
        hpText.transform.position = transform.position;
    }
    public void showPOWERtxt()
    {
        GameObject atkText = Instantiate(atktxt);
        atkText.transform.position = transform.position;
    }
    public void showMPOWERtxt()
    {
        GameObject MatkText = Instantiate(matkhptxt);
        MatkText.transform.position = transform.position;
    }


}

