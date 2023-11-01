using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SHootingtargets : MonoBehaviour
{

   public GameObject Bullet;
   public  Transform BulletPos;
    public GameObject closestEnemy;
    private GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }





    // Update is called once per frame
    public void Update()
    {
        closestEnemy = FindClosestEnemy("Player", 12f);

        if (_bullet == null && closestEnemy != null)
        {
            _bullet = Instantiate(Bullet, BulletPos.position, BulletPos.rotation);
            //NetworkServer.Spawn(_bullet);
            NavMeshAgent bulletnav = _bullet.GetComponent<NavMeshAgent>();
            if (bulletnav != null)
            {
                bulletnav.SetDestination(closestEnemy.transform.position);
            }
            Destroy(_bullet, 1.5f);
        }
    }


    public GameObject FindClosestEnemy(string Player, float searchRadius)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Player);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            if (distance < closestDistance && distance < searchRadius)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }





}
