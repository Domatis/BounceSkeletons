using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class LevelSlot : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image selectionObject;
    [SerializeField] private GameObject completedTick;
    [SerializeField] private int levelNumber;

    private bool levelUnlocked = false;

    private void Start() 
    {
        //At default set active false.
        selectionObject.enabled = false;
        completedTick.SetActive(false);

        Image image = GetComponent<Image>();

        //Check this level unlocked or not.
        if(GameDataManager.instance.LastUnlockedLevel >= levelNumber)
        {
            levelUnlocked = true;
            image.color = Color.white;  //Base color.
        }
        else
        {
            levelUnlocked = false;
            image.color = MenuUIManager.instance.LevelLockColor;
        }

        if(GameDataManager.instance.LastCompletedLevel >= levelNumber)
            completedTick.SetActive(true);

        //Check selected level this one or not if it's activate selection object.
        if(GameDataManager.instance.SelectedLevel == levelNumber)
        {
            selectionObject.enabled = true;
            MenuUIManager.instance.LevelSlotSelected(this);
        }
            
    }


    public void OnPointerDown(PointerEventData data)
    {
        if(!levelUnlocked) return;
        selectionObject.enabled = true;
        GameDataManager.instance.SelectedLevel = levelNumber;
        //Let know the menu ui manager for deactivate the last level slot.
        MenuUIManager.instance.LevelSlotSelected(this);
    }

    public void DisableSelection()
    {
        selectionObject.enabled = false;
    }
}

