using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// G�re la m�canique du mulitplicateur de score
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
    /// D�marre la coroutine et la stock dans un attribut de la classe
    /// </summary>
    public void StartCoroutineScoreMultiplier()
    {
        coroutineScoreMultiplier = StartCoroutine(nameof(CoroutineScoreMultiplier));
    }

    /// <summary>
    /// Arr�te la coroutine
    /// </summary>
    public void StopCoroutineScoreMultiplier()
    {
        StopCoroutine(coroutineScoreMultiplier);
    }

    /// <summary>
    /// Coroutine qui g�re l'avancement du multiplicateur de score
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineScoreMultiplier()
    {
        float tempsEcoule = 0.0f;
        UpdateSlider(0);

        // Ex�cuter la boucle tant que la r�paration n'est pas compl�t�e
        while (tempsEcoule < tempsMultiplicateurScore)
        {
            tempsEcoule += Time.deltaTime;
            float pourcentageAvancement = tempsEcoule / tempsMultiplicateurScore * 100;
            UpdateSlider(pourcentageAvancement);

            // Instruction indiquant � Unity d'attendre le prochain passage de la boucle de jeu 
            // avant de continuer l'ex�cution de la m�thode
            yield return null;
        }
        // Op�rations ex�cut�es apr�s la compl�tion
        ScoreMultiplierIncrease();
    }

    /// <summary>
    /// Met � jour le slider de multiplicateur de score
    /// </summary>
    /// <param name="percentage">le pourcentage d'avancement du slider</param>
    private void UpdateSlider(float percentage)
    {
        sliderScoreMultiplier.value = percentage;
    }

    /// <summary>
    /// Les actions � faire lorsque le multiplicateur de score augmente
    /// </summary>
    private void ScoreMultiplierIncrease()
    {
        animationController.SetTrigger("ScoreMultiplierIncrease");
        GameController.Instance.ChangeScoreMultiplier(1);
        StartCoroutineScoreMultiplier();
    }

    /// <summary>
    /// Les actions � faire lorsque le multiplicateur de score diminue
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
    /// G�re l'�venement de changement de multiplicateur de score
    /// </summary>
    /// <param name="functionName">la fonction � lancer</param>
    public void ScoreMultiplierChange(string functionName)
    {
        Invoke(functionName, 0.0f);
    }
}
