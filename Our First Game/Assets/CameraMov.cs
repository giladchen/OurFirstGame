using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset = new Vector3(0,0,-5);
       // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.transform.position-offset;
    }
}
