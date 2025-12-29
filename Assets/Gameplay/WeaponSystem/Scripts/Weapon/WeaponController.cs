using System;
using System.Collections.Generic;

public class WeaponController
{
    public event Action<WeaponInstance> WeaponChanged;
    public event Action<int, int> AmmoChanged;

    private readonly List<WeaponInstance> weapons = new();
    private int currentIndex;

    public WeaponInstance Current => weapons.Count > 0 ? weapons[currentIndex] : null;

    public void Initialize(IReadOnlyList<WeaponConfig> configs)
    {
        weapons.Clear();

        for (int i = 0; i < configs.Count; i++)
            weapons.Add(new WeaponInstance(configs[i]));

        currentIndex = 0;
        NotifyWeaponChanged();
    }

    public void NextWeapon() => SwitchWeapon(1);
    public void PrevWeapon() => SwitchWeapon(-1);

    public void Fire()
    {
        if (Current == null) return;

        if (Current.TryFire())
            NotifyAmmoChanged();
    }

    public void ReleaseFire() => Current?.ReleaseFire();

    public void Reload()
    {
        Current?.Reload();
        NotifyAmmoChanged();
    }

    private void SwitchWeapon(int dir)
    {
        if (weapons.Count <= 1) return;

        currentIndex = (currentIndex + dir + weapons.Count) % weapons.Count;
        NotifyWeaponChanged();
    }

    private void NotifyWeaponChanged()
    {
        WeaponChanged?.Invoke(Current);
        NotifyAmmoChanged();
    }

    private void NotifyAmmoChanged()
    {
        AmmoChanged?.Invoke(Current.MagazineAmmo, Current.TotalAmmo);
    }
}
