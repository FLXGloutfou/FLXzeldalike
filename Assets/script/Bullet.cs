using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // D�g�ts inflig�s par la balle

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifier si la balle entre en collision avec un ennemi
        if (other.CompareTag("Enemy"))
        {
            // Appliquer des d�g�ts � l'ennemi
            other.GetComponent<Enemy>().TakeDamage(damage);

            // D�truire la balle lorsqu'elle touche un ennemi
            Destroy(gameObject);
        }
    }
}

