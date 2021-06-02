using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSimple: MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce;
    void Update()
    {
        //se estiver no chão e tocar o botão jump execute a ação
        //isGrounded && 
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
