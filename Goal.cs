using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public FaderManager faderManager;

	void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene(0);
        faderManager.FadeIn();
    }
}
