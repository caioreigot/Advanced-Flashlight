using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class FirstPersonCamera : MonoBehaviour {
 
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public static float sensitivityX;
    public static float sensitivityY;
 
    public float minimumX = -360F;
    public float maximumX = 360F;
 
    public float minimumY = -90F;
    public float maximumY = 90F;

    [Header("Setup")]
    public Transform characterHead;
    public Transform characterBody;

    [HideInInspector]
    public bool isLocked = false; 
 
    float rotationY = 0F;

    // Player Ranged Raycast
    float raycastRange = 2.4f;

    GameObject targetPickable = null;
    GameObject pickableCanvas = null;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update() {
        MouseLook();
        RangeRaycast();
        HandlePickUp();
    }

    void MouseLook() {
        if (isLocked) return;

        if (axes == RotationAxes.MouseXAndY) {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
           
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
           
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

            characterBody.localEulerAngles = new Vector3(0, rotationX, 0);
        }
        
        else if (axes == RotationAxes.MouseX)
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

        else {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
           
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

    void LateUpdate() {
        transform.position = characterHead.position;
    }

    void RangeRaycast() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        targetPickable = null;

        if (Physics.Raycast(ray, out hit, raycastRange))
            if (hit.transform.gameObject.layer == 9) {
                targetPickable = hit.transform.gameObject;
            }
    }

    void HandlePickUp() {
        bool isPickableCanvasEnabled = IsPickableCanvasEnabled(pickableCanvas);

        if (targetPickable != null) {
            pickableCanvas = GetPickableCanvas();

            // Activating the World Canvas if it is not active
            if (!isPickableCanvasEnabled)
                pickableCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                Item targetItem = targetPickable.GetComponent<Pickable>().item;
                Pickable.ItemPickedEvent?.Invoke(targetItem);
                Destroy(targetPickable);
            }
        // If there is no pickable in the raycast but your canvas is active, disable it
        } else if (isPickableCanvasEnabled) {
            pickableCanvas.SetActive(false);
        }
    }

    GameObject GetPickableCanvas() {
        foreach (Transform child in targetPickable.transform)
            if (child.gameObject.layer == 10)
                return child.gameObject;

        return null;
    }

    bool IsPickableCanvasEnabled(GameObject pickableCanvas) {
        if (pickableCanvas == null) return false;
        return pickableCanvas.activeSelf;
    }

}
