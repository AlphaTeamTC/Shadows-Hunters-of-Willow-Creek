using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> models;

    //default index of list models
    private int selection = 0; 
    void Start()
    {
        models = new List<GameObject>();

        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);

        }
        models[selection].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
