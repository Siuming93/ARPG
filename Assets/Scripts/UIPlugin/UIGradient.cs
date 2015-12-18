using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UIPlugin
{
    /// <summary>
    /// 颜色渐变
    /// </summary>
    [AddComponentMenu("UI/Effects/Gradient")]
    public class UIGradient : BaseVertexEffect
    {
        [SerializeField] public Color32 topColor = Color.white;
        [SerializeField] public Color32 bottomColor = Color.black;

        public override void ModifyVertices(List<UIVertex> verts)
        {
            if (!IsActive())
                return;

            float topY = float.MinValue, bottomY = float.MaxValue;

            for (int i = 0; i < verts.Count; i++)
            {
                float y = verts[i].position.y;
                if (y > topY)
                    topY = y;
                if (y < bottomY)
                    bottomY = y;
            }

            float uiElementHeight = topY - bottomY;

            for (int i = 0; i < verts.Count; i++)
            {
                UIVertex vert = verts[i];
                vert.color = Color32.Lerp(bottomColor, topColor, (vert.position.y - bottomY)/uiElementHeight);
                verts[i] = vert;
            }
        }
    }
}