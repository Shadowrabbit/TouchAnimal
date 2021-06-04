// ******************************************************************
//       /\ /|       @file       NeedTouchAnimal.cs
//       \ V/        @brief		 需求 触摸某种动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 18:37:33
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SR.ModRimworldTouchAnimal
{
    [UsedImplicitly]
    public class NeedTouchAnimal : Need
    {
        private const float ThreshDesire = 0.01f; //渴望临界值
        private const float ThreshSatisfied = 0.1f; //满足临界值
        public override int GUIChangeArrow => -1;
        public override void SetInitialLevel() => CurLevelPercentage = Rand.Range(0.8f, 1f); //初始水平
        public string AddictionRaceDefName => AddictionHediff.AddictionRaceDefName; //导致成瘾的动物种族定义名称
        private string AddictionRaceLabel => AddictionHediff.AddictionRaceLabel; //导致成瘾的动物种族名字

        public NeedTouchAnimal(Pawn pawn)
            : base(pawn)
        {
        }

        /// <summary>
        /// 成瘾来源的hediff
        /// </summary>
        private HediffAddictionTouchAnimal AddictionHediff
        {
            get
            {
                var hediffs = pawn.health.hediffSet.hediffs;
                foreach (var hdf in hediffs)
                {
                    if (hdf is HediffAddictionTouchAnimal hediffAddiction &&
                        hediffAddiction.def.causesNeed == def)
                        return hediffAddiction;
                }

                return null;
            }
        }

        /// <summary>
        /// 成瘾程度
        /// </summary>
        public DrugDesireCategory CurCategory
        {
            get
            {
                //成瘾满足状态
                if (CurLevel > ThreshSatisfied)
                {
                    return DrugDesireCategory.Satisfied;
                }

                //戒断中或者寻求中
                return CurLevel < ThreshDesire ? DrugDesireCategory.Withdrawal : DrugDesireCategory.Desire;
            }
        }

        /// <summary>
        /// 需求水平
        /// </summary>
        public override float CurLevel
        {
            get => base.CurLevel;
            set
            {
                //设置前的成瘾状态
                var curCategory = CurCategory;
                base.CurLevel = value;
                if (CurCategory == curCategory)
                {
                    return;
                }

                //刷新hediff
                AddictionHediff?.Notify_NeedCategoryChanged();
            }
        }

        /// <summary>
        /// 需求水平间隔一段时间下降
        /// </summary>
        public override void NeedInterval()
        {
            if (IsFrozen)
            {
                return;
            }

            CurLevel -= def.fallPerDay / 400f;
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <returns></returns>
        public override string GetTipString() =>
            $"{LabelCap}({AddictionRaceLabel}):{CurLevelPercentage.ToStringPercent()}\n{def.description}";
    }
}