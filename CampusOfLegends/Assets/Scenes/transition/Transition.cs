using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    Animator animator;
    public string sceneName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Charge une nouvelle scène avec transition
    public void loadNextScene(string sceneName)
    {
        this.sceneName = sceneName;
        animator.SetTrigger("Out");
    }

    // Méthode appelée à la fin de l'animation
    public void OnTransitionComplete()
    {
        SceneManager.LoadScene(sceneName);
    }
}
