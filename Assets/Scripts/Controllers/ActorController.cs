using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    protected NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;

    protected Color taggedColor = Color.red;

    protected MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);

    public OnActorTagged onActorTagged;

    public bool IsTagged { get; protected set; }

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();

        SetTagged(false);

        onActorTagged += SetTagged;
    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            print("collided!");

            otherActor.onActorTagged(true);
            onActorTagged(false);
        }
    }

    protected virtual void OnDestroy()
    {
        agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    public void SetTagged(bool val)
    {
        IsTagged = val;

        if (renderer)
        {
            print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }
    }
}