using UnityEngine;

public class FryingPanTiltController : MonoBehaviour
{
    // 傾ける速度
    public float tiltSpeed = 50f;

    // 傾ける角度の制限（最大角度）
    public float maxTiltAngle = 45f;

    // マウス感度
    public float mouseSensitivity = 5.0f;

    // 回転の中心（フライパン本体オブジェクト）
    public Transform rotationCenter;

    void Update()
    {
        // 回転中心が設定されていない場合はスクリプトを停止
        if (rotationCenter == null)
        {
            Debug.LogWarning("回転中心（rotationCenter）が設定されていません");
            return;
        }

        // 入力取得
        float tiltX = Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") * mouseSensitivity;   // 前後方向の傾き
        float tiltZ = -Input.GetAxis("Horizontal") - Input.GetAxis("Mouse X") * mouseSensitivity; // 左右方向の傾き

        // 現在の回転角度を取得
        Vector3 currentRotation = transform.eulerAngles;

        // 回転角度を計算（ローカル回転で制限をかける）
        float newTiltX = Mathf.Clamp(
            NormalizeAngle(currentRotation.x + tiltX * tiltSpeed * Time.deltaTime), 
            -maxTiltAngle, maxTiltAngle
        );
        float newTiltZ = Mathf.Clamp(
            NormalizeAngle(currentRotation.z + tiltZ * tiltSpeed * Time.deltaTime), 
            -maxTiltAngle, maxTiltAngle
        );

        // YZ平面に制限（ローカル回転をリセット）
        float clampedTiltY = NormalizeAngle(currentRotation.y); // Z軸がYZ平面から外れないようにする

        // 回転の中心を基準に回転を適用
        transform.localEulerAngles = new Vector3(newTiltX, clampedTiltY, newTiltZ);
    }

    // 角度を -180 ～ 180 に正規化する
    private float NormalizeAngle(float angle)
    {
        return angle > 180 ? angle - 360 : angle;
    }
}
