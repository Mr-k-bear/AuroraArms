using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class M666s : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("M660-s");
			Tooltip.SetDefault("ta终于注意到拿反了\n无需弹药\n后坐力小了很多");
		}

		public override void SetDefaults() 
		{
			item.damage = 44;
			item.ranged = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 6;
			item.useAnimation = 6;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5f;
            item.scale = 1.82f;
			item.value = 10000;
			item.rare = 5;
			item.UseSound = SoundID.Item11;
			item.shoot = ProjectileType<M6s>();
			item.shootSpeed = 22.5f;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemType<ReverseGun>());
            recipe1.AddIngredient(520, 5);
            recipe1.AddIngredient(324);
            // recipe1.AddIngredient(549, 5);
            // recipe1.AddIngredient(547, 5);
            recipe1.AddTile(16);
			recipe1.SetResult(this);
			recipe1.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}
	}
}