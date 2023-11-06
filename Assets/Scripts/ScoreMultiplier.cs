using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gere la mecanique du mulitplicateur de score
/// </summary>
[RequireComponent(typeof(Slider))]
public class ScoreMultiplier : MonoBehaviour
{
    /// <summary>
    /// Le controleur d'animation
    /// </summary>
    private Animator animationController;

    [SerializeField] private Slider sliderScoreMultiplier;

    private Coroutine coroutineScoreMultiplier;

    private float tempsMultiplicateurScore = 10.0f;

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
    /// Demarre la coroutine et la stock dans un attribu de la classe
    /// </summary>
    public void StartCoroutineScoreMultiplier()
    {
        coroutineScoreMultiplier = StartCoroutine(nameof(CoroutineScoreMultiplier));
    }

    /// <summary>
    /// Arrete la coroutine
    /// </summary>
    public void StopCoroutineScoreMultiplier()
    {
        StopCoroutine(coroutineScoreMultiplier);
    }

    /// <summary>
    /// Coroutine qui gere l'avancement du multiplicateur de score
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
    /// Met a jour le slider de multiplicateur de score
    /// </summary>
    /// <param name="percentage">le pourcentage d'avancement du slider</param>
    private void UpdateSlider(float percentage)
    {
        sliderScoreMultiplier.value = percentage;
    }

    /// <summary>
    /// Les actions a faire lorsque le multiplicateur de score augmente
    /// </summary>
    private void ScoreMultiplierIncrease()
    {
        animationController.SetTrigger("MultiplicateurScoreAugmente");
        GameController.Instance.ChangeScoreMultiplier(1);
        StartCoroutineScoreMultiplier();
    }

    /// <summary>
    /// Les actions a faire lorsque le multiplicateur de score diminue
    /// </summary>
    public void ScoreMultiplierDecrease()
    {
        StopCoroutineScoreMultiplier();
        if (GameController.Instance.ScoreMultiplier > 1)
        {
            GameController.Instance.ChangeScoreMultiplier(-1);
        }
        StartCoroutineScoreMultiplier();
        animationController.SetTrigger("MultiplicateurScoreDiminue");
    }

    /// <summary>
    /// Gere l'evenement de changement de multiplicateur de score
    /// </summary>
    /// <param name="functionName">la fonction a lancer</param>
    public void ScoreMultiplierChange(string functionName)
    {
        Invoke(functionName, 0.0f);
    }
}
