using System;
using System.Collections.Generic;

using JsonMap.Math;

namespace JsonMap.Data
{
    public struct CharacterAgentData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Vector3 Position { get; set; }
        public float Weight { get; set; }
    }
}
