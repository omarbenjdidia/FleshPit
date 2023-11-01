using UnityEngine;
using UnityEngine.UI;

public class MyhealthBar : MonoBehaviour
{
    //younes health

    //public int health;
    public Slider slider;
    public PlayerControllerWithFreeLookCamera playerController;
    //public Bacteria bacteria;

    ////omar health
    //private float health1;
    //public Character neutrophil;
    //public Slider slider1;

    void Start()
    {
        //health = 100;
        slider = GetComponent<Slider>();
        ////omar health
        //health1 = 100f;
        //neutrophil = FindObjectOfType<Character>();
        //slider1 = GetComponent<Slider>();
    }
    
    private void Update()
    {

        //if(health<=0)
        //    Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }

    //omar health

    void FixedUpdate()
    {
        //Damage();
    }

    //public void Damage()
    //{
    //    if (neutrophil.IsDamaged == true)
    //    {
    //        health -= 10;
    //        neutrophil.IsDamaged = false;
    //        slider.value = health;
    //    }

    //    if (health <= 0f)
    //    {
    //        slider.value = health;
    //        neutrophil.NeutroAnim.SetBool("death", true);
    //        Destroy(GameObject.FindGameObjectWithTag("Character").transform.GetChild(1).gameObject, 2.0f);

    //    }

    //}
}
