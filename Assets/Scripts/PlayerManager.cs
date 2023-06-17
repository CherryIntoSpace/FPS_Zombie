using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    public int clip = 30;
    public Text healthText;
    public Text clipText;

    public GameManager gameManager;
    public GameObject BloodScreen;

    public void Hit(float damage)
    {
        BloodScreen.SetActive(true);

        health -= damage;
        healthText.text =  health.ToString() + " HP";

        if (health <= 0)
        {
            gameManager.EndGame();
        }

        Invoke("DisableBlood", 0.5f);
    }

    public void DisableBlood()
    {
        BloodScreen.SetActive(false);
    }

    public void ShowBullet(int magazine)
    {
        clip = magazine;
        clipText.text = clip.ToString();
    }
}
