using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    //public float waitTime = 2f;
    public float speed;

    private NavMeshAgent agent;
    [SerializeField] private Transform startPosition;
    private bool isReturning = false;
    private bool isFollowing = false;
    private bool isCoffeOrdered = false;
    //private bool isWatchingPlayer = false;

    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        if (isFollowing || isReturning) return;

        if (isCoffeOrdered)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;

            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
            }

            return;
        }


        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(FollowThenReturn());
        }
    }

    private IEnumerator FollowThenReturn()
    {
        isFollowing = true;
        animator.SetBool("isWalking", true);

        while (Vector3.Distance(transform.position, player.position) > 2f)
        {
            agent.SetDestination(player.position);
            yield return null;
        }

        agent.ResetPath();
        animator.SetBool("isWalking", false);

        NotificationSystem.Instance.ShowMessage("Привет. Они говорили, что ты придёшь. Ты ведь умеешь готовить кофе, верно?.. Сделай один для меня. Мне нужно немного тепла.", 5f);

        yield return new WaitForSeconds(6f);

        CoffeeStepManager.Instance.AdvanceStep();

        isCoffeOrdered = true;
        isFollowing = false;
        isReturning = true;
        animator.SetBool("isWalking", true);

        agent.SetDestination(startPosition.position);

        while (Vector3.Distance(transform.position, startPosition.position) > 0.5f)
        {
            yield return null;
        }

        isReturning = false;
        animator.SetBool("isWalking", false);
        //isWatchingPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CoffeeStepManager.Instance.IsCurrentStep(CoffeeStepManager.Step.ServeToNPC) && other.CompareTag("CoffeeCup"))
        {
            if (other.GetComponent<CoffeeCup>().IsCoffeeReady())
            {
                NotificationSystem.Instance.ShowMessage("Спасибо за кофе! Держи немного денег за работу.", 2f);
                Destroy(other.gameObject);
            }
            else
            {
                NotificationSystem.Instance.ShowMessage("Мне нужен кофе, а не пустой стаканчик!", 2f);
            }
        }
    }
}
