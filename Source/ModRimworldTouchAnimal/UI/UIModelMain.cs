// ******************************************************************
//       /\ /|       @file       UIModelMain.cs
//       \ V/        @brief      UI主窗口数据模型
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:40:53
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    public class UIModelMain
    {
        public List<PawnKindDef> listAllAnimalDef; //全部动物种族定义列表

        public UIModelMain()
        {
            SetAllAnimalDefs();
        }

        /// <summary>
        /// 获取选择列表绘制高度
        /// </summary>
        /// <param name="unSelected"></param>
        /// <returns></returns>
        public float GetDrawHeight(int unSelected)
        {
            //选中的数量 默认值为选中 除去未选中的值以外全是选中
            var selectedNum = listAllAnimalDef.Count - unSelected;
            //绘制高度数量
            var drawHeightNum = Math.Max(selectedNum, unSelected);
            return ModDef.RowHeight * (drawHeightNum + 1);
        }

        /// <summary>
        /// 设置全部动物种类定义
        /// </summary>
        /// <returns></returns>
        private void SetAllAnimalDefs()
        {
            //全部动物种类定义<kindDefName,pawnKindDef>
            var mapAllAnimalDefs = DefDatabase<PawnKindDef>.AllDefs.Where(CalcUtil.IsAnimal)
                .ToDictionary(pawnKindDef => pawnKindDef.defName);
            //动物列表按基础健康值排序
            listAllAnimalDef = mapAllAnimalDefs.Values.OrderBy(kvp => kvp.RaceProps.baseHealthScale).ToList();
        }
    }
}