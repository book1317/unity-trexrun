using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject CactusPrefab;
    private const float START_CACTUS_X = 8.0f;
    private const float START_CACTUS_Y = -1.37f;

    [SerializeField] private GameObject BirdPrefab;
    private const float START_BIRD_X = 8.0f;
    private const float START_BIRD_Y = -1.0f;

    [SerializeField] private float generateTime = 2.0f;
    private float generateCounter;
    private LevelController theLevel;

    void Start()
    {
        theLevel = FindObjectOfType<LevelController>();
    }

    void Update()
    {
        if (theLevel.currentState == LevelController.GameState.Playing)
        {
            if (generateCounter >= 0)
                generateCounter -= Time.fixedDeltaTime;
            else
            {
                CreateObstacle();
                generateCounter = generateTime;
            }
        }
    }

    void CreateObstacle()
    {
        GameObject theObstacleObject;

        if (theLevel.currentObstacle == LevelController.ObstacleState.Cactus)
        {
            theObstacleObject = Instantiate(CactusPrefab, new Vector3(START_CACTUS_X, START_CACTUS_Y, transform.position.z), Quaternion.identity);
        }
        else // Bird
        {
            int randomNumber = Random.Range(0, 3);
            if (randomNumber == 0)
            {
                theObstacleObject = Instantiate(BirdPrefab, new Vector3(START_BIRD_X, START_BIRD_Y, transform.position.z), Quaternion.identity);
            }
            else
            {
                theObstacleObject = Instantiate(CactusPrefab, new Vector3(START_CACTUS_X, START_CACTUS_Y, transform.position.z), Quaternion.identity);
            }
        }
        theLevel.allObstacle.Add(theObstacleObject.GetComponent<ObstacleController>());

    }
}
