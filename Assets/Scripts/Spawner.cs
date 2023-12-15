using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Объект спавна")]
    [SerializeField] GameObject[] enemyPrefab;
    [Header("Настройки спавна")]
    [SerializeField, Tooltip("Скорость объекта")] float speedMove;
    [Space(10)]
    [SerializeField, Tooltip("Первое число в Random.Range")] float fromSpeedSpawner;
    [SerializeField, Tooltip("Второе число в Random.Range")] float beforeSpeedSpawner;
    [Space(10)]
    [SerializeField, Tooltip("Позиция появления по X")] float positionX;
    [SerializeField, Tooltip("Появление от Y")] float fromPositionY;
    [SerializeField, Tooltip("Появление до Y")] float beforePositionY;
    [Space(10)]
    [SerializeField, Range(1, 2), Tooltip("1 - Враги с двух сторон, 2 - Враги слева")] int leftOrRight = 1;

    private bool loop = true;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(GeneratorEnemy());
        }
        while (loop);
    }
    private IEnumerator GeneratorEnemy()
    {
        yield return new WaitForSeconds(Random.Range(fromSpeedSpawner, beforeSpeedSpawner));

        int numberEnemy = Random.Range(0, enemyPrefab.Length);

        GameObject Enemy = Instantiate(enemyPrefab[numberEnemy]);

        bool EnemyLookLeft = Random.Range(0, 2) == leftOrRight;

        float x;
        float y = Random.Range(-fromPositionY, -beforePositionY);
        

        if (EnemyLookLeft)
        {
            x = positionX;
            Enemy.GetComponent<Enemy>().movement.x = -speedMove;
            Enemy.GetComponent<Transform>().position = new Vector3(x, y, -.5f);
        }
        else
        {
            x = -positionX;
            Enemy.GetComponent<Enemy>().movement.x = speedMove;
            Enemy.GetComponent<Transform>().Rotate(0f, 180f, 0f);
            Enemy.GetComponent<Transform>().position = new Vector3(x, y, .5f);
        }
    }
}