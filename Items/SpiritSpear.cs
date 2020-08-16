using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class SpiritSpear : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("精灵长枪");
			Tooltip.SetDefault("精灵的力量");
		}

		public override void SetDefaults() 
		{
			item.damage = 58;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 8;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<SpiritSpearPro>();
			item.shootSpeed = 3.8f;

            item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
		}

		public override bool CanUseItem(Player player) {
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(501, 10);
			recipe1.AddIngredient(1123);
			recipe1.AddTile(TileID.Anvils);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}
	}
}