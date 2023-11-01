using UnityEngine;
using UnityEngine.UI;

public class HealthBarTeammate : MonoBehaviour
{
    public float health;
    public Slider slider;
    //public Bacteria bacteria;
    void Start()
    {
        health = 100f;
        slider = GetComponent<Slider>(); 
    }
    
    private void Update()
    {
        if(health<=0)
            Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
