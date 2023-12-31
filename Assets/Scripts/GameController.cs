using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// S'occupe du fonctionnement de la partie
/// </summary>
public class GameController : SceneController
{
    /// <summary>
    /// Le singleton
    /// </summary>
    public static GameController Instance { get; private set; }
    /// <summary>
    /// Le score
    /// </summary>
    private int score = 0;
    /// <summary>
    /// Le multiplicateur de score
    /// </summary>
    public int ScoreMultiplier { get; private set; } = 1;
    /// <summary>
    /// Les points de vie du joueur
    /// </summary>
    private float hp;
    /// <summary>
    /// Les composantes de texte du score et du multiplicateur de score
    /// </summary>
    [SerializeField] private TextFormatter textScore;
    [SerializeField] private TextFormatter textScoreMultiplier;
    /// <summary>
    /// Le slider de la barre de vie
    /// </summary>
    [SerializeField] private Slider healthBar;
    /// <summary>
    /// Les points de vie maximale du joueur
    /// </summary>
    [SerializeField] private float maxHp = 100.0f;
    /// <summary>
    /// Le nombre de points de vie gagn� par seconde
    /// </summary>
    [SerializeField] private float hpRecoveredPerSec = 5.0f;
    /// <summary>
    /// Le temps entre les sons de zombies
    /// </summary>
    [SerializeField] private float timeBetweenSounds = 6.0f;
    /// <summary>
    /// Un tableau de sons de zombies
    /// </summary>
    [SerializeField] private AudioSource[] zombieSounds;

    /// <summary>
    /// Instancier le Singleton et mettre les valeurs de base
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        score = 0;
        ScoreMultiplier = 1;
        hp = maxHp;
    }

    /// <summary>
    /// Afficher les informations au joueur
    /// </summary>
    private void Start()
    {
        textScore.ChangeValue(this.score);
        textScoreMultiplier.ChangeValue(this.ScoreMultiplier);
        healthBar.value = hp;
        StartCoroutineZombieSound();
    }

    /// <summary>
    /// Remonte la vie du joueur � chaque frame si la vie est superieure � 0
    /// </summary>
    private void Update()
    {
        if (hp <= 0)
        {
            ChangeScene("Leaderboard");
        }
        hp += hpRecoveredPerSec * Time.deltaTime;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        healthBar.value = hp;
    }

    /// <summary>
    /// Commence la coroutine de sons de zombies
    /// </summary>
    public void StartCoroutineZombieSound()
    {
        StartCoroutine(nameof(CoroutineZombieSound));
    }

    /// <summary>
    /// Lance un son de zombie lorsque la coroutine est termin�e
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineZombieSound()
    {
        // Attend ce nombre de secondes dans le jeu (�chelle du temps de jeu)
        yield return new WaitForSeconds(timeBetweenSounds);

        ZombieSound();
        StartCoroutineZombieSound();
    }

    /// <summary>
    /// Lance un son al�atoire dans le tableau de sons
    /// </summary>
    private void ZombieSound()
    {
        int randomSoundIndex = Random.Range(0, zombieSounds.Length);
        zombieSounds[randomSoundIndex].Play();
    }

    /// <summary>
    /// Ajoute des points de score
    /// </summary>
    public void AddScore()
    {
        this.score += ScoreMultiplier * 100;
        textScore.ChangeValue(this.score);
    }

    /// <summary>
    /// Le joueur re�oit des d�gats
    /// </summary>
    /// <param name="damage">la quantite de degats recus</param>
    public void ReceiveDamage(float damage)
    {
        hp -= damage;
    }

    /// <summary>
    /// Change le multiplicateur de score
    /// </summary>
    /// <param name="scoreMultiplier">le multiplicateur de score � ajouter</param>
    public void ChangeScoreMultiplier(int scoreMultiplier = 1)
    {
        this.ScoreMultiplier += scoreMultiplier;
        this.textScoreMultiplier.ChangeValue(this.ScoreMultiplier);
    }

    /// <summary>
    /// Lorsque le script est desactiv�, enregistre le score dans les PlayerPrefs
    /// </summary>
    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
    }
}
