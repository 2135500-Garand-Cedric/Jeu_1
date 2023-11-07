using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ajoute des méthodes à la composante Slider
/// </summary>
[RequireComponent(typeof(Slider))]
public class SliderExtension : MonoBehaviour
{
    /// <summary>
    /// L'attribut qui stocke la difficulté
    /// </summary>
    private float difficulty = 50.0f;
    /// <summary>
    /// Le slider
    /// </summary>
    private Slider slider;

    /// <summary>
    /// Va chercher le Slider
    /// </summary>
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    /// <summary>
    /// Enregistre la valeur du Slider lorsqu'elle change
    /// </summary>
    public void ChangeDifficulty()
    {
        difficulty = slider.value;
    }

    /// <summary>
    /// Lorsque le script est desactivé, enregistre la difficulté dans les PlayerPrefs
    /// </summary>
    void OnDisable()
    {
        PlayerPrefs.SetFloat("difficulty", difficulty);
    }
}
