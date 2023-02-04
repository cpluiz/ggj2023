using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] EnemyMovement enemyMovement;
    [Range(0.5f, 40)]
    public float attackRange;
    [Range(1f,5f)]
    public float attackInterval;
    [SerializeField]
    private bool attack, isAttacking;
    private AnimatorStateInfo stateInfo, nextStateInfo;

    void Awake(){
        if(enemyAnimator == null){enemyAnimator = gameObject.GetComponent<Animator>();}
    }

    void Start(){
        enemyAnimator.Play("Idle");
    }

    void Update(){
        if(!attack && !isAttacking) attack = Vector3.Distance(transform.position, GameManager.instance.PlayerRef.transform.position) <= attackRange;
    }

    void LateUpdate(){
        AttackPlayer();
        enemyAnimator.SetBool("walking", !enemyMovement.stationary);
    }

    protected IEnumerator ReenableAttack(){
        //Se por algum motivo isso for chamado enquanto a animação de ataque ainda estiver rolando,
        //ABORTAR MISSÃO
        if(stateInfo.IsName("Attack") && stateInfo.normalizedTime < 1f) yield return null;
        //Caso contrário, aguarde o intervalo entre ataques para habilitar o próximo
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
        attack = false;
    }

    protected void AttackPlayer(){
        //Se foi detectado que o jogador está na distância de ataque, mas o ataque ainda não iniciou,
        //prepara os paranauês
        if(attack && !isAttacking){
            enemyMovement.StopMovement();
            enemyAnimator.SetBool("attacking", true);
            isAttacking = true;
            return;
        }
        if(isAttacking){
            //Se a animação atual ainda é a de ataque, sai da função sem fazer nada.
            stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            nextStateInfo = enemyAnimator.GetNextAnimatorStateInfo(0);
            if(
                stateInfo.IsName("Attack") || nextStateInfo.IsName("Attack")
            ) return;
            //Se a animação atual não é mais ataque, a animação de ataque já acabou, pode seguir o baile
            enemyAnimator.SetBool("attacking", false);
            enemyMovement.ResumeMovement();
            StartCoroutine(nameof(ReenableAttack));
        }
    }
}
