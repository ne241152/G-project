using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject batPrefab;
    public TextMeshProUGUI timerText;
    public UIManager uiManager;

    private Transform player;
    private float spawnTimer = 0f;
    private float gameTime = 0f;

    void Start() {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (timerText != null) {
            timerText.text = "Time: " + Mathf.FloorToInt(gameTime).ToString() + "s";
        }

        if (spawnTimer >= 1.0f) {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if (gameTime >= 180f) {
            if (timerText != null) timerText.text = "Game Clear!"; 
            
            if (uiManager != null) {
                uiManager.ShowResult(true); 
            } else {
                Time.timeScale = 0;
            }
            enabled = false;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * 10f;
        GameObject prefabToSpawn = zombiePrefab;

        if (gameTime >= 60f && Random.Range(0, 2) == 0) {
            prefabToSpawn = batPrefab;
        }

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}