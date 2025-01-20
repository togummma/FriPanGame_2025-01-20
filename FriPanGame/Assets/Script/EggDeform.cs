using UnityEngine;

public class EggDeform : MonoBehaviour
{
    public float deformSpeed = 2.0f;    // 変形速度
    public float deformIntensity = 0.1f; // 変形の強さ

    private Mesh originalMesh;         // 元のメッシュ
    private Vector3[] originalVertices; // 元の頂点
    private Vector3[] deformedVertices; // 変形後の頂点

    void Start()
    {
        // メッシュの取得
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilterが見つかりません: " + gameObject.name);
            return;
        }

        originalMesh = meshFilter.mesh;
        originalVertices = originalMesh.vertices;
        deformedVertices = new Vector3[originalVertices.Length];
        System.Array.Copy(originalVertices, deformedVertices, originalVertices.Length);
    }

    void Update()
    {
        if (originalMesh == null) return;

        // 頂点をぷるぷる動かす
        for (int i = 0; i < originalVertices.Length; i++)
        {
            float offset = Mathf.Sin(Time.time * deformSpeed + originalVertices[i].x * 10f) * deformIntensity;
            deformedVertices[i] = originalVertices[i] + Vector3.up * offset;
        }

        // メッシュの更新
        originalMesh.vertices = deformedVertices;
        originalMesh.RecalculateNormals();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突時に変形強度を一時的に増加
        deformIntensity = 0.3f;
        Invoke(nameof(ResetDeformIntensity), 0.5f); // 0.5秒後に元に戻す
    }

    private void ResetDeformIntensity()
    {
        deformIntensity = 0.1f;
    }
}
