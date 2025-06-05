using UnityEngine;

public class CapTrigger : MonoBehaviour
{
    [SerializeField] private CoffeeCup cup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cap"))
        {
            cup.EnableCap();
            CoffeeStepManager.Instance.AdvanceStep();
            Destroy(other.gameObject);
        }
    }
}
