using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    BuildManager bm;

    public GameObject ui;

    private Blueprints built;

    public Text UpgradeCost;
    public Text SellCost;
    public Button upButton;

    void Start()
    {
        bm = BuildManager.instance;
    }



    public void Sell()
    {
        bm.DestroyBuilding();
        bm.deselectNode();
    }

    public void Upgrade()
    {
        bm.Upgrade();
        bm.deselectNode();
    }

    public void Cancel()
    {
        bm.deselectNode();
    }

    public void setOn(Node n)
    {
        built = n.nodeBlueprint;
        SellCost.text = "$" + built.sellCost;

        if (!n.isUpgraded)
        {
            upButton.interactable = true;
            UpgradeCost.text = "$" + built.upgradeCost;
        }else
        {
            upButton.interactable = false;
            UpgradeCost.text = "DONE";
        }
        
        
    }


}
