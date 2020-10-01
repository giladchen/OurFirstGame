using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public ForceMode forceMode=ForceMode.Force;
    public float dirForce = 1.0f;
    public Rigidbody playerBody;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            playerBody.AddForce(0, 0, dirForce, forceMode);
        }
        if (Input.GetKey("a"))
        {
            playerBody.AddForce(-dirForce, 0, 0, forceMode);
        }
        if (Input.GetKey("d"))
        {
            playerBody.AddForce(dirForce, 0, 0, forceMode);
        }
        if (Input.GetKey("s"))
        {
            playerBody.AddForce(0, 0, -dirForce, forceMode);
        }
    }
}
