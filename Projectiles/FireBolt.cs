using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class FireBolt : ModProjectile {
        
        private float Th1 = 0;
        private float Th2 = 0;
        private float Tv1 = 0;
        private float Tv2 = 0;
        private float R = 0;
        private float F = 0;

        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.light = 3f;
			projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;


            drawOriginOffsetY = 0;
            drawOriginOffsetX = 11;
            drawOffsetX = -22;


            F = 8f;
            R = 1f;
            Tv1 = R;
            Tv2 = -R;
		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(255, 165, 0, 0);


        /*
        定义弹丸AI
        */
        public override void AI(){
            Cdust1(1);
        }

        /*
        计算当前帧 粒子速度
        */        
        private Vector2 GetVByR (float r) {
            float pvx = (float)projectile.velocity.X;
            float pvy = (float)projectile.velocity.Y;
            Vector2 dp = new Vector2(
                (float)pvx * (float)Math.Cos(r) + (float)pvy * (float)Math.Sin(r),
                (float)pvy * (float)Math.Cos(r) - (float)pvx * (float)Math.Sin(r)
            );
            float dis = Vector2.Distance(Vector2.Zero, dp);
            dp = Vector2.Divide(dp, dis);
            return Vector2.Multiply(dp, F);
        }

        /*
        粒子效果
        */
        public void Cdust1(int num){

            Tv1 += 0 - Th1;
            Th1 += Tv1 / 10;
            Tv2 += 0 - Th2;
            Th2 += Tv2 / 10;

            // int dr = (projectile.velocity.X > 0).ToDirectionInt();


            // Vector2 dpos = new Vector2(
            //     projectile.position.X,
            //     projectile.position.Y + (float)((dr == 1) ? 10f : 0)
            // );

            Vector2 dpos = projectile.Center;

            // Vector2 dds = new Vector2(
            //     0 * (float)Math.Cos(projectile.rotation) + -10f * (float)Math.Sin(projectile.rotation),
            //     -10f * (float)Math.Cos(projectile.rotation) + 0 * (float)Math.Sin(projectile.rotation)
            // );
        
            dpos -= projectile.velocity;

            Vector2 nn1 = GetVByR (Th1);
            Dust.NewDustPerfect(
                dpos, 
                DustType<RagingFire>(), 
                nn1,
                1,
                new Color(255, 165, 0, 0),
                1f
            );

            Vector2 nn2 = GetVByR (Th2);
            Dust.NewDustPerfect(
                dpos, 
                DustType<RagingFire>(), 
                nn2,
                1,
                new Color(255, 165, 0, 0),
                1f
            );

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
            // projectile.spriteDirection = projectile.direction;
            
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
            
            
        }
    }

}