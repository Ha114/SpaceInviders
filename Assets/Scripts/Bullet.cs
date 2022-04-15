using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isEnemyBullet = false;
    Vector3 dir = Vector2.up;
    Vector3 dirEnemy = Vector2.down;

    void Update()
    {
        if (!isEnemyBullet)
        {
            this.transform.position += dir * GameManager.instance.speed * Time.deltaTime;
            this.transform.GetComponent<SpriteRenderer>().color = GameManager.instance.color;
        }
        else
        {
            this.transform.position += dirEnemy * 7f * Time.deltaTime;
        }
    }
}
