using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDestroy : MonoBehaviour
{
    void Start()
    {
        if (PlayerDiamondCollision.hasDiamond)
        {
            Destroy(gameObject); 
        }
    }

}
