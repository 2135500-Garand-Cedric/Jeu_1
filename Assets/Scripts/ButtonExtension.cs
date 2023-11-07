using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ajoute des méthodes à la composante Bouton
/// </summary>
public class ButtonExtension : InfoChanger
{
    /// <summary>
    /// Lorsque le bouton est appuyé
    /// </summary>
    /// <param name="scene">la scène</param>
    public void OnClick(string scene)
    {
        OnSceneChange?.Invoke(scene);
    }
}
