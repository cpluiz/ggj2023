using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(1f,20f)]
    public float velocity = 10.0f;
    [Range(1.5f,5f)]
    public float runScale = 2;
    [Range(5f,200f)]
    public float turnVelocity = 20f;
    private float x,y;
    private bool isRunning;

    // Update is called once per frame
    void Update(){
        x = Input.GetAxis("Vertical");
        y = Input.GetAxis("Horizontal");
        isRunning = Input.GetButton("Run");
    }

    void FixedUpdate(){
        if(y!=0)transform.Rotate(Vector3.up * turnVelocity * Time.deltaTime * y);
        if(x!=0){
            PlayerController.instance.rigidBody.velocity = PlayerController.instance.transform.forward * x * velocity;
            if(isRunning) PlayerController.instance.rigidBody.velocity *= runScale;
        }
    }
    void LateUpdate(){
        PlayerController.SetAnimationParam("walking", x!=0 || y!=0);
        PlayerController.SetAnimationParam("running", isRunning);
        PlayerController.SetAnimationParam("walkSpeed", 0);
        if(y!=0){
            PlayerController.SetAnimationParam("walkSpeed", y);
        }
        if(x!=0){
            PlayerController.SetAnimationParam("walkSpeed", x);
            PlayerController.instance.rigidBody.velocity = PlayerController.instance.transform.forward * (x * velocity);
        }
    }

}
