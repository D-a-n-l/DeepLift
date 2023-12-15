using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("������ ������")]
    [SerializeField] GameObject[] enemyPrefab;
    [Header("��������� ������")]
    [SerializeField, Tooltip("�������� �������")] float speedMove;
    [Space(10)]
    [SerializeField, Tooltip("������ ����� � Random.Range")] float fromSpeedSpawner;
    [SerializeField, Tooltip("������ ����� � Random.Range")] float beforeSpeedSpawner;
    [Space(10)]
    [SerializeField, Tooltip("������� ��������� �� X")] float positionX;
    [SerializeField, Tooltip("��������� �� Y")] float fromPositionY;
    [SerializeField, Tooltip("��������� �� Y")] float beforePositionY;
    [Space(10)]
    [SerializeField, Range(1, 2), Tooltip("1 - ����� � ���� ������, 2 - ����� �����")] int leftOrRight = 1;

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