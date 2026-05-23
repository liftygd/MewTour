using System.Collections.Generic;

namespace MewTour.Reroll;

public class RerollConfig
{
    public void LoadConfig(
        Dictionary<string, List<string>> abilities,
        Dictionary<string, List<string>> passives)
    {
        abilities["butcher"] = [
            "HogRush", "Burp", "SelfMutilate", "ForceFeed", "Fartoom",
            "Mutilate", "SkullBash", "Shred", "Chomp", "Succ", "Consume",
            "Trudge", "BodySlam", "BloodMagic", "SmellBlood", "Vurp",
            "LunchTime", "Tromp", "LightenTheLoad", "Crushinator",
            "CannonBall", "Monch", "DeathWind", "Spoil", "Grill", "Roast", "BadGas",
            "ButcherPurge", "Binge", "MyTurn", "Gib", "Swallow", "Track", "Sharpen",
            "FireFart", "RoughToss", "Bowl", "BowlDash", "TaintedOffering",
            "DeliciousScent", "Cough", "Reflux", "Tryptophan", "HookBind", "Regurge",
            "Grapnel", "Rehook", "Contaminate", "LodgeHook", "Butcher", "Chonkwalk", "Indigestion_Fart"
        ];

        abilities["fighter"] = [
            "Dash", "Spin", "FirePunch", "IcePunch", "ThunderPunch", "FurySwipes", "SideSlash", "FighterLeap",
            "Uppercut", "Counter", "TailWhip", "Poke", "Nip", "Push", "FalconPunch", "Exert", "Enrage", "Tumble",
            "Confront", "Juiced", "CosmicPunch", "FighterTaunt", "GravitySlam", "Berserk", "BerserkDash",
            "Challenge", "Slap", "Stoopzerk", "LastThought", "SleeperHold", "Grapple", "ThinkTooHard", "Zoomzerk",
            "Reposition", "Bloodzerk", "Lacerate", "ExhaustingBlow", "ChaosRampage", "MeteorSlam",
            "MuscleMemory", "Inhale", "OneTwoPunch", "TeamSpin", "TeamFlex", "Huddle", "RagePunch", "BreakingPoint",
            "AssertDominance", "DumbMove", "SuckerPunch", "Stick", "Hurl", "BigPunch", "Ram"
        ];

        abilities["colorless"] = [
            "Block", "Rest", "Brace", "Roll", "SharpenClaws", "Reach",
            "ManaDrain", "SoothingGlow", "Ponder", "Focus", "Metabolize",
            "Brainstorm", "Momentum", "TapLand", "GainThorns", "HolyStep",
            "LoveTap", "PrepareToJump", "MarkJump", "BoostSpellRange", "PushMove",
            "Gamble", "SoulReap", "Hunt", "Flex", "HolyBlood", "Dart", "Smack",
            "Spit", "FetusSpit", "MiniHook", "MiniDistract", "ButtScoot",
            "Confusion", "PlayDead", "Reflect", "HealBolt", "SlipThrough",
            "Dump", "Snacks", "FeatherFeet", "Reduce", "Nerf", "Trip", "Copycat",
            "DollUp", "StackTheDeck", "Infiltrate", "Burst",
            "Suppress", "Endeavor", "LotteryShottery", "CatNap", "PissYourself",
            "FindARock", "BurgeoningBlast", "BurgeoningBarrier", "BurgeoningBattery",
            "HoseOff", "Taint", "PokeWound", "WasteTime", "Desecrate", "Contort",
            "RussianRoulette", "Step", "Interchange", "LookAtMe", "Rouse", "Shift",
            "Donate", "Magnet", "ScuffItOff", "BarfBall", "DexterousHit", "Till",
            "Itch", "Meow", "Swat", "LickHeal", "Purr", "Hiss", "Knead",
            "BuyCatnip", "VetVisit", "HireHitman", "SubwayRide", "GymMembership",
            "SuperCrateBox", "BBQ", "CPR", "Blow", "Toast", "Landscape", "Zap",
            "Sunburn", "ColdShoulder", "BlowKiss", "ForbiddenFart", "WetHairball"
        ];

        abilities["hunter"] = ["LineShot", "HailOfNails", "SpawnMaggotFriend", "SpawnPooterFriend",
            "Marked", "ScatterShot", "BrambleShot", "BearTrap", "TwinShot", "CrossShot",
            "SpawnBaitTrap", "BombShot", "SummonBrambles", "FireShot", "TrailBlazer", "FocusShot",
            "Shards", "TerrainWalk", "Extend", "ChaosShot", "NeedleShot", "SpikeTrap", "FleaShot",
            "WebTrap", "LastHit", "CupidsArrow", "ArrowFlurry", "HeavyShot", "StakeOut",
            "Snipe", "Diversion", "ArrowSmith", "TacticalRetreat", "Infest", "CollectPelt", "SentryMode",
            "Pheromones", "SpawnTomTomFriend", "ScoutMe", "ShootHere", "CraftArrow", "CharmTrap",
            "BounceShot", "Picnic", "SoothingShot", "Vivisect", "PoisonLace", "SlopThePigs", "SpiderInjector",
            "PersistentHunt", "Bunker"
        ];

        abilities["mage"] = [
            "Surf", "Bolt", "Fireball", "FreezeRay", "Blast", "MagicMissile", "WallOfFire",
            "MeteorStorm", "MegaBlast", "Slow", "WindSlash", "Warp", "MageTeleport", "MageSwap",
            "Absorb", "FireArmor", "ManaMeld", "Inspire", "Telefrag", "ChaosTeleport", "CryoHeal",
            "Gust", "Blizzard", "Inferno", "Thunderburst", "DealWithTheDevil", "ForbiddenFlame",
            "ForbiddenFlood", "WaterSphere", "ChainLightning", "Shatter", "ForbiddenFulmination",
            "FireBolt", "IcicleTaser", "FreezerBurn", "Corrupt", "Jolt", "Smolder", "FireSurge",
            "IceSurge", "LightningSurge", "Creshendo", "Divide", "ForbiddenFrost", "BlackMagic",
            "Teach", "HomingBlasts", "Replicate", "Magnify", "TriAttack"
        ];

        abilities["tank"] = ["HeadButt", "ThrowShield", "ChewCud", "AssBlast", "Chew", "BatterUp",
            "BackBreaker", "Intimidate", "Toss", "BellyFlop", "TankTrample", "TankSwap", "ToTheRescue",
            "TankTantrum", "Earthquake", "RockToss", "BarbedWire", "DrawAttention", "BowlOver",
            "Clap", "TankRockSong", "RockCrusher", "BodyGuard", "Gore", "RockBlast", "RockTomb",
            "BearHug", "Fissure", "BigRock", "FlipFlop", "Lunge", "Nudge",
            "StoneGaze", "Medusa", "Anchor", "EatRock", "PlantFeet", "IronHead", "GangUp", "Aftershock",
            "SteelSkin", "FaultLine", "Demolish", "PushThrough", "Spur",
            "Supper", "FullForce", "Sandstorm", "Thicken"
        ];

        abilities["medic"] = [
            "RangedHeal", "MeleeHeal", "Malaise", "OpenWounds", "Prayer", "Convert", "Cleanse", "Purge",
            "HereticMark", "Zealot", "Haste", "Rally", "BuddyUp", "HealingFall", "RallyCharge",
            "ReverseDamage", "Rebuke", "Wish", "WitchHunt", "FriendOrFoe", "Revive", "HolyLight",
            "BornAgain", "Benediction", "Crusade", "HallowedGround", "Enlighten",
            "Anoint", "EyeForAnEye", "WrathOfGod", "Adoubement", "DivineProtection", "ChosenWarrior",
            "SwiftSanctify", "DivineGift", "HolyWeapon", "GetDown", "MedicObey", "Awaken", "Baptism",
            "Pray", "Emergency", "GuardianAngel", "Booster", "Stimulants", "BlindingLight", "CircleOfProtection",
            "CallOver", "Grace", "TurnFoe", "HealingSalve", "Heathens"
        ];

        abilities["thief"] = [
            "Assassinate", "BoostBackstab", "PoisonGas", "PoisonNail", "WeakeningNail", "SharpNail",
            "Double", "CoinToss", "MoveAgain", "AttackAgain", "Camouflage", "Shadow", "TimeWalk",
            "DoubleLoot", "Distract", "Rebound", "CutPurse", "EagleEye", "PickPocket", "Backflip",
            "Blur", "GreedStep", "Stalk", "Declaw", "QuickRoll", "Slice", "PocketSand",
            "Nightshade", "Shadowshift", "SlingShade", "Caltrops", "PierceShot", "Cheat", "VenomBarrage",
            "LootCorpse", "SeverArtery", "Fade", "SharpenNail", "SneakUp", "Shank", "StealKidney", "StealLuck",
            "ThiefSwap", "Pierce", "WindUp", "TripleNails", "SkinDisguise", "Chakram", "Jitter", "StealTime",
            "Outskirts", "PoisonDip", "LuckyPenny"
        ];

        abilities["necromancer"] = [
            "MaggotArmy", "Reanimate", "Rebirth", "Pestilence", "Weakness", "SoulSuck", "EvilIncarnate",
            "SoulLink", "WeAreOne", "BloodRain", "AnimateDead", "DeathBloom", "Scare", "SoulTransfer",
            "RandomReap", "SlitWrists", "Whisper", "DarkStep", "Leeches", "Shriek", "LastGasp",
            "Seppuku", "RaiseTheDead", "FullMoon", "Unearth", "BloodGeyser", "Flatline", "Replace",
            "SummonBones", "GigaDrain", "Bloodletting", "MassPsychosis", "Debone", "Reap", "Haunt", "Spook",
            "CarrionShot", "LifeDrain", "CoffinFlop", "DonateBlood", "Seance", "GoLimp", "DemonicPact",
            "RandomSoulLink", "RandomDualSoulLink", "Curse", "LeechSwarm", "Feed", "Hush", "ReaperStep",
            "ForbiddenFamine", "FleshGolem", "TradeLife", "AbsorbSoul", "Gravecrawl", "DigUpTheDead", "SpiderEgg", "ClewOfLeeches"
        ];

        abilities["druid"] = [
            "CrowFlutter", "CrowFlap", "ManaBomb", "SongOfSpring", "GrantLife", "SquirrelSquad",
            "SquirrelFurySwipes", "SummonSquirrel", "SummonSnake", "SummonTurtle", "SummonToad",
            "DruidSwap", "BattleCry", "PullToSafety", "BrambleBurst", "FlowerFeet",
            "ThornyFeet", "Encourage", "Protection", "Promote", "SafetyDance", "WarCry", "TigerForm",
            "MonkeyForm", "RhinoForm", "SummonCatepillar", "SleepPowder", "CallTheWind", "InspirationalSong",
            "DeathMetal", "ChaChaSlide", "BestowWisdom", "RaccoonForm", "Scavenge", "SummonCrow",
            "WeWillRockYou", "TreeForm", "HydroPump", "ControlPlants", "ControlWater", "ControlAir",
            "FamiliarSelfDestruct", "FeralMelee", "Entangle", "Lullaby", "WeAreTheChampions", "Cheerlead",
            "NaturesBlessing", "ThrowEgg", "SquirrelForm", "PlantMushroom", "Serenade", "WindyStep",
            "ElkForm", "MockingbirdForm", "FromTheTrees"
        ];

        abilities["tinkerer"] = [
            "Research", "Discharge", "Repair", "ShoddyJetpack", "SpawnDecoy", "Switcheroo", "SpringShoes",
            "Flamethrower", "TurretShot", "RocketTurretShot", "AutoPilot", "Recycle", "BuildTurret",
            "RocketSkates", "DrillDown", "ArmorUp", "FreshOffTheForge", "ElectricNail", "Craft", "Shockwave",
            "Math", "Reprogram", "Improve", "Catbot", "Bombchu", "RemoteDetonator", "ShortCircuit",
            "Electrolyze", "EjectButton", "Firecrackers", "Upgrade", "Eureka", "PunchBot", "FastHands",
            "MechSuitEject", "MechSuitBarrage", "MechSuitDash", "UnreliableShield",
            "UnreliableMissile", "SpareParts", "BatteryNuke", "ExperimentalTeleporter", "ShockTherapy",
            "BuildNuke", "InstantBarrier", "VoltTackle", "Smash", "ShedScrap", "RepairArmor", "RocketRide",
            "Roomba_Bump", "RoboVac", "NurseBot", "TeslaCoil", "RefineMaterials", "Fabricate", "Sparks", "Hone"
        ];

        abilities["psychic"] = [
            "Telekinesis", "Suggestion", "MindControl", "MegaGrav", "PsyFlutter", "MagnetPull", "MindBlast",
            "PsychicChoke", "SkyShatter", "ReadMind", "AlterDNA", "Flicker", "MindMeld", "Vaccuum",
            "GrowHead", "Ping", "FlashForward", "Order", "TemporalShards", "RealityScramble", "Glare", "BlindingFlash",
            "Snatch", "FutureSight", "MassManaLeech", "BecomeEntropy", "FastForward", "AncestralRecall",
            "CumulativeBlast", "Hallucinate", "MassHysteria", "ExtraTurnQuestion", "MindCrack", "Reset", "Mimic",
            "ChaosSwap", "Asteroid", "Stasis", "Pass", "ThinkDeep", "Puppet", "YouSeeNothing", "ForceBlast",
            "IncreaseGravity", "Manifest", "Flip", "Withdraw", "ForceCone", "Inversion", "Echo", "Slipstream",
            "MindCrack_EldritchVisage"
        ];

        abilities["monk"] = [
            "Propell", "Hadouken", "Cartwheel", "StoneFists", "Transcend", "HipToss", "Bruise", "Slapback",
            "Finisher", "Reverberate", "ComboThrow", "ComboPull", "OneWithTheWind", "Pogo", "TrainArms",
            "Porcupine", "Anneal", "DeepDive", "HopAndBlock", "TrainMind", "Meditate", "DoomPunch",
            "KiBurst", "DragonPunch", "TrainLegs", "ReallyFastRun", "DetectWeakness",
            "KineticCharge", "AirBurst", "TrainBody", "ReleaseEnergy", "Pummel", "QuickAttack", "PerfectForm",
            "WarmupStretch", "FlyingFist", "RapidFlowSpin", "SpiritBomb", "OnePunch", "UnbridledHits",
            "Kamehameha", "SideStep", "UnimpededLunge", "DoubleDragon", "FistOfFate", "Nirvana", "EmptyMind",
            "Position", "ChargeFists", "Apprentice"
        ];

        passives["colorless"] = [
            "SelfAssured", "LuckDrain", "Infested", "Worms", "Amped", "Furious", "Deathless", "MetalDetector",
            "DeathProof", "Leader", "Mange", "ETank", "Careful", "DirtyClaws", "LateBloomer", "Study",
            "NaturalHealing", "LongShot", "FastFooted", "Slugger", "Pulp", "Amplify", "DeathBoon",
            "SantaSangre", "Untouched", "Daunt", "AnimalHandler", "WhipCracker", "PressurePoints", "Gassy",
            "Dealer", "Patience", "Wiggly", "MiniMe", "BareMinimum", "Unrestricted", "DeathsDoor", "OverConfident",
            "SerialKiller", "StrengthInNumbers", "Charming", "FirstImpression", "Scavenger", "ZenkaiBoost", "Protection", "Rockin",
            "Mania", "Lucky", "OneEighty", "JestersSoul", "HotBlooded", "ToxicBlooded", "BloodBlooded", "VoidSoul"
        ];

        passives["fighter"] = [
            "BloodLust", "Avenger", "Scars", "FasterWhenHit", "KillsHeal", "Vengeful", "HamsterStyle",
            "WeaponMaster", "ShoulderCheck", "SkullSmash", "TurtleStyle", "Overpowered", "FightMe",
            "HighAsYouCanCount", "DumbMuscle", "ThickSkull", "MostValuableCat", "RatStyle", "Boned",
            "ReflexPunch", "HitMe", "Smash", "PunchFace", "Recoil"
        ];

        passives["hunter"] = [
            "TakeAim", "TowerDefense", "HuntersBoon", "BroodMother", "TaintedMother", "Quiver",
            "SplitShot", "Hazardous", "ThornArrows", "Traps", "CatchProjectiles", "TrickyTraps",
            "GravityFalls", "HawkEye", "Spotters", "LuckSwing", "Host", "Sniper", "RubberArrows",
            "TalkToAnimals", "AnimalControl", "SleepDarts", "Survivalist", "Fleabag"
        ];

        passives["mage"] = [
            "Micronaps", "HolyMantel", "Shrapnel", "BurningPaws", "LightningPaws", "IcePaws", "PawMissile",
            "Overload", "ChargeUp", "Recharged", "EnergyStorm", "FireArmor", "IceArmor",
            "Resonance", "LearnFromMe", "LightningArmor", "LongCast", "LightUpTheStage", "ElementalAttunement",
            "LatentEnergy", "Five", "MagicGuru", "One", "Two", "Four"
        ];

        passives["tank"] = [
            "Thorns", "HeavyHanded", "SlackOff", "Scabs", "ThunderThighs", "Plow",
            "PetRocks", "ToadStyle", "ChainKnockback", "ProtectiveAura", "Wrestlemaniac", "MountainForm",
            "HomeRun", "RockAspect", "WideLoad", "HardHead", "MyLeg", "Hardy", "SlowAndSteady", "FollowUp",
            "CatAPult", "ShovingMatch", "Stoic", "PriorityTarget"
        ];

        passives["medic"] = [
            "HealingAura", "NaturalHealer", "Eternal", "Blessed", "AngelicInspiration",
            "TopOff", "SharingIsCaring", "Caretaker", "MoraleBoost", "RangedMedic", "Godspeed",
            "GodWarrior", "BreathOfLife", "ThouShaltNotKill", "ThouShaltNotCovet", "BlessingOfHolyFire",
            "AlmsForThePoor", "Purifier", "VeneratedTouch", "ProtectTheWeak", "ThouShaltObey", "EnchantedRelic",
            "BlessingOfSpirit", "Heathens"
        ];

        passives["thief"] = [
            "Backstabber", "GoldenClaws", "Shadow", "PoisonTips", "Burgle", "SwiftKiller", "LongStrider",
            "DoubleThrow", "BountyHunter", "RazorClaws", "Looter", "Zip", "WeakSpot",
            "Penetrate", "AfterImage", "Shiv", "Critical", "LootHoarder", "Cripple", "Agile", "Shank",
            "FlipACoin", "ShakeDown", "SweetSpot", "Pinpoint"
        ];

        passives["necromancer"] = [
            "Vampirism", "OneWithNothing", "BedBugs", "WormLord", "InfiniteRebirth", "SacrificialLamb",
            "OffloadPain", "CambionConception", "Leechmother", "Infected", "LastGasp",
            "RelentlessDead", "ChainsOfGuilt", "DarkPriest", "Undeath", "NumbingLeeches", "EternalHealth",
            "Torpor", "SoulBound", "Superstition", "ImmortalLeeches", "CorpseConnoisseur", "Parasitic",
            "SpreadSorrow"
        ];

        passives["druid"] = [
            "SuperCrow", "NaturesGuidance", "PoisonIvy", "Pathfinder", "EmptyVessels", "WildAnimals", "BarkSkin",
            "SoothingSong", "Teamwork", "Bouquet", "GoodVibrations", "VersatileVocalist", "LikeAFish", "Encore",
            "SpecialFriends", "SneakAttack", "WildStyle", "BuddySystem", "FlowerPower", "Feral",
            "RapGod", "Animalistic", "Maestro", "MegaMinions"
        ];

        passives["tinkerer"] = [
            "VersionTwo", "WeaponProficiency", "LivingBattery", "FuzzyFeet", "EMP",
            "MrMega", "EscapeSequence", "ItemProxy", "LightningRod", "ItsAlive", "Energizer", "ReactiveArmor",
            "Nanobots", "Scrapper", "DemoMan", "DuctTape", "ArmoredPlating", "BoobyTrap", "RobotArms",
            "Conductor", "Napalm", "Ingenuity", "Shrapnel_Tinkerer", "Blacksmith"
        ];

        passives["butcher"] = [
            "Putrefy", "NeverFull", "MainCourse", "FreshMeat", "Masochist", "Glutton", "Hooked", "Stompy",
            "Barbed", "GrapplingHook", "PainGain", "WideSwing", "Confrontational", "HeaveHook", "Harpooner",
            "LordOfTheFlies", "Schadenfreude", "Gurgitator", "LooseMeat", "Hack", "BowlingBall", "Testy",
            "Incubator", "DukeOfFlies"
        ];

        passives["psychic"] = [
            "Flying", "SoulShatter", "Glow", "Blink", "FullPower", "RealityShatter", "MentalStorm",
            "Wither", "Flourish", "PsySmack", "Beckon", "MindTempest", "Overflow", "Omniscience",
            "PsionicRepel", "Enlightened", "MadVisage", "PowerUp", "TrueSight", "Radiation",
            "Drag", "Twiddle", "RepressedMemories", "EldritchVisage"
        ];

        passives["monk"] = [
            "SafeSwitching", "Mixup", "Turnabout", "MonkeyStyle", "BrickSkin", "JaggedEdges", "MindBreaker",
            "CobraStyle", "Tenderize", "LongArms", "SpreadThePain", "Harden", "IronSkin", "JetFists",
            "EnergyFists", "UnburdenedMotion", "UnburdenedStrikes", "UnburdenedThoughts",
            "RunningJab", "PerfectTechnique", "RapidFlow", "CounterBarrage", "FlowState", "DancingLights"
        ];
    }
}