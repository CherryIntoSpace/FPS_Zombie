using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public int CurScene;
    public GameObject WeaponManager;
    // Start is called before the first frame update
    void Start()
    {
        CurScene = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider col)
    {
        if(CurScene <= 3)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Enter");
                LoadingSceneManager.LoadScene(++CurScene);
            }
        }
        else if(CurScene == 4)
        {
            if(col.gameObject.tag == "Player")
            {
                if(WeaponManager.GetComponent<WeaponManager>().KillCount > 0)
                {
                    LoadingSceneManager.LoadScene(5);
                }
                else
                {
                    LoadingSceneManager.LoadScene(8);
                }
            }
        }
        else if(CurScene == 5 || CurScene == 8)
        {
            if(col.gameObject.tag == "Player")
            {
                LoadingSceneManager.LoadScene(7);
            }
        }
    }
}
