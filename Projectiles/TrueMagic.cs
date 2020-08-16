using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class TrueMagic : ModProjectile {
        
        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.light = 1.8f;
			projectile.melee = true;
            projectile.knockBack = 0;
            projectile.scale = 1.1f;

			drawOriginOffsetY = 0;
            drawOriginOffsetX = 9;
            drawOffsetX = -18;
		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(200, 200, 255, 0);

        /*
        魔法飞刀
        */
        private void MagicNife(){
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi*.25f;
            Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.3f);
            projectile.ai[0] += 1f; // Use a timer to wait 15 ticks before applying gravity.
            if (projectile.ai[0] >= 60f)
            {
                projectile.ai[0] = 60f;
                projectile.velocity.Y = projectile.velocity.Y + 0.3f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
        }

        private void Missile(){
            float dx = Main.MouseWorld.X - projectile.position.X;
            float dy = Main.MouseWorld.Y - projectile.position.Y;
            projectile.velocity.Y = dy / 10f *1;
            projectile.velocity.X = dx / 10f *1;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi*.25f;
        }

        /*
        定义弹丸AI
        */
        public override void AI(){
            MagicNife();
            Cdust(1);
            // Csound();
            // Missile();
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
                    default(Color), 0.7f
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

                for(int i = 0; i < 6; i++){
                    
                    int index = Terraria.Projectile.NewProjectile(
                        projectile.Center.X - 10f + projectile.velocity.X / 2f, 
                        projectile.Center.Y - (100f * i), 
                        0,
                        60f,
                        119, (int)(projectile.damage * 1.2), 0f, 
                        projectile.owner, 
                        0f, 0f
                    );

                    Main.projectile[index].tileCollide = false;
                    Main.projectile[index].penetrate = 1;
                }

                for(int i = 0; i < 6; i++){
                    
                    int index = Terraria.Projectile.NewProjectile(
                        projectile.Center.X + projectile.velocity.X / 2f, 
                        projectile.Center.Y - (100f * i), 
                        0,
                        60f,
                        119, (int)(projectile.damage * 1.2), 0f, 
                        projectile.owner, 
                        0f, 0f
                    );

                    Main.projectile[index].tileCollide = false;
                    Main.projectile[index].penetrate = 1;
                }

                for(int i = 0; i < 6; i++){
                    
                    int index = Terraria.Projectile.NewProjectile(
                        projectile.Center.X + 10f + projectile.velocity.X / 2f, 
                        projectile.Center.Y - (100f * i), 
                        0,
                        60f,
                        119, (int)(projectile.damage * 1.2), 0f, 
                        projectile.owner, 
                        0f, 0f
                    );

                    Main.projectile[index].tileCollide = false;
                    Main.projectile[index].penetrate = 1;
                }

                Cdust(10);
                Main.PlaySound(
                    SoundID.DD2_CrystalCartImpact, 
                    projectile.position
                );
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(44, 500);
		}
    }

}