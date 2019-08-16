using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS; // import ARKit 2 Plugin
using Photon.Pun;
using Photon.Realtime;

public class FirePrefabsAction : MonoBehaviour
{
    public float speed;
    public int destroyTime;
    public GameObject contactPrefab;

    private UnityARSessionNativeInterface m_session; // Using ARKit 2 Interfaces

    void Start() {

    }

    void Update() {

        transform.position += transform.forward * (speed * Time.deltaTime);
        Destroy(gameObject, destroyTime);

    }

    void OnCollisionEnter(Collision firePrefabCollision)
	{

        if (firePrefabCollision.gameObject.name == "PlanetPrefab"){

            speed = 0;

            ContactPoint contact = firePrefabCollision.contacts[0];
            Quaternion contactPrefabRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 contactPrefabPostition = contact.point;

            GameObject networkedPrefab = PhotonNetwork.Instantiate(contactPrefab.name, contactPrefabPostition, contactPrefabRotation);
            networkedPrefab.transform.SetParent(GameObject.Find("PlanetPrefab").transform);
            Debug.Log("Name is: " + contactPrefab.name);
            PhotonNetwork.Destroy(gameObject);

        } else {

            speed = 0;
            PhotonNetwork.Destroy(gameObject);
            Debug.Log("firePrefabCollision Destroyed");

        }
	}


}