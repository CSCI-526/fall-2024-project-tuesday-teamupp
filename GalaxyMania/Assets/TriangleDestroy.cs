using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerTriangleCollision.collectTriangle)
        {
            Destroy(gameObject); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
