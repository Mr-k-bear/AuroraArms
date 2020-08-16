using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class TrueAuroraSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("真·极光");
			Tooltip.SetDefault("蕴含彗星的力量");
		}

		public override void SetDefaults() 
		{
			item.damage = 132;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.scale = 1.5f;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 1;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<TrueMagic>();
			item.shootSpeed = 11.2f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
			recipe1.AddIngredient(ItemType<AuroraSword>());
			recipe1.AddIngredient(1570);
			recipe1.AddTile(TileID.MythrilAnvil);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}
	}
}