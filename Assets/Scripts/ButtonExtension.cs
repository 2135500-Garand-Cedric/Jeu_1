using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ajoute des methodes a la composante Bouton
/// </summary>
public class ButtonExtension : InfoChanger
{
    /// <summary>
    /// Lorsque le bouton est appuye
    /// </summary>
    /// <param name="scene">la scene</param>
    public void OnClick(string scene)
    {
        OnSceneChange?.Invoke(scene);
    }
}
