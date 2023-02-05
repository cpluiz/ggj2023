using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance{get{return _instance;}}
    //TODO Alterar para script do tipo do player, quando houver
    public PlayerController PlayerRef;
    public TopDownCAM FollowCamRef;
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
        UpdateFollowCamRef();
    }

    public static void UpdatePlayerRef(){
        _instance.PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public static void UpdateFollowCamRef(){
        _instance.FollowCamRef = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TopDownCAM>();
    }
}
