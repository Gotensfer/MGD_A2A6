using System.Collections;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    public float speed = 1.0f;
    public float duration = 2.0f;

    private bool movingRight = true;
    private float moveTimer;

    void Start()
    {
        moveTimer = duration;
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            movingRight = !movingRight;
            moveTimer = duration;
        }

        float direction = movingRight ? 1 : -1;
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }
}