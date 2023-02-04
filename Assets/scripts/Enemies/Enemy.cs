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
        enemyAnimator.SetBool("attacking", AttackPlayer());
         if(HasParameter("walking", enemyAnimator))
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

    protected bool AttackPlayer(){
        //Se foi detectado que o jogador está na distância de ataque, mas o ataque ainda não iniciou,
        //prepara os paranauês
        if(attack && !isAttacking){
            enemyMovement.StopMovement();
            isAttacking = true;
            return true;
        }
        if(isAttacking){
            //Se a animação atual ainda é a de ataque, sai da função sem fazer nada.
            stateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            nextStateInfo = enemyAnimator.GetNextAnimatorStateInfo(0);
            if(stateInfo.IsName("Attack") || nextStateInfo.IsName("Attack")) return true;
            //Se a animação atual não é mais ataque, a animação de ataque já acabou, pode seguir o baile
            enemyMovement.ResumeMovement();
            StartCoroutine(nameof(ReenableAttack));
        }
        return false;
    }

    private bool HasParameter(string paramName, Animator animator){
        foreach (AnimatorControllerParameter param in animator.parameters)
            if (param.name == paramName)
                return true;
        return false;
    }
}
