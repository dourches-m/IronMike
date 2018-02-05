using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    private float endAttack = 0.07f;
    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        endAttack -= Time.deltaTime;
        if (endAttack <= 0.0f)
            Destroy(gameObject);
    }
}
