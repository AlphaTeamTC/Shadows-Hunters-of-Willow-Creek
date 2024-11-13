
using System;
using UnityEngine;
using Items;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    
    //Item Data
    [Header("Item Data")] [SerializeField] private ItemScriptableObject[] _items; 
    
    //Store UI to reflect data
    [Header("Store UI")] 
    [SerializeField] private Image _shopItemIcon; 
    [SerializeField] private TMP_Text _shopItemName;
    [SerializeField] private TMP_Text _shopItemPrice;
    [SerializeField] private TMP_Text _shopItemDescription;
    [SerializeField] private TMP_Text goldText; 
    

    //Gold amount 
    [SerializeField] private int goldAmount; 
    
    //CurrentItemId
    [SerializeField] private int currentItemId; 
    
    //Nur dialogue
    [SerializeField] private TMP_Text nurDialogue; 
    
    //Methods -------

    public void Start()
    {
        goldText.text = goldAmount.ToString();
    }

    /// <summary>
    //Method for bought item 
    /// </summary>
    /// <param name="itemId"></param>
    public void onItemSelected(int itemId)
    {
        if (_items[itemId] != null)
        {
            //Rarity/Name
            if (_items[itemId].ItemRarity == 0)
            {
                _shopItemName.color = Color.white;
                _shopItemName.text = $"{_items[itemId].ItemName}";
            }

            if (_items[itemId].ItemRarity == 1)
            {
                _shopItemName.color = Color.cyan;
                _shopItemName.text = $"{_items[itemId].ItemName}";
            }

            if (_items[itemId].ItemRarity == 2)
            {
                _shopItemName.color = Color.magenta;
                _shopItemName.text = $"{_items[itemId].ItemName}";
            }

            if (_items[itemId].ItemRarity == 3)
            {
                _shopItemName.color = Color.yellow;
                _shopItemName.text = $"{_items[itemId].ItemName}";
            }
            
            //Cost 
            _shopItemPrice.text = $"{_items[itemId].ItemCost} G";
            //Description
            _shopItemDescription.text = $"{_items[itemId].ItemDescription}";
            //Sprites
            _shopItemIcon.sprite = _items[itemId].ItemSprite;

        }
        else
        {
            Debug.LogError("Item doesn't exist!");
        }
        
    }
    
    /// <summary>
    /// Set current id items. 
    /// </summary>
    /// <param name="itemId"></param>
    public void setCurentItem(int itemId)
    {
        currentItemId = itemId;
        Debug.LogFormat($"Current item name: {_items[itemId].ItemName}");
    }


    public void OnBuyItem()
    {
        if (goldAmount < _items[currentItemId].ItemCost)
        {
            nurDialogue.text = "You don't have enough gold!";
        }
        else
        {
            nurDialogue.text = "Thank you!";
            int newGold =   goldAmount - _items[currentItemId].ItemCost;
            goldAmount = newGold;
            goldText.text = goldAmount.ToString();
        }
    }


    public void onExitShop()
    {
        nurDialogue.text = "Farewell!";
        Debug.Log("You exited the shop!");
    }




}
