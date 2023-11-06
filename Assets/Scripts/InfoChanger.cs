using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Les evenements
/// </summary>
public class InfoChanger : MonoBehaviour
{
    [SerializeField] protected UnityEvent<string> OnSceneChange;

    [SerializeField] protected UnityEvent<string> OnScoreMultiplierChange;
}
