using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    #region singleton
    public static Shoot instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    public GameObject burrel;
    public float interval = 2f;
    private Sprite spriteRenderer;

    void Start()
    {
        StartCoroutine(ShootBurrel());
    }

    IEnumerator ShootBurrel()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Instantiate(burrel, this.gameObject.transform.position, Quaternion.identity);
        }
    }

}
