using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed; //actual speed of movement
    public float rotSpeed; //speed of turning around
    float x; //value on x-axis
    float z; //value on z-axis
    Vector3 moveDirection; //3-dimensional vector to hold player's movement on x,y,z
    Animator anim; //Object of Animator
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        z = Input.GetAxis("Vertical"); //value along the Z-axis
        x = Input.GetAxis("Horizontal"); //value along the X-axis
        //y is vertical, so will not change except if character jumps
        moveDirection = new Vector3(x, 0, z);
        //move the character at the given walkspeed through the world space
        transform.Translate(moveDirection * walkSpeed * Time.deltaTime, Space.World);
        //as long as the character is moving, s/he rotates so they're facing the direction
        //of movement.
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
            rotSpeed * Time.deltaTime);
        }


    }
}
