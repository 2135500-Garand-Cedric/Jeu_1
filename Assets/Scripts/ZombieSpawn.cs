using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// S'occupe de la mécanique de spawn des zombies
/// </summary>
public class ZombieSpawn : MonoBehaviour
{
    /// <summary>
    /// Les batiments sur la carte
    /// </summary>
    [SerializeField] private Collider[] buildings;
    /// <summary>
    /// Le collider de la carte
    /// </summary>
    [SerializeField] private Collider map;
    /// <summary>
    /// Le GameObject à initialiser
    /// </summary>
    [SerializeField] private GameObject prototype;
    /// <summary>
    /// La voiture
    /// </summary>
    [SerializeField] private GameObject car;
    /// <summary>
    /// La vitesse de spawn de base
    /// </summary>
    [SerializeField] private float spawnPerMin = 20.0f;
    /// <summary>
    /// Les paramètres d'augmentation de vitesse de spawn
    /// </summary>
    [SerializeField] private float maxSpawnIncreasePerSec = 1.0f;
    [SerializeField] private float minSpawnIncreasePerSec = 0.5f;
    private float spawnIncreasePerSec;
    /// <summary>
    /// Les paramètres de vitesse de zombie
    /// </summary>
    [SerializeField] private float maxZombieSpeed = 10.0f;
    [SerializeField] private float minZombieSpeed = 4.0f;
    private float zombieSpeed;
    /// <summary>
    /// Les paramètres de dégats des zombies
    /// </summary>
    [SerializeField] private float maxDamage = 15.0f;
    [SerializeField] private float minDamage = 5.0f;
    private float damage;
    /// <summary>
    /// Le point de spawn pour la nouvelle instance
    /// </summary>
    private Vector3 spawnPoint;

    /// <summary>
    /// Démarre le processus de spawn lors de l'initialisation de l'objet
    /// </summary>
    void Start()
    {
        StartCoroutineSpawn();
    }

    /// <summary>
    /// Augmente la vitesse de spawn à chaque frame
    /// </summary>
    void Update()
    {
        spawnPerMin += spawnIncreasePerSec * Time.deltaTime;
    }

    /// <summary>
    /// Génère un nouveau point de spawn et génère le zombie à cette position
    /// </summary>
    public void Spawn()
    {
        bool spawnPointFound = false;
        while (!spawnPointFound)
        {
            spawnPointFound = GenerateRandomSpawnPoint();
        }
        CreateInstance();
    }

    /// <summary>
    /// Génère un point de spawn 
    /// </summary>
    /// <returns>vrai si un point de spawn valide a ete trouve, faux sinon</returns>
    private bool GenerateRandomSpawnPoint()
    {
        bool spawnPointFound = false;
        spawnPoint = GenerateRandomPoint();
        // Le point doit être dans un rayon plus grand que 7 autour de la voiture
        if (Vector3.Distance(spawnPoint, car.transform.position) > 7)
        {
            spawnPointFound = true;
        }
        // Vérifie si le point de spawn est à l'interieur d'un batiment
        foreach (Collider collider in buildings)
        {
            if (collider.bounds.Contains(spawnPoint))
            {
                spawnPointFound = false;
                break;
            }
        }
        return spawnPointFound;
    }

    /// <summary>
    /// Crée la nouvelle instance de zombie
    /// </summary>
    private void CreateInstance()
    {
        GameObject newInstance = Instantiate(prototype);
        newInstance.transform.position = spawnPoint;
        newInstance.GetComponent<Zombie>().SetCar(car);
        newInstance.GetComponent<Zombie>().SetSpeed(zombieSpeed);
        newInstance.GetComponent<Zombie>().SetDamage(damage);
    }

    /// <summary>
    /// Recupère la difficulté de la partie pour determiner les differents paramètres du jeu.
    /// Se fait lorsque le script est initialisé
    /// </summary>
    void OnEnable()
    {
        float difficulty = PlayerPrefs.GetFloat("difficulte");
        zombieSpeed = minZombieSpeed + ((maxZombieSpeed - minZombieSpeed) * difficulty / 100);
        spawnIncreasePerSec = minSpawnIncreasePerSec + ((maxSpawnIncreasePerSec - minSpawnIncreasePerSec) * difficulty / 100);
        damage = minDamage + ((maxDamage - minDamage) * difficulty / 100);
    }

    /// <summary>
    /// Commencer la coroutine de spawn
    /// </summary>
    public void StartCoroutineSpawn()
    {
        StartCoroutine(nameof(CoroutineSpawn));
    }

    /// <summary>
    /// Coroutine qui attend un certain nombre de seconde puis lance la fonction de spawn
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineSpawn()
    {
        // Attend ce nombre de secondes dans le jeu (échelle du temps de jeu)
        yield return new WaitForSeconds(60/spawnPerMin);

        Spawn();
        StartCoroutineSpawn();
    }

    // code inspire de ce site https://hamy.xyz/labs/unity-how-to-find-a-random-point-within-a-collider-mesh
    /// <summary>
    /// Génère un point aléatoire sur la carte
    /// </summary>
    /// <returns>un point aléatoire sur la carte</returns>
    public Vector3 GenerateRandomPoint()
    {
        float minX = map.bounds.size.x * -0.5f;
        float minZ = map.bounds.size.z * -0.5f;

        return new Vector3(Random.Range(minX, -minX), 1.25f, Random.Range(minZ, -minZ)) + new Vector3(-4.7f, 0, -4.9f);
    }
}
