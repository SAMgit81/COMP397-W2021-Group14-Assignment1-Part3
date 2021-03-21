using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Pauseable : MonoBehaviour
{

    public List<MonoBehaviour> scripts;
    public List<NavMeshAgent> agents;
    public bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;

        agents = FindObjectsOfType<NavMeshAgent>().ToList();

        foreach(var enemy in FindObjectsOfType<CryptoBehaviour>())
        {
            scripts.Add(enemy);
        }
        scripts.Add(FindObjectOfType<PlayerBehaviour>());
        scripts.Add(FindObjectOfType<CameraController>());
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

       foreach (var script in scripts)
        {
            script.enabled = !isGamePaused;
        }

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePaused;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
