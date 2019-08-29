using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent NavAgent;
    private Animator animator;

    public LayerMask Clickable;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Move();
            
        }

        if (NavAgent.velocity != Vector3.zero)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    private void Move()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, Clickable))
        {
            Vector3 clickPoint = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            NavAgent.SetDestination(hitInfo.point);
        }
    }
}
