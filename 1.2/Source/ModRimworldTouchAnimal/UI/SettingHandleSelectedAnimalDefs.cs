// ******************************************************************
//       /\ /|       @file       SettingHandleSelectedAnimalDefs.cs
//       \ V/        @brief      设置数据 动物触摸种类选择 
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-05 20:18:21
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using HugsLib.Settings;

namespace SR.ModRimWorldTouchAnimal
{
    public class SettingHandleSelectedAnimalDefs : SettingHandleConvertible
    {
        //动物种类定义选择数据<kindDefName,isSelected> 注意未操作过的定义不在map内 并且默认值为选中
        private Dictionary<string, bool> _mapPawnKindDefSelectedData;

        //恢复默认值后 _mapSelectedAnimalDefs为null 重新初始化
        public Dictionary<string, bool> MapPawnKindDefSelectedData =>
            _mapPawnKindDefSelectedData ?? (_mapPawnKindDefSelectedData = new Dictionary<string, bool>());


        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="settingValue"></param>
        public override void FromString(string settingValue)
        {
            if (settingValue.Equals(string.Empty))
            {
                return;
            }

            foreach (var strKvp in settingValue.Split(','))
            {
                var kvp = strKvp.Split('|');
                MapPawnKindDefSelectedData.Add(kvp[0], Convert.ToBoolean(kvp[1]));
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var data = MapPawnKindDefSelectedData.Select(kvp => $"{kvp.Key}|{kvp.Value}").ToList();
            return string.Join(",", data);
        }
    }
}