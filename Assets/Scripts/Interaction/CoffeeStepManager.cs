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
        Debug.Log("Следующий шаг: " + currentStep);
        switch (currentStep)
        {
            case Step.GrabCup:
                NotificationSystem.Instance.ShowMessage("Надо найти стаканы для кофе...", 2.5f);
                break;
            case Step.PlaceCup:
                {
                    if (!findCoffeeMAchineMessage)
                    {
                        NotificationSystem.Instance.ShowMessage("Вот они где! Теперь нужно поставить его в кофе машину.", 2.5f);
                        findCoffeeMAchineMessage = true;
                    }
                }
                break;
            case Step.BrewCoffee:

                break;
            case Step.GrabLid:
                NotificationSystem.Instance.ShowMessage("Теперь нам нужна крышка для стаканчика.", 2.5f);

                break;
            case Step.ServeToNPC:
                NotificationSystem.Instance.ShowMessage("Кофе готов, надо его отдать незнакомцу.", 2.5f);

                break;
            default:
                break;
        }
    }
    public void ReverceStep()
    {
        currentStep--;
        Debug.Log("Назад! Следующий шаг: " + currentStep);

    }

    public bool IsCurrentStep(Step step)
    {
        return currentStep == step;
    }
}
