using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnding : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    void LateUpdate(){
        Debug.Log("Player Time: "+player.time+" - Clip Length: "+player.clip.length);
        if((int)player.time >= (int)player.clip.length || Input.GetButton("Jump")){
            SceneManager.LoadScene("GameMapCpluiz");
        }
    }
}
