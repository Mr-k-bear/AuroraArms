
## 关于泰拉瑞亚模组

最近我表弟刚刚结束高考，空闲时间我们一起玩泰拉瑞亚。
当然，走完流程肯定是要玩腻的嘛，所以我们计划在玩的过程中，在游戏的各个阶段魔改出一些比原版稍微强力的装备，降低游戏过渡期的难度。
这篇文章我要整理回顾一下，我们设计的10把武器和开发过程，并做一下技术总结。

### 交流：

如果你想游玩这个模组，或参考这个模组的代码(不建议这么做，因为想到哪写到哪，代码很乱)，可以发邮箱 mrkbear@qq.com 与我交流。
之后我会把源码发到 GitHub 上，方便查看。

废话不多说，正式开始！！

## 概况

我们目前设计了三个类别的武器，和两把单独的武器。

> 剑：极光剑 -> 真·极光剑
> 机枪类：M660 -> M660-s -> M660-ss -> 泰拉加特林(未实现)
> 法杖类：星光杖 -> 环星光杖 -> 真·环星光杖 -> 泰拉光杖
> 弓：乌勒尔
> 长枪：精灵

这些武器贯穿在泰拉瑞亚过度时期阶段，有完整的合成链。
但是还是感觉很乱，就当娱乐了˶‾᷄ꈊ‾᷅˵。
顺便一提的是，起名真的困难，开发模组，你需要一位中二朋友！

## 剑

### 1、极光剑

![极光剑](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/1.gif)

射出寒冰射弹，射弹落地分裂为若干小冰射弹。
肉后初期武器，使用水晶碎块、光明魂、秘银锭合成。

这把剑是最开始设计的武器，刚开始接触Tmod的文档，不知道怎么的就做出来了。

射弹下坠效果是如何实现呢？

```c#
projectile.ai[0] += 1f; // 这里使用 ai[0] 存放当前更新次数
if (projectile.ai[0] >= 20f) {
	projectile.ai[0] = 20f;
	projectile.velocity.Y = projectile.velocity.Y + 0.3f;
}
// 限制最大速度
if (projectile.velocity.Y > 16f) { 
	projectile.velocity.Y = 16f;
}
```
只需要让射弹 Y 方向射弹速度，在每一帧 `++` 就好了，但是值得注意的是，如果射弹速度过大，会跳过检测帧造成穿墙穿怪的 BUG，所以要注意限制最大速度。

想制作射弹落地分裂的效果，我们需要理解 `Terraria.Properties` 类中的静态函数 `NewProjectile`

这里我简单整理了一下，各个参数的含义

```c#
public static int NewProjectile(
	float X,	  // 射弹生成位置 X 坐标
	float Y,	  // 射弹生成位置 Y 坐标
	float SpeedX,	  // 射弹 X 方向速度
	float SpeedY,	  // 射弹 Y 方向速度
	int Type,	  // 射弹类型
	int Damage,	  // 射弹伤害
	float KnockBack,  // 射弹击退力
	int Owner = 255,  // 射弹拥有者id
	float ai0 = 0f,   // 射弹 ai[0]
	float ai1 = 0f	  // 射弹 ai[1]
)
```

### 2、真·极光剑

![真·极光剑](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/2.gif)

射出寒冰射弹，射弹落地形成一道冰墙砸向敌人。
使用日食掉落的断剑合成

在射弹落地点生成，三条弹幕，每一条沿着 Y 轴正方向延申 6 个高速下落的冰弹幕。

具体实现算法：

```c#
for(int j = -1; j < 2; i++){ 
	for(int i = 0; i < 6; i++){   
		int index = Terraria.Projectile.NewProjectile(
			projectile.Center.X + projectile.velocity.X / 2f + j * 10f, 
			projectile.Center.Y - (100f * i), 
			0,
			60f,
			119, (int)(projectile.damage * 1.2), 0f, 
			projectile.owner, 
			0f, 0f
		);
	}
}
```

## 法杖

### 3、星光杖

![星光杖](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/3.gif)

发射一枚使用鼠标引力控制的弹幕，落地后向八个方向分裂出 8 个射弹。
肉前初期可以合成，使用钻石法杖，星星

使用鼠标吸引弹幕，听起来很难，实际上很简单，只需要让 鼠标 距离 弹幕 的距离，与弹幕速度成线性关系即可。

具体实现算法：

```c#
float dx = Main.MouseWorld.X - projectile.position.X;
float dy = Main.MouseWorld.Y - projectile.position.Y;
projectile.velocity.Y += dy / 200f;
projectile.velocity.X += dx / 200f;
```

### 4、环星光杖

![环星光杖](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/4.gif)

从四面八方攻击敌人
肉后初期可以合成

### 5、泰拉光杖

![泰拉光杖](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/5.gif)

环星光杖的升级版
使用日食掉落的断剑合成

以上两个生成环状弹幕的武器，相较之前的武器，实现难度高一些。
想要解决这个，需要先知道，如何生成一组排列成圆环的坐标。
这里我使用向量的旋转得到的。

一个向量 (x,y) 沿着点 (a, b) 旋转 o 度， 得到向量 (x1, y1) 的公式：

```
x1 = x * cos(o) + y * sin(o)
y1 = y * cos(o) - x * sin(o)
```

有了这个公式，问题解决了

使用 c# 实现后这个样子：

```c#
private Vector2 GetVByR (float r， float R) {
	float pvx = 0;
	float pvy = R;
	return new Vector2(
		(float)pvx * (float)Math.Cos(r) + (float)pvy * (float)Math.Sin(r),
		(float)pvy * (float)Math.Cos(r) + (float)pvx * (float)Math.Sin(r)
	);
}
```

`r` 指的是旋转角度 `R` 指的是半径。
这个算法将向量 `(o, R)` 旋转 `r` 度 得到一个新向量。
只要用原来射弹坐标与旋转向量加和，得到了我们要的结果。

实现：

```c#
for (int i = 0; i < N; i++){

	// 获得旋转向量
	Vector2 sp = GetVByR (Th, 10f);

	// 计算新射弹生成位置
	Vector2 pos = Ms - sp;

	Terraria.Projectile.NewProjectile(
		pos.X, 
		pos.Y, 
		sp.X / 10f, // 控制一下速度
		sp.Y / 10f, // 控制一下速度
		ProjectileType<RingBarrage>(), 
		(int)(projectile.damage * 1),  // 使用原先射弹100%的伤害
		0f, 
		projectile.owner, 
		0f, 0f
	);

	// 增加旋转角度 为 圆周 除上个数
	// 这里的 N 是生成个数
	Th += MathHelper.Pi * 2 / N;
} 
```

## 机枪

### 6、M660

![M660](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/6.gif)

一把后座力大到上天(真的能上天)的机枪
额，他没注意到他拿反了

肉前武器，但是由于太强了，所以改成向后开火，增加操作难度，平衡游戏。

这里的后坐力是如何实现的呢？
很简单，只要将射弹拥有者的速度减去射弹的速度就可以了。

```c#
Main.player[projectile.owner].velocity -= projectile.velocity * 0.1f;
```

但是这样会有一个严重的问题，角色在空中时，阻尼很小，所以不断向脚下开火导致角色升天。
这肯定是最不希望看到的事情。

解决方法：阈值限制

在速度超过某个阀时，停止加速。

查阅 FrameWork 文档，我们找到了向量计算距离函数 `Vector2.Distance`。

```c#
float dis = Vector2.Distance(Vector2.Zero, Main.player[projectile.owner].velocity);
if (dis > 9f) return;
```

### 7、M660-s

M660 升级版
更快的射速，有概率发射一颗追踪导弹。

![M660-s](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/7.gif)

### 8、M660-ss

M660-s 升级版
M660-s 的基础上，更快的射速，有概率发射一颗玛瑙散弹。

![M660-ss](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/8.gif)

以上两个武器，使用随机弹幕。

实现原理：生成随机数，并判定

```c#

// 生成一个 0, 1, 2, 3, 4, 之间的数字
int choice = Main.rand.Next(5);

// 如果是 0 生成追踪导弹
if (choice == 0) {
	Terraria.Projectile.NewProjectile(
		projectile.position.X, 
		projectile.position.Y, 
		projectile.velocity.X,
		projectile.velocity.Y,
		340, (int)(projectile.damage * 2.3f), 0f, 
		projectile.owner, 
		0f, 0f
	);
}

// 如果是 1 生成玛瑙散弹
if (choice == 1) {
	Terraria.Projectile.NewProjectile(
		projectile.position.X, 
		projectile.position.Y, 
		projectile.velocity.X,
		projectile.velocity.Y,
		661, (int)(projectile.damage * 2.3f), 0f, 
		projectile.owner, 
		0f, 0f
	);
}
```

上面代码中，追踪导弹和玛瑙散弹生成的概率，都是1/5;

## 弓

### 9、乌勒尔

![乌勒尔](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/9.gif)

穿透力极强的弓，射出旋转的弓箭

这把弓是 10 把武器中实现难度最高的武器。
下面我将详细讲解其中的原理。

首先我们需要理解 `Terraria.Dust` 类中的静态函数 `NewDust`

```c#
public static int NewDust(
	Vector2 Position,	// 粒子坐标
	int Width,		// 粒子生成区域宽度
	int Height,		// 粒子生成区域高度
	int Type,		// 粒子 ID
	float SpeedX = 0f,	// 粒子 X 方向速度
	float SpeedY = 0f,	// 粒子 Y 方向速度
	int Alpha = 0,		// 粒子 透明度
	Color newColor = default(Color),  // 粒子颜色
	float Scale = 1f	// 粒子缩放大小
);
```

我们将粒子效果生成想象成一把发射粒子的枪，而这个射弹的粒子效果可以理解为：
射弹拿这两把粒子枪，不断向身后发射粒子，同时这两把枪不断上下摆动。

发射粒子的过程就是调用 `NewDust` 函数，而剩下需要我们做的事情就是，想办法让枪摆动起来。

这里我们需要了解什么是角速度，角速度说白了就是一个物体旋转的快慢。
我们只要让粒子枪的角速度和粒子枪距某一角度的角度距离成线性关系，即可实现粒子枪摆动。

伪代码：
```
当前角速度 += 某固定角度 - 当前角度;
当前角度 += 当前角速度 / 限制速度的常数;
```

C#：计算某时刻的角度
```c#
private Vector2 GetVByR (float r) {

	// 射弹速度
	float pvx = projectile.velocity.X;
	float pvy = projectile.velocity.Y;

	// 将射弹速度旋转 r 角度
	Vector2 dp = new Vector2(
		(float)pvx * (float)Math.Cos(r) + (float)pvy * (float)Math.Sin(r),
		(float)pvy * (float)Math.Cos(r) - (float)pvx * (float)Math.Sin(r)
	);

	// 将旋转后的速度向量， 转换为标准向量
	float dis = Vector2.Distance(Vector2.Zero, dp);
	dp = Vector2.Divide(dp, dis);

	// 标准向量 点成 粒子枪发射粒子速度
	return Vector2.Multiply(dp, F);
}
```

下面是 粒子效果AI 完整算法

``` c#

// 计算两把粒子枪的角速度
Tv1 += 0 - Th1;
Th1 += Tv1 / 10;
Tv2 += 0 - Th2;
Th2 += Tv2 / 10;

// 让射弹生成位置偏移一个单位速度
// 这样看起来粒子好像在箭的尾部产生
Vector2 dpos = projectile.Center - projectile.velocit;

// 计算 粒子枪1 当前旋转角度
Vector2 nn1 = GetVByR (Th1);

// 粒子枪1 发射粒子
Dust.NewDustPerfect(
	dpos, 
	DustType<RagingFire>(), 
	nn1,
	1,
	new Color(255, 165, 0, 0),
	1f
);

// 计算 粒子枪2 当前旋转角度
Vector2 nn2 = GetVByR (Th2);

// 粒子枪2 发射粒子
Dust.NewDustPerfect(
	dpos, 
	DustType<RagingFire>(), 
	nn2,
	1,
	new Color(255, 165, 0, 0),
	1f
);

// 让射弹贴图方向与速度统一
projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
```


## 长枪

### 10、精灵(失败品)

![精灵(失败品)](http://img.ccpe.top/strangeKnowledge_TerrariaModShow/10.gif)

攻击时射出一个追踪弹幕，因为效果不好，暂时废除。