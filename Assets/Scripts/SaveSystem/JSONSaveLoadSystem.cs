using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSaveLoadSystem : MonoBehaviour
{
    public int srcNumbL1 = 0;
    public string helpL1 = "";
    public int srcNumbL2 = 0;
    public string helpL2 = "";
    public int awardL3 = 0;
    public int awardL4 = 0;
    public int timeLimitL5 = 0;
    public int awardL6 = 0;
    public int wrongLimitL7 = 0;
    public int awardL8 = 0;
    public int wrongLimitL9 = 0;
    public int timeLimitL10 = 0;
    public int timeLimitL11 = 0;
    public bool l1act = true;
    public bool l2act = true;
    public bool l34act = true;
    public bool l5act = true;
    public bool l6act = true;
    public bool l7act = true;
    public bool l8act = true;
    public bool l9act = true;
    public bool l10act = true;
    public bool l11act = true;

    public ConfigurableData confData;

    private void Start()
    {
        LoadFromJSON();
    }

    public void LoadFromJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/ConfigurationData.json");
        ConfigurableData configurableData = JsonUtility.FromJson<ConfigurableData>(json);

        srcNumbL1 = configurableData.srcNumbL1;
        helpL1 = configurableData.helpL1 ;
        srcNumbL2 = configurableData.srcNumbL2;
        helpL2 = configurableData.helpL2 ;
        awardL3 = configurableData.awardL3;
        awardL4 = configurableData.awardL4;
        timeLimitL5 = configurableData.timeLimitL5;
        awardL6 = configurableData.awardL6;
        wrongLimitL7 = configurableData.wrongLimitL7;
        awardL8 = configurableData.awardL8;
        wrongLimitL9 = configurableData.wrongLimitL9 ;
        timeLimitL10 = configurableData.timeLimitL10;
        timeLimitL11 = configurableData.timeLimitL11;
        l1act = configurableData.l1act;
        l2act = configurableData.l2act;
        l34act = configurableData.l34act;
        l5act = configurableData.l5act;
        l6act = configurableData.l6act;
        l7act = configurableData.l7act;
        l8act = configurableData.l8act;
        l9act = configurableData.l9act;
        l10act = configurableData.l10act;
        l11act = configurableData.l11act;
    }
}
