using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Enemy[] enemyPrefab;
    [Range(10f, 60f)]
    public float spawnInterval = 0.1f;
    [Range(1f, 5.5f)]
    public float spawnIntervalRandomRange = 0.1f;
    public bool spawnContinuously;
    [Range(1, 2000)]
    public int spawnLimit = 1;
    private int spawned = 0;
    void Start(){
        StartCoroutine(nameof(SpawnEnemy));
    }

    public void StopSpawn(){
        spawnContinuously = false;
    }

    private IEnumerator SpawnEnemy(){
        //Se não tiver inimigo para spawnar, nem tenta pra não dar pau.
        if(enemyPrefab.Length == 0){
            Debug.Log("Não ha inimigos para serem spawnados");
            yield return false;
        }
        float waitTime = spawnInterval + Random.Range(-spawnIntervalRandomRange, spawnIntervalRandomRange);
        Debug.Log("Esperando "+waitTime+" segundos");
        yield return new WaitForSeconds(waitTime);
        //Seleciona um dos inimigos disponíveis da lista para ser spawnado - se só tiver um, vai ele mesmo
        int enemyId = Random.Range(0, enemyPrefab.Length);
        Debug.Log("Spawnando");
        Enemy newEnemy = Instantiate(enemyPrefab[enemyId], transform.position, Quaternion.identity);
        //Se houver alguma chamada de comando pra preparar o inimigo, inserir aqui
        spawned ++;
        if(spawned < spawnLimit && spawnContinuously){
            StartCoroutine(nameof(SpawnEnemy));
        }
    }
}
