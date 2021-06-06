// ******************************************************************
//       /\ /|       @file       JobGiverTouchAnimal.cs
//       \ V/        @brief      行为树action节点 触摸动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com	
//       /  |                    
//      /  \\        @Modified   2021-06-03 18:28:26
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchAnimal
{
    [UsedImplicitly]
    public class JobGiverTouchAnimal : ThinkNode_JobGiver
    {
        protected virtual float MaxDistanceToTouch => 20f; //最大触发距离

        /// <summary>
        /// 尝试分配工作
        /// </summary>
        /// <param name="pawn">角色</param>	
        /// <returns>工作</returns>
        protected override Job TryGiveJob(Pawn pawn)
        {
            //健康状态不好时优先休息
            if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
            {
                return null;
            }

            var animal = FindAnimal(pawn);
            //如果能找到动物 尝试分配工作触摸动物
            return animal == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchAnimal, animal);
        }

        /// <summary>
        /// 在小人当前的地图 寻找可触摸的动物
        /// </summary>
        /// <param name="pawn">角色</param>
        /// <returns>动物</returns>
        protected virtual Pawn FindAnimal(Pawn pawn)
        {
            //尝试在附近寻找指定动物
            var currentMap = pawn.Map;
            //最大距离内 最近的 满足驯兽等级的 名单内的(非玩家角色没有此限制)
            Pawn targetAnimal = null;
            var cacheDistance = -1f;
            foreach (var animal in currentMap.mapPawns.AllPawnsSpawned)
            {
                var distance = animal.Position.DistanceTo(pawn.Position);
                //距离超过上限
                if (distance > MaxDistanceToTouch)
                {
                    continue;
                }

                //不属于动物
                if (!CalcUtil.IsAnimal(animal.kindDef))
                {
                    continue;
                }

                //选中设置检测
                if (!CheckSelected(pawn.Faction, animal.kindDef.defName))
                {
                    continue;
                }

                //驯兽等级不满足
                if (pawn.skills.GetSkill(SkillDefOf.Animals).Level <
                    CalcUtil.CalcRequireSkillLevel(animal.RaceProps.baseHealthScale))
                {
                    continue;
                }

                //距离没有之前缓存过的近
                if (distance >= cacheDistance && Math.Abs(cacheDistance + 1) >= 0.001f)
                {
                    continue;
                }

                cacheDistance = distance;
                targetAnimal = animal;
            }

            return targetAnimal;
        }

        /// <summary>
        /// 检测物种是否在MOD设置中被选中
        /// </summary>
        /// <param name="faction">派系</param>
        /// <param name="kindDefName">动物种类名称</param>
        /// <returns></returns>
        protected static bool CheckSelected(Faction faction, string kindDefName)
        {
            //非玩家阵营 全部种类开启
            if (faction != Faction.OfPlayer)
            {
                return true;
            }

            //玩家阵营 当前物种选中 或者没有储存过当前物种选择数据
            return !UIModWindowMain.Instance.MapPawnKindDefSelectedData.TryGetValue(kindDefName, out var isSelected) ||
                   isSelected;
        }
    }
}