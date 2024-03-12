using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class InteractableMaterial
    {
        public InteractableMaterial(Material material)
        {
            this.material = material;
        }

        public float HighlightStrength
        {
            get => material.GetFloat(HIGHLIGHTSTRENGTHFIELDNAME);
            set
            {
                float finalValue = Mathf.Clamp(value, 0.0f, 1.0f);
                material.SetFloat(HIGHLIGHTSTRENGTHFIELDNAME, finalValue);
            }
        }



        private Material material;

        private static readonly string HIGHLIGHTSTRENGTHFIELDNAME = "_HighlightStrength";
    }
}