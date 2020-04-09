﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Pets;
using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class FathomSwarmerEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fathom Swarmer Enchantment");
            Tooltip.SetDefault(
@"''
10% increased minion damage while submerged in liquid
Provides a moderate amount of light and moderately reduces breath loss in the abyss
Attacking and being attacked by enemies inflicts poison
Grants a sulphurous bubble jump that applies venom on hit
Effects of Corrosive Spine and Lumenous Amulet
Effects of Sand Cloak and Alluring Bait
Summons several pets");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 300000;
        }

        /*public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(70, 63, 69);
                }
            }
        }*/

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            calamity.Call("SetSetBonus", player, "fathomswarmer", true);
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
            {
                player.minionDamage += 0.1f;
            }
            calamity.GetItem("CorrosiveSpine").UpdateAccessory(player, hideVisual);
            calamity.GetItem("LumenousAmulet").UpdateAccessory(player, hideVisual);
            mod.GetItem("SulphurousEnchant").UpdateAccessory(player, hideVisual);

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            fargoPlayer.FathomEnchant = true;
            fargoPlayer.AddPet(SoulConfig.Instance.calamityToggles.SirenPet, hideVisual, calamity.BuffType("StrangeOrb"), calamity.ProjectileType("SirenYoung"));
            fargoPlayer.AddPet(SoulConfig.Instance.calamityToggles.FlakPet, hideVisual, ModContent.BuffType<FlakPetBuff>(), ModContent.ProjectileType<FlakPet>());
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerVisage>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBoots>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CorrosiveSpine>());
            recipe.AddIngredient(ModContent.ItemType<LumenousAmulet>());
            recipe.AddIngredient(ModContent.ItemType<SeasSearing>());
            recipe.AddIngredient(ModContent.ItemType<BelchingSaxophone>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousGrabber>());
            recipe.AddIngredient(ModContent.ItemType<FlakKraken>());
            recipe.AddIngredient(ModContent.ItemType<Atlantis>());
            recipe.AddIngredient(ModContent.ItemType<BrackishFlask>());
            recipe.AddIngredient(ModContent.ItemType<StrangeOrb>());
            recipe.AddIngredient(ModContent.ItemType<GeyserShell>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
