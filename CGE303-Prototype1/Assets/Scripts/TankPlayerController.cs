using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerController : MonoBehaviour
{
    //try setting this to 8 in the inspector public float speed;
    public float speed;

    //try setting this to 100 in the inspector public float turnSpeed;
    public float turnSpeed;

    public float horizontalInput;
    public float verticalInput;

    // Update is called once per frame
    void Update()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * Time.deltaTime * speed * verticalInput);

        if (verticalInput < 0)
        {

        transform.Rotate(Vector3.back, -turnSpeed * Time.deltaTime * horizontalInput);

        }

        else
        {

        transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime * horizontalInput);

        }

    }
}
