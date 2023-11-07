using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Affiche les informations dans l'interface de la page du tableau de classement
/// </summary>
public class DisplayController : MonoBehaviour
{
    /// <summary>
    /// Le score
    /// </summary>
    private int score = 0;
    /// <summary>
    /// La difficulté
    /// </summary>
    private float difficulty = 0.0f;
    /// <summary>
    /// Le score final
    /// </summary>
    private float finalScore = 0;
    /// <summary>
    /// Le nom du joueur
    /// </summary>
    private string playerName = "";
    /// <summary>
    /// Les composantes de texte pour afficher le score
    /// </summary>
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textDifficultyMultiplier;
    [SerializeField] private TextMeshProUGUI textFinalScore;
    /// <summary>
    /// Les composantes de texte pour afficher le tableau de classement
    /// </summary>
    [SerializeField] private TextMeshProUGUI[] leaderboardName;
    [SerializeField] private TextMeshProUGUI[] leaderboardScore;

    /// <summary>
    /// Affiche les informations de la partie lorsque la page de fin est affichée
    /// </summary>
    void Start()
    {
        int difficultyInt = (int)difficulty;
        float difficultyMultiplier = 1.0f + (float)difficultyInt / 100;
        finalScore = score * difficultyMultiplier;

        textScore.text = score.ToString() + "pts";
        textDifficultyMultiplier.text = difficultyMultiplier.ToString();
        textFinalScore.text = finalScore.ToString() + "pts";
        for (int i = 0; i < leaderboardName.Length; i++)
        {
            leaderboardName[i].text = (i+1).ToString() + ". ";
            leaderboardScore[i].text = "";
        }
    }

    /// <summary>
    /// Change le nom du joueur
    /// </summary>
    /// <param name="name">le nouveau nom</param>
    public void ChangeName(string name)
    {
        this.playerName = name;
    }

    /// <summary>
    /// Affiche le nom et le score du joueur dans le tableau de classement
    /// </summary>
    public void SaveScore()
    {
        leaderboardName[0].text = "1. " + playerName;
        leaderboardScore[0].text = finalScore.ToString() + "pts";
    }

    /// <summary>
    /// Lorsque le script est activé, va chercher le score et la difficulté dans les PlayerPrefs
    /// </summary>
    void OnEnable()
    {
        score = PlayerPrefs.GetInt("score");
        difficulty = PlayerPrefs.GetFloat("difficulty");
    }
}
