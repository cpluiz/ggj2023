using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public static PlayerController instance{get{return _instance;}}
    private static PlayerController _instance;
    [SerializeField] private CharacterMovement movmentController;
    [SerializeField] private PlayerAnimationController animController;
    [SerializeField] public Rigidbody rigidBody;

    void Awake(){
        if(_instance == null){
            _instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public static void SetAnimationParam(string parameter, bool value){
        _instance.animController.SetAnimationParam(parameter, value);
    }
    public static void SetAnimationParam(string parameter, float value){
        _instance.animController.SetAnimationParam(parameter, value);
    }
}
