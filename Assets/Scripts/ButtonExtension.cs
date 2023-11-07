using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ajoute des m�thodes � la composante Bouton
/// </summary>
public class ButtonExtension : InfoChanger
{
    /// <summary>
    /// Lorsque le bouton est appuy�
    /// </summary>
    /// <param name="scene">la sc�ne</param>
    public void OnClick(string scene)
    {
        OnSceneChange?.Invoke(scene);
    }
}
