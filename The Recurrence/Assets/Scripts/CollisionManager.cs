using UnityEngine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] float Delay;
    [SerializeField] AudioClip SuccessClip;
    [SerializeField] AudioClip ExplosionClip;
    bool isTransitioning = false;

    AudioSource As;

    void Start()
    {
        As = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning) {return;}
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
        isTransitioning = true;
        As.Stop();
        As.PlayOneShot(SuccessClip);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", Delay);
    }

    void Crashed()
    {
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

