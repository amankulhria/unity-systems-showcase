using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : MonoBehaviour
{
    public WeaponController WeaponController { get; private set; }

    [SerializeField] private UIWeaponHUD weaponHUD;

    private PlayerInputController input;

    private bool isInitialized;

    private void Awake()
    {
        input = GetComponent<PlayerInputController>();
        WeaponController = new WeaponController();

        BindInput();
    }

    private void BindInput()
    {
        input.FirePressed += OnFirePressed;
        input.FireReleased += OnFireReleased;
        input.ReloadPressed += OnReloadPressed;
        input.NextWeapon += OnNextWeapon;
        input.PreviousWeapon += OnPreviousWeapon;
    }

    private void UnbindInput()
    {
        input.FirePressed -= OnFirePressed;
        input.FireReleased -= OnFireReleased;
        input.ReloadPressed -= OnReloadPressed;
        input.NextWeapon -= OnNextWeapon;
        input.PreviousWeapon -= OnPreviousWeapon;
    }

    private void OnDestroy()
    {
        UnbindInput();
    }

    private void OnFirePressed()
    {
        if (!isInitialized) return;
        WeaponController.Fire();
    }

    private void OnFireReleased()
    {
        if (!isInitialized) return;
        WeaponController.ReleaseFire();
    }

    private void OnReloadPressed()
    {
        if (!isInitialized) return;
        WeaponController.Reload();
    }

    private void OnNextWeapon()
    {
        if (!isInitialized) return;
        WeaponController.NextWeapon();
    }

    private void OnPreviousWeapon()
    {
        if (!isInitialized) return;
        WeaponController.PrevWeapon();
    }


    public void ApplyLoadout(List<WeaponConfig> weapons)
    {
        if (weapons == null)
        {
            Debug.LogError("PlayerController: Loadout is null");
            return;
        }

        weaponHUD.Bind(WeaponController);
        WeaponController.Initialize(weapons);

        weaponHUD.gameObject.SetActive(true);

        isInitialized = true;
    }
}
