
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "Skills/Iceball")]
public class IceBall : Skill
{
    public GameObject IceballInstance;
    private BoxCollider boxCollider;
    private Transform spawnPosition;
    private NavMeshAgent NavIce; 
    public float force;
    public int damage;
    private Vector3 direction;
    public float searchRadius = 10f;
    public float desiredDistance = 5f;
    public override void Initialize(GameObject obj)
    {
        spawnPosition = obj.transform;
        force = 500f;
        damage = 10;
    }

    public override void Use()
    {
        // Instantiate the Iceball
        GameObject clonedSkillPrefab = Instantiate(IceballInstance, spawnPosition.position, spawnPosition.rotation);
        // Find the enemy object
        GameObject Enemy = GameObject.Find("Enemy");
        // Get the enemy's position
        Vector3 enemyPosition = Enemy.transform.position;
        GameObject Player = GameObject.Find("Player");
        Vector3 playerPosition = Player.transform.position;
        Vector3 displacement = enemyPosition - playerPosition;
        direction = displacement.normalized;
        clonedSkillPrefab.GetComponent<Rigidbody>().AddForce(direction * force , ForceMode.Force);


    }


    //private Vector3 GetDirectionToClosestEnemy(Transform origin)
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(origin.position, searchRadius);
    //    Transform closestEnemy = null;
    //    float closestDistance = Mathf.Infinity;

    //    // Find the closest enemy
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        if (hitCollider.CompareTag("Enemy"))
    //        {
    //            float distance = Vector3.Distance(origin.position, hitCollider.transform.position);
    //            if (distance < closestDistance)
    //            {
    //                closestEnemy = hitCollider.transform;
    //                closestDistance = distance;
    //            }
    //        }
    //    }

    //    // If there is a closest enemy, return a vector towards it
    //    if (closestEnemy != null)
    //    {
    //        Vector3 direction = closestEnemy.position - origin.position;
    //        direction.Normalize();
    //        return direction * desiredDistance;
    //    }
    //    else
    //    {
    //        return Vector3.zero;
    //    }
    //}


}
