using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected bool followPlayer, cannotMove;
    public bool stationary{get;protected set;}
    [Range(20, 500)]
    public float aggroRange;
    [Range(0.1f, 4f)]
    public float turnSpeed, moveSpeed = 1;
    [SerializeField]
    protected Vector3 lookAt, targetPoint;

    protected void Awake(){
        lookAt = Vector3.forward;
        targetPoint = Vector3.forward;
    }
    protected void Update(){
        followPlayer = PlayerIsOnAggroRange();
        if(followPlayer){FollowPlayerMovement();}else{DefaultMovement();}
    }

    protected bool PlayerIsOnAggroRange(){
        if(GameManager.instance.PlayerRef == null) return false;
        bool isFollowing = Vector3.Distance(this.transform.position, GameManager.instance.PlayerRef.transform.position) <= aggroRange;
        return isFollowing;
    }

    protected void MoveEnemy(){
        if(cannotMove || stationary) return;
        transform.position += transform.forward * Time.deltaTime * moveSpeed;    
    }

    protected void DefaultMovement(){
        if(cannotMove) return;
        lookAt = targetPoint - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed/2 * Time.deltaTime);
    }

    protected void FollowPlayerMovement(){
        if(cannotMove) return;
        lookAt = GameManager.instance.PlayerRef.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);
    }

    public void StopMovement(){
        if(cannotMove) return;
        cannotMove = true;
    }

    public void ResumeMovement(){
        if(!cannotMove) return;
        cannotMove = false;
    }
}
