using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Simples : MonoBehaviour
{

    /*
    simples movimentação do objeto, eixo X eZ 
    */
    
    public float velocidade;
    public Animator animator;

    

    float InputX, InputZ;
    Vector3 direcao;
    public Camera mainCamera;

    void Start()
    {
        //camera = Camera.main;
        animator = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ = Input.GetAxisRaw("Vertical");

        direcao = new Vector3(InputX,0.000f,InputZ);

       if(InputX!=0 || InputZ !=0){
           var camrot = mainCamera.transform.rotation;
           camrot.x =0;
           camrot.z=0;
           transform.Translate(0,0,velocidade*Time.deltaTime);
           animator.SetBool("walking",true);
           transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcao)*camrot,5*Time.deltaTime);
       }
    }
    
}