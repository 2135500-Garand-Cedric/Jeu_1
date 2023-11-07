using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// S'occupe de se deplacer entre les sc�nes
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Charge la nouvelle sc�ne
    /// </summary>
    /// <param name="newScene">la sc�ne � charger</param>
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
