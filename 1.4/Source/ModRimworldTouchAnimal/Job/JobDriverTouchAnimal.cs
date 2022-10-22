// ******************************************************************
//       /\ /|       @file       JobDriverTouchAnimal.cs
//       \ V/        @brief      行为 抚摸动物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-04 00:31:46
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchAnimal
{
    [UsedImplicitly]
    public class JobDriverTouchAnimal : JobDriver
    {
        private const float ChanceToAddiction = 0.05f; //触发成瘾的概率
        private const float XpSkillAnimalLearn = 200f; //每次触摸动物增加的技能经验
        private const int InteractiveTick = 60; //交互时长
        private readonly Toil _toilCacl; //结算步骤
        private Pawn Animal => (Pawn) job.GetTarget(TargetIndex.A); //动物

        public JobDriverTouchAnimal()
        {
            _toilCacl = new Toil()
            {
                initAction = OnToilsSuccess
            };
        }

        /// <summary>	
        /// 尝试行为前预定
        /// </summary>
        /// <param name="errorOnFailed"></param>
        /// <returns></returns>
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.targetA, job, errorOnFailed: errorOnFailed);
        }

        /// <summary>
        /// A是动物
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Toil> MakeNewToils()
        {
            //动物倒地判定行为失败
            this.FailOnDowned(TargetIndex.A);
            //走到动物附近
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            //走到动物附近的时候 动物已经死了的情况
            if (Animal.Dead)
            {
                yield break;
            }

            //撸1秒
            yield return Toils_General.WaitWith(TargetIndex.A, InteractiveTick, true, true);
            yield return _toilCacl;
        }

        /// <summary>
        /// 全部步骤成功时回调
        /// </summary>
        private void OnToilsSuccess()
        {
            //触发抚摸动物的回忆
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.SrThoughtTouchAnimal);
            //概率触发成瘾	
            CalcAddiction();
            //概率加入殖民者阵营
            CalcJoin();
            //计算需求
            CalcNeedTouchAnimal();
            //增加驯兽经验
            pawn.skills.Learn(SkillDefOf.Animals, XpSkillAnimalLearn);
        }

        /// <summary>
        /// 计算成瘾触发
        /// </summary>
        private void CalcAddiction()
        {
            //已经成瘾 不会重复触发
            if (Enumerable.Any(pawn.health.hediffSet.hediffs,
                heddif => heddif.def == HediffDefOf.SrHediffAddictionTouchAnimal))
            {
                return;
            }

            var randomNum = Random.Range(0f, 1f);
            if ((randomNum > ChanceToAddiction))
            {
                return;
            }

            if (!(HediffMaker.MakeHediff(HediffDefOf.SrHediffAddictionTouchAnimal, pawn) is HediffAddictionTouchAnimal
                hediffAddictionTouchAnimal))
            {
                Log.Error("error on making hediff: HediffAddictionTouchAnimal");
                return;
            }

            //设置成瘾对象动物
            hediffAddictionTouchAnimal.AddictionKindDefName = Animal.kindDef.defName;
            hediffAddictionTouchAnimal.AddictionKindLabel = Animal.kindDef.label;
            pawn.health.AddHediff(hediffAddictionTouchAnimal);
            //玩家阵营的小人上瘾会提示
            if (pawn.Faction == Faction.OfPlayer)
            {
                Messages.Message("MsgAddictionTouchAnimal".Translate(pawn.Label, Animal.Label),
                    MessageTypeDefOf.NeutralEvent);
            }
        }

        /// <summary>
        /// 计算加入殖民者
        /// </summary>
        private void CalcJoin()
        {
            //阵营相同
            if (Animal.Faction == pawn.Faction)
            {
                return;
            }

            //羁绊动物
            if (Animal.playerSettings?.Master != null)
            {
                return;
            }

            //随机过滤
            var randomNum = Random.Range(0f, 1f);
            if ((randomNum > CalcUtil.CalcChanceToJoin(pawn.skills.GetSkill(SkillDefOf.Animals).Level,
                Animal.RaceProps.baseHealthScale)))
            {
                return;
            }

            Animal.SetFaction(pawn.Faction);
            Messages.Message("MsgTouchAnimalJoin".Translate(pawn.Label,
                    Animal.Label, pawn.Faction),
                MessageTypeDefOf.NeutralEvent);
        }

        /// <summary>
        /// 计算触摸动物需求
        /// </summary>
        private void CalcNeedTouchAnimal()
        {
            //如果存在需求 恢复到最高水平
            var allNeeds = pawn.needs.AllNeeds;
            var needTouchAnimal = allNeeds.Where(t => t.def == NeedDefOf.SrNeedTouchAnimal).Cast<NeedTouchAnimal>()
                .FirstOrDefault();
            //需求不存在
            if (needTouchAnimal == null)
            {
                return;
            }

            //需求动物与当前动物种族不同
            if (!needTouchAnimal.AddictionKindDefName.Equals(Animal.kindDef.defName))
            {
                return;
            }

            needTouchAnimal.CurLevel = 1f;
        }
    }
}