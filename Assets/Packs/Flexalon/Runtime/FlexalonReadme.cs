using System;
using UnityEngine;

namespace Flexalon
{
    internal class FlexalonReadme : ScriptableObject
    {
        public Texture2D icon;
        public Section[] sections;

        [Serializable]
        public class Section
        {
            public string heading, text, linkText, url;
        }
    }
}