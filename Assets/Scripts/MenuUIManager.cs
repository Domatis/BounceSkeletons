using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    
    public static MenuUIManager instance;
    [Space]
    [SerializeField] private AudioSource asource;
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color redTextColor;
    [SerializeField] private Color levelLockColor;
    [Space]
    [SerializeField] private Text coinValueText;
    [SerializeField] private GameObject levelSelectionPanel;
    [Header("Skill Upgrade Panel Objects")]
    [SerializeField] private GameObject skillsPanel;
    [SerializeField] private GameObject thunderSelectionBack;
    [SerializeField] private GameObject rockSelectionBack;
    [SerializeField] private Text thunderExpText;
    [SerializeField] private Text rockExpText;
    [SerializeField] private GameObject skillUpgradeText;
    [SerializeField] private Text skillCoinValueText;
    [SerializeField] private GameObject skillMaxImage;
    [Header("Cannon Upgrade Panel Objects")]
    [SerializeField] private GameObject cannonUpgradePanel;
    [SerializeField] private Text cannonDamageText;
    [SerializeField] private Text projectileCountText;
    [SerializeField] private GameObject cannonUpgradeText;
    [SerializeField] private GameObject projectileCountUpgradeText;
    [SerializeField] private Text cannonCoinValueText;
    [SerializeField] private Text proCountCoinValueText;
    [SerializeField] private GameObject cannonMaxImage;
    [SerializeField] private GameObject projectileMaxImage;
    [Header("Player Gate Upgrade Panel Objects")]
    [SerializeField] private GameObject playerGatePanel;
    [SerializeField] private Text healthValueText;
    [SerializeField] private GameObject healthUpgradeText;
    [SerializeField] private Text healthCoinValueText;
    [SerializeField] private GameObject maxImageText;

    private bool panelOpen = false;

    private LevelSlot currentLevelSlot;

    public Color LevelLockColor
    {
        get{return levelLockColor;}
    }

    private void Awake() 
    {
        instance = this;
    }

  


    private void Start() 
    {

        Time.timeScale = 1; //Always set default at first.
        skillsPanel.SetActive(false);
        cannonUpgradePanel.SetActive(false);
        playerGatePanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        //TODO get information from gamedatamanager
        UpdateSkillPanelInformation();
        UpdateCannonPanelInformation();
        UpdateHealthPanelInformation();
        UpdateCoinValueText();
    }

    public void LevelSlotSelected(LevelSlot levelslot)
    {
        if(currentLevelSlot != null && currentLevelSlot != levelslot)
            currentLevelSlot.DisableSelection();
        currentLevelSlot = levelslot;
    }

    public void PlayButton()
    {
        asource.Play();
        SceneManager.LoadScene(1);
    }

    public void OpenLevelSelectionPanel()
    {
        if(panelOpen) return;
        panelOpen = true;
        asource.Play();
        panelOpen = true;
        levelSelectionPanel.SetActive(true);
    }


    public void OpenSkillPanel()
    {

        if(panelOpen) return;
        panelOpen = true;
        asource.Play();
        skillsPanel.SetActive(true);
        UpdateSkillPanelInformation();
    }

    public void OpenCannonPanel()
    {
        if(panelOpen) return;
        asource.Play();
        panelOpen = true;
        cannonUpgradePanel.SetActive(true);
        UpdateCannonPanelInformation();
    }

    public void PlayerGatePanel()
    {
        if(panelOpen) return;
        asource.Play();
        panelOpen = true;
        playerGatePanel.SetActive(true);
        UpdateHealthPanelInformation();
    }


    public void UpgradeSkillButton()
    {   
        asource.Play();
        GameDataManager.instance.UpgradeCurrentSelectedSkill();
          //After upgrade update the ui.
        UpdateSkillPanelInformation();
        UpdateCoinValueText();
    }

    public void UpgradeCannonDamageButton()
    {
        asource.Play();
        GameDataManager.instance.UpgradePlayerCannonDamage();

        UpdateCannonPanelInformation();
        UpdateCoinValueText();
    }

    public void UpgradeProjectileCountButton()
    {
        asource.Play();
        GameDataManager.instance.UpgradeProjectileCount();

        UpdateCannonPanelInformation();
        UpdateCoinValueText();
    }

    public void UpgradePlayerHealthButton()
    {
        asource.Play();
        GameDataManager.instance.UpgradePlayerHealth();

        UpdateHealthPanelInformation();
        UpdateCoinValueText();
    }

    public void PanelCloseButton(GameObject panel)
    {
        asource.Play();
        panel.SetActive(false);
        panelOpen = false;
    }

    public void SelectThunderSkill()
    {
        GameDataManager.instance.SetPlayerAbility(Abilities.AbilityTypes.thunderSkill);
        UpdateSkillPanelInformation();
    }

    public void SelectRockSkill()
    {
        GameDataManager.instance.SetPlayerAbility(Abilities.AbilityTypes.rockSkill);
        UpdateSkillPanelInformation();
    }

    public void UpdateSkillPanelInformation()
    {
        //Deactivate everything at first.
        thunderSelectionBack.SetActive(false);
        rockSelectionBack.SetActive(false);
        thunderExpText.gameObject.SetActive(false);
        rockExpText.gameObject.SetActive(false);
        skillUpgradeText.SetActive(false);
        skillMaxImage.SetActive(false);
        skillCoinValueText.gameObject.transform.parent.gameObject.SetActive(false);

        //Update both explanations text.
        //Rock throw skill explanation update.
        rockExpText.text  = string.Format("Spawn rocks for every line and deal {0}* damage to enemies hit with it",GameDataManager.instance.GetRockSkillDamage());
        thunderExpText.text = string.Format("Deal {0}* damage to all enemies",GameDataManager.instance.GetThunderSkillDamage());

        // Learn which skill selected and activate or deactivate selections and explanations.
        Abilities.AbilityTypes selectedType = GameDataManager.instance.GetSelectedAbility().abilityType;
        switch(selectedType)
        {
            case Abilities.AbilityTypes.thunderSkill:
            thunderSelectionBack.SetActive(true);
            thunderExpText.gameObject.SetActive(true);
            // Learn skill is maxed or not.
            Stats thunderStats = GameDataManager.instance.GetThunderSkillData(out bool thunderisMaxed);
            if(thunderisMaxed)
            {
                skillMaxImage.SetActive(true);

            }
            else
            {
                // Activate max image only if it's maxed or change value of coin for the selected skill.
                skillUpgradeText.SetActive(true);
                skillCoinValueText.text = thunderStats.costValue.ToString();
                if(GameDataManager.instance.IsGoldEnough(thunderStats.costValue))
                    skillCoinValueText.color = normalTextColor;
                else skillCoinValueText.color = redTextColor;
                skillCoinValueText.gameObject.transform.parent.gameObject.SetActive(true);
            }
            break;

            case Abilities.AbilityTypes.rockSkill:
            rockSelectionBack.SetActive(true);
            rockExpText.gameObject.SetActive(true);
            // Learn skill is maxed or not.
            Stats rockStats = GameDataManager.instance.GetRockSkillData(out bool rockisMaxed);
            if(rockisMaxed)
            {
                skillMaxImage.SetActive(true);

            }
            else
            {
                // Activate max image only if it's maxed or change value of coin for the selected skill.
                skillUpgradeText.SetActive(true);
                skillCoinValueText.text = rockStats.costValue.ToString();
                if(GameDataManager.instance.IsGoldEnough(rockStats.costValue))
                    skillCoinValueText.color = normalTextColor;
                else skillCoinValueText.color = redTextColor;
                skillCoinValueText.gameObject.transform.parent.gameObject.SetActive(true);
            }
            break;
        }   
    }

    public void UpdateCannonPanelInformation()
    {
        cannonUpgradeText.SetActive(false);
        projectileCountUpgradeText.SetActive(false);
        cannonCoinValueText.transform.parent.gameObject.SetActive(false);
        proCountCoinValueText.transform.parent.gameObject.SetActive(false);
        cannonMaxImage.SetActive(false);
        projectileMaxImage.SetActive(false);

        //Check cannon skill max or not and update the necessary data.
        Stats cannonStats = GameDataManager.instance.GetCannonData(out bool cannonisMaxed);
        if(cannonisMaxed)
        {
            cannonMaxImage.SetActive(true);
            
        }
        else
        {
            cannonUpgradeText.SetActive(true);
            cannonCoinValueText.text = cannonStats.costValue.ToString();
            if(GameDataManager.instance.IsGoldEnough(cannonStats.costValue))
                    cannonCoinValueText.color = normalTextColor;
                else cannonCoinValueText.color = redTextColor;
            cannonCoinValueText.transform.parent.gameObject.SetActive(true);
            
        }
        cannonDamageText.text = cannonStats.statData.ToString();

        //Check projectile count skill max or not and update the necessary data.
        Stats projectileStats = GameDataManager.instance.GetProjectileData(out bool projectileisMaxed);
        if(projectileisMaxed)
        {
            projectileMaxImage.SetActive(true);
        }

        else
        {
            projectileCountUpgradeText.SetActive(true);
            proCountCoinValueText.text = projectileStats.costValue.ToString();
            if(GameDataManager.instance.IsGoldEnough(projectileStats.costValue))
                    proCountCoinValueText.color = normalTextColor;
                else proCountCoinValueText.color = redTextColor;
            proCountCoinValueText.transform.parent.gameObject.SetActive(true);
        }

        projectileCountText.text = projectileStats.statData.ToString();
    }

    public void UpdateHealthPanelInformation()
    {
        healthUpgradeText.SetActive(false);
        healthCoinValueText.transform.parent.gameObject.SetActive(false);
        maxImageText.SetActive(false);

        Stats healthStats = GameDataManager.instance.GetPlayerHealthData(out bool ismaxed);

        if(ismaxed)
        {
            maxImageText.SetActive(true);
        }
        else
        {
            healthUpgradeText.SetActive(true);
            healthCoinValueText.text = healthStats.costValue.ToString();
            if(GameDataManager.instance.IsGoldEnough(healthStats.costValue))
                    healthCoinValueText.color = normalTextColor;
                else healthCoinValueText.color = redTextColor;
            healthCoinValueText.transform.parent.gameObject.SetActive(true);
        }

        healthValueText.text = healthStats.statData.ToString();
    }

    public void UpdateCoinValueText()
    {
        coinValueText.text = GameDataManager.instance.PlayerCoin.ToString();
    }

}
