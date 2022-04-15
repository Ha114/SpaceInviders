using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region singleton
    public static Spawner instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    Vector3 min;
    Vector3 max;
    private float speed = 0.1f;
    private float objectWidth;
    private float interval = 7f;

    public GameObject prefabRammingEnemy;
    public GameObject prefabShootingEnemy;
    public GameObject enemyBullet;
    public GameObject[,] enemiesEnemy = new GameObject[4, 5];
    public Vector3 dir = Vector2.right;

    int x = 0;

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        objectWidth = prefabRammingEnemy.transform.GetComponent<SpriteRenderer>().bounds.extents.x;

        SpawnEnemies();
        StartCoroutine(RandomShoot());
    }
    
    private void Update()
    {
        this.transform.position += dir * speed * Time.deltaTime;
        foreach (GameObject enemy in enemiesEnemy)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                if (enemy.transform.position.x > max.x - objectWidth - 0.2f)
                {
                    UpdateDirection(false);
                }
                if (enemy.transform.position.x < min.x + objectWidth + 0.2f)
                {
                    UpdateDirection(true);
                }
            }
        }
    }

    private void UpdateDirection(bool b)
    {
        if (b) { dir = Vector3.right; }
        else { dir = Vector3.left; }
        speed += 0.03f;
        Vector3 pos = transform.position;
        pos.y -= 0.1f;
        transform.position = pos;
    }
    void SpawnEnemies()
    {
        float x = min.x + 0.5f;
        float y = max.y - 1;
        float n = 0;
        float m = 0;
        GameObject units = prefabShootingEnemy;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject go = Instantiate(units, new Vector3(x + n, y - m, 0), Quaternion.identity);
                go.transform.SetParent(this.transform.transform);
                enemiesEnemy[i, j] = go;
                n += 0.7f;
            }
            units = prefabRammingEnemy;
            m += 0.7f;
            n = 0;
        }
    }
    IEnumerator RandomShoot()
    {
        bool isShoot = false;
        while (true)
        {
            yield return new WaitForSeconds(interval);

            while (!isShoot)
            {
                x = Random.Range(0, 5);
                 
                if (enemiesEnemy[0, x].activeInHierarchy)
                {
                    GameObject go = Instantiate(enemyBullet, enemiesEnemy[0, x].gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
                    if (interval != 4f) { interval -= 0.25f; }
                    isShoot = true;
                }
            }
            isShoot = false;
        }
    }

    public void UpdatePoints()
    {
        int row = x;
        int counter = 0;
        for (int i = 0; i < 4; i++)
        {
            if (enemiesEnemy[i, row].activeInHierarchy)
            {
                counter++;
            }
        }
        GameManager.instance.UpdateScore(-counter*2);
    }
}
