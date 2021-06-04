// ******************************************************************
//       /\ /|       @file       JobGiverTouchAnimal.cs
//       \ V/        @brief      行为树action节点 触摸动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com	
//       /  |                    
//      /  \\        @Modified   2021-06-03 18:28:26
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchAnimal
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
			return animal == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchAnimal, animal);
		}

		/// <summary>
		/// 在小人当前的地图 寻找指定动物
		/// </summary>
		/// <returns></returns>
		private static Pawn FindAnimal(Thing pawn)
		{
			//尝试在附近寻找指定动物
			var currentMap = pawn.Map;
			return currentMap.mapPawns.AllPawnsSpawned.Where(anyPawn => !(anyPawn.Position.DistanceTo(pawn.Position) > MaxDistanceToTouch))
				.FirstOrDefault(anyPawn => UIModWindowMain.Instance.model.mapAllAnimalDefs.ContainsKey(anyPawn.kindDef.defName));
		}
	}
}
