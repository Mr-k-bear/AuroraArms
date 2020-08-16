using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class M6ss : ModProjectile {

        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {

			projectile.width = 5;
			projectile.height = 5;
			projectile.friendly = true;
			projectile.light = 1.8f;
            projectile.timeLeft = 80;
			projectile.melee = true;
            // projectile.ignoreWater = true;
            // projectile.tileCollide = false;
			drawOriginOffsetY = -6;

            // projectile.ai[1] = 0;

		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(180, 180, 250, 10);

        /*
        定义弹丸AI
        */
        public override void AI(){
            
            projectile.ai[1] ++;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi*.25f;

            // if (projectile.ai[1] > 500 && projectile.alpha < 10) projectile.alpha = 10;

            if (projectile.ai[0] == 1) return;
            
            if (Main.myPlayer == projectile.owner){
                
                // projectile.velocity *= -1f;
                // projectile.alpha = 0;

                float dis = Vector2.Distance(Vector2.Zero, Main.player[projectile.owner].velocity);
                
                projectile.position += (projectile.velocity * 5);

                if (dis < 7.5f) 
                Main.player[projectile.owner].velocity -= projectile.velocity * 0.019f;

            } else {
                
                // projectile.velocity *= -1f;
                projectile.position += (projectile.velocity * 5);

            }

            int choice = Main.rand.Next(5); // choose a random number: 0, 1, or 2
            if (choice == 0) // use that number to select dustID: 15, 57, or 58
            {
                int index = Terraria.Projectile.NewProjectile(
                    projectile.position.X, 
                    projectile.position.Y, 
                    projectile.velocity.X,
                    projectile.velocity.Y,
                    340, (int)(projectile.damage * 2.3f), 0f, 
                    projectile.owner, 
                    0f, 0f
                );

                Main.projectile[index].friendly = true;
            }

            if (choice == 1) // use that number to select dustID: 15, 57, or 58
            {
                int index = Terraria.Projectile.NewProjectile(
                    projectile.position.X, 
                    projectile.position.Y, 
                    projectile.velocity.X,
                    projectile.velocity.Y,
                    661, (int)(projectile.damage * 2.3f), 0f, 
                    projectile.owner, 
                    0f, 0f
                );

                Main.projectile[index].friendly = true;
            }

            projectile.ai[0] = 1;
            
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
                    6, 
                    new Color(255, 0, 0, 0), 1.2f
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

                Cdust(4);

                Main.PlaySound(
                    SoundID.Item10, 
                    projectile.position
                );
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.AddBuff(44, 500);
		}
    }

}