using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using Photon.Pun;

public class FireManagerScript : MonoBehaviour {

    public GameObject enterPoint;
    public List<GameObject> firePrefabs = new List <GameObject>();
    //public int firePrefabsLogic;
    public UnityARCameraManager UnityARCameraManager;
    public GameObject uiLikeSwitcher;
    public GameObject uiDissSwithcer;
    private GameObject prefabsToFire;

    [SerializeField] private float firePrefabRate;
    private float firePrefabNextFire;

    void Numbers(int firePrefabsLogic)
    {

        //`int firePrefabsLogic` 0 = No, 1 = Yes
        prefabsToFire = firePrefabs[firePrefabsLogic];

    }

    void firePrefab()
    {
        //GameObject firePrefabs;
        Quaternion firePrefabRotation = UnityARCameraManager.GetMainCameraRotation();
        //firePrefabs = PhotonNetwork.Instantiate(prefabsToFire.name, enterPoint.transform.position, firePrefabRotation);
        PhotonNetwork.Instantiate(prefabsToFire.name, enterPoint.transform.position, firePrefabRotation, 0);

    }
	
	// Update is called once per frame
	void Update () {

        if (uiDissSwithcer.activeInHierarchy == true) {
            firePrefabsNoShooting();
        } else {
            firePrefabsYesShooting();
        }

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && Time.time > firePrefabNextFire)
            {

                firePrefabNextFire = Time.time + firePrefabRate;
                //Shooting `firePrefab` to AR Camera pointing position
                firePrefab();
            }
        }
	}

    public void firePrefabsNoShooting() {

        Numbers(0); // 0 = Diss

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && Time.time > firePrefabNextFire)
            {

                firePrefabNextFire = Time.time + firePrefabRate;
                //Shooting `firePrefab` to AR Camera pointing position
                firePrefab();
            }
        }
        
        if (Input.GetKey(KeyCode.Space) && Time.time > firePrefabNextFire) {
            
            firePrefabNextFire = Time.time + firePrefabRate;
            firePrefab();

        }

    }

    public void firePrefabsYesShooting() {

        Numbers(1); // 1 = Like

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began && Time.time > firePrefabNextFire)
            {

                firePrefabNextFire = Time.time + firePrefabRate;
                //Shooting `firePrefab` to AR Camera pointing position
                firePrefab();
            }
        }


        if (Input.GetKey(KeyCode.Space) && Time.time > firePrefabNextFire) {

            firePrefabNextFire = Time.time + firePrefabRate;
            firePrefab();

        }
        
    }

}
