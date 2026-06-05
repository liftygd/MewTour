using System.Collections.Generic;

namespace MewTour.Reroll;

public class RerollConfig
{
    public void LoadConfig(
        Dictionary<string, List<string>> abilities,
        Dictionary<string, List<string>> passives)
    {
        abilities["fighter"] = [
            "Dash", "Spin", "FirePunch", "IcePunch", "ThunderPunch", "FurySwipes", "SideSlash", "FighterLeap",
            "Uppercut", "Counter", "TailWhip", "Poke", "Nip", "Push", "FalconPunch", "Exert", "Enrage", "Tumble",
            "Confront", "Juiced", "CosmicPunch", "FighterTaunt", "GravitySlam", "Berserk", "Challenge",
            "Stoopzerk", "SleeperHold", "Grapple", "ThinkTooHard", "Zoomzerk", "Bloodzerk", "ExhaustingBlow",
            "ChaosRampage", "MeteorSlam", "MuscleMemory", "Inhale", "OneTwoPunch", "TeamSpin", "TeamFlex",
            "Huddle", "RagePunch", "BreakingPoint", "AssertDominance", "DumbMove", "SuckerPunch", "Stick",
            "Hurl", "BigPunch", "Pawbreaker", "Ram"
        ];

        abilities["colorless"] = [
            "Block", "Rest", "Brace", "Roll", "SharpenClaws", "Reach", "ManaDrain", "SoothingGlow", "Ponder",
            "Brainstorm", "Focus", "Metabolize", "GainThorns", "PrepareToJump", "BoostSpellRange", "PushMove",
            "Gamble", "SoulReap", "Hunt", "Flex", "Dart", "Smack", "Spit", "MiniHook", "MiniDistract",
            "ButtScoot", "Confusion", "Reflect", "PlayDead", "HealBolt", "SlipThrough", "Dump", "Snacks",
            "FeatherFeet", "Reduce", "Nerf", "Trip", "Copycat", "Metronome", "DollUp", "StackTheDeck",
            "Infiltrate", "Burst", "Suppress", "Endeavor", "LotteryShottery", "CatNap", "PissYourself",
            "FindARock", "BurgeoningBlast", "BurgeoningBarrier", "BurgeoningBattery", "HoseOff", "Taint",
            "PokeWound", "WasteTime", "Desecrate", "Contort", "RussianRoulette", "Step", "Interchange",
            "LookAtMe", "Rouse", "Shift", "Donate", "Magnet", "ScuffItOff", "BarfBall", "DexterousHit", "Till",
            "PathOfTheMage", "PathOfTheHunter", "PathOfTheThief", "PathOfTheFighter", "PathOfTheTank",
            "PathOfTheCleric", "PathOfTheButcher", "PathOfThePsychic", "PathOfTheTinkerer", "PathOfTheMonk",
            "PathOfTheDruid", "PathOfTheNecromancer", "Itch", "Meow", "Swat", "LickHeal", "Purr", "Hiss",
            "Knead", "BuyCatnip", "VetVisit", "HireHitman", "SubwayRide", "GymMembership", "SuperCrateBox",
            "BBQ", "CPR", "Blow", "Toast", "Landscape", "Zap", "Sunburn", "ColdShoulder", "BlowKiss",
            "ForbiddenFart", "WetHairball", "PathOfTheVoid", "PathOfTheJester"
        ];

        abilities["hunter"] = [
            "LineShot", "HailOfNails", "SpawnMaggotFriend", "SpawnPooterFriend", "Marked", "ScatterShot",
            "BrambleShot", "BearTrap", "TwinShot", "CrossShot", "SpawnBaitTrap", "BombShot", "SummonBrambles",
            "FireShot", "FocusShot", "Shards", "TerrainWalk", "Extend", "ChaosShot", "NeedleShot", "SpikeTrap",
            "FleaShot", "WebTrap", "LastHit", "CupidsArrow", "ArrowFlurry", "HeavyShot", "StakeOut",
            "BallOfSpiders", "Snipe", "Diversion", "ArrowSmith", "TacticalRetreat", "Infest", "CollectPelt",
            "SentryMode", "Pheromones", "SpawnTomTomFriend", "ScoutMe", "CraftArrow", "CharmTrap",
            "BounceShot", "Picnic", "SoothingShot", "Vivisect", "PoisonLace", "SlopThePigs", "SpiderInjector",
            "PersistentHunt", "Bunker"
        ];

        abilities["mage"] = [
            "Surf", "Bolt", "Fireball", "FreezeRay", "MagicMissile", "Blast", "WallOfFire", "HyperBeam",
            "MeteorStorm", "MegaBlast", "Slow", "WindSlash", "MageTeleport", "MageSwap", "Absorb", "Warp",
            "ManaMeld", "Inspire", "Telefrag", "ChaosTeleport", "CryoHeal", "Gust", "Blizzard", "Inferno",
            "Thunderburst", "DealWithTheDevil", "ForbiddenFlame", "ForbiddenFlood", "WaterSphere",
            "ChainLightning", "Shatter", "ForbiddenFulmination", "FireBolt", "IcicleTaser", "FreezerBurn",
            "Corrupt", "Jolt", "Smolder", "FireSurge", "IceSurge", "LightningSurge", "Creshendo", "Divide",
            "ForbiddenFrost", "BlackMagic", "Teach", "HomingBlasts", "Replicate", "Magnify", "TriAttack"
        ];

        abilities["tank"] = [
            "HeadButt", "ThrowShield", "ChewCud", "AssBlast", "Chew", "BatterUp", "BackBreaker", "Suplex",
            "Intimidate", "Toss", "BellyFlop", "TankTrample", "TankSwap", "ToTheRescue", "TankTantrum",
            "Earthquake", "RockToss", "BarbedWire", "DrawAttention", "BowlOver", "Clap", "TankRockSong",
            "RockCrusher", "BodyGuard", "Gore", "RockBlast", "RockTomb", "BearHug", "Fissure", "BigRock",
            "FlipFlop", "Lunge", "Nudge", "StoneGaze", "Medusa", "Anchor", "EatRock", "PlantFeet", "IronHead",
            "GangUp", "Aftershock", "SteelSkin", "FaultLine", "Demolish", "PushThrough", "Spur", "Supper",
            "FullForce", "Sandstorm", "Thicken"
        ];

        abilities["medic"] = [
            "RangedHeal", "MeleeHeal", "Malaise", "OpenWounds", "Prayer", "Convert", "Cleanse", "HereticMark",
            "Zealot", "Haste", "Rally", "BuddyUp", "HealingFall", "RallyCharge", "ReverseDamage", "Rebuke",
            "Wish", "WitchHunt", "FriendOrFoe", "Revive", "HolyLight", "Ethereal", "BornAgain", "Benediction",
            "Crusade", "Enlighten", "HallowedGround", "Anoint", "EyeForAnEye", "WrathOfGod", "Adoubement",
            "DivineProtection", "ChosenWarrior", "SwiftSanctify", "GetDown", "DivineGift", "HolyWeapon",
            "Awaken", "Baptism", "Pray", "Emergency", "GuardianAngel", "Booster", "Stimulants", "BlindingLight",
            "CircleOfProtection", "CallOver", "Grace", "TurnFoe", "HealingSalve"
        ];

        abilities["thief"] = [
            "MoveAgain", "Assassinate", "BoostBackstab", "PoisonGas", "PoisonNail", "WeakeningNail",
            "SharpNail", "CoinToss", "Shadow", "TimeWalk", "Distract", "Rebound", "CutPurse", "EagleEye",
            "PickPocket", "Blur", "GreedStep", "Stalk", "Backflip", "AttackAgain", "NailFlurry", "Declaw",
            "QuickRoll", "Slice", "PocketSand", "Nightshade", "Shadowshift", "SlingShade", "Caltrops",
            "PierceShot", "Cheat", "VenomBarrage", "LootCorpse", "SeverArtery", "Fade", "SharpenNail",
            "SneakUp", "StealKidney", "StealLuck", "ThiefSwap", "Pierce", "WindUp", "TripleNails",
            "SkinDisguise", "Jitter", "Chakram", "StealTime", "Outskirts", "PoisonDip", "LuckyPenny"
        ];

        // ==================== ПРОДВИНУТЫЕ КЛАССЫ (из advanced_classes.txt) ====================

        abilities["monk"] = [
            "Propell", "Hadouken", "Cartwheel", "StoneFists", "Transcend", "HipToss", "Bruise", "Slapback",
            "Finisher", "Reverberate", "ComboThrow", "ComboPull", "OneWithTheWind", "Pogo", "TrainArms",
            "Porcupine", "Anneal", "DeepDive", "HopAndBlock", "TrainMind", "Meditate", "DoomPunch",
            "KiBurst", "DragonPunch", "TrainLegs", "ReallyFastRun", "DetectWeakness", "HundredHandSlap",
            "KineticCharge", "AirBurst", "TrainBody", "ReleaseEnergy", "Pummel", "QuickAttack", "PerfectForm",
            "WarmupStretch", "FlyingFist", "SpiritBomb", "OnePunch", "UnbridledHits", "Kamehameha",
            "SideStep", "UnimpededLunge", "DoubleDragon", "FistOfFate", "Nirvana", "EmptyMind", "Position",
            "ChargeFists", "Apprentice"
        ];

        abilities["butcher"] = [
            "HogRush", "Burp", "SelfMutilate", "ForceFeed", "Fartoom", "Mutilate", "SkullBash", "Shred",
            "Chomp", "Succ", "Trudge", "BodySlam", "Consume", "BloodMagic", "SmellBlood", "Vurp",
            "SliceAndDice", "LunchTime", "Tromp", "LightenTheLoad", "Crushinator", "CannonBall", "Monch",
            "DeathWind", "Spoil", "Grill", "Roast", "BadGas", "ButcherPurge", "Binge", "MyTurn", "Gib",
            "Swallow", "Track", "Sharpen", "FireFart", "RoughToss", "TaintedOffering", "DeliciousScent",
            "Cough", "Reflux", "Tryptophan", "HookBind", "Regurge", "Grapnel", "Rehook", "Contaminate",
            "LodgeHook", "Butcher", "Chonkwalk"
        ];

        abilities["druid"] = [
            "ManaBomb", "SongOfSpring", "GrantLife", "SquirrelSquad", "SummonSquirrel", "DruidSwap",
            "BattleCry", "SummonSnake", "SummonTurtle", "SummonToad", "SummonBear", "PullToSafety",
            "BrambleBurst", "FlowerFeet", "ThornyFeet", "Encourage", "Protection", "Promote", "SafetyDance",
            "WarCry", "TigerForm", "MonkeyForm", "RhinoForm", "SummonCatepillar", "CallTheWind",
            "InspirationalSong", "DeathMetal", "ChaChaSlide", "BestowWisdom", "RaccoonForm", "SummonCrow",
            "WeWillRockYou", "TreeForm", "HydroPump", "ControlPlants", "ControlWater", "ControlAir",
            "Entangle", "Lullaby", "WeAreTheChampions", "Cheerlead", "NaturesBlessing", "ThrowEgg",
            "SquirrelForm", "PlantMushroom", "Serenade", "WindyStep", "ElkForm", "MockingbirdForm",
            "FromTheTrees"
        ];

        abilities["tinkerer"] = [
            "Research", "Discharge", "Repair", "ShoddyJetpack", "SpawnDecoy", "SpringShoes", "AutoPilot",
            "Recycle", "BuildTurret", "RocketSkates", "DrillDown", "ArmorUp", "FreshOffTheForge",
            "ElectricNail", "Craft", "Shockwave", "Math", "Reprogram", "Improve", "Catbot", "Bombchu",
            "RemoteDetonator", "EjectButton", "ShortCircuit", "Electrolyze", "Firecrackers", "Upgrade",
            "Eureka", "PunchBot", "FastHands", "MechSuit", "UnreliableShield", "UnreliableMissile",
            "SpareParts", "BatteryNuke", "ExperimentalTeleporter", "ShockTherapy", "BuildNuke",
            "InstantBarrier", "VoltTackle", "Smash", "ShedScrap", "RepairArmor", "RocketRide", "RoboVac",
            "NurseBot", "TeslaCoil", "RefineMaterials", "Fabricate", "Sparks"
        ];

        abilities["necromancer"] = [
            "MaggotArmy", "Reanimate", "Rebirth", "Pestilence", "Weakness", "SoulSuck", "EvilIncarnate",
            "SoulLink", "WeAreOne", "BloodRain", "AnimateDead", "DeathBloom", "Scare", "SoulTransfer",
            "Whisper", "SummonShade", "DarkStep", "Leeches", "Shriek", "FullMoon", "Unearth", "BloodGeyser",
            "Flatline", "Replace", "SummonBones", "GigaDrain", "Bloodletting", "MassPsychosis", "Debone",
            "Reap", "CarrionShot", "LifeDrain", "CoffinFlop", "DonateBlood", "Seance", "GoLimp",
            "DemonicPact", "Curse", "LeechSwarm", "Feed", "Hush", "ReaperStep", "ForbiddenFamine",
            "FleshGolem", "TradeLife", "AbsorbSoul", "Gravecrawl", "DigUpTheDead", "SpiderEgg", "ClewOfLeeches"
        ];

        abilities["psychic"] = [
            "Telekinesis", "Suggestion", "MindControl", "MegaGrav", "PsyFlutter", "MagnetPull", "MindBlast",
            "PsychicChoke", "SkyShatter", "Supernova", "AlterDNA", "Flicker", "MindMeld", "Vaccuum", "Ping",
            "FlashForward", "Order", "TemporalShards", "RealityScramble", "Glare", "BlindingFlash", "Snatch",
            "FutureSight", "MassManaLeech", "BecomeEntropy", "FastForward", "AncestralRecall",
            "CumulativeBlast", "Hallucinate", "MassHysteria", "ExtraTurnQuestion", "MindCrack", "Reset",
            "Mimic", "ChaosSwap", "Asteroid", "Stasis", "Pass", "ThinkDeep", "Puppet", "YouSeeNothing",
            "ForceBlast", "IncreaseGravity", "Manifest", "Flip", "Withdraw", "ForceCone", "Inversion",
            "Echo", "Slipstream"
        ];
        
        passives["fighter"] = [
            "BloodLust", "Avenger", "Scars", "FasterWhenHit", "KillsHeal", "Vengeful", "HamsterStyle",
            "WeaponMaster", "ShoulderCheck", "SkullSmash", "TurtleStyle", "Overpowered", "FightMe",
            "HighAsYouCanCount", "DumbMuscle", "ThickSkull", "MostValuableCat", "RatStyle", "Boned",
            "DualWield", "ReflexPunch", "HitMe", "Smash", "PunchFace", "Recoil"
        ];

        passives["colorless"] = [
            "SelfAssured", "LuckDrain", "Infested", "Worms", "Amped", "Furious", "MetalDetector",
            "DeathProof", "Leader", "Mange", "ETank", "Careful", "DirtyClaws", "LateBloomer", "Study",
            "SkillShare", "NaturalHealing", "LongShot", "FastFooted", "Slugger", "Pulp", "Amplify",
            "DeathBoon", "SantaSangre", "Untouched", "Daunt", "AnimalHandler", "WhipCracker",
            "PressurePoints", "Gassy", "Dealer", "Patience", "Wiggly", "MiniMe", "BareMinimum",
            "Unrestricted", "DeathsDoor", "OverConfident", "SerialKiller", "StrengthInNumbers",
            "FightersSoul", "MagesSoul", "TanksSoul", "HuntersSoul", "ThiefsSoul", "ClericsSoul",
            "NecromancersSoul", "TinkerersSoul", "DruidsSoul", "MonksSoul", "ButchersSoul", "PsychicsSoul",
            "Charming", "FirstImpression", "Scavenger", "ZenkaiBoost", "Protection", "Rockin", "Mania",
            "Lucky", "OneEighty", "JestersSoul", "HotBlooded", "VoidSoul"
        ];

        passives["hunter"] = [
            "TakeAim", "HuntersBoon", "BroodMother", "TaintedMother", "Quiver", "SplitShot", "Hazardous",
            "ThornArrows", "Traps", "CatchProjectiles", "TowerDefense", "TrickyTraps", "GravityFalls",
            "HawkEye", "Spotters", "LuckSwing", "Host", "Sniper", "RubberArrows", "TalkToAnimals",
            "AnimalControl", "SleepDarts", "Survivalist", "Fleabag", "ThrillOfTheHunt"
        ];

        passives["mage"] = [
            "Micronaps", "HolyMantel", "Shrapnel", "BurningPaws", "LightningPaws", "IcePaws", "PawMissile",
            "Overload", "ChargeUp", "Recharged", "EnergyStorm", "FireArmor", "IceArmor", "Resonance",
            "LearnFromMe", "LightningArmor", "LongCast", "LightUpTheStage", "ElementalAttunement",
            "LatentEnergy", "Five", "MagicGuru", "One", "Two", "Four"
        ];

        passives["tank"] = [
            "Thorns", "HeavyHanded", "SlackOff", "Scabs", "ThunderThighs", "Plow", "PetRocks", "ToadStyle",
            "ChainKnockback", "ProtectiveAura", "Wrestlemaniac", "MountainForm", "HomeRun", "RockAspect",
            "WideLoad", "HardHead", "MyLeg", "Hardy", "SlowAndSteady", "FollowUp", "CatAPult",
            "ShovingMatch", "Stoic", "PriorityTarget", "Bouncer"
        ];

        passives["medic"] = [
            "HealingAura", "NaturalHealer", "Eternal", "Blessed", "EvilPatron", "AngelicInspiration",
            "TopOff", "SharingIsCaring", "Caretaker", "MoraleBoost", "RangedMedic", "Godspeed",
            "GodWarrior", "BreathOfLife", "ThouShaltNotKill", "ThouShaltNotCovet", "BlessingOfHolyFire",
            "AlmsForThePoor", "Purifier", "VeneratedTouch", "ProtectTheWeak", "ThouShaltObey",
            "EnchantedRelic", "BlessingOfSpirit", "Heathens"
        ];

        passives["thief"] = [
            "Backstabber", "GoldenClaws", "Shadow", "PoisonTips", "Burgle", "SwiftKiller", "DoubleThrow",
            "BountyHunter", "RazorClaws", "Looter", "AlphaStrike", "Zip", "WeakSpot", "Penetrate",
            "AfterImage", "Shiv", "Critical", "LootHoarder", "Cripple", "Agile", "Shank", "FlipACoin",
            "ShakeDown", "SweetSpot", "Pinpoint"
        ];
        
        passives["monk"] = [
            "SafeSwitching", "Mixup", "Turnabout", "MonkeyStyle", "BrickSkin", "JaggedEdges",
            "MindBreaker", "CobraStyle", "Tenderize", "LongArms", "SpreadThePain", "Harden",
            "IronSkin", "JetFists", "EnergyFists", "Unstoppable", "UnburdenedMotion",
            "UnburdenedStrikes", "UnburdenedThoughts", "RunningJab", "PerfectTechnique", "RapidFlow",
            "CounterBarrage", "FlowState", "DancingLights"
        ];

        passives["butcher"] = [
            "Putrefy", "NeverFull", "MainCourse", "FreshMeat", "Masochist", "Glutton", "Hooked",
            "Stompy", "Barbed", "GrapplingHook", "PainGain", "WideSwing", "Confrontational",
            "HeaveHook", "Harpooner", "LordOfTheFlies", "Schadenfreude", "Gurgitator", "LooseMeat",
            "Hack", "BowlingBall", "Testy", "Indigestion", "Incubator", "DukeOfFlies"
        ];

        passives["druid"] = [
            "SuperCrow", "NaturesGuidance", "PoisonIvy", "Pathfinder", "EmptyVessels", "WildAnimals",
            "BarkSkin", "SoothingSong", "Teamwork", "Bouquet", "GoodVibrations", "VersatileVocalist",
            "LikeAFish", "Encore", "SpecialFriends", "SneakAttack", "WildStyle", "BuddySystem",
            "FlowerPower", "SuicideSquad", "Feral", "RapGod", "Animalistic", "Maestro", "MegaMinions"
        ];

        passives["tinkerer"] = [
            "VersionTwo", "WeaponProficiency", "LivingBattery", "FuzzyFeet", "ArmorSpecialist", "EMP",
            "MrMega", "EscapeSequence", "ItemProxy", "LightningRod", "ItsAlive", "Energizer",
            "ReactiveArmor", "Nanobots", "Scrapper", "DemoMan", "DuctTape", "ArmoredPlating",
            "BoobyTrap", "RobotArms", "Conductor", "Napalm", "Ingenuity", "Shrapnel_Tinkerer", "Blacksmith"
        ];

        passives["necromancer"] = [
            "Vampirism", "OneWithNothing", "BedBugs", "WormLord", "InfiniteRebirth", "SacrificialLamb",
            "DeathIncarnate", "OffloadPain", "CambionConception", "Leechmother", "Infected", "LastGasp",
            "RelentlessDead", "ChainsOfGuilt", "DarkPriest", "Undeath", "NumbingLeeches", "EternalHealth",
            "Torpor", "SoulBound", "Superstition", "ImmortalLeeches", "CorpseConnoisseur", "Parasitic",
            "SpreadSorrow"
        ];

        passives["psychic"] = [
            "Flying", "SoulShatter", "Glow", "Blink", "FullPower", "RealityShatter", "MentalStorm",
            "Wither", "Flourish", "PsySmack", "Beckon", "MindTempest", "Overflow", "Omniscience",
            "PsionicRepel", "Enlightened", "MadVisage", "PowerUp", "TrueSight", "Radiation",
            "GravityWell", "Drag", "Twiddle", "RepressedMemories", "EldritchVisage"
        ];
    }
}