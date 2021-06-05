// ******************************************************************
//       /\ /|       @file       UIModelMain.cs
//       \ V/        @brief      UI主窗口数据模型
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:40:53
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
    public class UIModelMain
    {
        public List<PawnKindDef> listAllAnimalDef; //全部动物种族定义列表

        public readonly Dictionary<string, PawnKindDef>
            mapAllAnimalDefs = new Dictionary<string, PawnKindDef>(); //全部动物种类定义<kindDefName,pawnKindDef>

        public readonly Dictionary<string, bool>
            mapSelectedAnimalDefs = new Dictionary<string, bool>(); //选中的动物种类定义<kindDefName,isSelected>

        public UIModelMain()
        {
            SetAllAnimalDefs();
            SetSelectedAnimalDefs();
        }

        /// <summary>
        /// 设置全部动物种类定义
        /// </summary>
        /// <returns></returns>
        private void SetAllAnimalDefs()
        {
            foreach (var pawnKindDef in DefDatabase<PawnKindDef>.AllDefs)
            {
                if (!CalcUtil.IsAnimal(pawnKindDef)) continue;
                mapAllAnimalDefs.Add(pawnKindDef.defName, pawnKindDef);
            }

            listAllAnimalDef = mapAllAnimalDefs.Values.OrderBy(kvp => kvp.RaceProps.baseHealthScale).ToList();
        }

        /// <summary>
        /// 设置选中的动物种类
        /// </summary>
        private void SetSelectedAnimalDefs()
        {
            foreach (var animalKindDefName in mapAllAnimalDefs.Keys)
            {
                mapSelectedAnimalDefs.Add(animalKindDefName, true);
            }
        }
    }
}