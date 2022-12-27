using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public void MoveToScene(int SceneID)
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadScene(SceneID);
    }
}
