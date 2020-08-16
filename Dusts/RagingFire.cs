using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AuroraArms.Dusts
{
	public class RagingFire : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.alpha = 3;
            dust.noGravity = true;
            dust.scale *= 3.5f;
		}

        // private Vector2 sp = Vector2.Zero;

		public override bool MidUpdate(Dust dust) {

            // if (sp != Vector2.Zero) dust.velocity = sp;
            // else sp = dust.velocity;

			float strength = dust.scale * 1.4f;
			if (strength > 1f) {
				strength = 1f;
			}
			Lighting.AddLight(dust.position, 0.9f * strength, 0.5f * strength, 0 * strength);
			return false;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor) 
			=> new Color(lightColor.R, lightColor.G, lightColor.B, 25);
	}
}