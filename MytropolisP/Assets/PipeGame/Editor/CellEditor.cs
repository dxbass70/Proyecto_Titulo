using UnityEngine;
using System.Collections;
using UnityEditor;

namespace MadFireOn
{
    [CustomEditor(typeof(CellScript))]
    public class CellEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CellScript cell = target as CellScript;

            cell.GetName();
            cell.GenerateNormalPipe();
            cell.GenerateHintPipe();

        }
    }
}//namespace MadFireOn
