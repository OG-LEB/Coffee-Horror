using UnityEngine;

public class CupPlacementZone : MonoBehaviour
{
    public Transform placementPosition; // �������, ���� ������ ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoffeeCup"))
        {
            CoffeeCup cup = other.GetComponent<CoffeeCup>();
            if (cup != null && CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.PlaceCup))
            {
                cup.AutoPlace(placementPosition);
            }
        }
    }
}
