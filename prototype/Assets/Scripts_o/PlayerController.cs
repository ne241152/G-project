using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public int hp = 100;
    public int currentExp = 0;
    public int expToNextLevel = 10;
    public int currentLevel = 1;

    public GameObject bulletPrefab;
    public TextMeshProUGUI hpText;
    public UIManager uiManager; 

    private bool hasBurst = false;
    private float gatlingTimer = 0f;
    private float burstTimer = 0f;
    private Rigidbody2D rb;

    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        UpdateHPText();
    }

    void Update()
    {
        // 画面が止まっている（ポーズ中）なら、プレイヤーの操作も完全に無効化する
        if (Time.timeScale == 0) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(moveX, moveY).normalized * speed;

        gatlingTimer += Time.deltaTime;
        if (gatlingTimer >= 0.5f) {
            FireGatling();
            gatlingTimer = 0f;
        }

        if (hasBurst) {
            burstTimer += Time.deltaTime;
            if (burstTimer >= 3.0f) {
                FireBurst();
                burstTimer = 0f;
            }
        }
    }

    void FireGatling()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var e in enemies) {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if (dist < minDist) { minDist = dist; nearest = e; }
        }

        if (nearest != null) {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 dir = (nearest.transform.position - transform.position).normalized;
            b.GetComponent<Rigidbody2D>().linearVelocity = dir * 10f;
        }
    }

    void FireBurst()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3.0f);
        foreach (var c in cols) {
            if (c.CompareTag("Enemy")) {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null) {
                    enemy.TakeDamage(2);
                    Vector2 kbDir = (c.transform.position - transform.position).normalized;
                    enemy.transform.position += (Vector3)kbDir * 1.5f;
                }
            }
        }
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        if (currentExp >= expToNextLevel) {
            currentExp -= expToNextLevel;
            expToNextLevel += 5;
            currentLevel++;
            
            if (uiManager != null) {
                uiManager.ShowLevelUp();
            } else {
                Time.timeScale = 0;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) {
            hp = 0;
            if (uiManager != null) {
                uiManager.ShowResult(false);
            } else {
                Time.timeScale = 0;
            }
        }
        UpdateHPText();
    }

    void UpdateHPText()
    {
        if (hpText != null) {
            hpText.text = "HP: " + hp.ToString();
        }
    }
}