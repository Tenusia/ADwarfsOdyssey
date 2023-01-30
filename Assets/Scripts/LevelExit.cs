using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float exitWaitDelayTime=1f;
    AudioPlayer audioPlayer;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        //If no enemy -> causes crash
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player") {return;}
        if(enemyMovement.bossIsAlive) {return;}
        audioPlayer.PlayClearLevel();
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(exitWaitDelayTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;    
        }
        
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }  
}
