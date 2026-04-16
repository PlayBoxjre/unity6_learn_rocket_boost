using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                ReloadScene( (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings );
                break;
            case "Fuel":
                Debug.Log("You picked up fuel!");
                break; 
            default:
                ReloadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }


        void ReloadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
