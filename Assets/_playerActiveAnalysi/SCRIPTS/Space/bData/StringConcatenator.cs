using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _playerActiveAnalysi.SCRIPTS.Space.bData
{
    public class StringConcatenator : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string ConcatenateStrings(List<string> stringsToConcatenate)
        {
            StringBuilder resultBuilder = new StringBuilder();
            foreach (var str in stringsToConcatenate)
            {
                resultBuilder.Append(str);
            }

            return resultBuilder.ToString();
        }
    }
}