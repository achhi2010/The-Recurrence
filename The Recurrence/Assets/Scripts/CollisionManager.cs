using UnityEngine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] float Delay;
    [SerializeField] AudioClip SuccessClip;
    [SerializeField] AudioClip ExplosionClip;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem ExplosionParticles;
    bool isTransitioning = false;
    bool disableCollision = false;

    AudioSource As;

    void Start()
    {
        As = GetComponent<AudioSource>();
    }
    void Update()
    {
       DebugKeys(); 
    }
    
    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }else if(Input.GetKeyDown(KeyCode.C))
        {
            disableCollision = !disableCollision;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || disableCollision) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Ready To Launch");
            break;

            case "Finish":
            Completed();
            break;

            default:
            Crashed();
            break;
        }
    }

    void Completed()
    {
        SuccessParticles.Play();
        isTransitioning = true;
        As.Stop();
        As.PlayOneShot(SuccessClip);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", Delay);
    }

    void Crashed()
    {
        ExplosionParticles.Play();
        isTransitioning = true;
        As.Stop();
        As.PlayOneShot(ExplosionClip);
        GetComponent<Movement>().enabled = false;
        Invoke("GameOver", Delay);
    }

    void NextLevel()
    {
        int SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = SceneBuildIndex + 1;
        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);
    }

    void GameOver()
    {
        int SceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneBuildIndex);
    }
}

