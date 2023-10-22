﻿using BepInEx;
using HarmonyLib;
using PoppyScyyeGameModes.Gamemodes;
using PoppyScyyeGameModes.Monos;
using UnboundLib;
using UnboundLib.Cards;
using System.Collections;
using UnboundLib.GameModes;
using PoppyScyyeGameModes.Cards;
using UnboundLib.Utils;

namespace PoppyScyyeGameModes
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("io.olavim.rounds.rwf", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.itemshops", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.pickncards")]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "dev.scyyepoppy.rounds.gamemodes";
        private const string ModName = "Poppys And Scyyes Gamemodes";
        private const string Version = "1.0.0"; // What version are we on (major.minor.patch)?

        /*
         * TODO: Make the gamemode work properly :"D
         */

        public static Main instance { get; private set; }

        void Awake()
        {
            instance = this;
            Unbound.RegisterCredits("Poppys And Scyyes Gamemodes", new string[] { "Poppycars", "Scyye" }, new string[] { "GitHub", "Poppycars", "Scyye" },
                new string[] {"https://github.com/poppycars22/PSGM", "https://github.com/poppycars22", "https://github.com/Scyye"});

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        internal CardInfo GiveSkillPointCard;

        void Start()
        {
            GameModeManager.AddHandler<SkillPointGM>(SkillPointHandler.GameModeID, new SkillPointHandler());
            GameModeManager.AddHandler<SkillPointGM>(SkillPointTeamHandler.GameModeID, new SkillPointTeamHandler());

            GameModeManager.AddHandler<RandomCardPickGM>(RandomCardPickHandler.GameModeID, new RandomCardPickHandler());
            GameModeManager.AddHandler<RandomCardPickGM>(RandomCardPickTeamHandler.GameModeID, new RandomCardPickTeamHandler());

            CustomCard.BuildCard<AmmoSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BlockCooldownSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<DamageSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<HealthSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BlockSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<RegenerationSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BounceSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<JumpSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<MoveSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c)           );
            CustomCard.BuildCard<ProjectileSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ProjectileSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ProjectileSimSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SpreadSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<KnockbackSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<ReloadSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<AttackSpeedSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<DoTSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<RespawnsSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SizeSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<SlowSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<PercentDamageSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BulletGravSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BulletSizeSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<BurstSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<LifestealSkillPoint>(c => ModdingUtils.Utils.Cards.instance.AddHiddenCard(c));
            CustomCard.BuildCard<GiveSkillPointCard>(c => GiveSkillPointCard = c);

            GameModeManager.AddHook(GameModeHooks.HookPickEnd, _ => SkillPointShop.WaitUntillShopDone());
            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);

            GameModeManager.AddHook(GameModeHooks.HookGameStart, RemoveSkillPointCard);

        }

        internal IEnumerator GameStart(IGameModeHandler gm)
        {
            yield return SkillPointShop.SkillUp();
            yield break;
        }
        
        internal IEnumerator RemoveSkillPointCard(IGameModeHandler gm)
        {
            if (!gm.Name.Contains("Skill_Point"))
            {
                CardManager.DisableCard(GiveSkillPointCard);
            } else
            {
                CardManager.EnableCard(GiveSkillPointCard);
            }
            yield break;
        }
    }
}
