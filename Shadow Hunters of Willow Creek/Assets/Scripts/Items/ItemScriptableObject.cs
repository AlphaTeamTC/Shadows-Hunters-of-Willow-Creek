using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    /// <summary>
    /// Scriptable object for item
    /// </summary>
    [CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
    public class ItemScriptableObject : ScriptableObject
    {
    
        [Header("Item Variable data")]
        
        //Name of the item
        [SerializeField, Tooltip("Name of the object")]
        private string _itemName;
        
         //Cost in G of the object 
        [SerializeField, Tooltip("Cost of the Object")]
        private int _itemCost;
        
        //Description of the item in the store
        [SerializeField, Tooltip("Description of the Object in the store")]
        private string _itemDescription;

        //Buff that the item gives
        [SerializeField, Tooltip("Buff the item gives")]
        private int _itemBuff; 
    
        //Bool to know if the item has a duration (to know if is potion)
        [SerializeField, Tooltip("Has the item have limited duration?")]
        private bool _itemHasDuration;
        
        //Item rarity (for armor/other items) 
        // 0 - comun
        // 1 - raro
        // 2 - muy raro 
        // 3 - epico 
        [SerializeField, Tooltip("Item rarity")]
        private int itemRarity;
        
        [Header("Item sprite data")]
        //Sprite of the item 
        [SerializeField, Tooltip("Sprite of the item")]
        private Sprite _itemSprite;
        
        //Getters for data
        public string ItemName => _itemName;
        public int ItemCost => _itemCost;
        public string ItemDescription => _itemDescription;
        public int ItemBuff => _itemBuff;
        public bool ItemHasDuration => _itemHasDuration;
        public int ItemRarity => itemRarity;
        
        //Getters for sprite
        public Sprite ItemSprite => _itemSprite;


    }
    
}

