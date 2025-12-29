using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Weapons/Weapon Database")]
public class WeaponDatabase : ScriptableObject
{
    public IReadOnlyList<WeaponConfig> PrimaryWeapons => primaryWeapons;
    public IReadOnlyList<WeaponConfig> SecondaryWeapons => secondaryWeapons;

    [SerializeField] private List<WeaponConfig> primaryWeapons;
    [SerializeField] private List<WeaponConfig> secondaryWeapons;
}
