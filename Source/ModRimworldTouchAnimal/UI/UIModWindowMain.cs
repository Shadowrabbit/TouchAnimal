// ******************************************************************
//       /\ /|       @file       UIModWindowMain.cs
//       \ V/        @brief      Mod主窗口的View+Controller
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-03 13:34:51
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using HugsLib;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchAnimal
{
	[StaticConstructorOnStartup]
	public class UIModWindowMain : ModBase
	{
		public static UIModWindowMain Instance { get; private set; }
		public UIModelMain model;
		private static readonly Color BilibiliBlue = new Color(0.984f, 0.447f, 0.6f, 1f);
		private static readonly Color BilibiliPink = new Color(0.137f, 0.769f, 0.898f, 1f);

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
			// var handle = Settings.GetHandle<string>("handle", "标题", "描述", "");
			// handle.CustomDrawer = rect =>
			// {
			// 	DrawBg(rect, Color.gray);
			// 	return false;
			// };
		}

		/// <summary>
		/// 绘制背景
		/// </summary>
		/// <param name="bgRect"></param>
		/// <param name="color"></param>
		private static void DrawBg(Rect bgRect, Color color)
		{
			var cacheColor = GUI.color;
			GUI.color = color;
			GUI.DrawTexture(bgRect, TexUI.FastFillTex);
			GUI.color = cacheColor;
		}

		/// <summary>
		/// 窗口标题
		/// </summary>
		/// <returns></returns>
		public override string ModIdentifier => "TouchAnimal";
	}
}
