using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using Photon.Pun;
using Photon.Realtime;

public class ARImageAnchorManager : MonoBehaviour
{
    [SerializeField]
    private float planetHeight;

    [SerializeField]
    private float planetScale;

    [SerializeField]
    private ARReferenceImage referenceImage;

    public GameObject imageAnchorGO;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;

    }

    void UpdateImageAnchor(ARImageAnchor arImageAnchor) {
        Debug.LogFormat("image anchor updated[{0}] : tracked => {1}", arImageAnchor.identifier, arImageAnchor.isTracked);
        if (arImageAnchor.referenceImageName == referenceImage.imageName)
        {
            if (arImageAnchor.isTracked)
            {
                if (!imageAnchorGO.activeSelf) {
                    imageAnchorGO.SetActive(true);
                }

                imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform) + new Vector3 (0f, planetHeight, 0f);
                imageAnchorGO.transform.localScale = new Vector3 (planetScale, planetScale, planetScale);
                imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
            }
        }

    }
}
