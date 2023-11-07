using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère la mécanique du mulitplicateur de score
/// </summary>
[RequireComponent(typeof(Slider))]
public class ScoreMultiplier : MonoBehaviour
{
    /// <summary>
    /// Le controleur d'animation
    /// </summary>
    private Animator animationController;
    /// <summary>
    /// Le slider de multiplicateur de score
    /// </summary>
    [SerializeField] private Slider sliderScoreMultiplier;
    /// <summary>
    /// La coroutine de multiplicateur de score
    /// </summary>
    private Coroutine coroutineScoreMultiplier;
    /// <summary>
    /// Le temps pour monter de multiplicateur de score
    /// </summary>
    [SerializeField] private float tempsMultiplicateurScore = 10.0f;

    /// <summary>
    /// Va chercher la controleur d'animation du multiplicateur de score
    /// </summary>
    private void Awake()
    {
        animationController = GetComponent<Animator>();
    }

    /// <summary>
    /// Commence la coroutine du multiplicateur de score lors de l'initialisation de l'objet
    /// </summary>
    private void Start()
    {
        StartCoroutineScoreMultiplier();
    }

    /// <summary>
    /// Démarre la coroutine et la stock dans un attribut de la classe
    /// </summary>
    public void StartCoroutineScoreMultiplier()
    {
        coroutineScoreMultiplier = StartCoroutine(nameof(CoroutineScoreMultiplier));
    }

    /// <summary>
    /// Arrête la coroutine
    /// </summary>
    public void StopCoroutineScoreMultiplier()
    {
        StopCoroutine(coroutineScoreMultiplier);
    }

    /// <summary>
    /// Coroutine qui gère l'avancement du multiplicateur de score
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineScoreMultiplier()
    {
        float tempsEcoule = 0.0f;
        UpdateSlider(0);

        // Exécuter la boucle tant que la réparation n'est pas complétée
        while (tempsEcoule < tempsMultiplicateurScore)
        {
            tempsEcoule += Time.deltaTime;
            float pourcentageAvancement = tempsEcoule / tempsMultiplicateurScore * 100;
            UpdateSlider(pourcentageAvancement);

            // Instruction indiquant à Unity d'attendre le prochain passage de la boucle de jeu 
            // avant de continuer l'exécution de la méthode
            yield return null;
        }
        // Opérations exécutées après la complétion
        ScoreMultiplierIncrease();
    }

    /// <summary>
    /// Met à jour le slider de multiplicateur de score
    /// </summary>
    /// <param name="percentage">le pourcentage d'avancement du slider</param>
    private void UpdateSlider(float percentage)
    {
        sliderScoreMultiplier.value = percentage;
    }

    /// <summary>
    /// Les actions à faire lorsque le multiplicateur de score augmente
    /// </summary>
    private void ScoreMultiplierIncrease()
    {
        animationController.SetTrigger("ScoreMultiplierIncrease");
        GameController.Instance.ChangeScoreMultiplier(1);
        StartCoroutineScoreMultiplier();
    }

    /// <summary>
    /// Les actions à faire lorsque le multiplicateur de score diminue
    /// </summary>
    public void ScoreMultiplierDecrease()
    {
        StopCoroutineScoreMultiplier();
        if (GameController.Instance.ScoreMultiplier > 1)
        {
            GameController.Instance.ChangeScoreMultiplier(-1);
        }
        StartCoroutineScoreMultiplier();
        animationController.SetTrigger("ScoreMultiplierDecrease");
    }

    /// <summary>
    /// Gère l'évenement de changement de multiplicateur de score
    /// </summary>
    /// <param name="functionName">la fonction à lancer</param>
    public void ScoreMultiplierChange(string functionName)
    {
        Invoke(functionName, 0.0f);
    }
}
