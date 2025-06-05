using UnityEngine;

public class CoffeeCup : Interactable
{
    public Transform holdPoint; // задать в инспекторе
    private Rigidbody rb;
    private bool isHeld = false;
    //private bool isInCoffeeMachineZone = false;
    [Header("Liquid in cup")]
    [SerializeField] private GameObject coffeeLiquid;
    //[SerializeField] private Vector3 liquidMinScale;    
    //[SerializeField] private Vector3 liquidMaxScale;    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coffeeLiquid.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 direction = (holdPoint.position - transform.position);
            rb.MovePosition(transform.position + direction * 10f * Time.fixedDeltaTime); // можно подкрутить скорость
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }
    }

    public override void Interact()
    {
        if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.GrabCup) && !isHeld)
        {
            isHeld = true;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            CoffeeStepManager.Instance.AdvanceStep();
        }
        else if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.PlaceCup) && isHeld)
        {
            isHeld = false;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            CoffeeStepManager.Instance.ReverceStep();

            //if (isInCoffeeMachineZone)
            //{
            //    // «адаЄм фиксированную позицию (подгони под свою машину)
            //    transform.position = new Vector3(0.5f, 1.0f, 0.5f);
            //    transform.rotation = Quaternion.identity;
            //    rb.isKinematic = true; // больше не двигаетс€
            //    CoffeeStepManager.Instance.AdvanceStep();
            //}
            //else
            //{
            //    Debug.Log("Cup released outside the coffee machine zone.");
            //}
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("CoffeeZone"))
    //    {
    //        isInCoffeeMachineZone = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("CoffeeZone"))
    //    {
    //        isInCoffeeMachineZone = false;
    //    }
    //}

    public void AutoPlace(Transform targetPoint)
    {
        isHeld = false;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.position = targetPoint.position;
        transform.rotation = targetPoint.rotation;

        CoffeeStepManager.Instance.AdvanceStep();
        Debug.Log("Cup auto-placed from trigger zone.");
    }

    public void UpdateLiquidInCup(float progress) 
    {
        if(!coffeeLiquid.activeSelf) coffeeLiquid.SetActive(true);

        Vector3 liquidObjectScale = new Vector3(0.75f + (0.25f * progress), 0.75f + (0.25f * progress), progress);
        coffeeLiquid.transform.localScale = liquidObjectScale;
    }
}
