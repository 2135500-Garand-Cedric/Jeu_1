using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// le code de cette classe a ete pris de ce lien https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#634f8281edbc2a65c86270c7
/// <summary>
/// S'occupe de garder la meme instance de musique dans toutes les scenes
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
