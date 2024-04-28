using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Pr�fabriqu� du personnage
    private GameObject playerInstance; // Instance actuelle du personnage
    private static bool playerSpawned = false; // Indique si le personnage a d�j� �t� instanci�

    private void Start()
    {
        // V�rifier si le personnage n'a pas d�j� �t� instanci�
        if (!playerSpawned)
        {
            // Instancier le personnage depuis le prefab
            playerInstance = Instantiate(playerPrefab);

            // Mettre � jour le flag pour indiquer que le personnage a �t� instanci�
            playerSpawned = true;
            Debug.Log("personnage true");

            // Emp�cher la destruction de ce GameObject lors du chargement d'une nouvelle sc�ne
            DontDestroyOnLoad(gameObject);
        }
    }
}
