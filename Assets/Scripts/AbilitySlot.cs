using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour,IPointerDownHandler
{
    
    [SerializeField] private Abilities currentAbility;  //TODO it needs to be selected while loading level.
    [SerializeField] private Text usageCountText;
    [SerializeField] private int numberOfUsage = 3;



    private void Start() 
    {
        currentAbility = GameDataManager.instance.SelectedAbility;
        UpdateUsageInfoText();   
        GetComponent<Image>().sprite = currentAbility.GetSprite();
    }


    public void OnPointerDown(PointerEventData eventdata)
    {
        UseAbility();
    }

    public void UseAbility()
    {
        if(currentAbility == null || numberOfUsage <= 0) return;

        numberOfUsage--;
        UpdateUsageInfoText();    //Update text of usage info.
        currentAbility.UseAbility();
    }

    public void UpdateUsageInfoText()
    {
        usageCountText.text = numberOfUsage.ToString();
    }
}
