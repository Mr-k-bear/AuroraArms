using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AuroraArms.Projectiles
{
	public class MagicSpirit : ModProjectile
	{

		public override void SetDefaults() {
			projectile.CloneDefaults(189);
			// projectile.CloneDefaults(116);
			// aiType = 485;

			projectile.width = 24;
			projectile.height = 24;

			drawOriginOffsetY = -7;
            drawOriginOffsetX = 0;
            drawOffsetX = -7;

		}

		public override void AI(){
			projectile.rotation = projectile.velocity.ToRotation();
		}
	
	}
}