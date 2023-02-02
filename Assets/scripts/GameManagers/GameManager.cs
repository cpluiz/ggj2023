using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance{get{return _instance;}}
    //TODO Alterar para script do tipo do player, quando houver
    public GameObject PlayerRef;
    void Awake(){
        if(_instance == null){
            DontDestroyOnLoad(this);
            _instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    void Start(){
        UpdatePlayerRef();
    }

    public static void UpdatePlayerRef(){
        _instance.PlayerRef = GameObject.FindGameObjectWithTag("Player");
    }
}
