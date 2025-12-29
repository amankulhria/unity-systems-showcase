using UnityEngine;

public class WeaponInstance
{
    public WeaponConfig Config { get; }

    public int MagazineAmmo { get; private set; }
    public int TotalAmmo { get; private set; }

    private float nextFireTime;
    private bool singleShotLock;

    public WeaponInstance(WeaponConfig config)
    {
        Config = config;
        MagazineAmmo = config.MagazineSize;
        TotalAmmo = config.MaxAmmo;
    }

    public bool TryFire()
    {
        if (!CanFire()) return false;

        if (Config.FireMode == EFireMode.Single && singleShotLock)
            return false;

        Shoot();
        singleShotLock = Config.FireMode == EFireMode.Single;
        return true;
    }

    public void ReleaseFire()
    {
        singleShotLock = false;
    }

    public void Reload()
    {
        int needed = Config.MagazineSize - MagazineAmmo;
        if (needed <= 0 || TotalAmmo <= 0) return;

        int loaded = Mathf.Min(needed, TotalAmmo);
        MagazineAmmo += loaded;
        TotalAmmo -= loaded;
    }

    private bool CanFire()
    {
        if (MagazineAmmo <= 0) return false;
        if (Time.time < nextFireTime) return false;
        return true;
    }

    private void Shoot()
    {
        MagazineAmmo--;
        nextFireTime = Time.time + Config.FireRate;
    }
}
