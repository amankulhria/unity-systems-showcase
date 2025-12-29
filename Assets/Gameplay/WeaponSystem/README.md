# Weapon Loadout & Weapon System

## Goal
Implement a modular weapon system with a pre-game loadout selection flow, allowing the player to equip multiple weapons while keeping gameplay, input, and UI cleanly separated.

---

## Concepts Implemented
- Primary & secondary weapon slots  
- Slot-based loadout validation  
- Event-driven communication between systems  
- Decoupled input, gameplay, and UI  
- ScriptableObject-based weapon configuration  
- Runtime weapon instances with ammo & fire logic  

---

## System Overview
- **PlayerLoadout**  
  Enforces slot rules and emits selection events  

- **UILoadoutScreen**  
  Reacts to loadout events and updates UI  

- **PlayerController**  
  Applies the selected loadout  

- **WeaponController**  
  Manages active weapons and firing logic  

- **UIWeaponHUD**  
  Displays current weapon and ammo  

---

## Weapon Loadout Rules
The player must equip:
- **2 Primary weapons**
- **1 Secondary weapon**

Gameplay starts only when all required slots are filled.

---

## Notes
This module is a self-driven architectural exercise inspired by common shooter game patterns.  
It contains no proprietary or confidential material.