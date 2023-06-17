using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRound : MonoBehaviour
{
    public GameObject player;
    public float health = 1000f;
    public bool checkStop;
    public GameManager gameManager;

    private NavMeshAgent bossAI;
    private Rigidbody bossRi;
    private Animator bossAnim;
    private Vector3 pT;
    private GameObject cover;
    private GameObject memo;
    private CapsuleCollider bossCa;
    private bool checkAttack;

    
    
   
    void Start()
    {
        bossAI = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossAnim = GetComponent<Animator>();
        bossRi = GetComponent<Rigidbody>();
        cover = GameObject.Find("Dumpster (3)");
        memo = GameObject.Find("Memo");
        bossCa = GetComponent<CapsuleCollider>();
        checkAttack = false;

    }

    void Update()
    {
        bossRi.velocity = Vector3.zero;
        pT = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(pT);
        bossAI.SetDestination(player.transform.position);
        checkStop = GameObject.Find("DialogueSystem1").GetComponent<DialogueSystem>().IsEnemyStop;

        if (checkStop)
        {
            bossAI.SetDestination(gameObject.transform.position);
        }
        else
        {
            bossAI.SetDestination(player.transform.position);
        }

        if(bossAI.velocity.magnitude > 1)
        {
            bossAnim.SetBool("IsRunning", true);
        }
        else
        {
            bossAnim.SetBool("IsRunning", false);
        }

        if (checkAttack)
        {
            int randomAtk = Random.Range(1, 3);
            bossAnim.SetTrigger("IsAttack" + randomAtk);
            bossAI.speed = 0;
            bossAI.velocity = Vector3.zero;
            bossAI.isStopped = true;
            bossRi.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            bossAnim.ResetTrigger("IsAttack1");
            bossAnim.ResetTrigger("IsAttack2");
            bossAI.speed = 5.5f;
            bossAI.isStopped = false;
            bossRi.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            checkAttack = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == player)
        {
            checkAttack = false;
        }
    }

    public bool Hit(float damage)
    {   
        health -= damage;
        memo.transform.position = new Vector3(0, -10, 0);
        if (health <= 0)
        { 
            bossAI.speed = 0;
            bossAI.velocity = Vector3.zero;
            bossAI.isStopped = true;
            bossRi.constraints = RigidbodyConstraints.FreezeAll;
            cover.SetActive(false);
            gameManager.enemiesAlive--;
            bossCa.enabled = false;
            bossAnim.ResetTrigger("IsAttack1");
            bossAnim.ResetTrigger("IsAttack2");
            bossAnim.SetBool("IsDead", true);
            Destroy(gameObject, 4);
            return true;
        }
        return false;
    }
}
