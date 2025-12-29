using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadoutScreen : MonoBehaviour
{
    public WeaponDatabase db;
    public Transform primaryContainer;
    public Transform secondaryContainer;
    public GameObject buttonPrefab;
    public Button playButton;
    public GameObject playButtonActive;

    private PlayerLoadout loadout;
    private PlayerController player;

    private void Start()
    {
        loadout = new PlayerLoadout(2, 1);
        player = FindObjectOfType<PlayerController>();

        loadout.LoadoutValidityChanged += OnValidityChanged;

        CreateButtons(db.PrimaryWeapons, primaryContainer);
        CreateButtons(db.SecondaryWeapons, secondaryContainer);

        playButton.onClick.AddListener(OnGoPressed);
        OnValidityChanged(false);
    }

    void CreateButtons(IEnumerable<WeaponConfig> weapons, Transform parent)
    {
        foreach (var weapon in weapons)
        {
            var ui = Instantiate(buttonPrefab, parent)
                        .GetComponent<UIWeaponButton>();

            ui.Setup(weapon, () => loadout.ToggleWeapon(weapon));

            loadout.WeaponSelectionChanged += (w, selected) =>
            {
                if (w == weapon)
                    ui.SetSelected(selected);
            };
        }
    }

    void OnValidityChanged(bool valid)
    {
        playButtonActive.SetActive(valid);
        playButton.interactable = valid;
    }

    void OnGoPressed()
    {
        player.ApplyLoadout(loadout.BuildFinalLoadout());
        gameObject.SetActive(false);
    }
}
