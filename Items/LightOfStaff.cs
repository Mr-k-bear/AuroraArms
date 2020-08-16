using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class LightOfStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("星光杖");
			Tooltip.SetDefault("生成冰碎片\n并使用引力控制");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults() {
			item.damage = 38;
			item.magic = true;
			item.mana = 10;
			item.width = 40;
			item.height = 40;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileType<LightMissile>();
			item.shootSpeed = 16f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(744);
			recipe.AddIngredient(75, 3);
			recipe.AddIngredient(1257, 10);
            recipe.AddTile(26);
			recipe.SetResult(this);
			recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(744);
			recipe.AddIngredient(75, 3);
			recipe.AddIngredient(57, 10);
            recipe.AddTile(26);
			recipe.SetResult(this);
			recipe.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}
	}
}