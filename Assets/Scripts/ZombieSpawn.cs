using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// S'occupe de la m�canique de spawn des zombies
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
    /// Le GameObject � initialiser
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
    /// Les param�tres d'augmentation de vitesse de spawn
    /// </summary>
    [SerializeField] private float maxSpawnIncreasePerSec = 1.0f;
    [SerializeField] private float minSpawnIncreasePerSec = 0.5f;
    private float spawnIncreasePerSec;
    /// <summary>
    /// Les param�tres de vitesse de zombie
    /// </summary>
    [SerializeField] private float maxZombieSpeed = 10.0f;
    [SerializeField] private float minZombieSpeed = 4.0f;
    private float zombieSpeed;
    /// <summary>
    /// Les param�tres de d�gats des zombies
    /// </summary>
    [SerializeField] private float maxDamage = 15.0f;
    [SerializeField] private float minDamage = 5.0f;
    private float damage;
    /// <summary>
    /// Le point de spawn pour la nouvelle instance
    /// </summary>
    private Vector3 spawnPoint;

    /// <summary>
    /// D�marre le processus de spawn lors de l'initialisation de l'objet
    /// </summary>
    void Start()
    {
        StartCoroutineSpawn();
    }

    /// <summary>
    /// Augmente la vitesse de spawn � chaque frame
    /// </summary>
    void Update()
    {
        spawnPerMin += spawnIncreasePerSec * Time.deltaTime;
    }

    /// <summary>
    /// G�n�re un nouveau point de spawn et g�n�re le zombie � cette position
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
    /// G�n�re un point de spawn 
    /// </summary>
    /// <returns>vrai si un point de spawn valide a ete trouve, faux sinon</returns>
    private bool GenerateRandomSpawnPoint()
    {
        bool spawnPointFound = false;
        spawnPoint = GenerateRandomPoint();
        // Le point doit �tre dans un rayon plus grand que 7 autour de la voiture
        if (Vector3.Distance(spawnPoint, car.transform.position) > 7)
        {
            spawnPointFound = true;
        }
        // V�rifie si le point de spawn est � l'interieur d'un batiment
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
    /// Cr�e la nouvelle instance de zombie
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
    /// Recup�re la difficult� de la partie pour determiner les differents param�tres du jeu.
    /// Se fait lorsque le script est initialis�
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
        // Attend ce nombre de secondes dans le jeu (�chelle du temps de jeu)
        yield return new WaitForSeconds(60/spawnPerMin);

        Spawn();
        StartCoroutineSpawn();
    }

    // code inspire de ce site https://hamy.xyz/labs/unity-how-to-find-a-random-point-within-a-collider-mesh
    /// <summary>
    /// G�n�re un point al�atoire sur la carte
    /// </summary>
    /// <returns>un point al�atoire sur la carte</returns>
    public Vector3 GenerateRandomPoint()
    {
        float minX = map.bounds.size.x * -0.5f;
        float minZ = map.bounds.size.z * -0.5f;

        return new Vector3(Random.Range(minX, -minX), 1.25f, Random.Range(minZ, -minZ)) + new Vector3(-4.7f, 0, -4.9f);
    }
}
