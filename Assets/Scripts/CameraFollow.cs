using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // 追いかける対象
    public float smoothSpeed = 0.125f; // カメラが追従する滑らかさ

    [Header("マップの移動限界（広さ）")]
    public Vector2 minBounds; // マップの左下端の座標 (X, Y)
    public Vector2 maxBounds; // マップの右上端の座標 (X, Y)

    void LateUpdate()
    {
        if (target == null) return;

        // プレイヤーと同じ位置を目指す（カメラのZ軸は-10などに固定）
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        
        // 滑らかに移動させる
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // マップの広さ（限界値）の中にカメラの座標を閉じ込める（Mathf.Clamp）
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        // 最終的なカメラの位置を適用
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}