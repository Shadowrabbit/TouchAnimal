// ******************************************************************
//       /\ /|       @file       DrawUtil.cs
//       \ V/        @brief      绘制工具
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-05 15:14:50
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Globalization;
using HugsLib.Settings;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    public static class DrawUtil
    {
        public static void DrawPawnKindInfo()
        {
        }

        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="bgRect"></param>
        /// <param name="color"></param>
        public static void DrawBg(Rect bgRect, Color color)
        {
            var cacheColor = GUI.color;
            GUI.color = color;
            GUI.DrawTexture(bgRect, TexUI.FastFillTex);
            GUI.color = cacheColor;
        }

        /// <summary>
        /// 绘制标签
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="rect"></param>
        /// <param name="bgColor"></param>
        private static void DrawLabel(string labelText, Rect rect, Color bgColor)
        {
            var labelRect = new Rect
            {
                x = rect.x + ModDef.LabelMargin / 2,
                y = rect.y + ModDef.LabelMargin / 2,
                width = rect.width - ModDef.LabelMargin,
                height = ModDef.RowHeight - ModDef.LabelMargin,
            };
            var cacheColor = GUI.color;
            var cacheAnchor = Text.Anchor;
            GUI.color = bgColor;
            GUI.DrawTexture(labelRect, TexUI.FastFillTex);
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(labelRect, labelText);
            GUI.color = cacheColor;
            Text.Anchor = cacheAnchor;
        }

        /// <summary>
        /// 绘制一行抚摸信息
        /// </summary>
        /// <param name="rect">这一行的位置</param>
        /// <param name="pawnKindName">动物种类名称</param>
        /// <param name="skillLevelRequire">需要的驯兽等级</param>
        /// <param name="chanceToJoin">加入殖民者阵营的几率</param>
        /// <returns></returns>
        public static void DrawTouchInfoRow(Rect rect, string pawnKindName, string skillLevelRequire,
            string chanceToJoin)
        {
            const float xCol1 = 0;
            var xCol2 = xCol1 + rect.width / 3;
            var xCol3 = xCol2 + rect.width / 3;
            DrawLabel(pawnKindName, new Rect(rect.x + xCol1, rect.y, rect.width / 3, ModDef.RowHeight),
                ModDef.BilibiliPink);
            DrawLabel(skillLevelRequire, new Rect(rect.x + xCol2, rect.y, rect.width / 3, ModDef.RowHeight),
                ModDef.BilibiliBlue);
            DrawLabel(chanceToJoin, new Rect(rect.x + xCol3, rect.y, rect.width / 3, ModDef.RowHeight),
                ModDef.BilibiliBlue);
        }
    }
}