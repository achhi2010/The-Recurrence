using UnityEngine;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class CollisionManager : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Ready To Launch");
            break;

            case "Finish":
                NextLevel();
                break;

            default:
                Debug.Log("Game Over");
                GameOver();
                break;
        }
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

