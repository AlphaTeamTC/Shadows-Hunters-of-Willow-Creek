using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    private List<GameObject> models;
    private int selection = 0;

    //To change the character name in character select 
    [SerializeField] private TMP_Text _character;

    //List of text
    private List<string> names = new() { "Fenrir Ha’it", "Lucius Blackwood", "Navi’srah de Naturah", "Ambrose Morningbane" };

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SelectCharacter(2);
    }

    /// <summary>
    /// Select the character
    /// </summary>
    /// <param name="index"></param>
    public void SelectCharacter(int index)
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

        // Character selection display
        models[selection].SetActive(true);

        // Character name change
        _character.text = names[selection];

        // Sprite change
        switch (selection)
        {
            case 0:
                CharacterInfo.sprite = FenrirInfo;
                break;
            case 1:
                CharacterInfo.sprite = LuciusInfo;
                break;
            case 2:
                CharacterInfo.sprite = NaviInfo;
                break;
            case 3:
                CharacterInfo.sprite = AmbroseInfo;
                break;
        }
    }
}
