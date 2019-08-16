//README: Set Diss / Like GameObject tag first to meaning of SB.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ScoreBarManagerScript : MonoBehaviour
{

    public Image scoreBarImage;
    public int maximumPrefabRange;
    public int explodingForceStrength;
    public int explodingDestoryTime;

    float imageMean;
    float likeAmount;
    float dissAmount;
    GameObject allPrefabs;

    public void FirePrefabeExploding()
    {

        //Merging all GameObject on PlanetPrefab in same array allArray
        GameObject[] likeArray;
        GameObject[] dissArray;
        GameObject[] allArray;
        likeArray = GameObject.FindGameObjectsWithTag("Like");
        dissArray = GameObject.FindGameObjectsWithTag("Diss");
        allArray = likeArray.Concat(dissArray).ToArray();

        //Adding Rigidbody / Photon components to allArray
        foreach (GameObject allPrefabs in allArray)
        {

            //Rigidbody
            Rigidbody allPrefabsRB = allPrefabs.gameObject.AddComponent<Rigidbody>();

            //Networking
            PhotonView allPrefabsPhoton = allPrefabs.AddComponent<PhotonView>();
            PhotonTransformView allPrefabsTranPhoton = allPrefabs.AddComponent<PhotonTransformView>();
            PhotonRigidbodyView allPrefabsRBPhoton = allPrefabs.gameObject.AddComponent<PhotonRigidbodyView>();

            //Setting Components before add AddComponent

            /*allPrefabsPhoton.ObservedComponents.Add(allPrefabsTranPhoton);
            allPrefabsPhoton.ObservedComponents.Add(allPrefabsRBPhoton);*/

            allPrefabsRB.AddForce(transform.up * explodingForceStrength, ForceMode.Impulse);
            allPrefabsRB.useGravity = true;
            allPrefabsRBPhoton.m_SynchronizeVelocity = true;
            allPrefabsRBPhoton.m_SynchronizeAngularVelocity = true;

            //Destroy allPrefabs in any server with delay
            Destroy(allPrefabs, explodingDestoryTime);
            //PhotonNetwork.Destroy(allPrefabs);


        }

    }



    // Use this for initialization
    void Start()
    {

        if (scoreBarImage != null)
        {
            scoreBarImage.fillAmount = 0.5f; // Set Bar Length to medium
        } // Set Bar Length to medium

    }

    // Update is called once per frame
    void Update()
    {

        likeAmount = (float)GameObject.FindGameObjectsWithTag("Like").Length;
        dissAmount = (float)GameObject.FindGameObjectsWithTag("Diss").Length;

        if (likeAmount != 0f)
        {

            imageMean = (likeAmount / (likeAmount + dissAmount));
            scoreBarImage.fillAmount = imageMean;

            if (/*imageMean == 0f*/likeAmount > 30 && (likeAmount + dissAmount) >= maximumPrefabRange) {
                FirePrefabeExploding();
            } else if (/*imageMean == 1f*/dissAmount > 30 && (likeAmount + dissAmount) >= maximumPrefabRange) {
                FirePrefabeExploding();
            }



        }

        //Debug.Log("likeAmount" + likeAmount + " dissAmount" + dissAmount);
    }
}
