using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerDiamondCollision.hasDiamond)
        {
            Destroy(gameObject); // Destroy the diamond if it has already been collected
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
