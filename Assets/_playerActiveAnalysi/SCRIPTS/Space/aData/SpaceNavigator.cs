using UnityEngine;

namespace _playerActiveAnalysi.SCRIPTS.Space.aData
{
    public class SpaceNavigator : MonoBehaviour
    {
        public void InitiateSpaceJourney()
        {
            UniWebView.SetAllowInlinePlay(true);

            var spaceAudios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var audioElement in spaceAudios)
            {
                audioElement.Stop();
            }

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}