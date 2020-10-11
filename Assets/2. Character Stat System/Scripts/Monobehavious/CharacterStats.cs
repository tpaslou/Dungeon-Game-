using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
  public CharacterStats_SO  characterDefinition;
  public CharacterInventory charInvetory;
  public GameObject characterWeaponslot;
  
  #region Constructors

  public CharacterStats()
  {
    charInvetory = CharacterInventory.instance;

  }
  #endregion

  #region Initializations

  private void Start()
  {
    if (!characterDefinition.SetManually)
    {
      characterDefinition.maxHealth = 100;
      characterDefinition.currentHealth = 50;
      characterDefinition.maxMana = 25;
      characterDefinition.currentMana=10;
      characterDefinition.maxWealth = 500;
      characterDefinition.currentWealth = 0;
      characterDefinition.baseResistance = 0;
      characterDefinition.currentResistance = 0;
      characterDefinition.maxEncumbrance = 50f;
      characterDefinition.currentEncumbrance = 0;
      characterDefinition.charExperience = 0;
      characterDefinition.charLevel = 0;
    }
  }

  #endregion

  #region Updates

  private void Update()
  {
    /*if (Input.GetMouseButtonDown(2))
    {
      characterDefinition.SaveCharacterData();
    }*/
  }

  #endregion
  
  #region Stat Increasers

  public void ApplyHealth(int healthAmount)
  {
    characterDefinition.ApplyHealth(healthAmount);
  }

  public void ApplyMana(int manaAmount)
  {
    characterDefinition.ApplyMana(manaAmount);
  }

  public void GiveWealth(int wealthAmount)
  {
    characterDefinition.GiveWealth(wealthAmount);
  }

  #endregion

  #region Stat Decreasers

  public void TakeDamage(int amount)
  {
    characterDefinition.TakeDamage(amount);
  }

  public void TakeMana(int amount)
  {
    characterDefinition.TakeMana(amount);
  }

  #endregion

  #region Weapon and armor Change

  public void ChangeWeapon(ItemPickUp weaponPickUp)
  {
    if (!characterDefinition.UnEquipWeapon(weaponPickUp, charInvetory, characterWeaponslot))
    {
      characterDefinition.EquipWeapon(weaponPickUp,charInvetory,characterWeaponslot);
    }//esle , future task
  }

  public void ChangeArmor(ItemPickUp armorPickUp)
  {
    if (!characterDefinition.UnEquipArmor(armorPickUp, charInvetory))
    {
      characterDefinition.EquipArmor(armorPickUp,charInvetory);
    }//esle , future task
  }
  #endregion

  #region Reporters

  public int GetHealth()
  {
    return characterDefinition.currentHealth;
  }

  public ItemPickUp GetCurrentWeapon()
  {
    return characterDefinition.weapon;
  }
  #endregion


}
