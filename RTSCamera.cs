using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    //This variable is for how fast the camera moves
    public float panSpeed = 20f;
    //This variable denotes how close to the edge of the screen the mouse needs to be in order to trigger camera movement
    public float panBorderThickness = 10f;
    //This variable limits how far the camera can pan on the X & Z axes
    public Vector2 panLimit = new Vector2(40f, 40f);
    //This variable is for how fast the camera zooms in/out
    public float scrollSpeed = 20f;
    //This variable is for how far the camera can zoom in
    public float minY = 20f;
    //This variable is for how far the camera can zoom out
    public float maxY = 100f;
    //This variable is to limit how far the camera can rotate on the y axis
    public float yawLimit = 60f;
    //This variable is for how fast the camera rotates on the y axis
    public float yawSpeed = 100f;
    //This variable store the current rotation of the camera
    private float yawRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        //This variable stores the current position of the camera
        Vector3 camPosition = transform.position;

        //Move camera up
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            camPosition.z += panSpeed * Time.deltaTime; //Multiplying by Time.deltaTime ensures movement is relative to time instead of frame rate
        }
        //Move camera down
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            camPosition.z -= panSpeed * Time.deltaTime;
        }
        //Move camera right
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            camPosition.x += panSpeed * Time.deltaTime;
        }
        //Move camera left
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            camPosition.x -= panSpeed * Time.deltaTime;
        }
        //Rotate camera left
        if(Input.GetKey("q"))
        {
            yawRotation += yawSpeed * Time.deltaTime;
        }
        //Rotate camera right
        if (Input.GetKey("e"))
        {
            yawRotation -= yawSpeed * Time.deltaTime;
        }
        //Rotate camera with mouse
        if(Input.GetMouseButton(2))
        {
            if(Input.GetAxisRaw("Mouse X") < 0)
            {
                yawRotation -= yawSpeed * Time.deltaTime;
            }
            if(Input.GetAxisRaw("Mouse X") > 0)
            {
                yawRotation += yawSpeed * Time.deltaTime;
            }
        }

        //Get value of scroll wheel for zoom in/out functionality
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        //Zoom camera in/out
        camPosition.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        //Limit maximum camera movement on X
        camPosition.x = Mathf.Clamp(camPosition.x, -panLimit.x, panLimit.x);
        //Limit maximum camera movement on Z
        camPosition.z = Mathf.Clamp(camPosition.z, -panLimit.y, panLimit.y);
        //Limit the maximum zoom in/out of camera
        camPosition.y = Mathf.Clamp(camPosition.y, minY, maxY);
        //Limit the min/max camera rotation on the Y
        yawRotation = Mathf.Clamp(yawRotation, -yawLimit, yawLimit);

        //Apply changes to camera position
        transform.position = camPosition;
        //Apply changes to camera y rotation
        transform.localEulerAngles = new Vector3(60f, yawRotation, 0f);
    }
}
