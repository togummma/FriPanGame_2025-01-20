using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    // フライパン本体オブジェクト
    public Transform fryingPanBase;

    // 目玉焼きのプレハブ
    public GameObject eggPrefab;

    // 目玉焼きの現在のインスタンス
    private GameObject currentEgg;

    // 目玉焼きを生成する高さ
    public float eggSpawnHeight = 0.5f;

    // 目玉焼きが削除される高さ
    public float eggDespawnHeight = -1.0f;

    // 再生成までの待機時間
    public float respawnDelay = 2.0f;

    // 再生成中の状態フラグ
    private bool isRespawning = false;

    void Update()
    {
        // 目玉焼きが存在する場合、高さを監視
        if (currentEgg != null)
        {
            if (currentEgg.transform.position.y < eggDespawnHeight)
            {
                Destroy(currentEgg);
                Debug.Log("目玉焼きを削除しました！");
                StartRespawnTimer();
            }
        }
        // 再生成中でない場合に生成
        else if (!isRespawning)
        {
            SpawnEgg();
        }
    }

    // 目玉焼きを生成するメソッド
    private void SpawnEgg()
    {
        if (fryingPanBase == null || eggPrefab == null)
        {
            Debug.LogWarning("フライパン本体または目玉焼きプレハブが設定されていません");
            return;
        }

        // フライパン本体の位置を基準に目玉焼きを生成 (ワールド基準)
        Vector3 spawnPosition = fryingPanBase.position + Vector3.up * eggSpawnHeight; // 指定の高さに生成
        Quaternion spawnRotation = Quaternion.identity; // ワールド基準の回転

        // 目玉焼きを生成し、参照を保持
        currentEgg = Instantiate(eggPrefab, spawnPosition, spawnRotation);
        Debug.Log("目玉焼きを生成しました！");
    }

    // 再生成タイマーを開始する
    private void StartRespawnTimer()
    {
        isRespawning = true;
        Invoke(nameof(ResetRespawnState), respawnDelay);
    }

    // 再生成可能な状態に戻す
    private void ResetRespawnState()
    {
        isRespawning = false;
    }
}
