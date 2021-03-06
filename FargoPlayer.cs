using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using FargowiltasSouls.NPCs;
using FargowiltasSouls.Projectiles;
using FargowiltasSouls.Buffs.Souls;
using FargowiltasSouls.Buffs.Masomode;
using FargowiltasSouls.Projectiles.Souls;
using FargowiltasSouls.Projectiles.BossWeapons;
using FargowiltasSouls.Projectiles.Masomode;
using FargowiltasSouls.Items.Weapons.SwarmDrops;
using FargowiltasSouls.NPCs.MutantBoss;

using FargowiltasSouls.Items.Summons;
using FargowiltasSouls.NPCs.EternityMode;

namespace FargowiltasSouls
{
    public partial class FargoPlayer : ModPlayer
    {
        //for convenience
        public bool IsStandingStill;
        public float AttackSpeed;
        public float wingTimeModifier;

        public bool FreeEaterSummon = true;
        public int Screenshake;

        public bool Wood;
        public bool QueenStinger;
        public bool EridanusEmpower;
        public int EridanusTimer;

        //minions
        public bool BrainMinion;
        public bool EaterMinion;
        public bool BigBrainMinion;
        public bool DukeFishron;

        //mount
        public bool SquirrelMount;

        //pet

        public bool ChibiDevi;
        public bool MutantSpawn;
        public bool BabyAbom;

        #region enchantments
        public bool PetsActive = true;
        public bool ShadowEnchant;
        public bool CrimsonEnchant;
        public bool SpectreEnchant;
        public bool BeeEnchant;
        public bool SpiderEnchant;
        public int SummonCrit = 20;
        public bool StardustEnchant;
        public bool FreezeTime = false;
        private int freezeLength = 300;
        public bool MythrilEnchant;
        public bool FossilEnchant;
        public bool FossilBones = false;
        private int boneCD = 0;
        public bool JungleEnchant;
        private int jungleCD;
        public bool ElementEnchant;
        public bool ShroomEnchant;
        public bool CobaltEnchant;
        public int CobaltCD = 0;
        public bool SpookyEnchant;
        public bool HallowEnchant;
        public bool ChloroEnchant;
        public bool VortexEnchant;
        public bool VortexStealth = false;
        public bool AdamantiteEnchant;
        public int AdamantiteCD = 0;
        public bool FrostEnchant;
        public int IcicleCount = 0;
        private int icicleCD = 0;
        public bool PalladEnchant;
        private int palladiumCD = 0;
        public bool OriEnchant;
        public bool OriSpawn = false;
        public bool MeteorEnchant;
        private int meteorTimer = 150;
        private int meteorCD = 0;
        private bool meteorShower = false;
        public bool MoltenEnchant;
        public bool CopperEnchant;
        private int copperCD = 0;
        public bool NinjaEnchant;
        public bool FirstStrike;
        public bool NearSmoke;
        private bool hasSmokeBomb;
        private int smokeBombCD;
        private int smokeBombSlot;
        public bool IronEnchant;
        public bool IronGuard;
        public bool TurtleEnchant;
        public bool ShellHide;
        public bool LeadEnchant;
        public bool GladEnchant;
        private int gladCount = 0;
        public bool GoldEnchant;
        public bool GoldShell;
        private int goldHP;
        public bool CactusEnchant;
        public bool ForbiddenEnchant;
        public bool MinerEnchant;
        public bool PumpkinEnchant;
        private int pumpkinCD;
        public bool SilverEnchant;
        public bool PlatinumEnchant;
        public bool NecroEnchant;
        public int NecroCD;
        public bool ObsidianEnchant;
        private int obsidianCD = 0;
        public bool TinEnchant;
        private int tinCD = 0;
        public int TinCrit = 4;
        public bool TikiEnchant;
        public bool TikiMinion;
        private int actualMinions;
        public bool SolarEnchant;
        public bool ShinobiEnchant;
        public bool ValhallaEnchant;
        public bool DarkEnchant;
        private int apprenticeCD = 0;
        Vector2 prevPosition;
        public bool RedEnchant;
        public bool TungstenEnchant;
        private float tungstenPrevSizeSave = -1;

        public bool MahoganyEnchant;
        public bool BorealEnchant;
        public bool WoodEnchant;
        public bool PalmEnchant;
        public bool ShadeEnchant;
        public bool PearlEnchant;

        public bool RainEnchant;
        private int rainDamage;

        public bool AncientCobaltEnchant;
        public bool AncientShadowEnchant;
        public bool SquireEnchant;
        public bool ApprenticeEnchant;
        public bool HuntressEnchant;
        public bool MonkEnchant;
        public int MonkDashing = 0;
        private int monkTimer;
        public bool SnowEnchant;
        public bool WizardEnchant;

        public bool Solar;
        public bool Nebula;

        public bool CosmoForce;
        public bool EarthForce;
        public bool LifeForce;
        public bool NatureForce;
        public bool SpiritForce;
        public bool ShadowForce;
        public bool TerraForce;
        public bool WillForce;
        public bool WoodForce;

        //calamity
        public bool AerospecEnchant;
        public bool AstralEnchant;
        public bool BrimflameEnchant;
        public bool ReaverEnchant;
        public bool StatigelEnchant;
        public bool DaedalusEnchant;
        public bool AtaxiaEnchant;
        public bool MolluskEnchant;
        public bool OmegaBlueEnchant;
        public bool GodSlayerEnchant;
        public bool SilvaEnchant;
        public bool DemonShadeEnchant;
        public bool SulphurEnchant;
        public bool FathomEnchant;

        #endregion

        //soul effects
        public bool MagicSoul;
        public bool ThrowSoul;
        public bool RangedSoul;
        public bool RangedEssence;
        public bool BuilderMode;
        public bool UniverseEffect;
        public bool FishSoul1;
        public bool FishSoul2;
        public bool TerrariaSoul;
        public int HealTimer;
        public int HurtTimer;
        public bool Eternity;
        private float eternityDamage = 0;

        //maso items
        public bool SlimyShield;
        public bool SlimyShieldFalling;
        public bool AgitatingLens;
        public int AgitatingLensCD;
        public bool CorruptHeart;
        public int CorruptHeartCD;
        public bool GuttedHeart;
        public int GuttedHeartCD = 60; //should prevent spawning despite disabled toggle when loading into world
        public bool NecromanticBrew;
        public bool PureHeart;
        public bool PungentEyeballMinion;
        public bool FusedLens;
        public bool GroundStick;
        public bool Probes;
        public bool MagicalBulb;
        public bool SkullCharm;
        public bool PumpkingsCape;
        public bool LihzahrdTreasureBox;
        public int GroundPound;
        public bool BetsysHeart;
        public bool BetsyDashing;
        public int BetsyDashCD = 0;
        public bool MutantAntibodies;
        public bool GravityGlobeEX;
        public bool CelestialRune;
        public bool AdditionalAttacks;
        public int AdditionalAttacksTimer;
        public bool MoonChalice;
        public bool LunarCultist;
        public bool TrueEyes;
        public bool CyclonicFin;
        public int CyclonicFinCD;
        public bool MasochistSoul;
        public bool MasochistHeart;
        public bool CelestialSeal;
        public bool SandsofTime;
        public bool DragonFang;
        public bool SecurityWallet;
        public bool FrigidGemstone;
        public bool WretchedPouch;
        public int FrigidGemstoneCD;
        public bool NymphsPerfume;
        public bool NymphsPerfumeRespawn;
        public int NymphsPerfumeCD = 30;
        public bool SqueakyAcc;
        public bool RainbowSlime;
        public bool SkeletronArms;
        public bool SuperFlocko;
        public bool MiniSaucer;
        public bool TribalCharm;
        public bool TribalAutoFire;
        public bool SupremeDeathbringerFairy;
        public bool GodEaterImbue;
        public bool MutantSetBonus;
        public bool Abominationn;
        public bool PhantasmalRing;
        public bool MutantsDiscountCard;
        public bool MutantsPact;
        public bool RabiesVaccine;
        public bool TwinsEX;
        public bool TimsConcoction;
        public bool ReceivedMasoGift;
        public bool Graze;
        public int GrazeCounter;
        public double GrazeBonus;
        public bool DevianttHearts;
        public int DevianttHeartsCD;
        public bool MutantEye;
        public bool MutantEyeVisual;
        public int MutantEyeCD;
        public bool AbominableWandRevived;
        public bool AbomRebirth;
        public bool WasHurtBySomething;

        //public int PreNerfDamage;

        //debuffs
        public bool Hexed;
        public bool Unstable;
        private int unstableCD = 0;
        public bool Fused;
        public bool Shadowflame;
        public bool Oiled;
        public bool DeathMarked;
        public bool noDodge;
        public bool noSupersonic;
        public bool Bloodthirsty;
        public bool SinisterIcon;
        public bool SinisterIconDrops;

        public bool GodEater;               //defense removed, endurance removed, colossal DOT
        public bool FlamesoftheUniverse;    //activates various vanilla debuffs
        public bool MutantNibble;           //disables potions, moon bite effect, feral bite effect, disables lifesteal
        public int StatLifePrevious = -1;   //used for mutantNibble
        public bool Asocial;                //disables minions, disables pets
        public bool WasAsocial;
        public bool HidePetToggle0 = true;
        public bool HidePetToggle1 = true;
        public bool Kneecapped;             //disables running :v
        public bool Defenseless;            //-30 defense, no damage reduction, cross necklace and knockback prevention effects disabled
        public bool Purified;               //purges all other buffs
        public bool Infested;               //weak DOT that grows exponentially stronger
        public int MaxInfestTime;
        public bool FirstInfection = true;
        public float InfestedDust;
        public bool Rotting;                //inflicts DOT and almost every stat reduced
        public bool SqueakyToy;             //all attacks do one damage and make squeaky noises
        public bool Atrophied;              //melee speed and damage reduced. maybe player cannot fire melee projectiles?
        public bool Jammed;                 //ranged damage and speed reduced, all non-custom ammo set to baseline ammos
        public bool Slimed;
        public byte lightningRodTimer;
        public bool ReverseManaFlow;
        public bool CurseoftheMoon;
        public bool OceanicMaul;
        public int MaxLifeReduction;
        public bool Midas;
        public bool MutantPresence;
        public bool DevianttPresence;
        public bool Swarming;
        public bool LowGround;
        public bool Flipped;
        public bool Mash;
        public bool[] MashPressed = new bool[4];
        public int MashCounter;
        public int StealingCooldown;
        public bool LihzahrdCurse;
        public bool LihzahrdBlessing;
        public bool Berserked;

        public int MasomodeCrystalTimer = 0;
        public int MasomodeFreezeTimer = 0;
        public int MasomodeSpaceBreathTimer = 0;

        public IList<string> disabledSouls = new List<string>();

        private Mod dbzMod = ModLoader.GetMod("DBZMOD");

        public override TagCompound Save()
        {
            //idk ech
            string name = "FargoDisabledSouls" + player.name;
            var FargoDisabledSouls = new List<string>();

            if (CelestialSeal) FargoDisabledSouls.Add("CelestialSeal");
            if (MutantsDiscountCard) FargoDisabledSouls.Add("MutantsDiscountCard");
            if (MutantsPact) FargoDisabledSouls.Add("MutantsPact");
            if (ReceivedMasoGift) FargoDisabledSouls.Add("ReceivedMasoGift");
            if (RabiesVaccine) FargoDisabledSouls.Add("RabiesVaccine");

            return new TagCompound {
                    {name, FargoDisabledSouls}
                }; ;
        }

        public override void Load(TagCompound tag)
        {
            string name = "FargoDisabledSouls" + player.name;
            //string log = name + " loaded: ";

            disabledSouls = tag.GetList<string>(name);

            CelestialSeal = disabledSouls.Contains("CelestialSeal");
            MutantsDiscountCard = disabledSouls.Contains("MutantsDiscountCard");
            MutantsPact = disabledSouls.Contains("MutantsPact");
            ReceivedMasoGift = disabledSouls.Contains("ReceivedMasoGift");
            RabiesVaccine = disabledSouls.Contains("RabiesVaccine");
        }

        public override void OnEnterWorld(Player player)
        {
            disabledSouls.Clear();

            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == NPCID.LunarTowerSolar
                || Main.npc[i].type == NPCID.LunarTowerVortex
                || Main.npc[i].type == NPCID.LunarTowerNebula
                || Main.npc[i].type == NPCID.LunarTowerStardust)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        var netMessage = mod.GetPacket();
                        netMessage.Write((byte)1);
                        netMessage.Write((byte)i);
                        netMessage.Send();
                        Main.npc[i].lifeMax *= 2;
                    }
                    else
                    {
                        Main.npc[i].GetGlobalNPC<FargoSoulsGlobalNPC>().SetDefaults(Main.npc[i]);
                        Main.npc[i].life = Main.npc[i].lifeMax;
                    }
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Mash)
            {
                const int increment = 1;

                if (triggersSet.Up)
                {
                    if (!MashPressed[0])
                        MashCounter += increment;
                    MashPressed[0] = true;
                }
                else
                {
                    MashPressed[0] = false;
                }

                if (triggersSet.Left)
                {
                    if (!MashPressed[1])
                        MashCounter += increment;
                    MashPressed[1] = true;
                }
                else
                {
                    MashPressed[1] = false;
                }

                if (triggersSet.Right)
                {
                    if (!MashPressed[2])
                        MashCounter += increment;
                    MashPressed[2] = true;
                }
                else
                {
                    MashPressed[2] = false;
                }

                if (triggersSet.Down)
                {
                    if (!MashPressed[3])
                        MashCounter += increment;
                    MashPressed[3] = true;
                }
                else
                {
                    MashPressed[3] = false;
                }
            }

            if (GoldShell)
            {
                return;
            }

            if (Fargowiltas.FreezeKey.JustPressed && StardustEnchant && !player.HasBuff(ModContent.BuffType<TimeStopCD>()))
            {
                player.AddBuff(ModContent.BuffType<TimeStopCD>(), 3600);
                FreezeTime = true;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ZaWarudo").WithVolume(1f).WithPitchVariance(.5f), player.Center);
            }

            if (Fargowiltas.GoldKey.JustPressed && GoldEnchant && !player.HasBuff(ModContent.BuffType<GoldenStasisCD>()))
            {
                player.AddBuff(ModContent.BuffType<GoldenStasis>(), 600);
                player.AddBuff(ModContent.BuffType<GoldenStasisCD>(), 3600);

                goldHP = player.statLife;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Zhonyas").WithVolume(1f), player.Center);
            }

            if (Fargowiltas.SmokeBombKey.JustPressed && NinjaEnchant && hasSmokeBomb && smokeBombCD == 0 && player.controlUseItem == false && player.itemAnimation == 0 && player.itemTime == 0)
            {
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 6;

                Projectile.NewProjectile(player.Center, velocity, ProjectileID.SmokeBomb, 0, 0, player.whoAmI);

                smokeBombCD = 15;
                player.inventory[smokeBombSlot].stack--;
            }

            if (Fargowiltas.BetsyDashKey.JustPressed && BetsysHeart && BetsyDashCD <= 0)
            {
                BetsyDashCD = 180;
                if (player.whoAmI == Main.myPlayer)
                {
                    player.controlLeft = false;
                    player.controlRight = false;
                    player.controlJump = false;
                    player.controlDown = false;
                    player.controlUseItem = false;
                    player.controlUseTile = false;
                    player.controlHook = false;
                    player.controlMount = false;

                    player.immune = true;
                    player.immuneTime = 2;
                    player.hurtCooldowns[0] = 2;
                    player.hurtCooldowns[1] = 2;

                    player.itemAnimation = 0;
                    player.itemTime = 0;

                    Vector2 vel = player.DirectionTo(Main.MouseWorld) * (MasochistHeart ? 25 : 20);
                    Projectile.NewProjectile(player.Center, vel, ModContent.ProjectileType<Projectiles.Masomode.BetsyDash>(), (int)(100 * player.meleeDamage), 0f, player.whoAmI);
                    player.AddBuff(ModContent.BuffType<Buffs.Souls.BetsyDash>(), 20);
                }
            }

            if (Fargowiltas.MutantBombKey.JustPressed && MutantEye && MutantEyeCD <= 0)
            {
                MutantEyeCD = 3600;

                if (!Main.dedServ && Main.LocalPlayer.active)
                    Main.LocalPlayer.GetModPlayer<FargoPlayer>().Screenshake = 30;

                const int invulTime = 90;
                player.immune = true;
                player.immuneTime = invulTime;
                player.hurtCooldowns[0] = invulTime;
                player.hurtCooldowns[1] = invulTime;

                Main.PlaySound(SoundID.Item84, player.Center);

                const int max = 100; //make some indicator dusts
                for (int i = 0; i < max; i++)
                {
                    Vector2 vector6 = Vector2.UnitY * 30f;
                    vector6 = vector6.RotatedBy((i - (max / 2 - 1)) * 6.28318548f / max) + Main.LocalPlayer.Center;
                    Vector2 vector7 = vector6 - Main.LocalPlayer.Center;
                    int d = Dust.NewDust(vector6 + vector7, 0, 0, 229, 0f, 0f, 0, default(Color), 3f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity = vector7;
                }

                for (int i = 0; i < 50; i++) //make some indicator dusts
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, 229, 0f, 0f, 0, default(Color), 2.5f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].noLight = true;
                    Main.dust[d].velocity *= 24f;
                }

                for (int i = 0; i < Main.maxProjectiles; i++) //clear projs
                {
                    if (Main.projectile[i].active && Main.projectile[i].hostile && Main.projectile[i].damage > 0
                        && !Main.projectile[i].GetGlobalProjectile<FargoGlobalProjectile>().ImmuneToMutantBomb)
                        Main.projectile[i].Kill();
                }
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].hostile && Main.projectile[i].damage > 0
                        && !Main.projectile[i].GetGlobalProjectile<FargoGlobalProjectile>().ImmuneToMutantBomb)
                        Main.projectile[i].Kill();
                }


                void SpawnSphereRing(int max2, float speed, int damage2, float rotationModifier)
                {
                    float rotation = 2f * (float)Math.PI / max2;
                    Vector2 vel = Vector2.UnitY * speed;
                    int type = ModContent.ProjectileType<PhantasmalSphereRing>();
                    for (int i = 0; i < max; i++)
                    {
                        vel = vel.RotatedBy(rotation);
                        Projectile.NewProjectile(player.Center, vel, type, damage2, 0f, Main.myPlayer, rotationModifier * player.direction, speed);
                    }
                }

                int damage = (int)(1700 * player.magicDamage);
                SpawnSphereRing(16, 16f, damage, -1f);
                SpawnSphereRing(16, 16f, damage, 1f);
            }
        }

        public override void ResetEffects()
        {
            if (CelestialSeal)
            {
                player.extraAccessory = true;
                player.extraAccessorySlots = 2;
            }

            AttackSpeed = 1f;
            if (Screenshake > 0)
                Screenshake--;

            Wood = false;

            wingTimeModifier = 1f;

            QueenStinger = false;
            EridanusEmpower = false;

            BrainMinion = false;
            EaterMinion = false;
            BigBrainMinion = false;
            DukeFishron = false;

            SquirrelMount = false;

            ChibiDevi = false;
            MutantSpawn = false;
            BabyAbom = false;

            #region enchantments 
            PetsActive = true;
            ShadowEnchant = false;
            CrimsonEnchant = false;
            SpectreEnchant = false;
            BeeEnchant = false;
            SpiderEnchant = false;
            StardustEnchant = false;
            MythrilEnchant = false;
            FossilEnchant = false;
            JungleEnchant = false;
            ElementEnchant = false;
            ShroomEnchant = false;
            CobaltEnchant = false;
            SpookyEnchant = false;
            HallowEnchant = false;
            ChloroEnchant = false;
            VortexEnchant = false;
            AdamantiteEnchant = false;
            FrostEnchant = false;
            PalladEnchant = false;
            OriEnchant = false;
            MeteorEnchant = false;
            MoltenEnchant = false;
            CopperEnchant = false;
            NinjaEnchant = false;
            FirstStrike = false;
            NearSmoke = false;
            hasSmokeBomb = false;
            IronEnchant = false;
            IronGuard = false;
            TurtleEnchant = false;
            ShellHide = false;
            LeadEnchant = false;
            GladEnchant = false;
            GoldEnchant = false;
            GoldShell = false;
            CactusEnchant = false;
            ForbiddenEnchant = false;
            MinerEnchant = false;
            PumpkinEnchant = false;
            SilverEnchant = false;
            PlatinumEnchant = false;
            NecroEnchant = false;
            ObsidianEnchant = false;
            TinEnchant = false;
            TikiEnchant = false;
            TikiMinion = false;
            SolarEnchant = false;
            ShinobiEnchant = false;
            ValhallaEnchant = false;
            DarkEnchant = false;
            RedEnchant = false;
            TungstenEnchant = false;

            MahoganyEnchant = false;
            BorealEnchant = false;
            WoodEnchant = false;
            PalmEnchant = false;
            ShadeEnchant = false;
            PearlEnchant = false;

            RainEnchant = false;
            AncientCobaltEnchant = false;
            AncientShadowEnchant = false;
            SquireEnchant = false;
            ApprenticeEnchant = false;
            HuntressEnchant = false;
            MonkEnchant = false;
            SnowEnchant = false;
            WizardEnchant = false;

            Solar = false;
            Nebula = false;

            CosmoForce = false;
            EarthForce = false;
            LifeForce = false;
            NatureForce = false;
            SpiritForce = false;
            TerraForce = false;
            ShadowForce = false;
            WillForce = false;
            WoodForce = false;

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumResetEffects();

            //calamity
            AerospecEnchant = false;
            AstralEnchant = false;
            BrimflameEnchant = false;
            ReaverEnchant = false;
            SulphurEnchant = false;
            StatigelEnchant = false;
            DaedalusEnchant = false;
            AtaxiaEnchant = false;
            MolluskEnchant = false;
            OmegaBlueEnchant = false;
            GodSlayerEnchant = false;
            SilvaEnchant = false;
            DemonShadeEnchant = false;
            FathomEnchant = false;

            #endregion

            //souls
            MagicSoul = false;
            ThrowSoul = false;
            RangedSoul = false;
            RangedEssence = false;
            BuilderMode = false;
            UniverseEffect = false;
            FishSoul1 = false;
            FishSoul2 = false;
            TerrariaSoul = false;
            Eternity = false;

            //maso
            SlimyShield = false;
            AgitatingLens = false;
            CorruptHeart = false;
            GuttedHeart = false;
            NecromanticBrew = false;
            PureHeart = false;
            PungentEyeballMinion = false;
            FusedLens = false;
            GroundStick = false;
            Probes = false;
            MagicalBulb = false;
            SkullCharm = false;
            PumpkingsCape = false;
            LihzahrdTreasureBox = false;
            BetsysHeart = false;
            BetsyDashing = false;
            MutantAntibodies = false;
            GravityGlobeEX = false;
            CelestialRune = false;
            AdditionalAttacks = false;
            MoonChalice = false;
            LunarCultist = false;
            TrueEyes = false;
            CyclonicFin = false;
            MasochistSoul = false;
            MasochistHeart = false;
            SandsofTime = false;
            DragonFang = false;
            SecurityWallet = false;
            FrigidGemstone = false;
            WretchedPouch = false;
            NymphsPerfume = false;
            NymphsPerfumeRespawn = false;
            SqueakyAcc = false;
            RainbowSlime = false;
            SkeletronArms = false;
            SuperFlocko = false;
            MiniSaucer = false;
            TribalCharm = false;
            SupremeDeathbringerFairy = false;
            GodEaterImbue = false;
            MutantSetBonus = false;
            Abominationn = false;
            PhantasmalRing = false;
            TwinsEX = false;
            TimsConcoction = false;
            Graze = false;
            DevianttHearts = false;
            MutantEye = false;
            MutantEyeVisual = false;
            AbomRebirth = false;
            WasHurtBySomething = false;

            //debuffs
            Hexed = false;
            Unstable = false;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            Bloodthirsty = false;
            SinisterIcon = false;
            SinisterIconDrops = false;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Atrophied = false;
            Jammed = false;
            ReverseManaFlow = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Midas = false;
            MutantPresence = false;
            DevianttPresence = false;
            Swarming = false;
            LowGround = false;
            Flipped = false;
            LihzahrdCurse = false;
            LihzahrdBlessing = false;
            Berserked = false;

            if (!Mash && MashCounter > 0)
            {
                MashCounter--;
            }
            Mash = false;
        }

        public override void OnRespawn(Player player)
        {
            if (NymphsPerfumeRespawn && !EModeGlobalNPC.AnyBossAlive())
            {
                player.statLife = player.statLifeMax2;
            }
        }

        public override void UpdateDead()
        {
            if (SandsofTime && !EModeGlobalNPC.AnyBossAlive() && player.respawnTimer > 10)
                player.respawnTimer -= Eternity ? 6 : 1;

            wingTimeModifier = 1f;
            FreeEaterSummon = true;
            if (Screenshake > 0)
                Screenshake--;

            EridanusEmpower = false;
            EridanusTimer = 0;

            //debuffs
            Hexed = false;
            Unstable = false;
            unstableCD = 0;
            Fused = false;
            Shadowflame = false;
            Oiled = false;
            Slimed = false;
            noDodge = false;
            noSupersonic = false;
            lightningRodTimer = 0;

            BuilderMode = false;

            SlimyShieldFalling = false;
            CorruptHeartCD = 60;
            GuttedHeartCD = 60;
            NecromanticBrew = false;
            GroundPound = 0;
            NymphsPerfume = false;
            NymphsPerfumeCD = 30;
            PungentEyeballMinion = false;
            MagicalBulb = false;
            LunarCultist = false;
            TrueEyes = false;
            BetsyDashing = false;

            GodEater = false;
            FlamesoftheUniverse = false;
            MutantNibble = false;
            Asocial = false;
            Kneecapped = false;
            Defenseless = false;
            Purified = false;
            Infested = false;
            Rotting = false;
            SqueakyToy = false;
            Atrophied = false;
            Jammed = false;
            CurseoftheMoon = false;
            OceanicMaul = false;
            DeathMarked = false;
            Midas = false;
            Bloodthirsty = false;
            SinisterIcon = false;
            SinisterIconDrops = false;
            Graze = false;
            GrazeBonus = 0;
            DevianttHearts = false;
            MutantEye = false;
            MutantEyeVisual = false;
            MutantEyeCD = 60;
            AbominableWandRevived = false;
            AbomRebirth = false;
            WasHurtBySomething = false;
            Mash = false;
            MashCounter = 0;

            MaxLifeReduction = 0;
        }

        public override void PreUpdate()
        {
            if (HurtTimer > 0)
                HurtTimer--;

            IsStandingStill = Math.Abs(player.velocity.X) < 0.05 && Math.Abs(player.velocity.Y) < 0.05;
            
            player.npcTypeNoAggro[0] = true;

            if (FargoSoulsWorld.MasochistMode)
            {
                //falling gives you dazed. wings save you
                if (player.velocity.Y == 0f && player.wingsLogic == 0 && !player.noFallDmg)
                {
                    int num21 = 25;
                    num21 += player.extraFall;
                    int num22 = (int)(player.position.Y / 16f) - player.fallStart;
                    if (player.mount.CanFly)
                    {
                        num22 = 0;
                    }
                    if (player.mount.Cart && Minecart.OnTrack(player.position, player.width, player.height))
                    {
                        num22 = 0;
                    }
                    if (player.mount.Type == 1)
                    {
                        num22 = 0;
                    }
                    player.mount.FatigueRecovery();

                    if (((player.gravDir == 1f && num22 > num21) || (player.gravDir == -1f && num22 < -num21)))
                    {
                        player.immune = false;
                        int dmg = (int)(num22 * player.gravDir - num21) * 10;
                        if (player.mount.Active)
                            dmg = (int)(dmg * player.mount.FallDamage);

                        player.Hurt(PlayerDeathReason.ByOther(0), dmg, 0);
                        player.AddBuff(BuffID.Dazed, 120);
                    }
                    player.fallStart = (int)(player.position.Y / 16f);
                }

                if (player.ZoneUnderworldHeight)
                {
                    if (!(player.fireWalk || PureHeart))
                        player.AddBuff(BuffID.OnFire, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneJungle)
                {
                    if (Framing.GetTileSafely(player.Center).wall == WallID.LihzahrdBrickUnsafe)
                        player.AddBuff(ModContent.BuffType<LihzahrdCurse>(), 2);

                    if (player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.Poisoned, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneSnow)
                {
                    //if (!PureHeart && !Main.dayTime && Framing.GetTileSafely(player.Center).wall == WallID.None)
                    //    player.AddBuff(BuffID.Chilled, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

                    if (player.wet && !MutantAntibodies)
                    {
                        //player.AddBuff(BuffID.Frostburn, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                        MasomodeFreezeTimer++;
                        if (MasomodeFreezeTimer >= 600)
                        {
                            player.AddBuff(BuffID.Frozen, Main.expertMode && Main.expertDebuffTime > 1 ? 60 : 120);
                            MasomodeFreezeTimer = -300;
                        }
                    }
                    else
                    {
                        MasomodeFreezeTimer = 0;
                    }
                }
                else
                {
                    MasomodeFreezeTimer = 0;
                }

                /*if (player.wet && !MutantAntibodies)
                {
                    if (player.ZoneDesert)
                        player.AddBuff(BuffID.Slow, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if (player.ZoneDungeon)
                        player.AddBuff(BuffID.Cursed, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    Tile currentTile = Framing.GetTileSafely(player.Center);
                    if (currentTile.wall == WallID.GraniteUnsafe)
                        player.AddBuff(BuffID.Weak, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if (currentTile.wall == WallID.MarbleUnsafe)
                        player.AddBuff(BuffID.BrokenArmor, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }*/

                if (player.ZoneCorrupt)
                {
                    if (!PureHeart)
                        player.AddBuff(BuffID.Darkness, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if(player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.CursedInferno, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneCrimson)
                {
                    if (!PureHeart)
                        player.AddBuff(BuffID.Bleeding, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                    if (player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.Ichor, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (player.ZoneHoly && (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight) && player.active)
                {
                    if (!PureHeart)
                        player.AddBuff(ModContent.BuffType<FlippedHallow>(), 120);
                    if (player.wet && !MutantAntibodies)
                        player.AddBuff(BuffID.Confused, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                }

                if (!PureHeart && Main.raining && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && player.HeldItem.type != ItemID.Umbrella)
                {
                    Tile currentTile = Framing.GetTileSafely(player.Center);
                    if (currentTile.wall == WallID.None)
                    {
                        if (player.ZoneSnow)
                            player.AddBuff(BuffID.Chilled, Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);
                        else
                            player.AddBuff(BuffID.Wet, 2);
                        /*if (Main.hardMode)
                        {
                            lightningCounter++;

                            if (lightningCounter >= 600)
                            {
                                //tends to spawn in ceilings if the player goes indoors/underground
                                Point tileCoordinates = player.Top.ToTileCoordinates();

                                tileCoordinates.X += Main.rand.Next(-25, 25);
                                tileCoordinates.Y -= 15 + Main.rand.Next(-5, 5);

                                for (int index = 0; index < 10 && !WorldGen.SolidTile(tileCoordinates.X, tileCoordinates.Y) && tileCoordinates.Y > 10; ++index) tileCoordinates.Y -= 1;

                                Projectile.NewProjectile(tileCoordinates.X * 16 + 8, tileCoordinates.Y * 16 + 17, 0f, 0f, ProjectileID.VortexVortexLightning, 0, 2f, Main.myPlayer,
                                    0f, 0f);

                                lightningCounter = 0;
                            }
                        }*/
                    } 
                }

                if (player.wet && !(player.accFlipper || player.gills || MutantAntibodies))
                    player.AddBuff(ModContent.BuffType<Lethargic>(), 2);

                if (!PureHeart && !player.buffImmune[BuffID.Suffocation] && player.ZoneSkyHeight && player.whoAmI == Main.myPlayer)
                {
                    bool inLiquid = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
                    if (!inLiquid || !player.gills)
                    {
                        player.breath -= 3;
                        if (++MasomodeSpaceBreathTimer > 10)
                        {
                            MasomodeSpaceBreathTimer = 0;
                            player.breath--;
                        }
                        if (player.breath == 0)
                            Main.PlaySound(SoundID.Item3);
                        if (player.breath <= 0)
                            player.AddBuff(BuffID.Suffocation, 2);
                    }
                }

                if (!PureHeart && !player.buffImmune[BuffID.Webbed] && player.stickyBreak > 0)
                {
                    Vector2 tileCenter = player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.wall == WallID.SpiderUnsafe)
                    {
                        player.AddBuff(BuffID.Webbed, 30);
                        player.stickyBreak = 0;

                        Vector2 vector = Collision.StickyTiles(player.position, player.velocity, player.width, player.height);
                        if (vector.X != -1 && vector.Y != -1)
                        {
                            int num3 = (int)vector.X;
                            int num4 = (int)vector.Y;
                            WorldGen.KillTile(num3, num4, false, false, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient && !Main.tile[num3, num4].active())
                                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, num3, num4, 0f, 0, 0, 0);
                        }
                    }
                }

                if (!PureHeart && Main.bloodMoon)
                    player.AddBuff(BuffID.WaterCandle, 2);

                if (!SandsofTime)
                {
                    Vector2 tileCenter = player.Center;
                    tileCenter.X /= 16;
                    tileCenter.Y /= 16;
                    Tile currentTile = Framing.GetTileSafely((int)tileCenter.X, (int)tileCenter.Y);
                    if (currentTile != null && currentTile.type == TileID.Cactus && currentTile.nactive())
                    {
                        int damage = 20;
                        if (player.ZoneCorrupt)
                        {
                            damage = 40;
                            player.AddBuff(BuffID.CursedInferno, Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }
                        if (player.ZoneCrimson)
                        {
                            damage = 40;
                            player.AddBuff(BuffID.Ichor, Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }
                        if (player.ZoneHoly)
                        {
                            damage = 40;
                            player.AddBuff(ModContent.BuffType<Flipped>(), Main.expertMode && Main.expertDebuffTime > 1 ? 150 : 300);
                        }
                        if (player.hurtCooldowns[0] <= 0) //same i-frames as spike tiles
                            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was pricked by a Cactus."), damage, 0, false, false, false, 0);
                    }
                }

                if (MasomodeCrystalTimer > 0)
                    MasomodeCrystalTimer--;
            }

            if (!Infested && !FirstInfection)
                FirstInfection = true;

            if (Eternity && TinCrit < 50)
                TinCrit = 50;
            else if(TerrariaSoul && TinCrit < 25)
                TinCrit = 25;
            else if (TerraForce && TinCrit < 10)
                TinCrit = 10;

            if (tinCD > 0)
                tinCD--;

            if(OriSpawn && !OriEnchant)
                OriSpawn = false;

            if (VortexStealth && !VortexEnchant)
                VortexStealth = false;

            if (Unstable)
            {
                if (unstableCD == 0)
                {
                    Vector2 pos = player.position;

                    int x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                    int y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                    Vector2 teleportPos = new Vector2(x, y);

                    while (Collision.SolidCollision(teleportPos, player.width, player.height) && teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                    {
                        x = Main.rand.Next((int)pos.X - 500, (int)pos.X + 500);
                        y = Main.rand.Next((int)pos.Y - 500, (int)pos.Y + 500);
                        teleportPos = new Vector2(x, y);
                    }

                    player.Teleport(teleportPos, 1);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, teleportPos.X, teleportPos.Y, 1);

                    unstableCD = 60;
                }
                unstableCD--;
            }

            if (CopperEnchant && copperCD > 0)
                copperCD--;

            if (ObsidianEnchant && obsidianCD > 0)
                obsidianCD--;

            if (GoldShell)
            {
                player.immune = true;
                player.immuneTime = 2;
                player.hurtCooldowns[0] = 2;
                player.hurtCooldowns[1] = 2;

                //immune to DoT
                if (player.statLife < goldHP)
                    player.statLife = goldHP;

                if (player.ownedProjectileCounts[ModContent.ProjectileType<GoldShellProj>()] <= 0)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<GoldShellProj>(), 0, 0, Main.myPlayer);
            }

            if ((CobaltEnchant || AncientCobaltEnchant) && CobaltCD > 0)
                CobaltCD--;

            if ((AdamantiteEnchant) && AdamantiteCD > 0)
                AdamantiteCD--;

            if (LihzahrdTreasureBox && player.gravDir > 0 && SoulConfig.Instance.GetValue(SoulConfig.Instance.LihzahrdBoxGeysers))
            {
                if (player.controlDown && !player.mount.Active && !player.controlJump)
                {
                    if (player.velocity.Y != 0f)
                    {
                        if (player.velocity.Y < 15f)
                            player.velocity.Y = 15f;
                        if (GroundPound <= 0)
                            GroundPound = 1;
                    }
                }
                if (GroundPound > 0)
                {
                    if (player.velocity.Y < 0f || player.mount.Active)
                    {
                        GroundPound = 0;
                    }
                    else if (player.velocity.Y == 0f)
                    {
                        if (player.whoAmI == Main.myPlayer)
                        {
                            int x = (int)(player.Center.X) / 16;
                            int y = (int)(player.position.Y + player.height + 8) / 16;
                            if (GroundPound > 15 && x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY
                                && Main.tile[x, y] != null && Main.tile[x, y].nactive() && Main.tileSolid[Main.tile[x, y].type])
                            {
                                GroundPound = 0;

                                int baseDamage = 80;
                                if (MasochistSoul)
                                    baseDamage *= 2;
                                Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<ExplosionSmall>(), baseDamage * 2, 12f, player.whoAmI);
                                y -= 2;
                                for (int i = -3; i <= 3; i++)
                                {
                                    if (i == 0)
                                        continue;
                                    int tilePosX = x + 16 * i;
                                    int tilePosY = y;
                                    if (Main.tile[tilePosX, tilePosY] != null && tilePosX >= 0 && tilePosX < Main.maxTilesX)
                                    {
                                        while (Main.tile[tilePosX, tilePosY] != null && tilePosY >= 0 && tilePosY < Main.maxTilesY
                                            && !(Main.tile[tilePosX, tilePosY].nactive() && Main.tileSolid[Main.tile[tilePosX, tilePosY].type]))
                                        {
                                            tilePosY++;
                                        }
                                        Projectile.NewProjectile(tilePosX * 16 + 8, tilePosY * 16 + 8, 0f, -8f, ModContent.ProjectileType<GeyserFriendly>(), baseDamage, 8f, player.whoAmI);
                                    }
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        player.maxFallSpeed = 15f;
                        GroundPound++;
                    }
                }
            }

            //horizontal dash
            if (MonkDashing > 0)
            {
                MonkDashing--;

                //no loss of height
                //player.maxFallSpeed = 0f;
                //player.fallStart = (int)(player.position.Y / 16f);
                //player.gravity = 0f;
                player.position.Y = player.oldPosition.Y;
                player.immune = true;

                if (MonkDashing == 0)
                {
                    player.velocity *= 0.5f;
                    player.dashDelay = 0;
                }
            }
            //vertical dash
            else if (MonkDashing < 0)
            {
                MonkDashing++;

                player.immune = true;
                player.maxFallSpeed *= 30f;
                player.gravity = 1.5f;

                if (MonkDashing == 0)
                {
                    player.velocity *= 0.5f;
                    player.dashDelay = 0;
                }
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (RabiesVaccine)
            {
                player.buffImmune[BuffID.Rabies] = true;
            }

            if (StealingCooldown > 0)
                StealingCooldown--;

            if (LihzahrdCurse)
            {
                player.dangerSense = false;
                player.InfoAccMechShowWires = false;
            }

            /*if ((player.HeldItem.type == ItemID.WireCutter || player.HeldItem.type == ItemID.WireKite)
                && (LihzahrdCurse || !player.buffImmune[ModContent.BuffType<LihzahrdCurse>()])
                && (Framing.GetTileSafely(player.Center).wall == WallID.LihzahrdBrickUnsafe
                || Framing.GetTileSafely(Main.MouseWorld).wall == WallID.LihzahrdBrickUnsafe))
                player.controlUseItem = false;*/

            if (Solar)
            {
                if (!player.setSolar && !TerrariaSoul) //nerf DR
                {
                    player.endurance -= 0.15f;
                }

                player.AddBuff(BuffID.SolarShield3, 5, false);
                player.setSolar = true;
                player.solarCounter++;
                int solarCD = 240;
                if (player.solarCounter >= solarCD)
                {
                    if (player.solarShields > 0 && player.solarShields < 3)
                    {
                        for (int i = 0; i < 22; i++)
                        {
                            if (player.buffType[i] >= BuffID.SolarShield1 && player.buffType[i] <= BuffID.SolarShield2)
                            {
                                player.DelBuff(i);
                            }
                        }
                    }
                    if (player.solarShields < 3)
                    {
                        player.AddBuff(BuffID.SolarShield1 + player.solarShields, 5, false);
                        for (int i = 0; i < 16; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0f, 0f, 100)];
                            dust.noGravity = true;
                            dust.scale = 1.7f;
                            dust.fadeIn = 0.5f;
                            dust.velocity *= 5f;
                        }
                        player.solarCounter = 0;
                    }
                    else
                    {
                        player.solarCounter = solarCD;
                    }
                }
                for (int i = player.solarShields; i < 3; i++)
                {
                    player.solarShieldPos[i] = Vector2.Zero;
                }
                for (int i = 0; i < player.solarShields; i++)
                {
                    player.solarShieldPos[i] += player.solarShieldVel[i];
                    Vector2 value = (player.miscCounter / 100f * 6.28318548f + i * (6.28318548f / player.solarShields)).ToRotationVector2() * 6f;
                    value.X = player.direction * 20;
                    player.solarShieldVel[i] = (value - player.solarShieldPos[i]) * 0.2f;
                }
                if (player.dashDelay >= 0)
                {
                    player.solarDashing = false;
                    player.solarDashConsumedFlare = false;
                }
                bool flag = player.solarDashing && player.dashDelay < 0;
                if (player.solarShields > 0 || flag)
                {
                    player.dash = 3;
                }
            }

            if (Nebula && !player.setNebula)
            {
                player.setNebula = true;

                if (player.nebulaCD > 0)
                    player.nebulaCD--;

                if (!TerrariaSoul && !CosmoForce) //cap boosters
                {
                    void DecrementBuff(int buffType)
                    {
                        for (int i = 0; i < player.buffType.Length; i++)
                        {
                            if (player.buffType[i] == buffType && player.buffTime[i] > 3)
                            {
                                player.buffTime[i] = 3;
                                break;
                            }
                        }
                    };

                    if (player.nebulaLevelDamage == 3)
                        DecrementBuff(BuffID.NebulaUpDmg3);
                    if (player.nebulaLevelLife == 3)
                        DecrementBuff(BuffID.NebulaUpLife3);
                    if (player.nebulaLevelMana == 3)
                        DecrementBuff(BuffID.NebulaUpMana3);
                }
            }

            if (CyclonicFin)
            {
                if (AbominableWandRevived) //has been revived already
                {
                    if (player.statLife >= player.statLifeMax2) //can revive again
                    {
                        AbominableWandRevived = false;

                        Main.PlaySound(SoundID.Item28, player.Center);

                        const int max = 50; //make some indicator dusts
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 vector6 = Vector2.UnitY * 8f;
                            vector6 = vector6.RotatedBy((i - (max / 2 - 1)) * 6.28318548f / max) + Main.LocalPlayer.Center;
                            Vector2 vector7 = vector6 - Main.LocalPlayer.Center;
                            int d = Dust.NewDust(vector6 + vector7, 0, 0, 87, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity = vector7;
                        }

                        for (int i = 0; i < 30; i++)
                        {
                            int d = Dust.NewDust(player.position, player.width, player.height, 87, 0f, 0f, 0, default(Color), 2.5f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity *= 8f;
                        }
                    }
                    else //cannot currently revive
                    {
                        player.AddBuff(ModContent.BuffType<AbomCooldown>(), 2);
                    }
                }
            }

            if (Flipped && !player.gravControl)
            {
                player.gravControl = true;
                player.controlUp = false;
                player.gravDir = -1f;
                //player.fallStart = (int)(player.position.Y / 16f);
                //player.jump = 0;
            }

            if (DevianttHearts)
            {
                if (DevianttHeartsCD > 0)
                    DevianttHeartsCD--;
            }

            if (Graze && ++GrazeCounter > 60) //decrease graze bonus over time
            {
                GrazeCounter = 0;
                if (GrazeBonus > 0f)
                    GrazeBonus -= 0.01;
            }

            if (LowGround)
            {
                player.waterWalk = false;
                player.waterWalk2 = false;
            }

            if (BetsysHeart && BetsyDashCD > 0)
            {
                BetsyDashCD--;
                if (BetsyDashCD == 0)
                {
                    Main.PlaySound(SoundID.Item9, player.Center);

                    for (int i = 0; i < 30; i++)
                    {
                        int d = Dust.NewDust(player.position, player.width, player.height, 87, 0, 0, 0, default, 2.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].velocity *= 4f;
                    }
                }
            }

            if (GravityGlobeEX && SoulConfig.Instance.GetValue(SoulConfig.Instance.StabilizedGravity, false))
                player.gravity = Player.defaultGravity;

            if (TikiEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.TikiMinions))
            {
                actualMinions = player.maxMinions;
                player.maxMinions = 100;

                if (player.numMinions >= actualMinions)
                    TikiMinion = true;
            }

            if (NinjaEnchant)
            {
                for (int j = 0; j < player.inventory.Length; j++)
                {
                    if (hasSmokeBomb)
                    {
                        break;
                    }

                    Item item = player.inventory[j];

                    if (item.type == ItemID.SmokeBomb)
                    {
                        hasSmokeBomb = true;
                        smokeBombSlot = j;
                    }
                }

                if (smokeBombCD != 0)
                {
                    smokeBombCD--;
                }
            }

            if (Atrophied)
            {
                player.meleeSpeed = 0.01f; //melee silence
                //just in case
                player.meleeDamage = 0.01f;
                player.meleeCrit = 0;
            }

            if (SlimyShield)
            {
                //player.justJumped use this tbh
                if (SlimyShieldFalling) //landing
                {
                    if (player.velocity.Y < 0f)
                        SlimyShieldFalling = false;

                    if (player.velocity.Y == 0f)
                    {
                        SlimyShieldFalling = false;
                        if (player.whoAmI == Main.myPlayer && player.gravDir > 0 && SoulConfig.Instance.GetValue(SoulConfig.Instance.SlimyShield))
                        {
                            Main.PlaySound(SoundID.Item21, player.Center);
                            Vector2 mouse = Main.MouseWorld;
                            int damage = 15;
                            if (SupremeDeathbringerFairy)
                                damage = 25;
                            if (MasochistSoul)
                                damage = 50;
                            damage = (int)(damage * player.meleeDamage);
                            for (int i = 0; i < 3; i++)
                            {
                                Vector2 spawn = new Vector2(mouse.X + Main.rand.Next(-200, 201), mouse.Y - Main.rand.Next(600, 901));
                                Vector2 speed = mouse - spawn;
                                speed.Normalize();
                                speed *= 10f;
                                Projectile.NewProjectile(spawn, speed, ModContent.ProjectileType<SlimeBall>(), damage, 1f, Main.myPlayer);
                            }
                        }
                    }
                }
                else if (player.velocity.Y > 3f)
                {
                    SlimyShieldFalling = true;
                }
            }

            if (AgitatingLens)
            {
                if (AgitatingLensCD++ > 12)
                {
                    AgitatingLensCD = 0;
                    if ((Math.Abs(player.velocity.X) >= 5 || Math.Abs(player.velocity.Y) >= 5) && player.whoAmI == Main.myPlayer && SoulConfig.Instance.GetValue(SoulConfig.Instance.AgitatedLens))
                    {
                        int damage = 12;
                        if (SupremeDeathbringerFairy)
                            damage = 24;
                        if (MasochistSoul)
                            damage = 60;
                        damage = (int)(damage * player.magicDamage);
                        int proj = Projectile.NewProjectile(player.Center, player.velocity * 0.1f, mod.ProjectileType("BloodScytheFriendly"), damage, 5f, player.whoAmI);

                    }
                }
            }

            if (GuttedHeart && player.whoAmI == Main.myPlayer)
            {
                //player.statLifeMax2 += player.statLifeMax / 10;
                GuttedHeartCD--;

                if (player.velocity == Vector2.Zero && player.itemAnimation == 0)
                    GuttedHeartCD--;

                if (GuttedHeartCD <= 0)
                {
                    GuttedHeartCD = 900;
                    if (SoulConfig.Instance.GetValue(SoulConfig.Instance.GuttedHeart))
                    {
                        int count = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<CreeperGutted>() && Main.npc[i].ai[0] == player.whoAmI)
                                count++;
                        }
                        if (count < 5)
                        {
                            int multiplier = 1;
                            if (PureHeart)
                                multiplier = 2;
                            if (MasochistSoul)
                                multiplier = 5;
                            if (Main.netMode == NetmodeID.SinglePlayer)
                            {
                                int n = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<CreeperGutted>(), 0, player.whoAmI, 0f, multiplier);
                                if (n != 200)
                                    Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                var netMessage = mod.GetPacket();
                                netMessage.Write((byte)0);
                                netMessage.Write((byte)player.whoAmI);
                                netMessage.Write((byte)multiplier);
                                netMessage.Send();
                            }
                        }
                        else
                        {
                            int lowestHealth = -1;
                            for (int i = 0; i < 200; i++)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<CreeperGutted>() && Main.npc[i].ai[0] == player.whoAmI)
                                {
                                    if (lowestHealth < 0)
                                        lowestHealth = i;
                                    else if (Main.npc[i].life < Main.npc[lowestHealth].life)
                                        lowestHealth = i;
                                }
                            }
                            if (Main.npc[lowestHealth].life < Main.npc[lowestHealth].lifeMax)
                            {
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    int damage = Main.npc[lowestHealth].lifeMax - Main.npc[lowestHealth].life;
                                    Main.npc[lowestHealth].life = Main.npc[lowestHealth].lifeMax;
                                    CombatText.NewText(Main.npc[lowestHealth].Hitbox, CombatText.HealLife, damage);
                                }
                                else if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    var netMessage = mod.GetPacket();
                                    netMessage.Write((byte)11);
                                    netMessage.Write((byte)player.whoAmI);
                                    netMessage.Write((byte)lowestHealth);
                                    netMessage.Send();
                                }
                            }
                        }
                    }
                }
            }

            //additive with gutted heart
            //if (PureHeart) player.statLifeMax2 += player.statLifeMax / 10;

            if (Slimed)
            {
                //slowed effect
                player.moveSpeed *= .7f;
                player.jump /= 2;
            }

            if (GodEater)
            {
                player.statDefense = 0;
                player.endurance = 0;
            }

            if (MutantNibble)
            {
                //disables lifesteal, mostly
                if (player.statLife > StatLifePrevious)
                    player.statLife = StatLifePrevious;
                else
                    StatLifePrevious = player.statLife;
            }
            else
            {
                StatLifePrevious = player.statLife;
            }

            if (Defenseless)
            {
                player.statDefense -= 30;
                player.endurance = 0;
                player.longInvince = false;
                player.noKnockback = false;
            }

            if (Purified)
            {
                KillPets();

                //removes all buffs/debuffs, but it interacts really weirdly with luiafk infinite potions.
                for (int i = 21; i >= 0; i--)
                {
                    if (player.buffType[i] > 0 && player.buffTime[i] > 0 && !Main.debuff[player.buffType[i]])
                        player.DelBuff(i);
                }
            }
            else if (Asocial)
            {
                KillPets();
                player.maxMinions = 0;
                player.maxTurrets = 0;
                player.minionDamage -= .5f;
            }
            else if (WasAsocial) //should only occur when above debuffs end
            {
                player.hideMisc[0] = HidePetToggle0;
                player.hideMisc[1] = HidePetToggle1;

                WasAsocial = false;
            }

            if (Rotting)
            {
                player.moveSpeed *= 0.75f;
            }

            if (Kneecapped)
            {
                player.accRunSpeed = player.maxRunSpeed;
            }

            if (OceanicMaul)
            {
                if (MaxLifeReduction > player.statLifeMax2 - 100)
                    MaxLifeReduction = player.statLifeMax2 - 100;
                player.statLifeMax2 -= MaxLifeReduction;
                //if (player.statLife > player.statLifeMax2) player.statLife = player.statLifeMax2;
            }
            else
            {
                MaxLifeReduction = 0;
            }

            if (Eternity)
                player.statManaMax2 = 999;
            else if (UniverseEffect)
                player.statManaMax2 += 300;

            Item heldItem = player.HeldItem;
            //fix your toggles terry
            if (TungstenEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.TungstenSize, false))
            {
                if (heldItem.damage > 0 && heldItem.scale < 2.5f)
                {
                    tungstenPrevSizeSave = heldItem.scale;
                    heldItem.scale = 2.5f;
                }
            }
            else if ((!SoulConfig.Instance.GetValue(SoulConfig.Instance.TungstenSize) || !TungstenEnchant) && tungstenPrevSizeSave != -1)
            {
                heldItem.scale = tungstenPrevSizeSave;
            }

            if (AdditionalAttacks && AdditionalAttacksTimer > 0)
                AdditionalAttacksTimer--;

            if (player.whoAmI == Main.myPlayer && player.controlUseItem && player.HeldItem.type == ModContent.ItemType<EaterLauncher>())
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 300);
                    offset.Y += (float)(Math.Cos(angle) * 300);
                    Dust dust = Main.dust[Dust.NewDust(
                        player.Center + offset - new Vector2(4, 4), 0, 0,
                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
                        )];
                    dust.velocity = player.velocity;
                    if (Main.rand.Next(3) == 0)
                        dust.velocity += Vector2.Normalize(offset) * 5f;
                    dust.noGravity = true;

                    Vector2 offset2 = new Vector2();
                    double angle2 = Main.rand.NextDouble() * 2d * Math.PI;
                    offset2.X += (float)(Math.Sin(angle2) * 500);
                    offset2.Y += (float)(Math.Cos(angle2) * 500);
                    Dust dust2 = Main.dust[Dust.NewDust(
                        player.Center + offset2 - new Vector2(4, 4), 0, 0,
                        DustID.PurpleCrystalShard, 0, 0, 100, Color.White, 1f
                        )];
                    dust2.velocity = player.velocity;
                    if (Main.rand.Next(3) == 0)
                        dust2.velocity += Vector2.Normalize(offset2) * -5f;
                    dust2.noGravity = true;
                }
            }

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumPostUpdate();
            
            if (MutantPresence)
            {
                player.statDefense /= 2;
                player.endurance /= 2;
            }

            /*if (DevianttPresence)
            {
                player.statDefense /= 2;
                player.endurance /= 2;
            }*/
        }

        public override float UseTimeMultiplier(Item item)
        {
            int useTime = item.useTime;
            int useAnimate = item.useAnimation;

            if (useTime == 0 || useAnimate == 0 || item.damage <= 0)
            {
                return 1f;
            }

            if (RangedEssence && item.ranged)
            {
                AttackSpeed += .05f;
            }

            if (RangedSoul && item.ranged)
            {
                AttackSpeed += .2f;
            }

            if (MagicSoul && item.magic)
            {
                AttackSpeed += .2f;
            }

            if (ThrowSoul && item.thrown)
            {
                AttackSpeed += .2f;
            }

            //checks so weapons dont break
            while (useTime / AttackSpeed < 1)
            {
                AttackSpeed -= .1f;
            }

            while (useAnimate / AttackSpeed < 3)
            {
                AttackSpeed -= .1f;
            }

            return AttackSpeed;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Shadowflame)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= 10;
            }

            if (GodEater)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;
                player.lifeRegen -= 170;

                player.lifeRegenTime = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenCount -= 70;
            }

            if (MutantNibble)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenTime = 0;
            }

            if (Infested)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= InfestedExtraDot();
            }

            if (Rotting)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= 2;
            }

            if (CurseoftheMoon)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;

                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount = 0;

                player.lifeRegenTime = 0;
                player.lifeRegen -= 15;
            }

            if (Oiled && player.lifeRegen < 0)
            {
                player.lifeRegen *= 2;
            }

            if (MutantPresence && FargoSoulsWorld.MasochistMode)
            {
                if (player.lifeRegen > 0)
                    player.lifeRegen = 0;
                
                if (player.lifeRegenCount > 0)
                    player.lifeRegenCount -= 7;

                if (player.lifeRegenTime > 0)
                    player.lifeRegenTime -= 7;
            }

            if (AbomRebirth)
            {
                player.lifeRegen = 0;
                player.lifeRegenCount = 0;
                player.lifeRegenTime = 0;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Shadowflame)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }

            if (Hexed)
            {
                if (Main.rand.Next(3) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.BubbleBlock, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.dust[dust].color = Color.GreenYellow;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }

            if (Infested)
            {
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, 44, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), InfestedDust);
                    Main.dust[dust].noGravity = true;
                    //Main.dust[dust].velocity *= 1.8f;
                    // Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }

            if (GodEater)
            {
                if (Main.rand.Next(3) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 86, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.2f;
                    Main.dust[dust].velocity.Y -= 0.15f;
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.15f;
                g *= 0.03f;
                b *= 0.09f;
                fullBright = true;
            }

            if (FlamesoftheUniverse)
            {
                drawInfo.drawPlayer.onFire = true;
                drawInfo.drawPlayer.onFire2 = true;
                drawInfo.drawPlayer.onFrostBurn = true;
                drawInfo.drawPlayer.ichor = true;
                drawInfo.drawPlayer.burned = true;
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f) //shadowflame
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
                fullBright = true;
            }

            if (CurseoftheMoon)
            {
                if (Main.rand.Next(5) == 0)
                {
                    int d = Dust.NewDust(player.Center, 0, 0, 229, player.velocity.X * 0.4f, player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                    Main.playerDrawDust.Add(d);
                }
                if (Main.rand.Next(5) == 0)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, 229, player.velocity.X * 0.4f, player.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Y -= 1f;
                    Main.dust[d].velocity *= 2f;
                    Main.playerDrawDust.Add(d);
                }
            }

            if (DeathMarked)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width, player.height, 109, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default(Color), 1.5f);
                    Main.dust[dust].velocity.Y--;
                    if (Main.rand.Next(3) != 0)
                    {
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].velocity *= 3f;
                        Main.dust[dust].velocity.Y -= 0.5f;
                    }
                    Main.playerDrawDust.Add(dust);
                }
                r *= 0.2f;
                g *= 0.2f;
                b *= 0.2f;
                fullBright = true;
            }

            if (Fused)
            {
                if (Main.rand.Next(2) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position + new Vector2(player.width / 2, player.height / 5), 0, 0, DustID.Fire, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default(Color), 2f);
                    Main.dust[dust].velocity.Y -= 2f;
                    Main.dust[dust].velocity *= 2f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].scale += 0.5f;
                        Main.dust[dust].noGravity = true;
                    }
                    Main.playerDrawDust.Add(dust);
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Eternity)
            {
                if (crit)
                {
                    damage *= 5;
                }
            }
            else if (UniverseEffect)
            {
                if (crit)
                {
                    damage = (int)(damage * 2.5f);
                }
            }

            if (Hexed || (ReverseManaFlow && proj.magic))
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (FirstStrike)
            {
                crit = true;
                damage = (int)(damage * 1.5f);
                player.ClearBuff(ModContent.BuffType<FirstStrike>());
            }

            if (proj.minion && Asocial)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            if ((proj.melee || proj.thrown) && Atrophied)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumModifyProj(proj, target, damage, crit);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Eternity)
            {
                if (crit)
                {
                    damage *= 5;
                }
            }
            else if (UniverseEffect)
            {
                if (crit)
                {
                    damage = (int)(damage * 2.5f);
                }
            }

            if (Hexed || (ReverseManaFlow && item.magic))
            {
                target.life += damage;
                target.HealEffect(damage);

                if (target.life > target.lifeMax)
                {
                    target.life = target.lifeMax;
                }

                damage = 0;
                knockback = 0;
                crit = false;

                return;

            }

            if (SqueakyToy)
            {
                damage = 1;
                Squeak(target.Center);
                return;
            }

            if (FirstStrike)
            {
                crit = true;
                damage = (int)(damage * 1.5f);
                player.ClearBuff(ModContent.BuffType<FirstStrike>());
            }

            if (Atrophied)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumModifyNPC(target, item, damage, crit);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (!SqueakyToy) return;
            damage = 1;
            Squeak(target.Center);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy || target.friendly)
                return;

            OnHitNPCEither(target, damage, knockback, crit, proj.type);
                
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SpectreOrbs) && !target.immortal)
            {
                if (SpectreEnchant && proj.type != ProjectileID.SpectreWrath)
                {
                    SpectreHurt(proj);

                    if (SpiritForce || (crit && Main.rand.Next(5) == 0))
                    {
                        SpectreHeal(target, proj);
                    }
                }
            }

            if (PearlEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.PearlwoodStars) && Main.rand.Next(4) == 0 && proj.type != ProjectileID.HallowStar && proj.damage > 0)
            {
                //holy stars
                Main.PlaySound(SoundID.Item10, proj.position);
                for (int num479 = 0; num479 < 10; num479++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 58, proj.velocity.X * 0.1f, proj.velocity.Y * 0.1f, 150, default(Color), 1.2f);
                }
                for (int num480 = 0; num480 < 3; num480++)
                {
                    Gore.NewGore(proj.position, new Vector2(proj.velocity.X * 0.05f, proj.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
                }
                float x = proj.position.X + (float)Main.rand.Next(-400, 400);
                float y = proj.position.Y - (float)Main.rand.Next(600, 900);
                Vector2 vector12 = new Vector2(x, y);
                float num483 = proj.position.X + (float)(proj.width / 2) - vector12.X;
                float num484 = proj.position.Y + (float)(proj.height / 2) - vector12.Y;
                int num485 = 22;
                float num486 = (float)Math.Sqrt((double)(num483 * num483 + num484 * num484));
                num486 = (float)num485 / num486;
                num483 *= num486;
                num484 *= num486;
                int num487 = proj.damage;
                int num488 = Projectile.NewProjectile(x, y, num483, num484, ProjectileID.HallowStar, num487, proj.knockBack, proj.owner, 0f, 0f);
                if (num488 != 1000)
                    Main.projectile[num488].ai[1] = proj.position.Y;
            }

            if (CyclonicFin)
            {
                //target.AddBuff(ModContent.BuffType<OceanicMaul>(), 900);
                //target.AddBuff(ModContent.BuffType<CurseoftheMoon>(), 900);

                if (crit && CyclonicFinCD <= 0 && proj.type != ModContent.ProjectileType<RazorbladeTyphoonFriendly>() && SoulConfig.Instance.GetValue(SoulConfig.Instance.FishronMinion))
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;

                    int dam = 150;
                    if (MutantEye)
                        dam *= 3;
                    int damageType;
                    if (proj.melee)
                    {
                        dam = (int)(dam * player.meleeDamage);
                        damageType = 1;
                    }
                    else if (proj.ranged)
                    {
                        dam = (int)(dam * player.rangedDamage);
                        damageType = 2;
                    }
                    else if (proj.magic)
                    {
                        dam = (int)(dam * player.magicDamage);
                        damageType = 3;
                    }
                    else if (proj.minion)
                    {
                        dam = (int)(dam * player.minionDamage);
                        damageType = 4;
                    }
                    /*else if (proj.thrown)
                    {
                        dam = (int)(dam * player.thrownDamage);
                        damageType = 5;
                    }*/
                    else
                    {
                        damageType = 0;
                    }
                    Projectile.NewProjectile(spawn, vel, ModContent.ProjectileType<SpectralFishron>(), dam, 10f, proj.owner, target.whoAmI, damageType);
                }
            }

            if (CorruptHeart && CorruptHeartCD <= 0)
            {
                CorruptHeartCD = 60;
                if (proj.type != ProjectileID.TinyEater && SoulConfig.Instance.GetValue(SoulConfig.Instance.CorruptHeart))
                {
                    Main.PlaySound(SoundID.NPCHit, (int)player.Center.X, (int)player.Center.Y, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 20; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust = Main.dust[index2];
                        dust.scale = dust.scale * 1.1f;
                        Main.dust[index2].noGravity = true;
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 2.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.8f;
                        Main.dust[index2].noGravity = true;
                    }
                    int num = 2;
                    if (Main.rand.Next(3) == 0)
                        ++num;
                    if (Main.rand.Next(6) == 0)
                        ++num;
                    if (Main.rand.Next(9) == 0)
                        ++num;
                    int dam = PureHeart ? 30 : 12;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int index = 0; index < num; ++index)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-35, 36) * 0.02f * 10f,
                            Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(dam * player.meleeDamage), 1.75f, player.whoAmI);
                }
            }

            if (FrigidGemstone && FrigidGemstoneCD <= 0 && !target.immortal && proj.type != ModContent.ProjectileType<Shadowfrostfireball>())
            {
                FrigidGemstoneCD = 30;
                float screenX = Main.screenPosition.X;
                if (player.direction < 0)
                    screenX += Main.screenWidth;
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 spawn = new Vector2(screenX, screenY);
                Vector2 vel = target.Center - spawn;
                vel.Normalize();
                vel *= 8f;
                int dam = (int)(40 * player.magicDamage);
                if (MasochistSoul)
                    dam *= 2;
                Projectile.NewProjectile(spawn, vel, ModContent.ProjectileType<Shadowfrostfireball>(), dam, 6f, player.whoAmI, target.whoAmI);
            }

            if (OriEnchant && proj.type == ProjectileID.FlowerPetal)
            {
                int[] fireDebuffs = { BuffID.OnFire, BuffID.CursedInferno, BuffID.Frostburn, BuffID.ShadowFlame};
                int debuff = Main.rand.Next(4);

                target.AddBuff(fireDebuffs[debuff], 300);
                target.immune[proj.owner] = 2;
            }


            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumHitProj(proj, target, damage, crit);
        }

        public void OnHitNPCEither(NPC target, int damage, float knockback, bool crit, int projectile = -1)
        {
            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.CopperLightning) && CopperEnchant && copperCD == 0)
            {
                CopperEffect(target);
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.ObsidianExplosion) && ObsidianEnchant && obsidianCD == 0)
            {
                Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<ExplosionSmall>(), damage, 0, player.whoAmI);
                obsidianCD = 30;
            }

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.ShadewoodEffect) && target.HasBuff(ModContent.BuffType<SuperBleed>()) && projectile != ModContent.ProjectileType<SuperBlood>())
            {
                for(int i = 0; i < Main.rand.Next(3, 6); i++)
                {
                    Projectile.NewProjectile(target.Center.X, target.Center.Y - 20, 0f + Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), ModContent.ProjectileType<SuperBlood>(), 20, 0f, Main.myPlayer);
                }
            }

            if (DevianttHearts && DevianttHeartsCD <= 0)
            {
                DevianttHeartsCD = MutantEye ? 300 : 600;

                if (Main.myPlayer == player.whoAmI)
                {
                    Vector2 offset = 300 * player.DirectionFrom(Main.MouseWorld);
                    for (int i = -3; i <= 3; i++)
                    {
                        Vector2 spawnPos = player.Center + offset.RotatedBy(Math.PI / 7 * i);
                        Vector2 speed = 17 * Vector2.Normalize(Main.MouseWorld - spawnPos);

                        int heartDamage = MutantEye ? 170 : 17;
                        heartDamage = (int)(heartDamage * player.minionDamage);

                        float ai1 = (Main.MouseWorld - spawnPos).Length() / 17;
                        Projectile.NewProjectile(spawnPos, speed, ModContent.ProjectileType<FriendHeart>(), heartDamage, 3f, player.whoAmI, -1, ai1);

                        for (int j = 0; j < 20; j++)
                        {
                            Vector2 vector6 = Vector2.UnitY * 7f;
                            vector6 = vector6.RotatedBy((j - (20 / 2 - 1)) * 6.28318548f / 20) + spawnPos;
                            Vector2 vector7 = vector6 - spawnPos;
                            int d = Dust.NewDust(vector6 + vector7, 0, 0, 86, 0f, 0f, 0, default(Color), 2f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity = vector7;
                        }
                    }
                }
            }

            if (GodEaterImbue)
            {
                if (target.FindBuffIndex(ModContent.BuffType<GodEater>()) < 0 && target.aiStyle != 37)
                {
                    if (target.type != ModContent.NPCType<MutantBoss>())
                    {
                        target.DelBuff(4);
                        target.buffImmune[ModContent.BuffType<GodEater>()] = false;
                    }
                    target.AddBuff(ModContent.BuffType<GodEater>(), 420);
                }
            }

            if (GladEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.GladiatorJavelins) && projectile != ProjectileID.JavelinFriendly && gladCount == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 spawn = new Vector2(target.Center.X + Main.rand.Next(-150, 151), target.Center.Y - Main.rand.Next(600, 801));
                    Vector2 speed = target.Center - spawn;
                    speed.Normalize();
                    speed *= 15f;
                    int p = Projectile.NewProjectile(spawn, speed, ProjectileID.JavelinFriendly, damage / 4, 1f, Main.myPlayer);
                    Main.projectile[p].tileCollide = false;
                    Main.projectile[p].penetrate = 1;
                    Main.projectile[p].extraUpdates = 2;
                }

                gladCount = WillForce ? 30 : 60;
            }

            if(RainEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.RainCloud) && projectile != ProjectileID.RainFriendly && player.ownedProjectileCounts[ModContent.ProjectileType<RainCloud>()] < 1)
            {
                rainDamage += damage;

                if(rainDamage > 500)
                {
                    Projectile.NewProjectile(target.Center, new Vector2(0, -2f), ModContent.ProjectileType<RainCloud>(), damage, 0, Main.myPlayer);
                    rainDamage = 0;
                }
            }

            if (SolarEnchant && !TerrariaSoul && Main.rand.Next(4) == 0)
                target.AddBuff(ModContent.BuffType<SolarFlare>(), 300);

            if (tinCD == 0)
            {
                if (Eternity)
                {
                    if (crit && TinCrit < 100)
                    {
                        TinCrit += 10;
                    }
                    else if (TinCrit >= 100)
                    {
                        if (damage / 10 > 0 && !player.moonLeech)
                        {
                            player.statLife += damage / 10;
                            player.HealEffect(damage / 10);
                            int max = MutantNibble ? StatLifePrevious : player.statLifeMax2;
                            if (player.statLife > max)
                                player.statLife = max;
                        }

                        if (SoulConfig.Instance.GetValue(SoulConfig.Instance.EternityStacking, false))
                        {
                            eternityDamage += .1f;
                        }
                    }
                }
                else if (TerrariaSoul)
                {
                    if (crit && TinCrit < 100)
                    {
                        TinCrit += 5;
                        tinCD = 15;
                    }
                    else if (TinCrit >= 100)
                    {
                        if (HealTimer <= 0 && damage / 25 > 0)
                        {
                            if (!player.moonLeech)
                            {
                                player.statLife += damage / 25;
                                player.HealEffect(damage / 25);
                            }
                            HealTimer = 10;
                        }
                        else
                        {
                            HealTimer--;
                        }
                    }
                }
                else if (TinEnchant && crit && TinCrit < 100)
                {
                    if (TerraForce)
                    {
                        TinCrit += 5;
                        tinCD = 30;
                    }
                    else
                    {
                        TinCrit += 4;
                        tinCD = 60;
                    }
                }
            }
            

            if (PalladEnchant && !TerrariaSoul && palladiumCD == 0 && !target.immortal && !player.moonLeech)
            {
                int heal = damage / 10;

                if ((EarthForce || WizardEnchant) && heal > 16)
                    heal = 16;
                else if (!EarthForce && !WizardEnchant && heal > 8)
                    heal = 8;
                else if (heal < 1)
                    heal = 1;
                player.statLife += heal;
                player.HealEffect(heal);
                palladiumCD = 240;
            }

            if (NymphsPerfume && NymphsPerfumeCD <= 0 && !target.immortal && !player.moonLeech)
            {
                NymphsPerfumeCD = 600;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Item.NewItem(target.Hitbox, ItemID.Heart);
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    var netMessage = mod.GetPacket();
                    netMessage.Write((byte)9);
                    netMessage.Write((byte)target.whoAmI);
                    netMessage.Send();
                }
            }

            if (UniverseEffect)
                target.AddBuff(ModContent.BuffType<FlamesoftheUniverse>(), 240, true);

            if (MasochistSoul)
            {
                if (target.FindBuffIndex(ModContent.BuffType<Sadism>()) < 0 && target.aiStyle != 37)
                {
                    if (target.type != ModContent.NPCType<MutantBoss>())
                    {
                        target.DelBuff(4);
                        target.buffImmune[ModContent.BuffType<Sadism>()] = false;
                    }
                    target.AddBuff(ModContent.BuffType<Sadism>(), 600);
                }
            }
            else
            {
                if (BetsysHeart && crit)
                    target.AddBuff(BuffID.BetsysCurse, 300);

                if (PumpkingsCape && crit)
                    target.AddBuff(ModContent.BuffType<Rotting>(), 300);

                if (QueenStinger)
                    target.AddBuff(SupremeDeathbringerFairy ? BuffID.Venom : BuffID.Poisoned, 120, true);

                if (FusedLens)
                    target.AddBuff(Main.rand.Next(2) == 0 ? BuffID.CursedInferno : BuffID.Ichor, 360);
            }

            if (!TerrariaSoul)
            {
                if (ShadowEnchant && Main.rand.Next(15) == 0)
                    target.AddBuff(BuffID.Darkness, 600, true);

                if (FrostEnchant)
                    target.AddBuff(BuffID.Frostburn, 300);

                if (LeadEnchant && Main.rand.Next(5) == 0)
                    target.AddBuff(ModContent.BuffType<LeadPoison>(), 30);
            }

            if (GroundStick && Main.rand.Next(10) == 0 && SoulConfig.Instance.GetValue(SoulConfig.Instance.LightningRod))
                target.AddBuff(ModContent.BuffType<LightningRod>(), 300);

            if (GoldEnchant)
                target.AddBuff(BuffID.Midas, 120, true);

            if (DragonFang && !target.boss && !target.buffImmune[ModContent.BuffType<ClippedWings>()] && Main.rand.Next(10) == 0)
            {
                target.velocity.X = 0f;
                target.velocity.Y = 10f;
                target.AddBuff(ModContent.BuffType<ClippedWings>(), 240);
                target.netUpdate = true;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy || target.friendly)
                return;

            OnHitNPCEither(target, damage, knockback, crit);

            if (SoulConfig.Instance.GetValue(SoulConfig.Instance.SpectreOrbs) && SpectreEnchant)
            {
                //forced orb spawn reeeee
                float num = 4f;
                float speedX = Main.rand.Next(-100, 101);
                float speedY = Main.rand.Next(-100, 101);
                float num2 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                num2 = num / num2;
                speedX *= num2;
                speedY *= num2;
                Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(target.position, new Vector2(speedX, speedY), ProjectileID.SpectreWrath, damage / 2, 0, player.whoAmI, target.whoAmI);

                if ((SpiritForce || (crit && Main.rand.Next(5) == 0)) && p != null)
                {
                    SpectreHeal(target, p);
                }
            }

            if (CyclonicFin)
            {
                //target.AddBuff(ModContent.BuffType<OceanicMaul>(), 900);
                //target.AddBuff(ModContent.BuffType<CurseoftheMoon>(), 900);

                if (crit && CyclonicFinCD <= 0 && SoulConfig.Instance.GetValue(SoulConfig.Instance.FishronMinion))
                {
                    CyclonicFinCD = 360;

                    float screenX = Main.screenPosition.X;
                    if (player.direction < 0)
                        screenX += Main.screenWidth;
                    float screenY = Main.screenPosition.Y;
                    screenY += Main.rand.Next(Main.screenHeight);
                    Vector2 spawn = new Vector2(screenX, screenY);
                    Vector2 vel = target.Center - spawn;
                    vel.Normalize();
                    vel *= 27f;
                    int dam = (int)(150 * player.meleeDamage);
                    if (MutantEye)
                        dam *= 3;
                    int damageType = 1;
                    Projectile.NewProjectile(spawn, vel, ModContent.ProjectileType<SpectralFishron>(), dam, 10f, player.whoAmI, target.whoAmI, damageType);
                }
            }

            if (CorruptHeart && CorruptHeartCD <= 0)
            {
                CorruptHeartCD = 60;
                if (SoulConfig.Instance.GetValue(SoulConfig.Instance.CorruptHeart))
                {
                    Main.PlaySound(SoundID.NPCHit, (int)player.Center.X, (int)player.Center.Y, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 20; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust = Main.dust[index2];
                        dust.scale = dust.scale * 1.1f;
                        Main.dust[index2].noGravity = true;
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(player.position, player.width, player.height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 2.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.8f;
                        Main.dust[index2].noGravity = true;
                    }
                    int num = 2;
                    if (Main.rand.Next(3) == 0)
                        ++num;
                    if (Main.rand.Next(6) == 0)
                        ++num;
                    if (Main.rand.Next(9) == 0)
                        ++num;
                    int dam = PureHeart ? 30 : 12;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int index = 0; index < num; ++index)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-35, 36) * 0.02f * 10f,
                            Main.rand.Next(-35, 36) * 0.02f * 10f, ProjectileID.TinyEater, (int)(dam * player.meleeDamage), 1.75f, player.whoAmI);
                }
            }

            if (FrigidGemstone && FrigidGemstoneCD <= 0 && !target.immortal)
            {
                FrigidGemstoneCD = 30;
                float screenX = Main.screenPosition.X;
                if (player.direction < 0)
                    screenX += Main.screenWidth;
                float screenY = Main.screenPosition.Y;
                screenY += Main.rand.Next(Main.screenHeight);
                Vector2 spawn = new Vector2(screenX, screenY);
                Vector2 vel = target.Center - spawn;
                vel.Normalize();
                vel *= 10f;
                int dam = (int)(40 * player.magicDamage);
                Projectile.NewProjectile(spawn, vel, ModContent.ProjectileType<Shadowfrostfireball>(), dam, 6f, player.whoAmI, target.whoAmI);
            }

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumHitNPC(target, item, crit);
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (ShroomEnchant && SoulConfig.Instance.ShroomiteShrooms && player.stealth == 0 && !item.noMelee && (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9)))
            {
                //hellish code from hammush
                float num340 = 0f;
                float num341 = 0f;
                float num342 = 0f;
                float num343 = 0f;
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
                {
                    num340 = -7f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
                {
                    num340 = -6f;
                    num341 = 2f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5))
                {
                    num340 = -4f;
                    num341 = 4f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
                {
                    num340 = -2f;
                    num341 = 6f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
                {
                    num341 = 7f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
                {
                    num343 = 26f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
                {
                    num343 -= 4f;
                    num342 -= 20f;
                }
                if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
                {
                    num342 += 6f;
                }
                if (player.direction == -1)
                {
                    if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
                    {
                        num343 -= 8f;
                    }
                    if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
                    {
                        num343 -= 6f;
                    }
                }
                num340 *= 1.5f;
                num341 *= 1.5f;
                num343 *= (float)player.direction;
                num342 *= player.gravDir;
                Projectile.NewProjectile((float)(hitbox.X + hitbox.Width / 2) + num343, (float)(hitbox.Y + hitbox.Height / 2) + num342, (float)player.direction * num341, num340 * player.gravDir, ModContent.ProjectileType<ShroomiteShroom>(), item.damage / 2, 0f, player.whoAmI, 0f, 0f);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (npc.GetGlobalNPC<FargoSoulsGlobalNPC>().CurseoftheMoon)
                damage = (int)(damage * 0.85);
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<NPCs.DeviBoss.DeviBoss>()))
            {
                ((NPCs.DeviBoss.DeviBoss)Main.npc[EModeGlobalNPC.deviBoss].modNPC).playerInvulTriggered = true;
            }

            if (FargoSoulsWorld.MasochistMode && EModeGlobalNPC.BossIsAlive(ref EModeGlobalNPC.moonBoss, NPCID.MoonLordCore)
                && player.Distance(Main.npc[EModeGlobalNPC.moonBoss].Center) < 2500)
            {
                player.AddBuff(ModContent.BuffType<CurseoftheMoon>(), 180);
            }

            if (IronGuard && internalTimer > 0 && !player.immune)
            {
                player.immune = true;
                int invul = player.longInvince ? 120 : 60;
                player.immuneTime = invul;
                player.hurtCooldowns[0] = invul;
                player.hurtCooldowns[1] = invul;
                player.AddBuff(BuffID.ParryDamageBuff, 300);
                return false;
            }

            if (SqueakyAcc && SoulConfig.Instance.GetValue(SoulConfig.Instance.SqueakyToy) && Main.rand.Next(10) == 0)
            {
                Squeak(player.Center);
                damage = 1;
            }

            if (DeathMarked)
            {
                damage *= 3;
            }

            return true;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            WasHurtBySomething = true;

            if (MythrilEnchant && !TerrariaSoul)
            {
                player.AddBuff(ModContent.BuffType<DisruptedFocus>(), 300);
            }

            if(TinEnchant)
            {
                if(Eternity)
                {
                    TinCrit = 50;
                    eternityDamage = 0;
                }
                else if (TerrariaSoul && TinCrit != 25)
                {
                    TinCrit = 25;
                }
                else if(TerraForce && TinCrit != 10)
                {
                    TinCrit = 10;
                }
                else if(TinCrit != 4)
                {
                    TinCrit = 4;
                }
            }

            if (HurtTimer <= 0)
            {
                HurtTimer = 20;

                if (MoonChalice)
                {
                    if (SoulConfig.Instance.GetValue(SoulConfig.Instance.AncientVisions))
                    {
                        int dam = 50;
                        if (MasochistSoul)
                            dam *= 2;
                        for (int i = 0; i < 5; i++)
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
                                ModContent.ProjectileType<AncientVision>(), (int)(dam * player.minionDamage), 6f, player.whoAmI);
                    }
                }
                else if (CelestialRune && SoulConfig.Instance.GetValue(SoulConfig.Instance.AncientVisions))
                {
                    Projectile.NewProjectile(player.Center, new Vector2(0, -10), ModContent.ProjectileType<AncientVision>(),
                        (int)(40 * player.minionDamage), 3f, player.whoAmI);
                }

                if (LihzahrdTreasureBox && SoulConfig.Instance.GetValue(SoulConfig.Instance.LihzahrdBoxSpikyBalls))
                {
                    int dam = 60;
                    if (MasochistSoul)
                        dam *= 2;
                    for (int i = 0; i < 9; i++)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, Main.rand.Next(-10, 11), Main.rand.Next(-10, 11),
                            ModContent.ProjectileType<LihzahrdSpikyBallFriendly>(), (int)(dam * player.meleeDamage), 2f, player.whoAmI);
                }

                if (WretchedPouch && SoulConfig.Instance.GetValue(SoulConfig.Instance.WretchedPouch))
                {
                    Vector2 vel = new Vector2(9f, 0f).RotatedByRandom(2 * Math.PI);
                    int dam = 30;
                    if (MasochistSoul)
                        dam *= 3;
                    for (int i = 0; i < 6; i++)
                    {
                        Vector2 speed = vel.RotatedBy(2 * Math.PI / 6 * (i + Main.rand.NextDouble() - 0.5));
                        float ai1 = Main.rand.Next(10, 80) * (1f / 1000f);
                        if (Main.rand.Next(2) == 0)
                            ai1 *= -1f;
                        float ai0 = Main.rand.Next(10, 80) * (1f / 1000f);
                        if (Main.rand.Next(2) == 0)
                            ai0 *= -1f;
                        Projectile.NewProjectile(player.Center, speed, ModContent.ProjectileType<ShadowflameTentacle>(),
                            (int)(dam * player.magicDamage), 3.75f, player.whoAmI, ai0, ai1);
                    }
                }

                if (MoltenEnchant && SoulConfig.Instance.GetValue(SoulConfig.Instance.MoltenExplosion))
                {
                    int baseDamage = 150;
                    if (NatureForce)
                        baseDamage = 250;
                    if (TerrariaSoul)
                        baseDamage = 500;

                    Projectile p = FargoGlobalProjectile.NewProjectileDirectSafe(player.Center, Vector2.Zero, ModContent.ProjectileType<Explosion>(), (int)(baseDamage * player.meleeDamage), 0f, Main.myPlayer);
                    if (p != null)
                        p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }

            if (Midas && Main.myPlayer == player.whoAmI)
                player.DropCoins();

            GrazeBonus = 0;
            GrazeCounter = 0;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            bool retVal = true;

            if (player.statLife <= 0) //revives
            {
                /*if (MutantSetBonus && player.whoAmI == Main.myPlayer && retVal && player.FindBuffIndex(ModContent.BuffType<MutantRebirth>()) == -1)
                {
                    player.statLife = player.statLifeMax2;
                    player.HealEffect(player.statLifeMax2);
                    player.immune = true;
                    player.immuneTime = 180;
                    player.hurtCooldowns[0] = 180;
                    player.hurtCooldowns[1] = 180;
                    Main.NewText("You've been revived!", Color.LimeGreen);
                    player.AddBuff(ModContent.BuffType<MutantRebirth>(), 10800);
                    Projectile.NewProjectile(player.Center, -Vector2.UnitY, ModContent.ProjectileType<GiantDeathray>(), (int)(7000 * player.minionDamage), 10f, player.whoAmI);
                    retVal = false;
                }*/
                if (player.whoAmI == Main.myPlayer && retVal && AbomRebirth)
                {
                    if (!WasHurtBySomething)
                    {
                        player.statLife = 1;
                        return false; //this is deliberate
                    }
                }

                if (player.whoAmI == Main.myPlayer && retVal && player.FindBuffIndex(ModContent.BuffType<Revived>()) == -1)
                {
                    if (Eternity)
                    {
                        int heal = player.statLifeMax2 / 2 > 200 ? player.statLifeMax2 / 2 : 200;
                        player.statLife = heal;
                        player.HealEffect(heal);
                        player.immune = true;
                        player.immuneTime = player.longInvince ? 180 : 120;
                        Main.NewText("You've been revived!", 175, 75);
                        player.AddBuff(ModContent.BuffType<Revived>(), 10800);
                        retVal = false;
                    }
                    else if (TerrariaSoul)
                    {
                        player.statLife = 200;
                        player.HealEffect(200);
                        player.immune = true;
                        player.immuneTime = player.longInvince ? 180 : 120;
                        Main.NewText("You've been revived!", 175, 75);
                        player.AddBuff(ModContent.BuffType<Revived>(), 14400);
                        retVal = false;
                    }
                    else if (FossilEnchant)
                    {
                        int heal = SpiritForce ? 100 : 20;
                        player.statLife = heal;
                        player.HealEffect(heal);
                        player.immune = true;
                        player.immuneTime = player.longInvince ? 300 : 200;
                        FossilBones = true;
                        Main.NewText("You've been revived!", 175, 75);
                        player.AddBuff(ModContent.BuffType<Revived>(), 18000);
                        retVal = false;
                    }
                }

                if (player.whoAmI == Main.myPlayer && retVal && CyclonicFin && !AbominableWandRevived)
                {
                    AbominableWandRevived = true;
                    int heal = 1;
                    player.statLife = heal;
                    player.HealEffect(heal);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    Main.NewText("You've been revived!", Color.Yellow);
                    player.AddBuff(ModContent.BuffType<AbomRebirth>(), MutantEye ? 600 : 900);
                    retVal = false;
                }
            }
            
            //add more tbh
            if (Infested && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " could not handle the infection.");
            }

            if (Rotting && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " rotted away.");
            }

            if ((GodEater || FlamesoftheUniverse || CurseoftheMoon) && damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was annihilated by divine wrath.");
            }

            if (DeathMarked)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was reaped by the cold hand of death.");
            }

            /*if (MutantPresence)
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " was penetrated.");
            }*/

            if (StatLifePrevious > 0 && player.statLife > StatLifePrevious)
                StatLifePrevious = player.statLife;

            if (MutantSetBonus && player.whoAmI == Main.myPlayer && player.statLife > 0 && SoulConfig.Instance.GetValue(SoulConfig.Instance.ReviveDeathray, false))
            {
                player.immune = true;
                if (player.immuneTime < 180)
                    player.immuneTime = 180;
                if (player.hurtCooldowns[0] < 180)
                    player.hurtCooldowns[0] = 180;
                if (player.hurtCooldowns[1] < 180)
                    player.hurtCooldowns[1] = 180;
                Projectile.NewProjectile(player.Center, -Vector2.UnitY, ModContent.ProjectileType<GiantDeathray>(), (int)(7000 * player.minionDamage), 10f, player.whoAmI);
            }

            return retVal;
        }

        public override void PostUpdateEquips()
        {
            player.wingTimeMax = (int)(player.wingTimeMax * wingTimeModifier);

            if (noDodge)
            {
                player.onHitDodge = false;
                player.shadowDodge = false;
                player.blackBelt = false;
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (IronGuard)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
        }

        public void AddPet(bool toggle, bool vanityToggle, int buff, int proj)
        {
            if(vanityToggle)
            {
                PetsActive = false;
                return;
            }

            if (SoulConfig.Instance.GetValue(toggle) && player.FindBuffIndex(buff) == -1 && player.ownedProjectileCounts[proj] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, 0, 0f, player.whoAmI);
            }
        }

        public void AddMinion(bool toggle, int proj, int damage, float knockback)
        {
            if(player.ownedProjectileCounts[proj] < 1 && player.whoAmI == Main.myPlayer && SoulConfig.Instance.GetValue(toggle))
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, proj, damage, knockback, Main.myPlayer);
        }

        private void KillPets()
        {
            int petId = player.miscEquips[0].buffType;
            int lightPetId = player.miscEquips[1].buffType;

            player.buffImmune[petId] = true;
            player.buffImmune[lightPetId] = true;

            player.ClearBuff(petId);
            player.ClearBuff(lightPetId);

            //memorizes player selections
            if (!WasAsocial)
            {
                HidePetToggle0 = player.hideMisc[0];
                HidePetToggle1 = player.hideMisc[1];

                if (Asocial)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                        if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].minion)
                            Main.projectile[i].Kill();
                }

                WasAsocial = true;
            }

            //disables pet and light pet too!
            if (!player.hideMisc[0])
            {
                player.TogglePet();
            }

            if (!player.hideMisc[1])
            {
                player.ToggleLight();
            }

            player.hideMisc[0] = true;
            player.hideMisc[1] = true;
        }

        public void Squeak(Vector2 center)
        {
            if (!Main.dedServ)
            {
                int rng = Main.rand.Next(6);

                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/SqueakyToy/squeak" + (rng + 1)).WithVolume(1f).WithPitchVariance(.5f), center);
            }
        }

        private int InfestedExtraDot()
        {
            int buffIndex = player.FindBuffIndex(ModContent.BuffType<Infested>());
            if (buffIndex == -1)
                return 0;

            int timeLeft = player.buffTime[buffIndex];
            float baseVal = (float)(MaxInfestTime - timeLeft) / 120; //change the denominator to adjust max power of DOT
            int modifier = (int)(baseVal * baseVal + 8);

            InfestedDust = baseVal / 10 + 1f;
            if (InfestedDust > 5f)
                InfestedDust = 5f;

            return modifier;
        }

        public void AllDamageUp(float dmg)
        {
            player.magicDamage += dmg;
            player.meleeDamage += dmg;
            player.rangedDamage += dmg;
            player.minionDamage += dmg;

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumDamage(dmg);

            //if (Fargowiltas.Instance.CalamityLoaded) CalamityDamage(dmg);

            //if (Fargowiltas.Instance.DBZMODLoaded) DBTDamage(dmg);
        }

        //private void CalamityDamage(float dmg)
        //{
        //    ModLoader.GetMod("CalamityMod").Call("AddRogueDamage", player, dmg);
        //}

        //private void DBTDamage(float dmg)
        //{
        //    DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>();
        //    dbtPlayer.KiDamage += dmg;
        //}

        public void AllCritUp(int crit)
        {
            player.meleeCrit += crit;
            player.rangedCrit += crit;
            player.magicCrit += crit;

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumCrit(crit);

            //if (Fargowiltas.Instance.CalamityLoaded) CalamityCrit(crit);

            //if (Fargowiltas.Instance.DBZMODLoaded) DBTCrit(crit);
        }

        //private void CalamityCrit(int crit)
        //{
        //    ModLoader.GetMod("CalamityMod").Call("AddRogueCrit", player, crit);
        //}

        //private void DBTCrit(int crit)
        //{
        //    DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>();
        //    dbtPlayer.kiCrit += crit;
        //}

        public void AllCritEquals(int crit)
        {
            player.meleeCrit = crit;
            player.rangedCrit = crit;
            player.magicCrit = crit;
            SummonCrit = crit;

            //if (Fargowiltas.Instance.ThoriumLoaded) ThoriumCritEquals(crit);

            //if (Fargowiltas.Instance.CalamityLoaded) CalamityCritEquals(crit);

            //if (Fargowiltas.Instance.DBZMODLoaded) DBTCritEquals(crit);
        }

        //private void CalamityCritEquals(int crit)
        //{
        //    player.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().throwingCrit = crit;
        //}

        //private void DBTCritEquals(int crit)
        //{
        //    DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>();
        //    dbtPlayer.kiCrit = crit;
        //}

        public int HighestDamageTypeScaling(int dmg)
        {
            List<float> types = new List<float> { player.meleeDamage, player.rangedDamage, player.magicDamage, player.minionDamage};
            
            return (int)(types.Max() * dmg);
        }

        public override bool PreItemCheck()
        {
            if (Berserked || (TribalCharm && SoulConfig.Instance.TribalCharm && player.HeldItem.type != ItemID.RodofDiscord && player.HeldItem.fishingPole == 0 ))
            {
                TribalAutoFire = player.HeldItem.autoReuse;
                player.HeldItem.autoReuse = true;
            }    

            /*if (FargoSoulsWorld.MasochistMode) //maso item nerfs
            {
                PreNerfDamage = player.HeldItem.damage;
                player.HeldItem.damage = (int)(player.HeldItem.damage * MasoItemNerfs(player.HeldItem.type));
            }*/

            return true;
        }

        public override void PostItemCheck()
        {
            if (Berserked || (TribalCharm && SoulConfig.Instance.TribalCharm && player.HeldItem.type != ItemID.RodofDiscord))
            {
                player.HeldItem.autoReuse = TribalAutoFire;
            }

            /*if (FargoSoulsWorld.MasochistMode) //revert maso item nerfs
            {
                player.HeldItem.damage = PreNerfDamage;
            }*/
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            if (FargoSoulsWorld.MasochistMode)
                mult *= MasoItemNerfs(item.type);
        }

        private float MasoItemNerfs(int type)
        {
            switch (type)
            {
                case ItemID.DaedalusStormbow:
                case ItemID.StarCannon:
                case ItemID.DD2BetsyBow:
                case ItemID.ElectrosphereLauncher:
                case ItemID.SnowmanCannon:
                    return 2f / 3f;

                case ItemID.Razorpine:
                case ItemID.BlizzardStaff:
                    AttackSpeed -= 2f / 3f;
                    return 2f / 3f;

                case ItemID.Uzi:
                case ItemID.Megashark:
                case ItemID.ChlorophyteShotbow:
                case ItemID.BeesKnees:
                case ItemID.PhoenixBlaster:
                case ItemID.LastPrism:
                case ItemID.Tsunami:
                case ItemID.Phantasm:
                case ItemID.OnyxBlaster:
                case ItemID.ChainGun:
                case ItemID.HellwingBow:
                case ItemID.Beenade:
                case ItemID.Handgun:
                case ItemID.DemonScythe:
                case ItemID.SpikyBall:
                case ItemID.SDMG:
                case ItemID.Xenopopper:
                case ItemID.NebulaArcanum:
                    return 0.75f;

                default:
                    return 1f;
            }
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (bait.type == ModContent.ItemType<TruffleWormEX>())
            {
                caughtType = 0;
                bool spawned = false;
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].bobber
                        && Main.projectile[i].owner == player.whoAmI && player.whoAmI == Main.myPlayer)
                    {
                        Main.projectile[i].ai[0] = 2f; //cut fishing lines
                        Main.projectile[i].netUpdate = true;

                        if (!spawned && Main.projectile[i].wet && FargoSoulsWorld.MasochistMode && !NPC.AnyNPCs(NPCID.DukeFishron)) //should spawn boss
                        {
                            spawned = true;
                            if (Main.netMode == NetmodeID.SinglePlayer) //singleplayer
                            {
                                EModeGlobalNPC.spawnFishronEX = true;
                                NPC.NewNPC((int)Main.projectile[i].Center.X, (int)Main.projectile[i].Center.Y + 100,
                                    NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, player.whoAmI);
                                EModeGlobalNPC.spawnFishronEX = false;
                                Main.NewText("Duke Fishron EX has awoken!", 50, 100, 255);
                            }
                            else if (Main.netMode == NetmodeID.MultiplayerClient) //MP, broadcast(?) packet from spawning player's client
                            {
                                var netMessage = mod.GetPacket();
                                netMessage.Write((byte)77);
                                netMessage.Write((byte)player.whoAmI);
                                netMessage.Write((int)Main.projectile[i].Center.X);
                                netMessage.Write((int)Main.projectile[i].Center.Y + 100);
                                netMessage.Send();
                            }
                            else if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.BroadcastChatMessage(Terraria.Localization.NetworkText.FromLiteral("???????"), Color.White);
                            }
                        }
                    }
                }
                if (spawned)
                {
                    bait.stack--;
                    if (bait.stack <= 0)
                        bait.SetDefaults(0);
                }
            }
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            if (player.whoAmI == Main.myPlayer && GuttedHeart && SoulConfig.Instance.GetValue(SoulConfig.Instance.GuttedHeart))
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.type == ModContent.NPCType<CreeperGutted>() && npc.ai[0] == player.whoAmI)
                    {
                        int heal = npc.lifeMax - npc.life;

                        if (heal > 0)
                        {
                            npc.HealEffect(heal);
                            npc.life = npc.lifeMax;
                        }
                    }
                }
            }
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (BetsyDashing) //dont draw player during betsy dash
                while (layers.Count > 0)
                    layers.RemoveAt(0);

            if (SquirrelMount)
            {
                foreach (PlayerLayer playerLayer in layers)
                {
                    if (playerLayer != PlayerLayer.MountBack && playerLayer != PlayerLayer.MountFront && playerLayer != PlayerLayer.MiscEffectsFront && playerLayer != PlayerLayer.MiscEffectsBack)
                    {
                        playerLayer.visible = false;
                    }
                }
            }
        }

        public override void ModifyScreenPosition()
        {
            if (Screenshake > 0)
                Main.screenPosition += Main.rand.NextVector2Circular(7, 7);
        }
    }
}
