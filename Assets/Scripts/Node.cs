using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    private Renderer rend;
    private Color defaultColor;

    public Color NEMcolor;                          //not enough money Color
    public Color selectColor;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public Blueprints nodeBlueprint;
    [HideInInspector]
    public GameObject turret;


    public Vector3 Offset;
    BuildManager bm;

    private Vector3 clickPos;                                  //variable to store where the player clicked

    void Start()
    {
        rend = GetComponent<Renderer>();                       //set the node's renderer as the variable rend
        defaultColor = rend.material.color;                    //set defaultColor variable to the color that the renderer starts with
        bm = BuildManager.instance;                            //set the bm as the build manager instance
    }

    

    void OnMouseDown()
    {
        clickPos = Input.mousePosition;
    }

    

    void OnMouseUp()
    {
        

            if (EventSystem.current.currentSelectedGameObject != null)
                return;                                         //exit method if there's an object between the cursor and the node



        if (Input.mousePosition != clickPos)
            return;


        bm.selectNode(this);
        return;  

    }





    //Coloring
    public void selectedColor()
    {
        rend.material.color = selectColor;                   //change material to selectColor
        
    }


    public void NEM()
    {
        rend.material.color = NEMcolor;                     //change material to NEM color
    }


    public void noColor()
    {
        rend.material.color = defaultColor;                 //change the material to default color
    }



}
