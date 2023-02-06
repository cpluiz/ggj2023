using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour{
    [Header("Configurações básicas do inimigo")]
    [SerializeField] EnemyMovement enemyMovement;
    [Range(0.5f, 40)]
    public float attackRange;
    [Range(1f,5f)]
    public float attackInterval;
    [SerializeField] bool stunAttack;
    [SerializeField] protected Transform stunLocation;
    [SerializeField] bool knockbackAttack;
    [SerializeField] [Range(0.5f, 10f)] protected float knockbackForce;
    [Header("Referências e variáveis de acompanhamento")]
    [SerializeField] Animator enemyAnimator;
    [SerializeField]
    private bool attack, isAttacking, stunnedPlayer;
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
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        enemyAnimator.SetBool("attacking", AttackPlayer());
         if(HasParameter("walking", enemyAnimator))
            enemyAnimator.SetBool("walking", !enemyMovement.stationary);
    }

    protected IEnumerator ReenableAttack(){
        if(stunnedPlayer){
            GameManager.instance.PlayerRef.UnlockStun();
            stunnedPlayer = false;
        }
        yield return new WaitForSeconds(attackInterval);
        isAttacking = false;
        attack = false;
    }

    protected bool AttackPlayer(){
        //Se foi detectado que o jogador está na distância de ataque, mas o ataque ainda não iniciou,
        //prepara os paranauês
        if(attack && !isAttacking){
            enemyMovement.StopMovement();
            StopAllCoroutines();
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
    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
    }
    void OnTriggerStay(Collider other){
        if(other.gameObject.tag != "Player" || !isAttacking || stunnedPlayer) return;
        if(stunAttack){
            stunnedPlayer = true;
            GameManager.instance.PlayerRef.ApplyStunAttack(stunLocation, transform);
            return;
        }
        if(knockbackAttack){
            GameManager.instance.PlayerRef.ApplyKnockback(knockbackForce, transform);
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Scenary") ) return;
    }
}
