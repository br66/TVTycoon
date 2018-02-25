using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Vision
{
    public class Program : ScriptableObject
    {
        public enum ProgramType
        {
            Show,
            Movie,
            Sports
        };

        public string Name { get; set; }
        public ProgramType Type { get; set; }
        public float Rating { get; set; }
        public Network InitalNetwork { get; set; }
        public Network CurrentNetwork { get; set; }

    }
}