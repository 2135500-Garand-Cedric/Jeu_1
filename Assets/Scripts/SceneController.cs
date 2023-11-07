using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// S'occupe de se deplacer entre les scènes
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Charge la nouvelle scène
    /// </summary>
    /// <param name="newScene">la scène à charger</param>
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
