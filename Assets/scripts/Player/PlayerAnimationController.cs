using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour{
    [SerializeField] private Animator playerAnim;
    [SerializeField] private AnimatorStateInfo stateInfo, nextStateInfo;

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
    public bool GetAnimationBoolean(string parameter){
        return playerAnim.GetBool(parameter);
    }
    public bool AnimationIsFinished(string stateName){
        for(int i = 0; i < playerAnim.layerCount; i++){
            stateInfo = playerAnim.GetCurrentAnimatorStateInfo(i);
            nextStateInfo = playerAnim.GetNextAnimatorStateInfo(i);
            if(stateInfo.IsTag(stateName) || nextStateInfo.IsTag(stateName)) return false;
        }
        return !playerAnim.IsInTransition(0);
    }
}
