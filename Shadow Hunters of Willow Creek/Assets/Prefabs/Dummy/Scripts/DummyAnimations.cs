using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimations : MonoBehaviour
{

    /// <summary>
    /// Variables for life and damage values for the dummy.
    /// </summary>


    /// <summary>
    /// Private variable of animator
    /// </summary>
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            anim.Play("Base Layer.Hit", 0);
        }
    }

}
