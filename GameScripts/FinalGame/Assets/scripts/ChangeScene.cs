using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ChangeScene : MonoBehaviour
{
    public void SceneLoader(int SceneIndex){ //takes in the next scene's number as input
        SceneManager.LoadScene(SceneIndex);
    }
}
