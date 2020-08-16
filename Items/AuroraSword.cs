using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class AuroraSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("极光");
			Tooltip.SetDefault("蕴含彗星的力量\n放出易碎的冰锥");
		}

		public override void SetDefaults() 
		{
			item.damage = 56;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<Magic>();
			item.shootSpeed = 10.5f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(484);
			recipe1.AddIngredient(502, 12);
			recipe1.AddIngredient(520, 8);
			recipe1.AddTile(TileID.MythrilAnvil);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(1192);
			recipe2.AddIngredient(502, 12);
			recipe2.AddIngredient(520, 8);
			recipe2.AddTile(TileID.MythrilAnvil);
			recipe2.SetResult(this);
			recipe2.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();

		}
	}
}