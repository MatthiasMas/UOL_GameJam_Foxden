using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public EnemyMovement enemy;
    [SerializeField]
    private float fov = 60f;
    public float viewDistance = 4f;
    [SerializeField]
    private Transform firePoint;

    private Vector3 origin = new Vector3(0.35f, -0.20f, 0);
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
        if (Physics.Raycast(firePoint.position, direction, out hit, viewDistance, layerMask))
        {
            print(direction);
            float angle = Vector3.Angle(transform.position + transform.forward, direction);
            print(angle);
            if (angle < fov / 2)
            {
                print("hit player");
            }
        }
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
