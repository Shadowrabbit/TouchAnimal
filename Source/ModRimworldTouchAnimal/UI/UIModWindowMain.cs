// ******************************************************************
//       /\ /|       @file       UIModWindowMain.cs
//       \ V/        @brief      Mod主窗口的View+Controller
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:34:51
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using HugsLib;
using HugsLib.Settings;
using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    [UsedImplicitly]
    [StaticConstructorOnStartup]
    public class UIModWindowMain : ModBase
    {
        public static UIModWindowMain Instance { get; private set; }
        private SettingHandle<int> _currentSkillLevelAnimal;
        private SettingHandle<string> _handleChanceToJoinList;
        public UIModelMain model;

        public UIModWindowMain()
        {
            Instance = this;
        }

        /// <summary>
        /// 定义文件加载后回调
        /// </summary>
        public override void DefsLoaded()
        {
            InitSetting();
        }

        /// <summary>
        /// 初始化设置数据
        /// </summary>
        private void InitSetting()
        {
            model = new UIModelMain();
            //假定驯兽等级
            _currentSkillLevelAnimal = Settings.GetHandle("handleCurrentSkillLevelAnimal", "假设驯兽技能等级",
                "假定当前角色的驯兽等级", 12, Validators.IntRangeValidator(1, 20));
            //信息列表
            _handleChanceToJoinList =
                Settings.GetHandle("handleChanceToJoinList", "触摸行为信息",
                    "当前驯兽等级下,动物被抚摸后加入殖民者阵营的概率", string.Empty);
            _handleChanceToJoinList.CustomDrawerHeight = (model.listAllAnimalDef.Count + 1) * ModDef.RowHeight;
            _handleChanceToJoinList.CustomDrawer = DrawBehaviourTouchInfo;
            //选择列表
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        /// <returns></returns>
        public override string ModIdentifier => "TouchAnimal";

        /// <summary>
        /// 绘制触摸信息
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool DrawBehaviourTouchInfo(Rect rect)
        {
            //背景
            DrawUtil.DrawBg(rect, Color.white);
            DrawUtil.DrawTouchInfoRow(rect, "动物种类", "所需驯兽等级", "加入几率");
            //绘制信息
            foreach (var pawnKindDef in model.listAllAnimalDef)
            {
                rect.y += ModDef.RowHeight;
                DrawUtil.DrawTouchInfoRow(rect, pawnKindDef.label,
                    $"{CalcUtil.CalcRequireSkillLevel(pawnKindDef.RaceProps.baseHealthScale)}",
                    $"{CalcUtil.CalcChanceToJoin(_currentSkillLevelAnimal.Value, pawnKindDef.RaceProps.baseHealthScale)}");
            }

            //不需要序列化
            return false;
        }
    }
}