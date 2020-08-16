using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class ReverseGun : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("M660");
			Tooltip.SetDefault("ta没有注意到拿反了\n无需弹药\n后坐力大到起飞");
		}

		public override void SetDefaults() 
		{
			item.damage = 22;
			item.ranged = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 9;
			item.useAnimation = 9;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5f;
            item.scale = 1.82f;
			item.value = 10000;
			item.rare = 5;
			item.UseSound = SoundID.Item11;
			item.shoot = ProjectileType<ReversePro>();
			item.shootSpeed = 22.5f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(95);
            recipe1.AddIngredient(22, 8);
            // recipe1.AddIngredient(548, 5);
            // recipe1.AddIngredient(549, 5);
            // recipe1.AddIngredient(547, 5);
            recipe1.AddTile(16);
			recipe1.SetResult(this);
			recipe1.AddRecipe();


			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(95);
            recipe2.AddIngredient(704, 8);
            // recipe1.AddIngredient(548, 5);
            // recipe1.AddIngredient(549, 5);
            // recipe1.AddIngredient(547, 5);
            recipe2.AddTile(16);
			recipe2.SetResult(this);
			recipe2.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-70, 0);
		}
	}
}