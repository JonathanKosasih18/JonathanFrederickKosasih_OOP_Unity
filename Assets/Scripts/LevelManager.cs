using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        animator.enabled = false;
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.enabled = true;
        animator.SetTrigger("EndTransition");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("StartTransition");
        SceneManager.LoadSceneAsync(sceneName);

        if (sceneName == "Main")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                Player.Instance.transform.position = new Vector3(0, -3, 0);
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
