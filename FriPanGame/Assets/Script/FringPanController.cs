using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PanController : MonoBehaviour
{
    // 回転速度を調整するためのパラメータ
    public float rotationSpeed = 100f;

    // フライパンの回転の中心となるオブジェクト
    public Transform rotationCenter;

    private Rigidbody rb;

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // 重力の影響を受けないようにする
        rb.constraints = RigidbodyConstraints.FreezeRotation; // 回転制約を解除
    }

    void FixedUpdate()
    {
        // マウス入力を取得
        float mouseX = Input.GetAxis("Mouse X"); // 左右の動き
        float mouseY = Input.GetAxis("Mouse Y"); // 前後の動き

        // X軸回転 (ワールド空間基準)
        float xRotation = mouseY * rotationSpeed * Time.fixedDeltaTime;

        // Z軸回転 (オブジェクト空間基準)
        float zRotation = -mouseX * rotationSpeed * Time.fixedDeltaTime;

        // 回転の中心を基準に計算
        Vector3 direction = transform.position - rotationCenter.position; // 中心からオブジェクトへのベクトル

        // ワールド空間でのX軸回転
        Quaternion xQuaternion = Quaternion.AngleAxis(xRotation, Vector3.right);

        // オブジェクト空間でのZ軸回転
        Quaternion zQuaternion = Quaternion.AngleAxis(zRotation, transform.forward);

        // 合成された回転を適用
        Quaternion totalRotation = xQuaternion * zQuaternion;

        // 新しい方向ベクトルを計算
        Vector3 newDirection = totalRotation * direction;

        // 新しい位置を計算
        Vector3 newPosition = rotationCenter.position + newDirection;

        // Rigidbodyで移動と回転を適用
        rb.MovePosition(newPosition);
        rb.MoveRotation(totalRotation * rb.rotation);
    }
}
