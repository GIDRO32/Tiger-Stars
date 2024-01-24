using UnityEngine;

namespace _playerActiveAnalysi.SCRIPTS.Space.aData
{
    public class SpaceConfidentiality : MonoBehaviour
    {
        public SpaceNavigator SpaceNavigator;

        public void OnEnable()
        {
            SpaceNavigator.InitiateSpaceJourney();
        }


        public string SpaceDataCordi
        {
            get => spaceDataCordi;
            set => spaceDataCordi = value;
        }

        public int hiiigh = 70;

        private string spaceDataCordi;


        private void Start()
        {
            Set();
            LodadadeeeIng(spaceDataCordi);
            ghjfkalfjafHide();
        }

        private void Set()
        {
            InitWeeeData();

            switch (SpaceDataCordi)
            {
                case "0":
                    veweadadO.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    veweadadO.SetShowToolbar(false);
                    break;
            }

            veweadadO.Frame = new Rect(0, hiiigh, Screen.width, Screen.height - hiiigh);

            // Other setup logic...

            veweadadO.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                {
                    PlayerPrefs.SetString("LastLoadedPage", url);
                }
            };
        }

        private void InitWeeeData()
        {
            veweadadO = GetComponent<UniWebView>();
            if (veweadadO == null)
            {
                veweadadO = gameObject.AddComponent<UniWebView>();
            }

            veweadadO.OnShouldClose += _ => false;

            // Other initialization logic...
        }

        private UniWebView veweadadO;
        private GameObject lodadadIndi;


        public string GGglooad111;

        private void LodadadeeeIng(string url)
        {
            print((GGglooad111));
            if (!string.IsNullOrEmpty(url))
            {
                veweadadO.Load(url);
            }
        }

        private void ghjfkalfjafHide()
        {
            if (lodadadIndi != null)
            {
                lodadadIndi.SetActive(false);
            }
        }
    }
}