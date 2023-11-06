using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// S'occupe de se deplacer entre les scenes
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Charge la nouvelle scene
    /// </summary>
    /// <param name="newScene">la scene a charger</param>
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
