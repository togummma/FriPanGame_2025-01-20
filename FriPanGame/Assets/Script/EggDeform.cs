using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class SoftBodyPhysics : MonoBehaviour
{
    public float stiffness = 10f;  // 弾性係数
    public float damping = 0.1f;  // 減衰係数
    public float mass = 1f;       // 頂点の質量

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    private Vector3[] velocities;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        // メッシュの頂点情報を取得
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        velocities = new Vector3[originalVertices.Length];

        originalVertices.CopyTo(displacedVertices, 0);
    }

    void Update()
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            Vector3 displacement = displacedVertices[i] - originalVertices[i];
            Vector3 restoringForce = -displacement * stiffness; // バネの力
            Vector3 dampingForce = -velocities[i] * damping;   // 減衰の力
            Vector3 acceleration = (restoringForce + dampingForce) / mass;

            velocities[i] += acceleration * Time.deltaTime; // 速度の更新
            displacedVertices[i] += velocities[i] * Time.deltaTime; // 位置の更新
        }

        // メッシュを更新
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();

        // MeshColliderを更新
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
