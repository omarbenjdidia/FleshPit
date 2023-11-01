using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObject,IDamageable
{
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public EnemyScriptableObject EnemyScriptableObject;
    public int Health = 100;
    [SerializeField]
    private ProgressBar HealthBar;
    private float MaxHealth;


    private void Awake()
    {
        MaxHealth = Health;
        Agent = GetComponent<NavMeshAgent>();
    }
    //public virtual void OnEnable()
    //{
    //    SetupAgentFromConfiguration();
    //}

   

    //public virtual void SetupAgentFromConfiguration()
    //{
    //    Agent.acceleration = EnemyScriptableObject.Acceleration;
    //    Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
    //    Agent.areaMask = EnemyScriptableObject.AreaMask;
    //    Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
    //    Agent.baseOffset = EnemyScriptableObject.BaseOffset;
    //    Agent.height = EnemyScriptableObject.Height;
    //    Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
    //    Agent.radius = EnemyScriptableObject.Radius;
    //    Agent.speed = EnemyScriptableObject.Speed;
    //    Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;

       

    //    Health = EnemyScriptableObject.Health;
    //}

    public void TakeDamage(int Damage)
    {
       
        Health -= Damage;
        HealthBar.SetProgress(Health / MaxHealth, 3);

        if (Health < 0)
        {
            OnDied();
            Agent.enabled = false;
        }
    }

    private void OnDied()
    {
        Destroy(Agent, 1f);
        Destroy(HealthBar.gameObject, 1f);
    }
   
    public Transform GetTransform()
    {
        return transform;
    }
}
