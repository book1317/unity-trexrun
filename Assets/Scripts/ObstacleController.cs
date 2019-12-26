using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float minX = -10;
    public float speed = 0.15f;
    private LevelController theLevel;

    void Start()
    {
        theLevel = FindObjectOfType<LevelController>();
    }

    void Update()
    {
        if (theLevel.currentState == LevelController.GameState.Playing)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            if (transform.position.x < minX)
                theLevel.RemoveObstacle(this);
        }
    }
}
