using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] int sceneID;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneID);
    }
}
