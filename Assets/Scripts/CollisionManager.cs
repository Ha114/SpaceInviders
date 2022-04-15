using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.instance.SaveLastResult();
                gameObject.SetActive(false);
                SceneManager.LoadScene(2); //GameOverScene
            }
            else if (collision.gameObject.CompareTag("Bullet"))
            {
                gameObject.SetActive(false);
                GameManager.instance.UpdateScore(1);
                GameManager.instance.UpdateDestroyEnemies();
                Destroy(collision.gameObject);
            }
        }
        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                gameObject.SetActive(false);
                Destroy(collision.gameObject);
                Debug.Log("Collision");
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                Spawner.instance.UpdatePoints();
            }
        }

    }
}
