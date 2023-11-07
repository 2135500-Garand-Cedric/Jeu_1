using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// S'occupe de gèrer le mouvement du zombie
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
    /// Les dégats par seconde du zombie
    /// </summary>
    private float dps = 5.0f;
    /// <summary>
    /// La quantité de dégats lorsque le zombie entre en collision avec la voiture
    /// </summary>
    public float DamageOnCollision { get; private set; } = 10.0f; 

    /// <summary>
    /// Deplace le zombie vers la voiture à chaque frame de jeu
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
    /// Set la quantité de dégats du zombie
    /// </summary>
    /// <param name="damage">la quantité de dégats</param>
    public void SetDamage(float damage)
    {
        this.DamageOnCollision = damage;
        this.dps = damage / 2;
    }

    /// <summary>
    /// Lorsque le zombie reste en contact avec un objet
    /// </summary>
    /// <param name="other">l'objet qui est entré en collision avec le zombie</param>
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject == car)
        {
            GameController.Instance.ReceiveDamage(dps * Time.deltaTime);
        }
    }
}

