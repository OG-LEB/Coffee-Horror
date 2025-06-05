using UnityEngine;

public class CoffeeStepManager : MonoBehaviour
{
    public static CoffeeStepManager Instance;

    public enum Step
    {
        GrabCup,
        PlaceCup,
        PourCoffee,
        GrabLid,
        Assemble,
        ServeToNPC
    }

    public Step currentStep = Step.GrabCup;

    private void Awake()
    {
        Instance = this;
    }

    public void AdvanceStep()
    {
        currentStep++;
        Debug.Log("��������� ���: " + currentStep);
    }
    public void ReverceStep() 
    {
        currentStep--;
        Debug.Log("�����! ��������� ���: " + currentStep);

    }

    public bool IsCurrentStep(Step step)
    {
        return currentStep == step;
    }
}
