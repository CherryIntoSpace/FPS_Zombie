using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public Animator enemyAnimator;
    public float damage = 20f;
    public float health = 100f;
    public bool IsStop;
    public GameManager gameManager;

    private CapsuleCollider bodyColl;
    private NavMeshAgent enemy;
    private Vector3 pT;

    public bool Hit(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameManager.enemiesAlive--;
            enemyAnimator.SetBool("Dead", true);
            bodyColl.enabled = false;
            enemy.speed = 0;
            enemy.velocity = Vector3.zero;
            enemy.isStopped = true;
            Destroy(gameObject,2);
            return true;
        }
        return false;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        bodyColl = GetComponent<CapsuleCollider>();
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        pT = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(pT);
        enemy.SetDestination(player.transform.position);
        IsStop = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>().IsEnemyStop;

        if(IsStop == true)
        {
            enemy.SetDestination(gameObject.transform.position);
        }
        else
        {
            enemy.destination = player.transform.position;
        }

        if (enemy.velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            enemyAnimator.SetTrigger("Attack");
            player.GetComponent<PlayerManager>().Hit(damage);
        }
    }
}
