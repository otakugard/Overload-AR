using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    //public GameObject Panels;

    public void GotoScene(string Going) {
        SceneManager.LoadScene(Going);
    }

    public void GotoPanel(GameObject namedPanels)
    {
        namedPanels.SetActive(true);
    }
}
