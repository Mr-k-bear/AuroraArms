using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class RingBarrage : ModProjectile {


        public float MX {
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

        public float MY {
			get => projectile.ai[1];
			set => projectile.ai[1] = value;
		}

        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {

			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.light = 1.8f;
            projectile.timeLeft = 80;
			projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
			drawOriginOffsetY = -12;
		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(200, 200, 255, 0);

        /*
        定义弹丸AI
        */
        public override void AI(){

            // if (checkCenter()) projectile.tileCollide = true;

            // projectile.velocity.X += ((MX - projectile.position.X) / 100f);
            // projectile.velocity.Y += ((MY - projectile.position.Y) / 100f);

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi*.25f;
        }

        /*
        粒子效果
        */
        public void Cdust(int num){

            int choice = Main.rand.Next(3); 

            choice = DustType<EthFlame>();

            for (int i = 0; i < num; i++){
                    Dust.NewDust(
                    projectile.position, 
                    projectile.width, 
                    projectile.height, 
                    choice, 
                    projectile.velocity.X * 0.25f, 
                    projectile.velocity.Y * 0.25f, 
                    150, 
                    default(Color), 1.2f
                );
            }
            
        }

        /*
        声音
        */
        public void Csound(){
            if (projectile.soundDelay != 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item56, projectile.position);
            }
        }

        public override void Kill(int timeLeft){
            
            if (Main.netMode != NetmodeID.Server && projectile.owner == Main.myPlayer){

                Cdust(2);

                Main.PlaySound(
                    SoundID.DD2_CrystalCartImpact, 
                    projectile.position
                );
            }
        }
    }

}