using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject player;

    public ParticleSystem MuzzleFlash;
    public AudioSource ShootSound;
    public AudioSource ReloadSound;
    public float range = 100f;
    public float damage = 25f;
    public int magazine = 10;

    public Animator Anim;
    public bool Killed;
    public int KillCount;

    public GameObject BloodEffect;
    public GameObject BulletHole;

    private float curFirerate;
    private float gunFirerate;
    private int curScene;

    
    private void Start()
    {  
        KillCount = 0;
        gunFirerate = 0.3f;
        Killed = false;
        curFirerate = 0;
        curScene = SceneManager.GetActiveScene().buildIndex;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    private void Update()
    {
        if(curScene != 8)
        {
            if (Input.GetButtonDown("Reload") && !Input.GetButton("Fire1") && magazine < 30 && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
            {
                Reload();
            }
            tryFire();
            gunFirerateCalc();
        }
    }

    private void tryFire()
    {
        if (Input.GetButton("Fire1") && !Input.GetButtonDown("Reload") && magazine > 0
           && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Reload") && curFirerate <= 0)
        {
            Shoot();
        }
    }
    private void gunFirerateCalc()
    {
        if(curFirerate > 0)
        {
            curFirerate -= Time.deltaTime;
        }
    }
    private void playAnim()
    {
        MuzzleFlash.Play();
        ShootSound.Play();
    }
    void Shoot()
    {
        curFirerate = gunFirerate;
        playAnim();
        magazine--;
        player.GetComponent<PlayerManager>().ShowBullet(magazine);
        Anim.SetTrigger("TriggerShooting");
        Anim.SetBool("IsWalking", false);
        Anim.SetBool("IsIdle", false);
        RaycastHit hit;

        if(Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {

            
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            BossRound bossManager = hit.transform.GetComponent<BossRound>();
            if(enemyManager != null)
            {
                GameObject go = Instantiate(BloodEffect, hit.point + hit.normal * .1f, Quaternion.LookRotation(-hit.normal));
                Destroy(go, 1);

                Killed = enemyManager.Hit(damage);
                if(Killed == true)
                {
                    KillCount++;
                    
                }
            }
            else if (bossManager != null)
            {
                GameObject go = Instantiate(BloodEffect, hit.point + hit.normal * .1f, Quaternion.LookRotation(-hit.normal));
                Destroy(go, 1);

                Killed = bossManager.Hit(damage);
                if (Killed == true)
                {
                    KillCount++;
                }
            }
            else
            {
                GameObject go = Instantiate(BulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(go, 1);
            }
        }
    }

    void Reload()
    {
        ReloadSound.Play();
        Anim.SetTrigger("TriggerReload");
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            magazine = 30;
            player.GetComponent<PlayerManager>().ShowBullet(magazine);
        }
    }
}
