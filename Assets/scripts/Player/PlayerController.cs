using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public static PlayerController instance{get{return _instance;}}
    private static PlayerController _instance;
    [SerializeField] private CharacterMovement movmentController;
    [SerializeField] private PlayerAnimationController animController;
    [SerializeField] public Rigidbody rigidBody;
    [SerializeField] private int lives = 5;
    [SerializeField] private int minLives = 2;
    [SerializeField] [Range(0.5f, 4f)] private float invulnerabilityDelay = 1.5f;
    [SerializeField] private bool takingDammage;

    void Awake(){
        if(_instance == null){
            _instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    void Start(){
        GameManager.instance.CanvasControllerRef.SetLifes(lives);
    }

    public static void SetAnimationParam(string parameter, bool value){
        _instance.animController.SetAnimationParam(parameter, value);
    }
    public static void SetAnimationParam(string parameter, float value){
        _instance.animController.SetAnimationParam(parameter, value);
    }

    public void ApplyDammage(){
        _instance.TakeDammage();
    }

    public void ApplyStunAttack(Transform stunLocation, Transform enemyLocation){
        _instance.TakeDammage();
        _instance.movmentController.ReceiveStunAttack(stunLocation, enemyLocation);
    }

    public void UnlockStun(){
        _instance.movmentController.RecoverFromStun();
    }

    public void ApplyKnockback(float force, Transform lookAt){
        _instance.TakeDammage();
        _instance.movmentController.ApplyKnockback(force, lookAt);
    }

    private void TakeDammage(){
        if(takingDammage) return;
        takingDammage = true;
        lives --;
        GameManager.instance.CanvasControllerRef.SetLifes(lives);
        if(lives <= minLives){
            //TODO GameOver
            return;
        }
        StartCoroutine(nameof(InvulnerabilityDelay));
    }

    private IEnumerator InvulnerabilityDelay(){
        yield return new WaitForSeconds(invulnerabilityDelay);
        takingDammage = false;
    }
}
