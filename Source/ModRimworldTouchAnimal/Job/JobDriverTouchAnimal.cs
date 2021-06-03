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
using RimWorld;
using UnityEngine;
using UnityEngine.Assertions;
using Verse;
using Verse.AI;

namespace SR.ModRimworldTouchAnimal
{
    public abstract class JobDriverTouchAnimal : JobDriver
    {
        private const float ChanceToAddiction = 0.1f; //触发成瘾的概率
        private const float ChanceToJoin = 0.2f; //触发宠物加入殖民者的概率
        private const int InteractiveTick = 60; //交互时长
        private readonly Toil _toilCacl; //结算步骤
        private Pawn Animal => (Pawn) job.GetTarget(TargetIndex.A); //动物

        protected JobDriverTouchAnimal()
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
            return true;
        }

        /// <summary>
        /// A是宠物
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Toil> MakeNewToils()
        {
            //宠物倒地判定行为失败
            this.FailOnDownedOrDead(TargetIndex.A);
            //走到宠物附近
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            //走到宠物附近的时候 宠物已经死了的情况
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
            CalcNeedTouchCat();
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

            var hediffAddictionTouchAnimal =
                HediffMaker.MakeHediff(HediffDefOf.SrHediffAddictionTouchAnimal, pawn) as HediffAddictionTouchAnimal;
            Assert.IsNotNull(hediffAddictionTouchAnimal);
            //  hediffAddictionTouchAnimal.
            pawn.health.AddHediff(hediffAddictionTouchAnimal);
            //玩家阵营的小人上瘾会提示
            if (pawn.Faction == Faction.OfPlayer)
            {
                Messages.Message("MsgAddictionTouchCat".Translate(pawn.Label), MessageTypeDefOf.NeutralEvent);
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

            var randomNum = Random.Range(0f, 1f);
            if ((randomNum > ChanceToJoin))
            {
                return;
            }

            Animal.SetFaction(pawn.Faction);
            Messages.Message("MsgTouchPetJoin".Translate(pawn.Label,
                    Animal.Label, pawn.Faction),
                MessageTypeDefOf.NeutralEvent);
        }

        /// <summary>
        /// 计算撸猫需求
        /// </summary>
        private void CalcNeedTouchCat()
        {
            // //如果存在撸猫需求 恢复到最高水平
            // var allNeeds = pawn.needs.AllNeeds;
            // var needTouchCat = allNeeds.Where(t => t.def == NeedDefOf).Cast<NeedTouchPet>()
            //     .FirstOrDefault();
            // //当前角色不存在撸猫需求
            // if (needTouchCat == null)
            // {
            //     return;
            // }
            //
            // needTouchCat.CurLevel = 1f;
        }
    }
}