﻿using BepInEx;
using R2API;
using R2API.Networking;
using R2API.Utils;
using RoR2;
using ShrineOfRepair.Modules.Interactables;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace ShrineOfRepair.Modules
{
    [BepInPlugin("com.Viliger.ShrineOfRepair", "ShrineOfRepair", "1.2.2")]
	[BepInDependency(R2API.R2API.PluginGUID)]
	[R2APISubmoduleDependency(nameof(LanguageAPI), nameof(PrefabAPI), nameof(NetworkingAPI), nameof(DirectorAPI))]

	[BepInDependency("bubbet.bubbetsitems", BepInDependency.DependencyFlags.SoftDependency)]

	public class ShrineOfRepairPlugin : BaseUnityPlugin
	{
		public static PluginInfo PInfo { get; private set; }

		internal static BepInEx.Logging.ManualLogSource MyLogger;

		private void Awake()
        {
			//#if DEBUG == true
			//On.RoR2.Networking.NetworkManagerSystemSteam.OnClientConnect += (s, u, t) => { };
			//#endif

			PInfo = Info;

			MyLogger = Logger;

			new ShrineOfRepairLanguages().Init();

			new ShrineOfRepairConfigManager().Init(Paths.ConfigPath);

			var InteractableTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(InteractableBase)));

			foreach(var interactableType in InteractableTypes)
            {
				InteractableBase interactable = (InteractableBase)System.Activator.CreateInstance(interactableType);
				interactable.Init();
				MyLogger.LogInfo($"Interactable: {interactable.InteractableLangToken} loaded.");
            }
		}
	}
}
