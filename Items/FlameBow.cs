using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class FlameBow : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("乌勒尔");
			Tooltip.SetDefault("极强的穿透力\n无需弹药\n在时间的推进下而被人遗忘");
		}

		public override void SetDefaults() 
		{
			item.damage = 2333;
			item.ranged = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 100;
			item.useAnimation = 100;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
            item.scale = 1.82f;
			item.noMelee = true;
			item.value = 10000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<FireBolt>();
			item.shootSpeed = 22.5f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(120);
            recipe1.AddIngredient(1225, 18);
            recipe1.AddIngredient(548, 5);
            recipe1.AddIngredient(549, 5);
            recipe1.AddIngredient(547, 5);
            recipe1.AddTile(TileID.MythrilAnvil);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-7, 0);
		}
	}
}