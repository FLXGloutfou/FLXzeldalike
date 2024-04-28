using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Préfabriqué du personnage
    private GameObject playerInstance; // Instance actuelle du personnage
    private static bool playerSpawned = false; // Indique si le personnage a déjà été instancié

    private void Start()
    {
        // Vérifier si le personnage n'a pas déjà été instancié
        if (!playerSpawned)
        {
            // Instancier le personnage depuis le prefab
            playerInstance = Instantiate(playerPrefab);

            // Mettre à jour le flag pour indiquer que le personnage a été instancié
            playerSpawned = true;
            Debug.Log("personnage true");

            // Empêcher la destruction de ce GameObject lors du chargement d'une nouvelle scène
            DontDestroyOnLoad(gameObject);
        }
    }
}
