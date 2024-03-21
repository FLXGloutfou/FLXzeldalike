using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public Transform target; // Le transform du personnage principal à suivre
    public float speed = 5f; // Vitesse de déplacement du compagnon

    void Update()
    {
        if (target != null)
        {
            // Calculer la direction vers le personnage principal
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // Déplacer le compagnon vers le personnage principal
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
