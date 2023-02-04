using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyMovement enemyMovement;
    [Range(20, 500)]
    public float attackRange;
    [SerializeField]
    private bool attack, isAttacking;

    void Awake(){
        if(enemyAnimator == null){enemyAnimator = gameObject.GetComponent<Animator>();}
    }

    void Start(){
        enemyAnimator.Play("Idle");
    }

    void Update(){
        attack = Vector3.Distance(transform.position, GameManager.instance.PlayerRef.transform.position) <= attackRange;
    }

    protected void AttackPlayer(){
        if(attack && !isAttacking){
            enemyMovement.StopMovement();
            isAttacking = true;
            //TODO aplicar animação
            return;
        }
        if(attack && isAttacking){
            Debug.Log(enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            //isAttacking = enemyAnimator.GetCurrentAnimatorStateInfo(0).nameHash == "idle";
        }
    }
}
