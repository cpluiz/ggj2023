using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(0,2f)]
    public float velocityDrag;
    [Range(1f,50f)]
    public float velocity = 10.0f;
    [Range(1.5f,5f)]
    public float runScale = 2;
    [Range(5f,200f)]
    public float turnVelocity = 20f;
    private float x,y;
    private bool isRunning, isShooting;
    [SerializeField]
    private bool isStuned;
    [SerializeField]
    private Transform stunnedPosition, stunnedLookAt;

    // Update is called once per frame
    void Update(){
        x = Input.GetAxis("Vertical");
        y = Input.GetAxis("Horizontal");
        isRunning = Input.GetButton("Run");
        isShooting = Input.GetButton("Fire1");
    }

    void FixedUpdate(){
        if(PlayerController.instance.shooting || isShooting) return;
        if(isStuned){
            transform.position = stunnedPosition.position;
            transform.LookAt(stunnedLookAt, Vector3.up);
            return;
        };
        if(y!=0)transform.Rotate(Vector3.up * turnVelocity * Time.fixedDeltaTime * y * (isRunning ? runScale : 1));
        if(x!=0){
            //PlayerController.instance.rigidBody.velocity = 
            //    (PlayerController.instance.transform.forward * x * (velocity * 100) * Time.fixedDeltaTime * (isRunning ? runScale : 1)) // velocidade de movimentação horizontal
            //    + new Vector3(0,PlayerController.instance.rigidBody.velocity.y,0); // mantendo a velocidade vertical para não comprometer o pulo
            PlayerController.instance.rigidBody.AddForce(
                PlayerController.instance.transform.forward * x * velocity * (isRunning ? runScale : 1)
                ,ForceMode.Impulse
            );
        }
    }
    void LateUpdate(){
        if(isStuned || PlayerController.instance.shooting || isShooting){
            PlayerController.SetAnimationParam("walking", false);
            return;
        }
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        PlayerController.SetAnimationParam("walking", x!=0 || y!=0);
        PlayerController.SetAnimationParam("running", isRunning);
        PlayerController.SetAnimationParam("walkSpeed", 0);
        if(y!=0){
            PlayerController.SetAnimationParam("walkSpeed", y);
        }
        if(x!=0){
            PlayerController.SetAnimationParam("walkSpeed", x);
        }
        //Adjust the "ground" velocity by a fixed drag, without affecting the vertical drag
        PlayerController.instance.rigidBody.velocity = 
            new Vector3(
                PlayerController.instance.rigidBody.velocity.x / (1f + velocityDrag),
                PlayerController.instance.rigidBody.velocity.y,
                PlayerController.instance.rigidBody.velocity.z / (1f + velocityDrag)
            );
    }

    public void ReceiveStunAttack(Transform stunPosition, Transform lookAt){
        //TODO Aplicar dano ao personagem aqui
        stunnedPosition = stunPosition;
        stunnedLookAt = lookAt;
        isStuned = true;
    }

    public void RecoverFromStun(){
        isStuned = false;
    }

    public void ApplyKnockback(float force, Transform lookAt){
        stunnedLookAt = lookAt;
        transform.LookAt(stunnedLookAt, Vector3.up);
        PlayerController.instance.rigidBody.AddForce(Vector3.back * force * 10, ForceMode.Impulse);
    }

}
