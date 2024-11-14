using System.Collections.Generic;
using UnityEngine;
using Items;
using TMPro;
using UnityEngine.UI;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        Dictionary<ItemScriptableObject, int> inventory = new Dictionary<ItemScriptableObject, int>();

        [Header("Item Data")]
        [SerializeField] private ItemScriptableObject potionOne;
        [SerializeField] private ItemScriptableObject potionTwo;
        [SerializeField] private ItemScriptableObject itemOne;
        [SerializeField] private ItemScriptableObject itemTwo;

        [Header("Item Amount")]
        public int itemOneAmount = 1;
        public int itemTwoAmount = 2;
        public int potionOneAmount = 2;
        public int potionTwoAmount = 4;

        [Header("Inventory UI")]
        [SerializeField] private Image InventoryItemOneImage;
        [SerializeField] private Image InventoryItemTwoImage;
        [SerializeField] private Image InventoryItemThreeImage;
        [SerializeField] private Image InventoryItemFourImage;

        [SerializeField] private TMP_Text itemOneTAmountText;
        [SerializeField] private TMP_Text itemTwoTAmountText;
        [SerializeField] private TMP_Text itemThreeTAmountText;
        [SerializeField] private TMP_Text itemFourTAmountText;

        [SerializeField] private Sprite nothingSprite;

        private void Start()
        {
            // Inicializar el inventario
            inventory.Add(potionOne, potionOneAmount);
            inventory.Add(potionTwo, potionTwoAmount);
            inventory.Add(itemOne, itemOneAmount);
            inventory.Add(itemTwo, itemTwoAmount);

            // Mostrar inventario en la UI al inicio
            UpdateInventoryUI();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Alpha1)) RemoveItemAmount(potionOne, 1);
            if (Input.GetKey(KeyCode.Alpha2)) RemoveItemAmount(potionTwo, 1);
            if (Input.GetKey(KeyCode.Alpha3)) RemoveItemAmount(itemOne, 1);
            if (Input.GetKey(KeyCode.Alpha4)) RemoveItemAmount(itemTwo, 1);
        }

        public void RemoveItemAmount(ItemScriptableObject item, int amount)
        {
            if (!inventory.ContainsKey(item)) return;

            inventory[item] = Mathf.Max(0, inventory[item] - amount);  // Evita valores negativos
            UpdateItemUI(item);
        }

        private void UpdateItemUI(ItemScriptableObject item)
        {
            // Actualizar el sprite y el texto basado en el item y su cantidad
            if (item == potionOne)
            {
                InventoryItemOneImage.sprite = inventory[item] > 0 ? potionOne.ItemSprite : nothingSprite;
                itemOneTAmountText.text = $"x{inventory[item]}";
            }
            else if (item == potionTwo)
            {
                InventoryItemTwoImage.sprite = inventory[item] > 0 ? potionTwo.ItemSprite : nothingSprite;
                itemTwoTAmountText.text = $"x{inventory[item]}";
            }
            else if (item == itemOne)
            {
                InventoryItemThreeImage.sprite = inventory[item] > 0 ? itemOne.ItemSprite : nothingSprite;
                itemThreeTAmountText.text = $"x{inventory[item]}";
            }
            else if (item == itemTwo)
            {
                InventoryItemFourImage.sprite = inventory[item] > 0 ? itemTwo.ItemSprite : nothingSprite;
                itemFourTAmountText.text = $"x{inventory[item]}";
            }
        }

        private void UpdateInventoryUI()
        {
            // Actualiza toda la UI al inicio o después de una modificación significativa
            UpdateItemUI(potionOne);
            UpdateItemUI(potionTwo);
            UpdateItemUI(itemOne);
            UpdateItemUI(itemTwo);
        }
    }
}

