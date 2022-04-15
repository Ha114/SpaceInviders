using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool mouseLeftDown = false;
    private bool mouseRightDown = false;

    Vector3 min;
    Vector3 max;
    private float objectWidth;

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));     

        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
    }

    //for the left button
    public void OnLeftMouseDown()
    {
        mouseLeftDown = true;
    }
    public void OnLeftMouseUp()
    {
        mouseLeftDown = false;
    }

    //for the right button
    public void OnRightMouseDown()
    {
        mouseRightDown = true;
    }
    public void OnRightMouseUp()
    {
        mouseRightDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseLeftDown && transform.position.x > min.x+objectWidth)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (mouseRightDown && transform.position.x < max.x-objectWidth)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

}
