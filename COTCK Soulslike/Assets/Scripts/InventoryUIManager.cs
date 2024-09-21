using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public PlayerInventoryManager inventoryManager;

    // UI Elements
    public GameObject weaponGrid;
    public GameObject consumableGrid;
    public GameObject ammoGrid;
    public GameObject colorSlotGrid;

    public GameObject itemDetailsPanel;
    public Text itemNameText;
    public Text itemDescriptionText;

    public Button equipMainButton;
    public Button equipOffButton;
    public Button useConsumableButton;

    public Image selectionHighlight;

    // Item slot prefab
    public GameObject itemSlotPrefab;

    private List<GameObject> activeItemSlots = new List<GameObject>();

    private void Start()
    {
        ShowWeaponGrid();
    }

    public void ShowWeaponGrid()
    {
        PopulateWeaponTab();

        weaponGrid.SetActive(true);
        consumableGrid.SetActive(false);
        ammoGrid.SetActive(false);
        colorSlotGrid.SetActive(false);
    }

    public void ShowConsumableGrid()
    {
        PopulateConsumableTab();

        weaponGrid.SetActive(false);
        consumableGrid.SetActive(true);
        ammoGrid.SetActive(false);
        colorSlotGrid.SetActive(false);
    }

    public void ShowAmmoGrid()
    {
        PopulateAmmoTab();

        weaponGrid.SetActive(false);
        consumableGrid.SetActive(false);
        ammoGrid.SetActive(true);
        colorSlotGrid.SetActive(false);
    }

    public void ShowColorGrid()
    {
        PopulateColorsTab();

        weaponGrid.SetActive(false);
        consumableGrid.SetActive(false);
        ammoGrid.SetActive(false);
        colorSlotGrid.SetActive(true);
    }

    public void PopulateWeaponTab()
    {
        ClearActiveItemSlots();

        foreach (var weapon in inventoryManager.weaponInventory)
        {
            GameObject itemSlot = Instantiate(itemSlotPrefab, weaponGrid.transform);
            itemSlot.transform.Find("Item Count").GetComponent<Text>().text = "";
            activeItemSlots.Add(itemSlot);

            itemSlot.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(weapon));
            itemSlot.GetComponent<Button>().onClick.AddListener(() => HighlightSelectedItem(itemSlot));
        }
    }

    public void PopulateConsumableTab()
    {
        ClearActiveItemSlots();

        foreach (string category in inventoryManager.consumables.Keys)
        {
            List<Consumable> consumableList = (List<Consumable>)inventoryManager.consumables[category];
            GameObject itemSlot = Instantiate(itemSlotPrefab, consumableGrid.transform);
            itemSlot.transform.Find("Item Count").GetComponent<Text>().text = consumableList.Count.ToString();
            activeItemSlots.Add(itemSlot);

            itemSlot.GetComponent<Button>().onClick.AddListener(() => ShowItemDetails(consumableList[0]));
            itemSlot.GetComponent<Button>().onClick.AddListener(() => HighlightSelectedItem(itemSlot));
        }
    }


    // TODO
    public void PopulateAmmoTab()
    {
        ClearActiveItemSlots();
    }

    // TODO
    public void PopulateColorsTab()
    {
        ClearActiveItemSlots();
    }


    private void ShowItemDetails(Item item)
    {
        itemDetailsPanel.SetActive(true);
        itemNameText.text = item.Name;
        itemDescriptionText.text = item.Description;

        if (item is Weapon)
        {
            equipMainButton.gameObject.SetActive(true);
            equipMainButton.onClick.RemoveAllListeners();
            equipMainButton.onClick.AddListener(() => EquipWeapon((Weapon)item, true));

            equipOffButton.gameObject.SetActive(true);
            equipOffButton.onClick.RemoveAllListeners();
            equipOffButton.onClick.AddListener(() => EquipWeapon((Weapon)item, false));
        }
        else
        {
            equipMainButton.gameObject.SetActive(false);
            equipOffButton.gameObject.SetActive(false);
        }
        if (item is Consumable)
        {
            useConsumableButton.gameObject.SetActive(true);
            useConsumableButton.onClick.RemoveAllListeners();
            useConsumableButton.onClick.AddListener(() => UseConsumable((Consumable)item));
        }
        else
        {
            useConsumableButton.gameObject.SetActive(false);
        }
        if (item is ColorPassive)
        {

        }
        else
        {

        }
    }

    private void UseConsumable(Consumable consumable)
    {
        inventoryManager.UseConsumable(consumable.Category);
    }

    private void EquipWeapon(Weapon weapon, bool isMainHand)
    {
        inventoryManager.EquipWeapon(weapon, isMainHand);
    }

    private void ClearActiveItemSlots()
    {
        foreach (var itemSlot in activeItemSlots)
        {
            Destroy(itemSlot);
        }
        activeItemSlots.Clear();
    }

    public void HighlightSelectedItem(GameObject selectedItemSlot)
    {
        selectionHighlight.transform.position = selectedItemSlot.transform.position;
        selectionHighlight.gameObject.SetActive(true);
    }
}
