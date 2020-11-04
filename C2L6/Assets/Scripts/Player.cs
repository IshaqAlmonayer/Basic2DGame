using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(0,0);
        
        if(Input.GetAxis("Horizontal")>0)
        {
            direction = new Vector2(1, 0);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            direction = new Vector2(-1, 0);
        }

        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        
        transform.Translate(movement);
     }
}
