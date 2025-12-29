using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Weapons/Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    [Header("UI")]
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private EWeaponSlotType slotType;

    [Header("Combat")]
    [SerializeField] private EFireMode fireMode;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float damage = 20f;

    [Header("Ammo")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private int maxAmmo = 90;
    [SerializeField] private float reloadTime = 1.5f;

    public string WeaponName => weaponName;
    public Sprite WeaponIcon => weaponIcon;
    public EWeaponSlotType SlotType => slotType;
    public EFireMode FireMode => fireMode;
    public float FireRate => fireRate;
    public float Damage => damage;
    public int MagazineSize => magazineSize;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;
}
