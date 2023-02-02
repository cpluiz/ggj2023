using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private bool followPlayer;
    [Range(20, 500)]
    public float aggroRange;
    [Range(0.1f, 4f)]
    public float turnSpeed, moveSpeed = 1;
    [SerializeField]
    private Vector3 lookAt, targetPoint;

    void Awake(){
        lookAt = Vector3.forward;
        ChangeDirection();
        StartCoroutine(nameof(MaybeChangeDirection));
    }
    void Update(){
        followPlayer = PlayerIsOnAggroRange();
        if(followPlayer){FollowPlayerMovement();}else{DefaultMovement();}
        MoveEnemy();
    }

    private bool PlayerIsOnAggroRange(){
        if(GameManager.instance.PlayerRef == null) return false;
        bool isFollowing = Vector3.Distance(this.transform.position, GameManager.instance.PlayerRef.transform.position) <= aggroRange;
        return isFollowing;
    }

    protected void MoveEnemy(){
        transform.position += transform.forward * Time.deltaTime * moveSpeed;    
    }

    protected void DefaultMovement(){
        // Se o inimigo já estiver próximo ao ponto alvo da movimentação atual, mude o alvo
        while(Mathf.Approximately(Vector3.Distance(targetPoint, transform.position), 5f)){ChangeDirection();}; 
        lookAt = targetPoint - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed/2 * Time.deltaTime);
    }

    protected void FollowPlayerMovement(){
        lookAt = GameManager.instance.PlayerRef.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);
    }

    protected IEnumerator MaybeChangeDirection(){
        yield return new WaitForSeconds(Random.Range(2, 5));
        if(Random.Range(0, 101) > 30){
            ChangeDirection();
        }
        StartCoroutine(nameof(MaybeChangeDirection));
    }

    private void ChangeDirection(){
        targetPoint = Vector3.forward + new Vector3(
            Random.Range(transform.position.x - turnSpeed * 5, transform.position.x + turnSpeed * 5), // target X
            0, // target Y
            Random.Range(transform.position.z - turnSpeed * 5, transform.position.z + turnSpeed * 5) // target Z
        );
    }
}
