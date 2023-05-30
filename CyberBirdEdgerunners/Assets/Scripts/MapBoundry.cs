using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class MapBoundry : MonoBehaviour
{
    private EdgeCollider2D _collider;
    private Mesh _mesh;

    [SerializeField] private float _size;
    [SerializeField] private int _edges;

    [SerializeField] private DataGameObjectList _enemyData;

    private void Start()
    {
        _collider = GetComponent<EdgeCollider2D>();
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        MakeCircleCollider();
        DrawFilledCircle();
    }

    private void Update()
    {
        DestroyEnemiesOutsideBoundry();
    }

    private void DestroyEnemiesOutsideBoundry()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
        Collider2D[] hit = Physics2D.OverlapCircleAll(pos, _size);

        List<GameObject> enemiesCollided = new List<GameObject>();

        foreach (Collider2D enemy in hit)
        {
            enemiesCollided.Add(enemy.gameObject);
        }

        foreach (GameObject enemy in _enemyData.GetData())
        {
            if (enemiesCollided.Contains(enemy))
                continue;

            Destroy(enemy);
        }
    }

    private void DrawFilledCircle()
    {
        Vector3[] polygonPoints = GetPointsOnCircle();
        int[] polygonTriangles = GetCircleTriangles();
        _mesh.Clear();
        _mesh.vertices = polygonPoints;
        _mesh.triangles = polygonTriangles;
    }

    private int[] GetCircleTriangles()
    {
        int triangleAmount = _edges - 2;
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < triangleAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i + 1);
            newTriangles.Add(i + 2);
        }
        return newTriangles.ToArray();
    }

    private void MakeCircleCollider()
    {
        _collider.points = Vector3ArrayToVector2(GetPointsOnCircle());
    }

    private Vector2[] Vector3ArrayToVector2(Vector3[] array)
    {
        Vector2[] newArray = new Vector2[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }

    private Vector3[] GetPointsOnCircle()
    {
        Vector3[] points = new Vector3[_edges + 1];

        for (int i = 0; i <= _edges; i++)
        {
            float circumferenceProgress = (float)i / _edges;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScale = Mathf.Cos(currentRadian);
            float yScale = Mathf.Sin(currentRadian);

            float x = xScale * _size;
            float y = yScale * _size;

            Vector3 currentPos = new Vector3(x, y, 0);

            points[i] = currentPos;
        }

        return points;
    }

    public bool InsideBoundry(Vector2 position)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0);
        float radius = _size;
        float centerToPosition = Vector2.Distance(pos, position);
        if (centerToPosition < radius)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _size);
    }
}
