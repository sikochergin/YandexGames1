using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSys : MonoBehaviour
{
    private float _timenow = 0f;

    private void Update()
    {
        _timenow += Time.deltaTime;
        if ( _timenow > 5f)
        {
            Destroy(gameObject);
        }
    }
}
