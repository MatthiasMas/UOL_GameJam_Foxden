
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public EnemyMovement enemy;
    public EnemyAttack enemyAttack;
    [SerializeField]
    private float fov = 60f;
    public float viewDistance = 4f;
    [SerializeField]
    private Transform firePoint;

    private Vector3 origin = Vector3.zero;
    private Mesh mesh;
    private Transform playerTransform;
    

    private int layerMask = 1 << 8;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int rayCount = 30;
        float angle = 0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        transform.localEulerAngles = new Vector3(0, 0, 90 + (fov / 2));

        Vector3 direction = playerTransform.position - firePoint.position;
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, direction, out hit, viewDistance - Vector2.Distance(firePoint.transform.position,transform.parent.parent.transform.position), layerMask))
        {
            Vector3 forwardDirection = firePoint.transform.position - transform.parent.parent.transform.position;
            float angle = Vector3.Angle(direction, forwardDirection);
            if (angle < fov / 2)
            {
                enemy.isInFOV = true;
                enemyAttack.canAttack = true;
            }
        }
        else
        {
            enemy.isInFOV = false;
            enemyAttack.canAttack = false;
        }
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
