using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 1;


    public delegate void OnCollectEvent(int collectedPoints);
    public static event OnCollectEvent OnCollect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur a collect� l'objet
            Collect();
        }
    }

    void Collect()
    {


        OnCollect?.Invoke(points);

        Destroy(gameObject);
    }
}
