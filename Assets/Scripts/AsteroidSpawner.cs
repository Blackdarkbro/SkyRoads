using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;

    public int leftBorderPos = -6;
    public int rightBorderPos = 6;

    public static int dificultyCoef = 0;

    void SpawnAsteroid()
    {
        float middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;
        Vector3 asteroidPos = new Vector3(Random.Range(leftBorderPos, rightBorderPos), 1.5f, middleOfBlockZ);

        GameObject asteroid = Instantiate(asteroidPrefab, asteroidPos, Quaternion.identity);

        asteroid.transform.SetParent(gameObject.transform, true);
    }

    void SpawnTwoAsteroidsSpaceBetween()
    {
        float middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;

        Vector3 asteroid1Pos = new Vector3(leftBorderPos + 1, 1.5f, middleOfBlockZ);
        Vector3 asteroid2Pos = new Vector3(rightBorderPos - 1, 1.5f, middleOfBlockZ);

        GameObject asteroid1 = Instantiate(asteroidPrefab, asteroid1Pos, Quaternion.identity);
        GameObject asteroid2 = Instantiate(asteroidPrefab, asteroid2Pos, Quaternion.identity);

        asteroid1.transform.SetParent(gameObject.transform, true);
        asteroid2.transform.SetParent(gameObject.transform, true);
    }

    void SpawnTwoAsteroidsBeside()
    {
        float middleOfBlockZ = transform.position.z - GetComponent<BoxCollider>().bounds.size.z / 2;

        int side = Random.Range(1, 3);

        Vector3 asteroid1Pos;
        Vector3 asteroid2Pos;

        if (side == 1)
        {
            asteroid1Pos = new Vector3(leftBorderPos + 1, 1.5f, middleOfBlockZ);
            asteroid2Pos = new Vector3(leftBorderPos + 6, 1.5f, middleOfBlockZ);
        } else
        {
            asteroid1Pos = new Vector3(rightBorderPos - 1, 1.5f, middleOfBlockZ);
            asteroid2Pos = new Vector3(rightBorderPos - 6, 1.5f, middleOfBlockZ);
        }

        GameObject asteroid1 = Instantiate(asteroidPrefab, asteroid1Pos, Quaternion.identity);
        GameObject asteroid2 = Instantiate(asteroidPrefab, asteroid2Pos, Quaternion.identity);

        asteroid1.transform.SetParent(gameObject.transform, true);
        asteroid2.transform.SetParent(gameObject.transform, true);
    }

    private void OnEnable()
    {
        int asteroidAppearChacne = Random.Range(1, 8 - dificultyCoef);
        int asteroidCountChance = Random.Range(1, 10 - dificultyCoef);

        if (asteroidAppearChacne < 2)
        {
            if (asteroidCountChance < 2)
            {
                int chance = Random.Range(1, 3);
                switch (chance)
                {
                    case 1: SpawnTwoAsteroidsBeside(); break;
                    case 2: SpawnTwoAsteroidsSpaceBetween(); break;
                }
            } else
            {
                SpawnAsteroid();
            }
        }
    }
}
