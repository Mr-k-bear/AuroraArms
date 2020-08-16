using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class CircleStarsStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("真·星光杖");
			Tooltip.SetDefault("生成致命的寒冰碎片\n从四面八方攻击敌人");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults() {
			item.damage = 53;
			item.magic = true;
			item.mana = 35;
			item.width = 40;
			item.height = 40;
			item.useTime = 55;
			item.useAnimation = 55;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileType<RingBarrageGeneration>();
			item.shootSpeed = 0f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<LightOfStaff>());
			recipe.AddIngredient(3261, 8);
			recipe.AddIngredient(1570);
            recipe.AddTile(26);
			recipe.SetResult(this);
			recipe.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}
	}
}