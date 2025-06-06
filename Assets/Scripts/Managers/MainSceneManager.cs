using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject PlayerObj;
    [SerializeField] private Transform PlayerSpawnPoint;
    [Header("Effects")]
    [SerializeField] private GameObject DarkCameraEffects, FriendlyCameraEffects;
    [SerializeField] private GameObject EnterShopTrigger, ExitShopTrigger;

    private void Start()
    {
        SpawnPlayer();
        DarkCameraEffects.SetActive(true);
        FriendlyCameraEffects.SetActive(false);
        EnterShopTrigger.SetActive(true);
        ExitShopTrigger.SetActive(false);
    }
    private void SpawnPlayer() 
    {
        PlayerObj.transform.position = PlayerSpawnPoint.position;
    }
    public void SwitchToDarkCameraEffects() 
    {
        DarkCameraEffects.SetActive(true);
        FriendlyCameraEffects.SetActive(false);
        EnterShopTrigger.SetActive(true);
        ExitShopTrigger.SetActive(false);
    }
    public void SwitchToFriendlyCameraEffects()
    {
        DarkCameraEffects.SetActive(false);
        FriendlyCameraEffects.SetActive(true);
        EnterShopTrigger.SetActive(false);
        ExitShopTrigger.SetActive(true);
    }
}
