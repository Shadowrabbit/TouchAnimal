// ******************************************************************
//       /\ /|       @file       JobGiverTouchAnimal.cs
//       \ V/        @brief      行为树action节点 触摸动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com	
//       /  |                    
//      /  \\        @Modified   2021-06-03 18:28:26
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using JetBrains.Annotations;
using RimWorld;
using SR.ModRimworldTouchAnimal;
using Verse;
using Verse.AI;

namespace ModRimworldTouchAnimal.AI
{
	[UsedImplicitly]
	public class JobGiverTouchAnimal : ThinkNode_JobGiver
	{
		private const float MaxDistanceToTouch = 20; //最大触发距离

		/// <summary>
		/// 尝试分配工作
		/// </summary>
		/// <param name="pawn"></param>	
		/// <returns></returns>
		protected override Job TryGiveJob(Pawn pawn)
		{
			//健康状态不好时优先休息
			if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
			{
				return null;
			}

			var animal = FindAnimal(pawn);
			//如果能找到动物 尝试分配工作触摸动物
			//todo
			return null;
			//return cat == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchCat, cat);
		}

		/// <summary>
		/// 在小人当前的地图 寻找指定动物
		/// </summary>
		/// <returns></returns>
		private static Pawn FindAnimal(Thing pawn)
		{
			//尝试在附近寻找指定动物
			var currentMap = pawn.Map;
			foreach (var anyPawn in currentMap.mapPawns.AllPawnsSpawned)
			{
				//迭代器中当前的pawn离我们的小人距离太远了 不触发
				if (anyPawn.Position.DistanceTo(pawn.Position) > MaxDistanceToTouch)
				{
					continue;
				}
				//mod设置中没有选中当前物种
				if (!UIModWindowMain.Instance.model.mapAnimals.ContainsKey(anyPawn.kindDef.race.defName))
				{
					continue;
				}
				return anyPawn;
			}

			return null;
		}
	}
}
