// ******************************************************************
//       /\ /|       @file       HediffAddictionTouchAnimal.cs
//       \ V/        @brief      buff 触摸动物成瘾
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 18:55:23
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SR.ModRimworldTouchAnimal
{
	[UsedImplicitly]
	public class HediffAddictionTouchAnimal : HediffWithComps
	{
		private const int DefaultStageIndex = 0; //默认阶段

		private const int WithdrawalStageIndex = 1; //戒断阶段

		//当前hediff阶段
		public override int CurStageIndex => Need == null || Need.CurCategory != DrugDesireCategory.Withdrawal
			? DefaultStageIndex
			: WithdrawalStageIndex;

		//触摸动物需求
		private NeedTouchAnimal Need
		{
			get
			{
				if (pawn.Dead)
				{
					return null;
				}

				var allNeeds = pawn.needs.AllNeeds;
				return allNeeds.Where(t => t.def == def.causesNeed).Cast<NeedTouchAnimal>().FirstOrDefault();
			}
		}

		/// <summary>
		/// 提示
		/// </summary>
		public override string TipStringExtra => Need != null
			? $"{"CreatesNeed".Translate()}:{Need.LabelCap}({Need.CurLevelPercentage.ToStringPercent("F0")})"
			: null;

		/// <summary>
		/// 需求类别改变
		/// </summary>
		public void Notify_NeedCategoryChanged() => pawn.health.Notify_HediffChanged(this);
	}
}
