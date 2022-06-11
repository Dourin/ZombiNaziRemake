using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public int enemiesAlive = 0;

    public int round = 0;
    

    public GameObject[] spawnPoints;

    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;

    public GameObject pauseMenu;

    public TextMeshProUGUI roundNum;
    public TextMeshProUGUI roundsSurvived;
    public GameObject endScreen;

    public Animator blackScreenAnimator;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //healthNum.text = "Health " + player.health.ToString();
        if (enemiesAlive == 0) {
            round++;
            NextWave(round);
            roundNum.text = "Round: " + round.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();

        }
    }

    public void NextWave(int round) {
        for (int i = 0; i < round*10; i++) {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<MonsterController>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
        if(round%5==0){
            for (int ii = 0; ii < round*5; ii++) {
                GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemySpawned = Instantiate(enemyPrefab2, spawnPoint.transform.position, Quaternion.identity);
                enemySpawned.GetComponent<MonsterController>().gameManager = GetComponent<GameManager>();
                enemiesAlive++;
            }
        }
    }

    

    public void MainMenu() {
        Time.timeScale = 1; 
        AudioListener.volume = 1;
        blackScreenAnimator.SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", .4f);
    }

    void LoadMainMenuScene() {
        SceneManager.LoadScene("menu");
    }

    public void Pause() {
        Cursor.visible = true;
         LoadMainMenuScene();
    }

    
}
