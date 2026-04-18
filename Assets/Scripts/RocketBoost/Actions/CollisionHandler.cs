using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadlLevelDelay = 3f;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] AudioClip landAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] float volumnScale = 0.3f;
    AudioSource audioSource;
    // 用于处理多次碰撞问题，碰撞后，isControllable设置为false，禁止再次碰撞
    bool isControllable = true;

    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private  void RespondToDebugKey()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable; // toggle collidable
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // isControllable 
        if (!isControllable || !isCollidable) return;

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
        isControllable = false;

        // todo add sfx and particle effect
        // 1. play sfx 
        audioSource.Stop();
        audioSource.PlayOneShot(landAudio, volumnScale);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", loadlLevelDelay);
    }

    // 为了避免使用“reloadScene"硬编码风格提取的方法
    private void StartCrashSequence()
    {
        isControllable = false;
        // todo add sfx and particle effect

        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudio, volumnScale);
        crashParticles.Play();
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
