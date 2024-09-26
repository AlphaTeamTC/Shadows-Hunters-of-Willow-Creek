using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> models;

    //default index of list models
    private int selection = 0;


    //To change the character name in character select 
    [SerializeField]
    private TMP_Text _character;

    //List of text
    private List<string> names = new List<string> { "Fenrir Ha’it", "Lucius Blackwood", "Navi’srah de Naturah", "Ambrose Morningbane" };

    //Sprites
    public Sprite FenrirInfo;
    public Sprite LuciusInfo;
    public Sprite NaviInfo;
    public Sprite AmbroseInfo;

    public Image CharacterInfo; 

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

    public void Select(int index)
    {
        if (index == selection)
        {
            Debug.Log("The index and the selection you made is the same.");
            return;

        }
            

        if (index < 0 || index >= models.Count)
        {
            Debug.Log("The index is lower than 0 or is higher than the amount of models");
            return;
        }    
           

        models[selection].SetActive(false);
        selection = index;
        
        //Selección de personaje realizada
        models[selection].SetActive(true);

        //Cambio de personaje 
        _character.text = names[selection];

        //Cambio de sprite 
        if (selection == 0)
        {
            CharacterInfo.sprite = FenrirInfo;
        }

        if(selection == 1)
        {
            CharacterInfo.sprite = LuciusInfo;
        }

        if (selection == 2)
        {
            CharacterInfo.sprite = NaviInfo;
        }

        if (selection == 3)
        {
            CharacterInfo.sprite = AmbroseInfo;
        }





    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Select(2);
    }
}
