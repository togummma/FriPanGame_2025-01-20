using UnityEngine;

public class PanCollisionController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // タグが "food" の場合、SEを再生
        if (collision.collider.CompareTag("food"))
        {
            Debug.Log("フライパンに食材が入りました！");
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("フライパンから食材が出ました！");
        // タグが "food" の場合、SEを停止
        if (collision.collider.CompareTag("food"))
        {
            audioSource.Stop();
        }
    }
}
