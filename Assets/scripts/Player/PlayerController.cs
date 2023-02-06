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
    [SerializeField] private bool takingDammage, shooting;
    [SerializeField] public Transform bowPosition;
    [SerializeField] public Bow bowRef;

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

    void FixedUpdate(){
        if(shooting) return;
        Shoot(Input.GetButton("Fire1"));
    }

    void LateUpdate(){
        if(shooting && animController.AnimationIsFinished("Shot")){
            shooting = false;
            animController.SetAnimationParam("attacking", shooting);
            StartCoroutine(nameof(StopShotting));
        }
    }

    private void Shoot(bool shoot){
        if(!shoot || bowRef.gameObject.activeSelf) return;
        shooting = true;
        bowRef.ShowBow(shooting);
        bowRef.Shoot();
        animController.SetAnimationParam("attacking", shooting);
        StartCoroutine(nameof(ShottingStun));
    }

    private IEnumerator ShottingStun(){
        movmentController.ShotingStun(true);
        yield return new WaitForSeconds(0.7f);
        movmentController.ShotingStun(false);
    }

    private IEnumerator StopShotting(){
        yield return new WaitForSeconds(1.5f);
        bowRef.gameObject.SetActive(shooting);
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
