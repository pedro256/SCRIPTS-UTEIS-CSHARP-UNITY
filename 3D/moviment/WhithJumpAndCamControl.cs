using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV3CAM : MonoBehaviour
{
    
    public float velocidade;
    private Animator animator;
    float InputX, InputZ;
    Vector3 direcao;
    public Camera mainCamera;

    public float velocidadeCamera;
    public float velocidadeRotacaoCamera;
    public  Vector3 cameraOffSet;

    public Rigidbody rb;
    public float jumpForce;
    public LayerMask layerMask;
    public bool isGrounded = false;
    public float groundCheckSize;
    public Vector3 groundCheckPosition;


    void Start()
    {
        animator = GetComponent<Animator>();
      
    }

    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        InputZ = Input.GetAxisRaw("Vertical");

        direcao = new Vector3(InputX,0.000f,InputZ);

       if(InputX!=0 || InputZ !=0 ){
           var camrot = mainCamera.transform.rotation;
           camrot.x =0;
           camrot.z=0;
           //animator.SetBool("walking", true);
            //script de movimento
            var velocidadeContext = velocidade;
            if (!isGrounded)
            {
                velocidadeContext = velocidade / 2;
            }
            transform.Translate(0,0,velocidadeContext*Time.deltaTime);
           transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcao)*camrot,5*Time.deltaTime);


       }
       /*
       else if(InputX==0 && InputZ ==0){
            animator.SetBool("walking",false);
       }
       */
       //configuração de rotação da camera
       // direção do personagem se ajusta de acordo com a direção da cam

       mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, transform.rotation, velocidadeCamera*Time.deltaTime);

       // sistema de pulo
       var groundCheck = Physics.OverlapSphere(transform.position+groundCheckPosition,groundCheckSize, layerMask);
       if(groundCheck.Length !=0){
           isGrounded = true;
       }else{
           isGrounded = false;
       }
        animator.SetBool("jumping", !isGrounded);

        //se estiver no chão e tocar o botão jump execute a ação
        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

    }
    //toda movimentação de camera deve ser nesse método
    //camera seguindo
    
    private void LateUpdate() {
        //calculando uma distancia entre a camera e o personagem no eixo x,y,z
        var pos = transform.position - mainCamera.transform.forward* cameraOffSet.z 
        + mainCamera.transform.up * cameraOffSet.y 
        + mainCamera.transform.right* cameraOffSet.x;
        
        //inserindo configuração da ditancia
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,pos,velocidadeCamera*Time.deltaTime);

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCheckPosition, groundCheckSize);
    }
}
