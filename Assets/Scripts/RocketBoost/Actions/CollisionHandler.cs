using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadlLevelDelay = 3f;
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        // todo add sfx and particle effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", loadlLevelDelay);
    }

    // 为了避免使用“reloadScene"硬编码风格提取的方法
    private void StartCrashSequence()
    {
        // todo add sfx and particle effect


        // 移动和当前脚本再Player身上，可以直接获取Movement脚本，并停用，防止碰撞后，还可以操作问题
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", loadlLevelDelay);

    }

    void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
    void ReloadScene( )
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
