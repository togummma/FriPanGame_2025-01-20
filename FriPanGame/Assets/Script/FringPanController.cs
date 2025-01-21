using UnityEngine;

public class PanController : MonoBehaviour
{
    // 回転速度を調整するためのパラメータ
    public float rotationSpeed = 100f;

    // フライパンの回転の中心となるオブジェクト
    public Transform rotationCenter;

    void Update()
    {
        // マウス入力を取得
        float mouseX = Input.GetAxis("Mouse X"); // 左右の動き
        float mouseY = Input.GetAxis("Mouse Y"); // 前後の動き

        // X軸回転 (マウスの前後)
        float xRotation = mouseY * rotationSpeed * Time.deltaTime;

        // Z軸回転 (マウスの左右)
        float zRotation = -mouseX * rotationSpeed * Time.deltaTime;

        // ワールド空間のX軸とZ軸で回転を適用
        Vector3 rotationPoint = rotationCenter.position; // 中心の座標のみ使用
        transform.RotateAround(rotationPoint, Vector3.right, xRotation); // X軸回転
        transform.RotateAround(rotationPoint, Vector3.forward, zRotation); // Z軸回転
    }
}
