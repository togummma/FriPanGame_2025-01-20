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

        // X軸回転 (マウスの前後) - ワールド基準
        float xRotation = mouseY * rotationSpeed * Time.deltaTime;

        // Z軸回転 (マウスの左右) - オブジェクト基準
        float zRotation = -mouseX * rotationSpeed * Time.deltaTime;

        // 回転の中心位置
        Vector3 rotationPoint = rotationCenter.position;

        // X軸回転 (ワールド空間)
        transform.RotateAround(rotationPoint, Vector3.right, xRotation);

        // Z軸回転 (オブジェクト空間)
        transform.RotateAround(rotationPoint, transform.forward, zRotation);
    }
}
