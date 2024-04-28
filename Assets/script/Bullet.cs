using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Dégâts infligés par la balle

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si la balle entre en collision avec un ennemi
        if (other.CompareTag("Enemy"))
        {
            // Appliquer des dégâts à l'ennemi
            other.GetComponent<Enemy>().TakeDamage(damage);

            // Détruire la balle lorsqu'elle touche un ennemi
            Destroy(gameObject);
        }
    }
}

