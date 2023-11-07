using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// le code de cette classe a �t� pris de ce lien https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#634f8281edbc2a65c86270c7
/// <summary>
/// S'occupe de garder la m�me instance de musique dans toutes les sc�nes
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
