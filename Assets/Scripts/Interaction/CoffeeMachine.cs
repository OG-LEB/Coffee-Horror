using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CoffeeMachine : MonoBehaviour
{
    [Header("Settings")]
    public float brewTime = 3f;
    //public AudioSource pourSound;
    //public ParticleSystem steamEffect;
    //public Renderer coffeeLiquid; // Объект с материалом, который имитирует наполнение
    public Transform cupDetectionPoint;
    public float detectionRadius = 0.2f;

    private bool isBrewing = false;
    private float brewTimer = 0f;
    private CoffeeCup cup;
    [SerializeField] private AudioSource audioSource;

    [Header("Visual")]
    [SerializeField] private GameObject CoffeeFlow;
    
    private void Start()
    {
        CoffeeFlow.SetActive(false);
    }
    void Update()
    {
        if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.BrewCoffee) && !isBrewing)
        {
            Collider[] hits = Physics.OverlapSphere(cupDetectionPoint.position, detectionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("CoffeeCup"))
                {
                    cup = hit.GetComponent<CoffeeCup>();
                    StartBrewing();
                    break;
                }
            }
        }

        if (isBrewing)
        {
            brewTimer += Time.deltaTime;

            // Наполняем стакан визуально (например, меняем _FillAmount)
            float progress = Mathf.Clamp01(brewTimer / brewTime);
            //coffeeLiquid.material.SetFloat("_FillAmount", progress);
            //Debug.Log($"Coffee is brewing! {progress}%");
            cup.UpdateLiquidInCup(progress);
            if (brewTimer >= brewTime)
            {
                FinishBrewing();
                //Debug.Log("Coffee is ready!");
            }
        }
    }

    void StartBrewing()
    {
        CoffeeFlow.SetActive(true);
        isBrewing = true;
        brewTimer = 0f;
        //pourSound?.Play();
        //steamEffect?.Play();
        audioSource.Play();
        //Debug.Log("Start brewing coffee...");
    }

    void FinishBrewing()
    {
        CoffeeFlow.SetActive(false);
        isBrewing = false;
        //pourSound?.Stop();
        //steamEffect?.Stop();
        audioSource.Stop();
        cup.SetCapTrigger(true);
        CoffeeStepManager.Instance.AdvanceStep();
        cup.CanDrag = true;
        //Debug.Log("Coffee brewed.");
    }
}
