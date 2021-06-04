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

namespace SR.ModRimWorldTouchAnimal
{
	[UsedImplicitly]
	public class HediffAddictionTouchAnimal : HediffWithComps
	{
		public string AddictionKindDefName { get; set; } //导致成瘾的动物种类定义名称
		public string AddictionKindLabel { get; set; } //导致成瘾的动物种类名字
		private const int DefaultStageIndex = 0; //默认阶段
		private const int WithdrawalStageIndex = 1; //戒断阶段

		/// <summary>
		/// 提示
		/// </summary>
		public override string TipStringExtra => Need != null
			? $"{"CreatesNeed".Translate()}:{Need.LabelCap}({Need.CurLevelPercentage.ToStringPercent("F0")})"
			: null;

		/// <summary>
		/// 括号中的标签
		/// </summary>
		public override string LabelInBrackets
		{
			get
			{
				var labelInBrackets = base.LabelInBrackets;
				if (def.CompProps<HediffCompProperties_SeverityPerDay>() == null)
				{
					return labelInBrackets;
				}
				//严重性越低代表成瘾结束进度越高
				var stringPercent = (1f - Severity).ToStringPercent("F0");
				return !labelInBrackets.NullOrEmpty()
					? $"{labelInBrackets}{AddictionKindLabel}{stringPercent}"
					: stringPercent;
			}
		}

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
		/// 需求类别改变
		/// </summary>
		public void Notify_NeedCategoryChanged() => pawn.health.Notify_HediffChanged(this);
	}
}
