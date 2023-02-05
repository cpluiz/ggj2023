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
    public CanvasController CanvasControllerRef;
    void Awake(){
        if(_instance == null){
            _instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    void Start(){
        UpdatePlayerRef();
        UpdateFollowCamRef();
        UpdateCanvasControllerRef();
    }

    public static void UpdatePlayerRef(){
        _instance.PlayerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public static void UpdateFollowCamRef(){
        _instance.FollowCamRef = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<TopDownCAM>();
    }

    public static void UpdateCanvasControllerRef(){
        _instance.CanvasControllerRef = GameObject.FindGameObjectWithTag("CanvasController").GetComponent<CanvasController>();
    }
}
