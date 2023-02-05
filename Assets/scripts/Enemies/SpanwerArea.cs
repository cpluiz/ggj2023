using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwerArea : MonoBehaviour{
    public EnemySpawn[] spanwers;

    void OnTriggerEnter(Collider other){
        foreach(EnemySpawn spawner in spanwers){
            spawner.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        foreach(EnemySpawn spawner in spanwers){
            spawner.gameObject.SetActive(true);
        }
    }
}
