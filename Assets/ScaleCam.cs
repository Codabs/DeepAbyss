using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, 0.7f, 0.8f), transform.localPosition.z);
    }
}
