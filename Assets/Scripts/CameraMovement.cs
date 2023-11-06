using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe du mouvement de la camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// La distance de la camera avec la voiture
    /// </summary>
    [SerializeField] private float cameraDistance = 10.0f;
    /// <summary>
    /// La voiture
    /// </summary>
    [SerializeField] private GameObject car;

    /// <summary>
    /// Fait en sorte que la camera soit toujours derriere la voiture
    /// </summary>
    private void LateUpdate()
    {
        transform.position = car.transform.position - car.transform.forward * cameraDistance + new Vector3(0, cameraDistance, 0);
        transform.LookAt(car.transform.position);
    }
}
