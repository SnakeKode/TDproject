using UnityEngine;

public class cameraController : MonoBehaviour
{
    Vector3 touchStart;
    public Node selectedNode;
    private bool fixCam = false;
    public float groundY = 0f;
    public Camera Cam;
    public float Speed = 1f;
    public float zoomMag = 1000f;
    public Vector2 zoomBound = new Vector2(0f, 0f);
    public Vector2 Xbound = new Vector2(0f, 80f);
    public Vector2 Zbound = new Vector2(-40f, 35f);
    public Vector3 selectionOffset = new Vector3(0, 10f,0);


    private Vector3 defPos;

    void Start()
    {
        defPos = Camera.main.transform.position;
    }
    void Update()
    {


        if (fixCam)
            return;                             //if fixCam is true than don't go into this script

         if (Input.GetMouseButtonDown(0))
        {
            touchStart = GetWorldPosition(groundY);     //when the left mouse button is pushed we register the cursor's position in the relation to groundY
        }

         

        if (Input.GetMouseButton(0))
        {
            
            Vector3 dir = touchStart - GetWorldPosition(groundY);
            
            
            Vector3 nextPos = Camera.main.transform.position + dir * Speed;
            nextPos.x = Mathf.Clamp(nextPos.x, Xbound.x, Xbound.y);
            nextPos.z = Mathf.Clamp(nextPos.z, Zbound.x, Zbound.y);


            Camera.main.transform.position = nextPos;
            
        }


        zoom(Input.GetAxis("Mouse ScrollWheel"));

    }

    void zoom(float f)
    {
        Vector3 pos = Cam.transform.position;
        pos.y -= f * zoomMag * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, zoomBound.x, zoomBound.y);

        Cam.transform.position = pos;
    }

    private Vector3 GetWorldPosition(float y)
    {
        Ray mousePos = Cam.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    public void CameraOn(Node n)
    {

        Camera.main.transform.position = (n.transform.position + selectionOffset);
        fixCam = true;
        
    }

    public void freeCam()
    {
        fixCam = false;
        Camera.main.transform.position = defPos;
    }

}
