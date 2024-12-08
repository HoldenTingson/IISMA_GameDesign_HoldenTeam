using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ActiveInventory : Singleton<ActiveInventory>
{
    public bool canToggle = true;
    private int _activeSlotIndexNum = 0;
    private PlayerControls _playerControls;
    private Dictionary<string, WeaponInfo> preloadedWeapons = new Dictionary<string, WeaponInfo>();
    private Dictionary<string, Sprite> preloadedImages = new Dictionary<string, Sprite>();

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Inventory.Keyboard.performed += ctx =>
        {
            if (this != null)
            {
                ToggleActiveSlot((int)ctx.ReadValue<float>());
            }
        };

        PreloadWeapon("Bow");
        PreloadWeapon("Staff");
        PreloadImage("Bow");
        PreloadImage("Staff");
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }


    public void EquipStartingWeapon()
    {
        ToggleActiveSlot(1);
  
    }

    public void RestartWeapon()
    {
        Debug.Log("RestartWeapon called");

        for (int i = 1; i < transform.childCount; i++) 
        {
            Transform inventorySlot = transform.GetChild(i);
            InventorySlot slot = inventorySlot.GetComponent<InventorySlot>();

            if (slot != null)
            {
                slot.SetWeaponInfo(null);

                Transform weaponIcon = inventorySlot.Find("Weapon");
                if (weaponIcon != null)
                {
                    Destroy(weaponIcon.gameObject);
                }
            }
        }
    }

    private void ToggleActiveSlot(int indexNum)
    {
            _activeSlotIndexNum = indexNum - 1;

            InventorySlot selectedSlot =
                transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>();
            if (selectedSlot != null && selectedSlot.GetWeaponInfo() != null && canToggle)
            {
                foreach (Transform inventorySlot in transform)
                {
                    inventorySlot.GetChild(0).gameObject.SetActive(false);
                }

                this.transform.GetChild(indexNum - 1).GetChild(0).gameObject.SetActive(true);

                ChangeActiveWeapon();
            }
    }


    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo() == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(_activeSlotIndexNum)
            .GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform);
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }

    public void PreloadWeapon(string weaponName)
    {
        var weaponAddress = $"Assets/Tingson_Holden/ScriptableObjects/{weaponName}.asset";
        Addressables.LoadAssetAsync<WeaponInfo>(weaponAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                preloadedWeapons[weaponName] = handle.Result;
                Debug.Log($"{weaponName} preloaded.");
            }
            else
            {
                Debug.LogError($"Failed to preload {weaponName}.");
            }
        };
    }

    public void PreloadImage(string weaponName)
    {
        var imageAddress = $"Assets/Tingson_Holden/Sprites/UI/{weaponName}.png";
        Addressables.LoadAssetAsync<Sprite>(imageAddress).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                preloadedImages[weaponName] = handle.Result;
                Debug.Log($"{weaponName} icon preloaded.");
            }
            else
            {
                Debug.LogError($"Failed to preload icon for {weaponName}.");
            }
        };
    }

    public void AddWeapon(string weaponName)
    {
        if (preloadedWeapons.TryGetValue(weaponName, out WeaponInfo weaponInfo))
        {
            AssignWeaponToSlot(weaponInfo, preloadedImages[weaponName]);
        }
        else
        {
            Debug.LogWarning($"Weapon {weaponName} not preloaded. Loading dynamically.");
            var weaponAddress = $"Assets/Tingson_Holden/ScriptableObjects/{weaponName}.asset";
            Addressables.LoadAssetAsync<WeaponInfo>(weaponAddress).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var loadedWeapon = handle.Result;
                    Sprite weaponIcon = preloadedImages.ContainsKey(weaponName) ? preloadedImages[weaponName] : null;
                    AssignWeaponToSlot(loadedWeapon, weaponIcon);
                }
                else
                {
                    Debug.LogError($"Failed to load weapon: {weaponName}");
                }
            };
        }
    }

    private void AssignWeaponToSlot(WeaponInfo weaponInfo, Sprite weaponIcon)
    {
        foreach (Transform inventorySlot in transform)
        {
            InventorySlot slot = inventorySlot.GetComponent<InventorySlot>();
            if (slot != null && slot.GetWeaponInfo() == null)
            {
                slot.SetWeaponInfo(weaponInfo);

                GameObject weaponIconObject = new GameObject("Weapon");
                weaponIconObject.transform.SetParent(slot.transform); 
                weaponIconObject.transform.localPosition = Vector3.zero; 
                weaponIconObject.transform.localScale = Vector3.one; 

                Image iconImage = weaponIconObject.AddComponent<Image>();
                if (weaponIcon != null)
                {
                    iconImage.sprite = weaponIcon; 
                    iconImage.enabled = true; 
                }

                Debug.Log("Weapon and icon added to inventory.");
                break;
            }
        }
    }
}
