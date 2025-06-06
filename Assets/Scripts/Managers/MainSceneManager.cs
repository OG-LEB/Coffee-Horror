using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private Transform PlayerSpawnPoint;
    [Header("Effects")]
    [SerializeField] private GameObject DarkCameraEffects, FriendlyCameraEffects;
    [SerializeField] private GameObject EnterShopTrigger, ExitShopTrigger;
    [Header("Sounds")]
    [SerializeField] private AudioSource ambient;
    [SerializeField] private AudioClip dark, coffeeStore;
    [SerializeField] private AudioSource glitch;


    private void Start()
    {
        SpawnPlayer();
        DarkCameraEffects.SetActive(true);
        FriendlyCameraEffects.SetActive(false);
        EnterShopTrigger.SetActive(true);
        ExitShopTrigger.SetActive(false);
        NotificationSystem.Instance.ShowMessage("Какое мрачное место. Надо осмотреться...", 5f);

        ambient.loop = true;
        ambient.clip = dark;
        ambient.Play();
    }
    private void SpawnPlayer() 
    {
        PlayerObj.transform.position = PlayerSpawnPoint.position;
    }
    public void SwitchToDarkCameraEffects() 
    {
        glitch.Play();
        DarkCameraEffects.SetActive(true);
        FriendlyCameraEffects.SetActive(false);
        EnterShopTrigger.SetActive(true);
        ExitShopTrigger.SetActive(false);
        ambient.clip = dark;
        ambient.Play();
    }
    public void SwitchToFriendlyCameraEffects()
    {
        glitch.Play();
        DarkCameraEffects.SetActive(false);
        FriendlyCameraEffects.SetActive(true);
        EnterShopTrigger.SetActive(false);
        ExitShopTrigger.SetActive(true);
        ambient.clip = coffeeStore;
        ambient.Play();
    }

    public void Monologue(string message) 
    {
        NotificationSystem.Instance.ShowMessage(message, 5f);
    }
}
