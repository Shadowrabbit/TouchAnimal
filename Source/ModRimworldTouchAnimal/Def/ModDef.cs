// ******************************************************************
//       /\ /|       @file       ModDef.cs
//       \ V/        @brief      模组相关定义
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-04 00:00:04
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using UnityEngine;

namespace SR.ModRimWorldTouchAnimal
{
    public static class ModDef
    {
        public const float LabelMargin = 1f; //标题间距
        public const float RowHeight = 40f; //每行高度
        public static readonly Color BilibiliPink = new Color(0.984f, 0.447f, 0.6f, 1f);
        public static readonly Color BilibiliBlue = new Color(0.137f, 0.769f, 0.898f, 1f);
        public static readonly Color MouseOverColor = new Color(0.6f, 0.6f, 0.4f, 1f);
        public const float MaxHealthScale = 15f; //以敲击兽的8f为基础假想的最高生命基准数
        public const int MinSkillRequire = 5; //最小技能需求等级
        public const int MaxSkillRequire = 20; //最大技能需求等级
    }
}