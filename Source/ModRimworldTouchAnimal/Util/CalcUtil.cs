// ******************************************************************
//       /\ /|       @file       CalcUtil.cs
//       \ V/        @brief      计算工具
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-05 15:22:33
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    public static class CalcUtil
    {
        /// <summary>
        /// 计算加入阵营的概率
        /// </summary>
        /// <param name="currentSkillLevel">当前技能等级</param>
        /// <param name="baseHealthScale">基础生命缩放</param>
        /// <returns></returns>
        public static float CalcChanceToJoin(int currentSkillLevel, float baseHealthScale)
        {
            var value = (currentSkillLevel - baseHealthScale) / 200;
            return value > 0 ? value : 0;
        }

        /// <summary>
        /// 计算触发触摸行为所需的驯兽等级
        /// </summary>
        /// <param name="baseHealthScale"></param>
        /// <returns></returns>
        public static int CalcRequireSkillLevel(float baseHealthScale)
        {
            //健康缩放因数
            var healthScalingFactor = baseHealthScale / ModDef.MaxHealthScale;
            //触发抚摸行为所需要的驯兽等级
            return (int) Math.Round(Mathf.Lerp(ModDef.MinSkillRequire, ModDef.MaxSkillRequire, healthScalingFactor),
                MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 是否是动物
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool IsAnimal(PawnKindDef def) => def.race != null && def.RaceProps.Animal;
    }
}