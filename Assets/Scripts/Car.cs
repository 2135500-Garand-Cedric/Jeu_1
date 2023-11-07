using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// S'occupe du mouvement et des collisions de la voiture
/// </summary>
public class Car : InfoChanger
{
    /// <summary>
    /// La vitesse de la voiture
    /// </summary>
    [SerializeField] private float speed = 2.0f;
    /// <summary>
    /// La vitesse de rotation de la voiture
    /// </summary>
    [SerializeField] private float rotationSpeed = 45.0f;
    /// <summary>
    /// La position de la voiture
    /// </summary>
    public Vector3 position = Vector3.zero;
    
    /// <summary>
    /// S'occupe du déplacement de la voiture
    /// </summary>
    void Update()
    {
        // se deplacer
        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody>().transform.position += speed * Time.deltaTime * this.GetComponent<Rigidbody>().transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody>().transform.position += speed * Time.deltaTime * -this.GetComponent<Rigidbody>().transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody>().transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody>().transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        this.position = this.GetComponent<Rigidbody>().transform.position;
    }

    /// <summary>
    /// S'occupe de gèrer lorsque la voiture entre en collision avec un autre objet
    /// </summary>
    /// <param name="other">L'objet qui est entré en collision avec la voiture</param>
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            bool zombieKill = false;
            float addedVector = -0.6f;
            // Lance 5 rays devant la voiture et détecte si un zombie a été touché
            while (addedVector <= 0.6f)
            {
                Vector3 start = this.GetComponent<Rigidbody>().transform.position + this.GetComponent<Rigidbody>().transform.right * addedVector;
                Vector3 end = this.GetComponent<Rigidbody>().transform.forward * 10;
                end.y = 1.0f;
                start.y = 1.0f;
                bool kill = RayTest(new Ray(start, end), other);
                if (kill)
                {
                    zombieKill = true;
                    break;
                }
                addedVector += 0.3f;
            }
            if (zombieKill)
            {
                GameController.Instance.AddScore();
            }
            else
            {
                ScoreMultiplierChange("ScoreMultiplierDecrease");
                GameController.Instance.ReceiveDamage(other.gameObject.GetComponent<Zombie>().DamageOnCollision);
            }
        }
    }

    /// <summary>
    /// S'occupe de gèrer ce que le Ray a touché lorsque la voiture entre en collision
    /// </summary>
    /// <param name="ray">Le Ray lancé</param>
    /// <param name="other">L'objet qui est entré en collision avec la voiture</param>
    /// <returns></returns>
    private bool RayTest(Ray ray, Collision other)
    {
        bool zombieKill = false;
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData))
        {
            if (hitData.collider.gameObject.layer == LayerMask.NameToLayer("Zombie"))
            {
                Destroy(other.gameObject);
                zombieKill = true;
            }
        }
        return zombieKill;
    }

    /// <summary>
    /// Lance l'evenement pour avertir d'un changement de multiplicateur de score
    /// </summary>
    /// <param name="functionName">La fonction a appeler</param>
    public void ScoreMultiplierChange(string functionName)
    {
        OnScoreMultiplierChange?.Invoke(functionName);
    }
}
