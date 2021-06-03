// ******************************************************************
//       /\ /|       @file       UIModelMain.cs
//       \ V/        @brief      
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:40:53
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace SR.ModRimworldTouchAnimal
{
	public class UIModelMain
	{
		public Dictionary<string, ThingDef> mapAnimals = new Dictionary<string, ThingDef>();

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
			bool IsAnimal(ThingDef def) => def.race != null && def.race.Animal;
			mapAnimals =
				(from thingDef in DefDatabase<ThingDef>.AllDefs where IsAnimal(thingDef) select thingDef).ToDictionary(animalDef =>
					animalDef.defName);
		}
	}
}
