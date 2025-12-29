using UnityEngine;
using System;

public class PlayerInputController : MonoBehaviour
{
    public event Action FirePressed;
    public event Action FireReleased;
    public event Action ReloadPressed;
    public event Action NextWeapon;
    public event Action PreviousWeapon;

    void Update()
    {
        HandleFire();
        HandleReload();
        HandleWeaponScroll();
    }

    private void HandleFire()
    {
        if (Input.GetMouseButton(0))
            FirePressed?.Invoke();

        if (Input.GetMouseButtonUp(0))
            FireReleased?.Invoke();
    }

    private void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ReloadPressed?.Invoke();
    }

    private void HandleWeaponScroll()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0)
            NextWeapon?.Invoke();
        else if (scroll < 0)
            PreviousWeapon?.Invoke();
    }
}
