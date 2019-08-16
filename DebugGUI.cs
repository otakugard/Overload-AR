using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGUI : MonoBehaviour {

    [SerializeField] private GameObject enterPoint;
    [SerializeField] private GameObject mainCamera;
    public UnityARCameraManager UnityARCameraManager;
    public MyWorldMapManager MyWorldMapManager;
    public ARImageAnchorManager ARImageAnchorManager;


    void Start() {
        
	}

	void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        Quaternion debugrot = UnityARCameraManager.GetMainCameraRotation();
        Vector3 planetpos = ARImageAnchorManager.imageAnchorGO.transform.position;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("mainCamera rotation" + debugrot.x + " " + debugrot.y + " " + debugrot.z);
        GUILayout.Label("Planet postition" + planetpos.x + " " + planetpos.y + " " + planetpos.z);
    }

}