using UnityEngine;
using UnityEngine.UI;

public class UIWeaponHUD : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Text ammoText;

    private WeaponController controller;

    public void Bind(WeaponController newController)
    {
        if (controller != null)
        {
            controller.WeaponChanged -= UpdateWeapon;
            controller.AmmoChanged -= UpdateAmmo;
        }

        controller = newController;

        controller.WeaponChanged += UpdateWeapon;
        controller.AmmoChanged += UpdateAmmo;
    }

    private void UpdateWeapon(WeaponInstance weapon)
    {
        weaponIcon.sprite = weapon.Config.WeaponIcon;
    }

    private void UpdateAmmo(int mag, int total)
    {
        ammoText.text = $"{mag} / {total}";
    }
}
