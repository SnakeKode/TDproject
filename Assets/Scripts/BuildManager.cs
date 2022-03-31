using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public cameraController cm;
    public GameObject ShopUI;
    public GameObject UpgradesUI;
    public GameObject PowersUI;

    public Upgrades upgrades;

    //AWAKE** check if the instance of the BM is set then set it to this
    void Awake()
    {  
        if(instance != null)
        {
            Debug.LogError("MORE THAN ONE BM IN SCENE");        //if an instance already exists then we have a technical issue
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Manager.GameEnded)
        {
            PowersUI.SetActive(false);
            ShopUI.SetActive(false);
            UpgradesUI.SetActive(false);
        }
    }

    public GameObject buildEffect;

    private Blueprints Building;

    public Node selectedNode;
    

    public bool CanBuild { get { return Building != null; } }                   //property that can never be set
    public bool HasMoney { get { return Stats.money >= Building.cost; } }       //check if player has enough money for building



    public void selectNode(Node node) 
    {
        //Color Control
        if (selectedNode != null)
        {
            selectedNode.noColor();
        }
        
        selectedNode = node;
        selectedNode.selectedColor();

        //fixing camera
        cm.CameraOn(selectedNode);

        

        if (node.turret == null)
        {
            
            UpgradesUI.SetActive(false);
            ShopUI.SetActive(true);
            PowersUI.SetActive(false);
        }
        else
        {
            Building = selectedNode.nodeBlueprint;
            UpgradesUI.SetActive(true);
            upgrades.setOn(selectedNode);
            ShopUI.SetActive(false);
            PowersUI.SetActive(false);
            
        }
    }

    public void deselectNode()
    {
        selectedNode.noColor();
        selectedNode = null;
        Building = null;
        cm.freeCam();
        ShopUI.SetActive(false);
        UpgradesUI.SetActive(false);
        PowersUI.SetActive(true);
    }

    public void Build()
    {
        //if the player's money is not enough to buy the turret than show a message and exit from the script
        if (!HasMoney)
        {
            Debug.Log("Not enough cash...Stranger.");
            return;
        }

        GameObject turret = (GameObject)Instantiate(Building.prefab, selectedNode.transform.position + selectedNode.Offset, Quaternion.identity);     //build the turret
        GameObject effect = (GameObject)Instantiate(buildEffect, selectedNode.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Stats.money -= Building.cost;                      //reduce cost from current money
        Stats.Score += Building.scoreWorth;                 //add scoreWorth to score
        selectedNode.turret = turret;                      //set turret property on node to the built turret
        selectedNode.nodeBlueprint = Building;            //set node blueprint to selected blueprint
    }

    public void Upgrade()
    {
        if (Stats.money < Building.upgradeCost)
        {
            Debug.Log("Not enough cash...Stranger.");
            return;
        }
        
        
        //destroy old tower prefab
        Destroy(selectedNode.turret);


        //build upgraded tower
        GameObject turret = (GameObject)Instantiate(Building.upgradedPrefab, selectedNode.transform.position + selectedNode.Offset, Quaternion.identity);     //build the turret
        GameObject effect = (GameObject)Instantiate(buildEffect, selectedNode.transform.position, Quaternion.identity);
        Stats.money -= Building.upgradeCost;                      //reduce upgradeCost from current money
        Stats.Score += Building.upgradeScoreWorth;
        selectedNode.turret = turret;
        selectedNode.isUpgraded = true;                           //set isUpgraded on node to true
    }

   
    public void DestroyBuilding()
    {
        Destroy(selectedNode.turret);
        Stats.money += Building.sellCost;
    }


    public void SelectBuilding(Blueprints turret)
    {
        Building = turret;                    //used with Shop script to get the turret info
        if (!HasMoney)
        {
            selectedNode.NEM();
        }
    }

    public Blueprints getBuilding()
    {
        return Building;
    }
}
