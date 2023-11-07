using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Formatteur de texte
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFormatter : MonoBehaviour
{
    private string textModel;
    void Awake()
    {
        // Récupère le modèle de texte du composant
        textModel = GetComponent<TextMeshProUGUI>().text;
    }

    /// <summary>
    /// Change les deux dièses par un valeur
    /// </summary>
    /// <param name="value">la valeur à mettre</param>
    public void ChangeValue(int value)
    {
        GetComponent<TextMeshProUGUI>().text = textModel.Replace("##", value.ToString());
    }
}
