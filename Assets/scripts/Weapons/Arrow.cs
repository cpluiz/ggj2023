using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour{
    void OnCollisionEnter(Collision other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Scenary")){
            StartCoroutine(nameof(DestroyArrow));
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.transform.name);
    }

    private IEnumerator DestroyArrow(){
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
