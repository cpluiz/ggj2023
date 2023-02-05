using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour{
    [SerializeField] private Animator playerAnim;

    void Awake(){
        if(playerAnim == null)
            playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetAnimationParam(string parameter, bool value){
        playerAnim.SetBool(parameter, value);
    }

    public void SetAnimationParam(string parameter, float value){
        playerAnim.SetFloat(parameter, value);
    }
}
