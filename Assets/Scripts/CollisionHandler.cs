using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip failureClip;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem failureParticle;
    

    
    

    AudioSource audioSource;

    bool isTransitioning;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    float loadDelay = 2f;

    private void Update()
    {
        RespondToDebugging();
    }

    private void RespondToDebugging()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNewLevel();
            Debug.Log("Debbuging mod activated. Loading new level.");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
            Debug.Log("Debbuging mod activated. Collisions: OFF.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) 
        {
            return;
        }
        if (collisionDisable)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                Debug.Log("Success sequence started.");
                StartNewLevelSequence();
                break;
            default:
                Debug.Log("Crash sequence started.");
                StartCrashSequence();
                break;          
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(failureClip);
        // todo add particle effect 
        failureParticle.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartNewLevelSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successClip); 
        isTransitioning = true;
        // todo add particle effect 
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNewLevel", loadDelay);
    }
}
