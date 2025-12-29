using System;
using System.Collections.Generic;

public class PlayerLoadout
{
    public event Action<WeaponConfig, bool> WeaponSelectionChanged;
    public event Action<bool> LoadoutValidityChanged;

    private readonly Dictionary<EWeaponSlotType, List<WeaponConfig>> slots;
    private readonly Dictionary<EWeaponSlotType, int> maxSlots;

    public PlayerLoadout(int maxPrimary, int maxSecondary)
    {
        slots = new()
        {
            { EWeaponSlotType.Primary, new List<WeaponConfig>() },
            { EWeaponSlotType.Secondary, new List<WeaponConfig>() }
        };

        maxSlots = new()
        {
            { EWeaponSlotType.Primary, maxPrimary },
            { EWeaponSlotType.Secondary, maxSecondary }
        };
    }

    public IReadOnlyList<WeaponConfig> GetWeapons(EWeaponSlotType type)
        => slots[type];

    public bool ToggleWeapon(WeaponConfig weapon)
    {
        if (weapon == null) return false;

        var list = slots[weapon.SlotType];

        bool selected;
        if (list.Contains(weapon))
        {
            list.Remove(weapon);
            selected = false;
        }
        else
        {
            if (list.Count >= maxSlots[weapon.SlotType])
                return false;

            list.Add(weapon);
            selected = true;
        }

        WeaponSelectionChanged?.Invoke(weapon, selected);
        LoadoutValidityChanged?.Invoke(IsValid());

        return selected;
    }

    public bool IsValid()
    {
        foreach (var pair in maxSlots)
        {
            if (slots[pair.Key].Count != pair.Value)
                return false;
        }
        return true;
    }

    public List<WeaponConfig> BuildFinalLoadout()
    {
        var result = new List<WeaponConfig>();
        foreach (var list in slots.Values)
            result.AddRange(list);
        return result;
    }
}
