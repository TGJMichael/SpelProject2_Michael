using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewNextScene : MonoBehaviour
{
    // from https://answers.unity.com/questions/1348567/disable-load-new-level-until-all-enemies-are-dead.html
    public int NeedEnemyDiedToGoNextScene;
    public int CurrentEnemyDied;
    public GameObject NextLevelLoaderObject;
    public void CheckPlayerCanGoNextLevel()
    {
        CurrentEnemyDied += 1;

        if (CurrentEnemyDied >= NeedEnemyDiedToGoNextScene)
        {
            NextLevelLoaderObject.SetActive(true);
        }
    }
}
