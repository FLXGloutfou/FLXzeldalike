using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public Transform target; // Le transform du personnage principal � suivre
    public float speed = 5f; // Vitesse de d�placement du compagnon

    void Update()
    {
        if (target != null)
        {
            // Calculer la direction vers le personnage principal
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // D�placer le compagnon vers le personnage principal
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
