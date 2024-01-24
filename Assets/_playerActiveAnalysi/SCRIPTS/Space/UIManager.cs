using UnityEngine;

namespace _playerActiveAnalysi.SCRIPTS.Space
{
    public class UIManager : MonoBehaviour
    {
        public static void CompleteSequence(GameObject gameObj, bool state)
        {
            gameObj.SetActive(state);
        }
    }
}