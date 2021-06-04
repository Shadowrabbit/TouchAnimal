// ******************************************************************
//       /\ /|       @file       UIModelMain.cs
//       \ V/        @brief      UI主窗口数据模型
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:40:53
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
	public class UIModelMain
	{
		public readonly Dictionary<string, PawnKindDef>
			mapAllAnimalDefs = new Dictionary<string, PawnKindDef>(); //全部动物种类定义<kindDefName,pawnKindDef>
		public Dictionary<string, bool> mapSelectedRaceDefs = new Dictionary<string, bool>(); //选中的动物种类定义<kindDefName,isSelected>

		public UIModelMain()
		{
			SetAllAnimalDefs();
		}

		/// <summary>
		/// 设置全部动物定义
		/// </summary>
		/// <returns></returns>
		private void SetAllAnimalDefs()
		{
			bool IsAnimal(PawnKindDef def) => def.race != null && def.RaceProps.Animal;
			foreach (var pawnKindDef in DefDatabase<PawnKindDef>.AllDefs)
			{
				if (!IsAnimal(pawnKindDef)) continue;
				mapAllAnimalDefs.Add(pawnKindDef.defName, pawnKindDef);
				//todo
				Log.Warning(pawnKindDef.label);
			}
		}
	}
}
