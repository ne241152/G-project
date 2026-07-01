using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 3f;
    public int damage = 10;

    void Start()
    {
        // 爆発範囲と見た目の大きさを一致させる
        transform.localScale = new Vector3(radius * 2, radius * 2, 1);

        //Explosionを中心に半径radiusの円を作り、その中の敵を取得する
        //Collider2D[]はColliderを入れる箱
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D c in cols)//取得したColliderを1個ずつ見る
        {
            if (c.CompareTag("Enemy"))//攻撃するEnemyかを判別する
            {
                Enemy enemy = c.GetComponent<Enemy>();//TakeDamageを使うためEnemy.csを取り出す
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);//ダメージを与える
                }
            }
        }
        Destroy(gameObject, 0.3f);//爆発後に0.3秒後消える
    }
}
