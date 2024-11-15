using UnityEngine;
using Items;
using TMPro;
using UnityEngine.UI;

namespace Shop
{
    public class ShopMenu : MonoBehaviour
    {
        //Item Data
        [Header("Item Data")]
        [SerializeField] private ItemScriptableObject[] _items;

        //Store UI to reflect data
        [Header("Store UI")]
        [SerializeField] private Image _shopItemIcon;
        [SerializeField] private TMP_Text _shopItemName;
        [SerializeField] private TMP_Text _shopItemPrice;
        [SerializeField] private TMP_Text _shopItemDescription;
        [SerializeField] private TMP_Text _goldText;

        //Gold amount 
        [SerializeField] private int _goldAmount;

        //CurrentItemId
        [SerializeField] private int _currentItemId;

        //Nur dialogue
        [SerializeField] private TMP_Text _nurDialogue;

        private void Start()
        {
            _goldText.text = _goldAmount.ToString();
        }

        /// <summary>
        //Method for bought item 
        /// </summary>
        /// <param name="itemId"></param>
        public void OnItemSelected(int itemId)
        {
            if (_items[itemId] != null)
            {
                // Rarity/Name
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

                // Cost 
                _shopItemPrice.text = $"{_items[itemId].ItemCost} G";
                // Description
                _shopItemDescription.text = $"{_items[itemId].ItemDescription}";
                // Sprites
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
        public void SetCurentItem(int itemId)
        {
            _currentItemId = itemId;
            Debug.LogFormat($"Current item name: {_items[itemId].ItemName}");
        }

        /// <summary>
        /// Manages the logic for buying an item.
        /// </summary>
        public void OnBuyItem()
        {
            if (_goldAmount < _items[_currentItemId].ItemCost)
            {
                _nurDialogue.text = "You don't have enough gold!";
            }
            else
            {
                _nurDialogue.text = "Thank you!";
                int newGold = _goldAmount - _items[_currentItemId].ItemCost;
                _goldAmount = newGold;
                _goldText.text = _goldAmount.ToString();
            }
        }

        /// <summary>
        /// Displays a farewell message when the player exits the shop.
        /// </summary>
        public void OnExitShop()
        {
            _nurDialogue.text = "Farewell!";
            Debug.Log("You exited the shop!");
        }
    }
}