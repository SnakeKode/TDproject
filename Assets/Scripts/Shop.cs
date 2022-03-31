using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager bm;
    public Text bomberCost;
    public Text turretCost;
    public Text beamerCost;
    public Blueprints turret;
    public Blueprints bomber;
    public Blueprints beamer;

    void Start()
    {
        bm = BuildManager.instance;
        turretCost.text = "$" + turret.cost;
        bomberCost.text = "$" + bomber.cost;
        beamerCost.text = "$" + beamer.cost;
    }



    public void SelectTurret()
    {
        Debug.Log("Turret selected");
        bm.SelectBuilding(turret);
    }

    public void SelectBomber()
    {
        Debug.Log("Bomber selected");
        bm.SelectBuilding(bomber);
    }

    public void SelectBeamer()
    {
        Debug.Log("Beamer Selected");
        bm.SelectBuilding(beamer);
    }

    public void Build ()
    {
        if (bm.selectedNode.turret != null)
            return;
        
        if (bm.CanBuild)
        {
            bm.Build();
            bm.deselectNode();
        }
    }

    public void Cancel()
    {
        bm.deselectNode();
    }

}
