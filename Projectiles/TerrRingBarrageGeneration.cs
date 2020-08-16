using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AuroraArms.Dusts;
using static Terraria.ModLoader.ModContent;

namespace AuroraArms.Projectiles{

    public class TerrRingBarrageGeneration : ModProjectile {
        
        private bool isAtt = false;
        private float Th = 0;
        private float Ty = 0f;
        private float R = 0;
        private int N = 0;
        private Vector2 Ms = new Vector2(
             Main.MouseWorld.X - 2f,
             Main.MouseWorld.Y
        );

        /*
        这里设置弹幕初始值
        */
        public override void SetDefaults() {
			projectile.width = 0;
			projectile.height = 0;
			projectile.friendly = true;
			projectile.light = 3f;
			projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = 9999;

            drawOriginOffsetY = 0;
            drawOriginOffsetX = 6;
            drawOffsetX = -22;
            
            R = 350f;
            N = 40;
		}

        /*
        设置弹丸颜色
        */
        public override Color? GetAlpha(Color lightColor) => new Color(255, 165, 0, 0);


        /*
        定义弹丸AI
        */
        public override void AI(){

            projectile.velocity = Vector2.Zero;

            if (projectile.ai[0] == 1) {
                projectile.Kill();
                return; 
            }

            projectile.ai[0] = 1;

            if (Main.myPlayer != projectile.owner) return;

            float plus = MathHelper.Pi * 2 / N;

            for (int i = 0; i < N; i++){

                Vector2 sp = GetVByR (Th);
                Vector2 pos = Ms - sp;
                Vector2 sp2 = new Vector2(
                    (float)sp.X * (float)Math.Cos(Ty) + (float)sp.Y * (float)Math.Sin(Ty),
                    (float)sp.Y * (float)Math.Cos(Ty) - (float)sp.X * (float)Math.Sin(Ty)
                );

                int index = Terraria.Projectile.NewProjectile(
                    pos.X, 
                    pos.Y, 
                    sp2.X / 10,
                    sp2.Y / 10,
                    ProjectileType<RingBarrage>(), 
                    (int)(projectile.damage * 1), 0f, 
                    projectile.owner, 
                    0f, 0f
                );

                Main.projectile[index].ai[0] = Ms.X;
                Main.projectile[index].ai[1] = Ms.Y;

                Th += plus;
            } 

            R -= 200f;
            N -= 20;
            // Ty -= .2f;
            plus = MathHelper.Pi * 2 / N;

            for (int i = 0; i < N; i++){

                Vector2 sp = GetVByR (Th);
                Vector2 pos = Ms - sp;
                Vector2 sp2 = new Vector2(
                    (float)sp.X * (float)Math.Cos(Ty) + (float)sp.Y * (float)Math.Sin(Ty),
                    (float)sp.Y * (float)Math.Cos(Ty) - (float)sp.X * (float)Math.Sin(Ty)
                );

                int index = Terraria.Projectile.NewProjectile(
                    pos.X, 
                    pos.Y, 
                    sp2.X / 10,
                    sp2.Y / 10,
                    ProjectileType<RingBarrage>(), 
                    (int)(projectile.damage * 1), 0f, 
                    projectile.owner, 
                    0f, 0f
                );

                Main.projectile[index].ai[0] = Ms.X;
                Main.projectile[index].ai[1] = Ms.Y;

                Th += plus;
            }
           
        }

        /*
        计算当环形位置
        */        
        private Vector2 GetVByR (float r) {
            float pvx = 0;
            float pvy = R;
            return new Vector2(
                (float)pvx * (float)Math.Cos(r) + (float)pvy * (float)Math.Sin(r),
                (float)pvy * (float)Math.Cos(r) + (float)pvx * (float)Math.Sin(r)
            );
        }

        /*
        粒子效果
        */
        public void Cdust1(int num){

            
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