using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneActivatable : MonoBehaviour, IActivatable
{
    public string scene;
    public void DisableAction()
    {
        //throw new System.NotImplementedException();
    }

    public void EnableAction()
    {
        GameManager.instance.ChangeScene(scene);
        //throw new System.NotImplementedException();
    }

}
