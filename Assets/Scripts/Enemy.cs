using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Zombie, Bat }
    public EnemyType type = EnemyType.Zombie;
    
    public int hp = 3;
    public float speed = 2.0f;
    public int attackDmg = 10;
    public GameObject expPrefab;

    private Transform player;
    private float damageTimer = 0f;
    private Vector2 batDir;

    void Start()
    {
        // プレイヤーの位置情報を取得
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        
        if (type == EnemyType.Bat) {
            hp = 1; speed = 4.0f; attackDmg = 5;
            // 出現時にプレイヤーの方向を記録し、その方向へまっすぐ飛ぶ設定
            if (player != null) {
                batDir = (player.position - transform.position).normalized;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        if (type == EnemyType.Zombie) {
            // プレイヤーを常に追尾
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else if (type == EnemyType.Bat) {
            // 記録した方向へ直進
            transform.position += (Vector3)batDir * speed * Time.deltaTime;
        }

        // 画面外に離れすぎたらメモリ節約のために消す
        if (Vector2.Distance(transform.position, player.position) > 25f) Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // プレイヤーと接触し続けている間、0.5秒ごとにダメージを与える
        if (col.CompareTag("Player")) {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 0.5f) {
                col.GetComponent<PlayerController>().TakeDamage(attackDmg);
                damageTimer = 0f;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) {
            // 倒されたらその場に経験値をドロップして消滅
            Instantiate(expPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}