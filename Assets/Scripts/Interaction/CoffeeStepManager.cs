using UnityEngine;

public class CoffeeStepManager : MonoBehaviour
{
    public static CoffeeStepManager Instance;

    private bool findCoffeeMAchineMessage = false;
    public enum Step
    {
        Dialog,
        GrabCup,
        PlaceCup,
        BrewCoffee,
        GrabLid,
        //Assemble,
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
        switch (currentStep)
        {
            case Step.GrabCup:
                NotificationSystem.Instance.ShowMessage("���� ����� ������� ��� ����...", 2.5f);
                break;
            case Step.PlaceCup:
                {
                    if (!findCoffeeMAchineMessage)
                    {
                        NotificationSystem.Instance.ShowMessage("��� ��� ���! ������ ����� ��������� ��� � ���� ������.", 2.5f);
                        findCoffeeMAchineMessage = true;
                    }
                }
                break;
            case Step.BrewCoffee:

                break;
            case Step.GrabLid:
                NotificationSystem.Instance.ShowMessage("������ ��� ����� ������ ��� ����������.", 2.5f);

                break;
            case Step.ServeToNPC:
                NotificationSystem.Instance.ShowMessage("���� �����, ���� ��� ������ ����������.", 2.5f);

                break;
            default:
                break;
        }
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
