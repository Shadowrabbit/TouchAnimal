// ******************************************************************
//       /\ /|       @file       ThinkNodeChancePerHourTouchAnimal.cs
//       \ V/        @brief      行为树条件节点 每小时触发抚摸动物的概率
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-04 00:22:15
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchAnimal
{
    [UsedImplicitly]
    public class ThinkNodeChancePerHourTouchAnimal : ThinkNode_ChancePerHour
    {
        private const float MtbHoursTouchAnimal = 20f;

        /// <summary>
        /// 每小时N分之一的概率触发
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        protected override float MtbHours(Pawn pawn)
        {
            return MtbHoursTouchAnimal;
        }
    }
}