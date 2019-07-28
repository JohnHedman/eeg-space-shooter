using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoundry : MonoBehaviour
{
    
    public float shipBoundry = 25.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;


        // If the ship goes out of the boundry, let's delete it.
        if(pos.x > shipBoundry || pos.x < -shipBoundry)
        {
            Destroy(gameObject);
        }
        if(pos.y < -shipBoundry)
        {
            Destroy(gameObject);
        }

        
    }
}
