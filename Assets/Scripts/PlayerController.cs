using UnityEngine;
using UnityEngine.UI;//追加
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public int hp = 100;
    public int currentExp = 0;
    public int expToNextLevel = 10;
    public int currentLevel = 1;
    public int BattlePhase = 1;

    public GameObject bulletPrefab;
    public TextMeshProUGUI hpText;
    public UIManager uiManager; 
    public Slider expSlider;//追加

    //攻撃系
    private bool hasBurst = false;
    private float gatlingTimer = 0f;
    private float burstTimer = 0f;
    public float damageRate = 1.0f;
    public float gatlingInterval = 0.5f;
    private Rigidbody2D rb;

    void Start() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        UpdateHPText();
        UpdateExpBar();//追加
    }

    void Update()
    {
        // 画面が止まっている（ポーズ中）なら、プレイヤーの操作も完全に無効化する
        if (Time.timeScale == 0) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(moveX, moveY).normalized * speed;

        gatlingTimer += Time.deltaTime;
        if (gatlingTimer >= gatlingInterval) {
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
                uiManager.ShowLevelUp(BattlePhase);
            } else {
                Time.timeScale = 0;
            }
            BattlePhase++;
        }
        UpdateExpBar();
    }

    public void IncreaseHP(int amount)
    {
        hp += amount;
        UpdateHPText();
    }

    public void IncreaseDefense(float percent)
    {
        damageRate *= (100f - percent) / 100f;
    }

    public void IncreaseAttackSpeed(float percent)
    {
        gatlingInterval *= (100f - percent) / 100f;
    }

    public void UpdateExpBar()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToNextLevel;
            expSlider.value = currentExp;
        }
    }

    public void TakeDamage(int dmg)
    {
        dmg = Mathf.RoundToInt(dmg * damageRate);
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