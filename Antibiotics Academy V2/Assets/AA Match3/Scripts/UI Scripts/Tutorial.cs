using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [System.Serializable]
    public class Tutorial                //a class to store sentences 
    {
        [TextArea(3, 10)]
        public string[] sentences;
    }

}