using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _playerActiveAnalysi.SCRIPTS
{
    public class LoadingByTime : MonoBehaviour
    {
        public Slider loadingSlider;
        public string sceneToLoad;
        private float loadingTime = 8f;

        void Start()
        {
            StartCoroutine(LoadSceneAfterDelay());
        }

        IEnumerator LoadSceneAfterDelay()
        {
            float elapsedTime = 0;

            while (elapsedTime < loadingTime)
            {
                elapsedTime += Time.deltaTime;
                loadingSlider.value = elapsedTime / loadingTime;
                yield return null;
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}