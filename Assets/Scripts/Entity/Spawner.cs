using NTC.Pool;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [SerializeField]
    private bool loop = true;

    [Range(0, 2)]
    [Tooltip("0 - Left side 1 - Both sides 2 - Right side")]
    [SerializeField]
    private int sideSpawn;

    [SerializeField]
    private EnemyMovement[] enemyPrefab;

    [SerializeField]
    private Vector2 timeSpawn;

    [SerializeField]
    private PositionPreset leftSide;

    [SerializeField]
    private PositionPreset rightSide;

    private Vector2 direction;

    public Vector2 Direction => direction;
    
    private float x;

    private float y;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Spawner");
            return;
        }

        instance = this;
    }

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(Generate());
        }
        while (loop);
    }

    private IEnumerator Generate()
    {
        yield return new WaitForSeconds(Random.Range(timeSpawn.x, timeSpawn.y));

        int numberEnemy = Random.Range(0, enemyPrefab.Length);

        EnemyMovement Enemy = NightPool.Spawn(enemyPrefab[numberEnemy]);

        bool EnemyLookLeft = true;

        if (sideSpawn == 1)
        {
            EnemyLookLeft = Random.Range(0, 2) == 0 ? true : false;
        }

        if (sideSpawn == 0 || (sideSpawn == 1 && EnemyLookLeft == true))
        {
            SetPositions(Enemy, Vector2.right, rightSide.positionX, rightSide.positionY);
        }
        else if (sideSpawn == 2 || (sideSpawn == 1 && EnemyLookLeft == false))
        {
            SetPositions(Enemy, Vector2.left, leftSide.positionX, leftSide.positionY);
        }
    }

    private void SetPositions(EnemyMovement entity, Vector2 directionMove, Vector2 positionX, Vector2 positionY)
    {
        direction = directionMove;

        x = Random.Range(positionX.x, positionX.y);
        y = Random.Range(positionY.x, positionY.y);

        entity.transform.position = new Vector3(x, y, 1f);

        entity.Rotate();
    }
}

[System.Serializable]
public struct PositionPreset
{
    public Vector2 positionX;

    public Vector2 positionY;
}