using UnityEngine;
using UnityEngine.UI;
using System;

public class UIWeaponButton : MonoBehaviour
{
    public Image iconImage;
    public Text nameText;
    public Button button;
    public GameObject selectedHighlight;

    private Action onClick;

    public void Setup(WeaponConfig weapon, Action clickCallback)
    {
        iconImage.sprite = weapon.WeaponIcon;
        nameText.text = weapon.WeaponName;

        onClick = clickCallback;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }

    public void SetSelected(bool state)
    {
        selectedHighlight.SetActive(state);
    }
}
