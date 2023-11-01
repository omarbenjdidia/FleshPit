using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // uncomment this at the moment :::public Joystick joystick;
    public Animator NeutroAnim;
    public bool IsDamaged = false;
    private bool _mouvement;
    public Transform PlayerSprite;
    public float speed;

    void Start()
    {
        NeutroAnim = GetComponent<Animator>(); 
    }

    //public void FixedUpdate()
    //{
    //    CharacterMouvement();
    //    Vector3 v = new Vector3(joystick.Horizontal, 0, joystick.Vertical);   
    //}

    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "spit")
    //    {
    //        IsDamaged = true;
    //    }
    //}

    //public void CharacterMouvement()
    //{
    //    if (joystick.Horizontal > 0 || joystick.Horizontal < 0 || joystick.Vertical > 0 || joystick.Vertical < 0)
    //    {
    //        PlayerSprite.gameObject.SetActive(true);
    //        PlayerSprite.position = new Vector3(joystick.Horizontal + transform.position.x, -1.54f, joystick.Vertical + transform.position.z);
    //        transform.LookAt(new Vector3(PlayerSprite.position.x, 0, PlayerSprite.position.z));
    //        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    //        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    //        if (NeutroAnim.GetBool("walk") != true)
    //        {
    //            NeutroAnim.SetBool("walk", true);
    //        }
    //        _mouvement = true;
    //    }
    //    else if (_mouvement == true)
    //    {
    //        NeutroAnim.SetBool("walk", false);
    //        PlayerSprite.gameObject.SetActive(false);
    //        _mouvement = false;
    //    }
    //}
}



