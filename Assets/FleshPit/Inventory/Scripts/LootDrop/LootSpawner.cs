using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LootSpawner : MonoBehaviour
{
    public ItemDatabaseObject itemDatabase;

    public GameObject lootPrefab;
    //public GameObject damageTextPrefab;
    [Range(0.1f,2)]
    public float dropScale = 1;
     float repeatRate =5;
    // Start is called before the first frame update
    void Start()
    {

        //InvokeRepeating("spawnLoot", 0f, repeatRate);
    }



    [ContextMenu("Drop Loot")]
    public void spawnLoot()
    {
        GameObject x = lootPrefab;
        ItemObject atom = itemDatabase.Items[Random.Range(1,7)];
        x.GetComponent<DropFollowPlayer>().item = atom;
        
        GameObject drop = Instantiate(x, transform.position, Quaternion.identity);
        drop.transform.localScale = Vector3.one* dropScale;

        //GameObject damageTxt = Instantiate(damageTextPrefab, transform.position + Vector3.up * 2 + RandomPointInCube(1, 1, .5f), Quaternion.Euler(0, 180, 0));
        //damageTxt.transform.localScale = Vector3.one * dropScale;

        //damageTxt.GetComponentInChildren<TextMeshPro>().text = Random.Range(10,80).ToString();
    }

    public Vector3 RandomPointInCube(float x, float y, float z)
    {
        float randomX = Random.Range(-x , x);
        float randomY = Random.Range(-y , y);
        float randomZ = Random.Range(-z , z);

        return new Vector3(randomX, randomY, randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            spawnLoot();
        }
    }
}
