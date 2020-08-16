using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class LightMissile : ModProjectile {
        
        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.light = 1.8f;
			projectile.melee = true;
			drawOriginOffsetY = -12;
		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(228, 228, 255, 0);

        private void Missile(){
            float dx = Main.MouseWorld.X - projectile.position.X;
            float dy = Main.MouseWorld.Y - projectile.position.Y;
            projectile.velocity.Y += dy / 200f *1;
            projectile.velocity.X += dx / 200f *1;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi*.25f;
        }

        /*
        定义弹丸AI
        */
        public override void AI(){
            Cdust(1);
            // Csound();
            Missile();
        }

        /*
        粒子效果
        */
        public void Cdust(int num){

            int choice = Main.rand.Next(3); // choose a random number: 0, 1, or 2
            if (choice == 0) // use that number to select dustID: 15, 57, or 58
            {
                choice = 15;
            }
            else if (choice == 1)
            {
                choice = 57;
            }
            else
            {
                choice = 58;
            }

            for (int i = 0; i < num; i++){
                    Dust.NewDust(
                    projectile.position, 
                    projectile.width, 
                    projectile.height, 
                    choice, 
                    projectile.velocity.X * 0.25f, 
                    projectile.velocity.Y * 0.25f, 
                    150, 
                    default(Color), 1.7f
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

                float Fspeed = (float)Vector2.DistanceSquared(Vector2.Zero, projectile.velocity) / 150 + 2.5f;
                float[,] Lspeed = new float[8,2]{
                    {0, -Fspeed},{Fspeed, 0},{0, Fspeed},{-Fspeed, 0},
                    {Fspeed*0.8f, -Fspeed*0.8f},{Fspeed*0.8f, Fspeed*0.8f},{-Fspeed*0.8f, Fspeed*0.8f},{-Fspeed*0.8f, -Fspeed*0.8f}
                };

                for(int i = 0; i < 8; i++){
                    float speedX = -projectile.velocity.X * Main.rand.NextFloat(.4f, .7f) + Main.rand.NextFloat(-8f, 8f);
		            float speedY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    Terraria.Projectile.NewProjectile(
                        projectile.position.X, 
                        projectile.position.Y, 
                        Lspeed[i,0],
                        Lspeed[i,1],
                        i > 4 ? 173 : 156, (int)(projectile.damage * 0.4), 0f, 
                        projectile.owner, 
                        0f, 0f
                    );
                }

                Cdust(10);
                Main.PlaySound(
                    SoundID.DD2_CrystalCartImpact, 
                    projectile.position
                );
            }
        }
    }

}