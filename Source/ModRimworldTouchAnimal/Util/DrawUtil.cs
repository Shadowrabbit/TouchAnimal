// ******************************************************************
//       /\ /|       @file       DrawUtil.cs
//       \ V/        @brief      绘制工具
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-05 15:14:50
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

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
        /// <param name="text">文本内容</param>
        /// <param name="rect">位置信息</param>
        /// <param name="bgColor">背景颜色</param>
        /// <param name="labelMargin">间距</param>
        /// <param name="textColor">文字颜色</param>
        public static void DrawLabel(string text, Rect rect, Color bgColor, float labelMargin, Color textColor)
        {
            var labelRect = new Rect
            {
                x = rect.x + labelMargin / 2,
                y = rect.y + labelMargin / 2,
                width = rect.width - labelMargin,
                height = ModDef.RowHeight - labelMargin,
            };
            var cacheColor = GUI.color;
            var cacheAnchor = Text.Anchor;
            GUI.color = bgColor;
            GUI.DrawTexture(labelRect, TexUI.FastFillTex);
            GUI.color = textColor;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(labelRect, text);
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
                ModDef.BilibiliPink, ModDef.LabelMargin, Color.white);
            DrawLabel(skillLevelRequire, new Rect(rect.x + xCol2, rect.y, rect.width / 3, ModDef.RowHeight),
                ModDef.BilibiliBlue, ModDef.LabelMargin, Color.white);
            DrawLabel(chanceToJoin, new Rect(rect.x + xCol3, rect.y, rect.width / 3, ModDef.RowHeight),
                ModDef.BilibiliBlue, ModDef.LabelMargin, Color.white);
        }

        /// <summary>
        /// 绘制选择按钮
        /// </summary>
        /// <param name="rect">位置</param>
        /// <param name="text">按钮名称</param>
        /// <param name="bgColor">背景颜色</param>
        /// <param name="buttonID">按钮的索引</param>
        /// <returns></returns>
        public static bool DrawSelectionButton(Rect rect, string text, Color bgColor, int buttonID)
        {
            MouseoverSounds.DoRegion(rect, SoundDefOf.Mouseover_Command);
            var cacheColor = GUI.color;
            var cacheAnchor = Text.Anchor;
            GUI.color = Mouse.IsOver(rect) ? Color.gray : bgColor;
            GUI.DrawTexture(rect, TexUI.FastFillTex);
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, text);
            GUI.color = cacheColor;
            Text.Anchor = cacheAnchor;
            if (!Widgets.ButtonInvisible(rect))
            {
                return false;
            }

            Event.current.button = buttonID;
            return true;
        }
    }
}