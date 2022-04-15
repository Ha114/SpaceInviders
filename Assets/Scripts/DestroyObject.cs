using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyObject : MonoBehaviour
{
    Vector3 min;
    Vector3 max;
    private float objectWidth;

    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        if (transform.position.y > max.y || transform.position.y < min.y + 1.6f)
        {
            if (gameObject.name == "ShoutingUnit(Clone)" || gameObject.name == "RammingUnits(Clone)") 
            {
                GameManager.instance.SaveLastResult();
                Destroy(gameObject);
                SceneManager.LoadScene(2); //GameOverScene
            }
            Destroy(gameObject);
        }
    }
}
