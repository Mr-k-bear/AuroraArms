using AuroraArms.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Items
{
	public class TerrStarsStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("泰拉光杖");
			Tooltip.SetDefault("生成致命的寒冰碎片\n层层包围敌人");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults() {
			item.damage = 110;
			item.magic = true;
			item.mana = 25;
			item.width = 40;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = ProjectileType<TerrRingBarrageGeneration>();
			item.shootSpeed = 0f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<CircleStarsStaff>());
			recipe.AddIngredient(3467, 4);
            recipe.AddTile(412);
			recipe.SetResult(this);
			recipe.AddRecipe();

			// ModRecipe recipetest = new ModRecipe(mod);
			// recipetest.SetResult(this);
			// recipetest.AddRecipe();
		}
	}
}