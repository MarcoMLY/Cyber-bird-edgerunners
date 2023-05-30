using Data;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private AnimationCurve[] _spawnAccelerationCurves;
    [SerializeField] private GameObject[] _enemies;


    [SerializeField] private int _maxEnemies;

    [SerializeField] private float _timeDivider;
    [SerializeField] private float _waitUntilSpawn;
    private float _timer;

    [SerializeField] private DataGameObject _cameraData;
    private Camera _camera;

    [SerializeField] private DataGameObject _boundryData;
    private MapBoundry _boundry;

    [SerializeField] private DataGameObjectList _enemyData;

    [SerializeField] private Vector2 _spawnBounderySizeTop;
    [SerializeField] private Vector2 _spawnBounderySizeBottom;
    [SerializeField] private Vector2 _spawnBounderySizeRight;
    [SerializeField] private Vector2 _spawnBounderySizeLeft;
    private Vector2[] _spawnBounderySizes;

    [SerializeField] private Vector2 _spawnBounderyPositionTop;
    [SerializeField] private Vector2 _spawnBounderyPositionBottom;
    [SerializeField] private Vector2 _spawnBounderyPositionRight;
    [SerializeField] private Vector2 _spawnBounderyPositionLeft;
    private Vector2[] _spawnBounderyPositions;

    private void Start()
    {
        _camera = _cameraData.Variable.GetComponent<Camera>();
        _boundry = _boundryData.Variable.GetComponent<MapBoundry>();
        _spawnBounderySizes = new Vector2[] { _spawnBounderySizeTop, _spawnBounderySizeBottom, _spawnBounderySizeRight, _spawnBounderySizeLeft };
        _spawnBounderyPositions = new Vector2[] { _spawnBounderyPositionTop, _spawnBounderyPositionBottom, _spawnBounderyPositionRight, _spawnBounderyPositionLeft };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.unscaledDeltaTime;
            return;
        }

        _timer = _waitUntilSpawn;

        if (_enemyData.Variable.Count >= _maxEnemies)
        {
            return;
        }

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = _enemies.Length - 1; i >= 0; i--)
        {
            float chance = _spawnAccelerationCurves[i].Evaluate(Time.timeSinceLevelLoad / _timeDivider) * 100;

            if (Random.Range(0, 100) > chance)
                continue;

            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (_boundry.InsideBoundry(mousePos))
                mousePos *= -1;

            int boundryIndex = PickBoundry(mousePos);

            Vector2 pos = (Vector2)transform.position;
            Vector2 boundryPos = pos + _spawnBounderyPositions[boundryIndex];
            Vector2 boundrySize = _spawnBounderySizes[boundryIndex];

            float minX = boundryPos.x - (boundrySize.x / 2);
            float MaxX = boundryPos.x + (boundrySize.x / 2);
            float minY = boundryPos.y - (boundrySize.y / 2);
            float MaxY = boundryPos.y + (boundrySize.y / 2);

            Vector2 randomPos = RandomPosition(minX, MaxX, minY, MaxY);

            Instantiate(_enemies[i], randomPos, Quaternion.identity);
        }
    }

    private int PickBoundry(Vector2 mousePos)
    {
        int verticalBoundry = mousePos.x > 0 ? 1 : -1;
        int horizontalBoundry = mousePos.y > 0 ? 1 : -1;
        int vertical = 0;
        int horizontal = 0;

        if (verticalBoundry == 1)
            vertical = 2;
        if (verticalBoundry == -1)
            vertical = 3;

        if (horizontalBoundry == 1)
            horizontal = 0;
        if (horizontalBoundry == -1)
            horizontal = 1;

        int[] boundries = new int[] { vertical, horizontal };
        return boundries[Random.Range(0, 2)];
    }

    private Vector2 RandomPosition(float minX, float maxX, float minY, float maxY)
    {
        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        Vector2 randomPos = new Vector2(posX, posY);
        return randomPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2[]  spawnBounderySizes = new Vector2[] { _spawnBounderySizeTop, _spawnBounderySizeBottom, _spawnBounderySizeRight, _spawnBounderySizeLeft };
        Vector2[]  spawnBounderyPositions = new Vector2[] { _spawnBounderyPositionTop, _spawnBounderyPositionBottom, _spawnBounderyPositionRight, _spawnBounderyPositionLeft };

        for (int i = spawnBounderySizes.Length - 1; i >= 0; i--)
        {
            Vector2 pos = (Vector2)transform.position;
            Gizmos.DrawWireCube(pos + spawnBounderyPositions[i], spawnBounderySizes[i]);
        }
    }
}
