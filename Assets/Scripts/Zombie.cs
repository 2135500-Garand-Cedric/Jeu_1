using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// S'occupe de gerer le mouvement du zombie
/// </summary>
public class Zombie : MonoBehaviour
{
    /// <summary>
    /// La voiture
    /// </summary>
    private GameObject car;
    /// <summary>
    /// La vitesse de deplacement du zombie
    /// </summary>
    [SerializeField] private float moveSpeed = 2.0f;
    /// <summary>
    /// Les degats par seconde du zombie
    /// </summary>
    private float dps = 5.0f;
    /// <summary>
    /// La quantite de degats lorsque le zombie entre en collision avec la voiture
    /// </summary>
    public float DamageOnCollision { get; private set; } = 10.0f; 

    /// <summary>
    /// Deplace le zombie vers la voiture a chaque frame de jeu
    /// </summary>
    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, car.transform.position, step);
        transform.LookAt(car.transform.position);
    }

    /// <summary>
    /// Set la voiture
    /// </summary>
    /// <param name="car">La voiture</param>
    public void SetCar(GameObject car)
    {
        this.car = car;
    }

    /// <summary>
    /// Set la vitesse de mouvement du zombie
    /// </summary>
    /// <param name="moveSpeed">la vitesse de mouvement</param>
    public void SetSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    /// <summary>
    /// Set la quantite de degats du zombie
    /// </summary>
    /// <param name="damage">la quantite de degats</param>
    public void SetDamage(float damage)
    {
        this.DamageOnCollision = damage;
        this.dps = damage / 2;
    }

    /// <summary>
    /// Lorsque le zombie reste en contact avec un objet
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == car)
        {
            GameController.Instance.ReceiveDamage(dps * Time.deltaTime);
        }
    }
}

