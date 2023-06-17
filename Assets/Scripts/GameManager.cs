using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive;

    public int SetEnemy;


    public GameObject[] spawnPoints;

    public GameObject enemyPrefab;

    public GameObject endScreen;

    public GameObject BloodScreen;

    private int CurScene;


    // Start is called before the first frame update
    void Start()
    {
        enemiesAlive = 0;
        CurScene = SceneManager.GetActiveScene().buildIndex;
        spawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurScene < 5 && enemiesAlive <= SetEnemy / 2)
        {
            spawnEnemy();
        }
    }


    public void Restart()
    {
        Time.timeScale = 1;
        LoadingSceneManager.LoadScene(CurScene);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        LoadingSceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        BloodScreen.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        endScreen.SetActive(true);
    }

    public void spawnEnemy()
    {
        while (enemiesAlive < SetEnemy)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemiesAlive++;
            if (CurScene < 5)
            {
                enemySpawned.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            }
            else
            {
                enemySpawned.GetComponent<BossRound>().gameManager = GetComponent<GameManager>();
            }
        }
    }
}
