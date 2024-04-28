using UnityEngine;
using Rewired;
using Unity.VisualScripting;

public class Helico : MonoBehaviour
{
    public GameObject player;
    public Transform playerExitPoint;
    public Transform playerSitPoint;
    public GameObject vehicle;
    public GameObject playerModel;
    public GameObject pet;
    public float vehicleMoveSpeed;
    public float interactionDistance;

    private bool isPlayerInside = false;
    private Player rewiredPlayer;

    private PlayerControl PlayerControl;
    public bool useController = true; // Paramètre pour contrôler l'utilisation du contrôleur
    private InventoryManager inventoryManager;

    void Start()
    {
        PlayerControl = GameObject.FindObjectOfType<PlayerControl>();
        rewiredPlayer = ReInput.players.GetPlayer(0);
        inventoryManager = GameObject.Find("inventory").GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, vehicle.transform.position) <= interactionDistance && rewiredPlayer.GetButtonDown("Interact"))
        {
            // Vérifie si le joueur a au moins un fuel dans son inventaire
            if (inventoryManager.HasFuel())
            {
                if (!isPlayerInside)
                    EnterVehicle();
                else
                    ExitVehicle();
            }
            else
            {
                Debug.Log("You need at least one fuel to enter the helicopter!");
            }
        }

        if (isPlayerInside)
        {
            if (PlayerControl.useController)
            {
                ProcessControllerInputs();
            }
            else
            {
                ProcessKeyboardInputs();
            }
        }

    }

    void ProcessControllerInputs()
    {
        // Contrôles du véhicule avec le contrôleur
        float horizontalInput = rewiredPlayer.GetAxis("MoveHorizontal");
        float verticalInput = rewiredPlayer.GetAxis("MoveVertical");

        // Appliquer les mouvements au véhicule
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;
        vehicle.transform.position += movement * Time.deltaTime * vehicleMoveSpeed;
    }

    void ProcessKeyboardInputs()
    {
        // Contrôles du véhicule avec le clavier
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Appliquer les mouvements au véhicule
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;
        vehicle.transform.position += movement * Time.deltaTime * vehicleMoveSpeed;
    }

    void EnterVehicle()
    {
        pet.SetActive(false);
        playerModel.SetActive(false);
        player.transform.position = playerSitPoint.position;
        player.transform.parent = vehicle.transform;
        isPlayerInside = true;
    }

    void ExitVehicle()
    {
        pet.SetActive(true);
        playerModel.SetActive(true);
        player.transform.parent = null;
        player.transform.position = playerExitPoint.position;
        isPlayerInside = false;
    }
}
