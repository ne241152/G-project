using UnityEngine;

public class Experience : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        // プレイヤーが触れたら
        if (col.CompareTag("Player")) {
            col.GetComponent<PlayerController>().AddExp(1); // 経験値を1追加
            Destroy(gameObject); // アイテムは消える
        }
    }
}

//てすとあああああああああああああ