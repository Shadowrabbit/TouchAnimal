// ******************************************************************
//       /\ /|       @file       NeedDefOf.cs
//       \ V/        @brief      需求相关定义
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-04 09:22:09
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;

namespace SR.ModRimWorldTouchAnimal
{
    [DefOf]
    public static class NeedDefOf
    {
        [UsedImplicitly] public static readonly NeedDef SrNeedTouchAnimal; //触摸动物
    }
}