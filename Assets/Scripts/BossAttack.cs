using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public PlayerManager playerM;
    public GameObject player;
    public float damage = 20f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerM = GameObject.Find("First_Person_Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            playerM.Hit(damage);
        }
    }
}
