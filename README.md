# About
This project was created to help me understand the basics of online gaming infrastructure. Before this, I had not worked on an online project.<br/>
**写这个项目是为了帮助我自己理解联机游戏的基本构成。在此之前，我没有做过联机项目。**

I chose the classic game Pong as the prototype because of its simplicity. Making an online version of it means the network protocol doesn't need to be too complex.<br/>
**选择经典游戏 Pong 作为原型，因为它简单。做它的联机版，网络协议就不需要太复杂。**

The client side was developed using Unity, and the server side with .Net Core, both utilizing C#.<br/>
**客户端用 Unity 做开发，服务端选 .Net Core，都用 C#。**

For the communication protocol, I chose TCP because it is reliable and newcomer-friendly, eliminating the need for additional assurance work required by UDP.<br/>
**通讯协议，选择 TCP，可靠且对新人友好，不需要像 UDP 那样做额外的一些确保工作。**

For network synchronization, I opted for state synchronization, which doesn't rely on deterministic physics. Therefore, the core game logic had to be implemented on the server side.<br/>
**网络同步选择状态同步，这样就不依赖确定性物理。因此，需要把游戏核心逻辑都写在服务端上。**

To achieve this, I did the following.<br/>
**为此我做了以下的事情。**

# Lifecycle Management
Unity's lifecycle has flaws; physical simulation is not triggered every frame, which could potentially miss player input frames. <br/>
**Unity 的生命周期有缺陷，物理模拟不是每帧触发，因此有可能错过玩家的输入帧。**

The solution was to disable Physics.AutoSimulation and simulate FixedUpdate within Update to ensure FixedUpdate is executed once every frame. Then, Physics2D.Simulate(dt) is manually triggered within FixedUpdate to simulate physics.<br/>
**解决方案是：关闭 Physics.AutoSimulation，自己在 Update 里模拟实现 FixedUpdate，确保 FixedUpdate 每帧必定执行一次，然后在 FixedUpdate 中调 Physics2D.Simulate(dt) 手动触发物理模拟。**

.Net Core lacks a game lifecycle, so I implemented my version based on the improved Unity lifecycle.<br/>
**.Net Core 没有游戏生命周期，所以我照着改进后的 Unity 的生命周期自己实现了一份。**

# Physics Simulation
The physics engine is used for collision detection, penetration recovery, and rebounding. <br/>
**物理引擎用于实现物体的碰撞检测、穿透恢复、反弹。**

In the .Net Core environment, it was necessary to introduce a physics engine. <br/>
**.Net Core 环境下需要自己引入物理引擎。**

Open-source options include Bullet and Box2D, but I felt Ping didn't require such comprehensive functionalities. <br/>
**开源的选择有 Bullet 和 Box2D，我觉得 Ping 用不到那么多功能，它值得用一份更轻量级的物理模块。**

Thus, I created [Pulse](https://github.com/onovich/Pulse), a more lightweight physics module. <br/>
**于是我就做了 [Pulse](https://github.com/onovich/Pulse)。**

To share data structures between the server and client without depending on Unity's native data structures, I isolated a library within [Abacus](https://github.com/onovich/Abacus).<br/>
**为了让服务端和客户端共用一些数据结构，我避免了对 Unity 的部分原生数据结构的依赖，单独拆了一个库在 [Abacus](https://github.com/onovich/Abacus) 里。**

# Network Transmission
To gain a deeper understanding of network communication, I abandoned all high-level abstractions and chose to work directly with Sockets.<br/>
**为了更深地理解网络通讯，我抛弃了所有高级封装，选择直接面向 Socket。**

 In version 0.2.0 of the project, I used Socket.Select to listen to multiple clients. <br/>
 **在项目的 0.2.0 版本里，我使用 Socket.Sellect 实现对多客户端的监听。**

Version 0.2.2 introduced a message queue. <br/>
**在 0.2.2 版本里，引入了消息队列。**

During these phases, the interaction between the lower-level code and the upper layers was quite cumbersome, leading to a less clear architecture. <br/>
**在这两个时期，底层代码和上层交互相当繁琐，架构也因此变得不清晰。**

To improve this, during the 0.3.0 version period, I encapsulated the network library's lower layer, resulting in [Rill](https://github.com/onovich/Rill). <br/>
**为了改善这一点，在 0.3.0 版本期间，我对网络库底层进行了封装，于是有了 [Rill](https://github.com/onovich/Rill)。**

Both the client and server sides of Rill utilize multithreading, marking my first formal foray into concurrent programming outside of async await.<br/>
**Rill 的客户端和服务端都使用了多线程，这是我在 async await 之外第一次正式面对并发编程。**

[LitIO](https://github.com/onovich/LitIO), which I developed two years ago while learning about binaries, came in handy now. It became a foundational dependency for Rill, responsible for serialization and deserialization parts, and is currently performing well.<br/>
**两年前学习二进制的时候我做了 [LitIO](https://github.com/onovich/LitIO)，现在派上了用场。它成为了 Rill 的底层依赖，负责序列化和反序列化的部分，目前运行良好。**

# Configuration Loading

The server lacks an implementation similar to ScriptableObject, necessitating custom configuration reading. Given the proven usability of [LitIO](https://github.com/onovich/LitIO), I also employed it for binary file reading and writing. <br/>
**服务端没有类似  ScriptableObject 的实现，需要自己做配置读取。由于 [LitIO](https://github.com/onovich/LitIO) 的可用性被验证，我顺便就用它处理了二进制文件的读写。**

Thus, I bypassed the need to explore databases and their ORMs and avoided using JSON. <br/>
**因此，我绕过了数据库及其 ORM 选型需要花的功夫，也避免了使用 Json。**

I implemented a configuration tool on the client side that reads ScriptableObject and bakes it into a binary file, which the server can read using LitIO, resulting in excellent performance.<br/>
**我在客户端实现了一个配置工具，它可以读取 ScriptableObject 并将其烘焙为二进制文件，服务端则可以用 LitIO 读取它，性能极佳。**

# Project Status

The project has passed both local and remote tests. It has implemented connection, joining rooms, entering the game, transmitting player commands, synchronizing paddles and ball positions, synchronizing time, and synchronizing scores.<br/>
**目前项目通过了本地测试和远端测试。实现了连接、加入房间、进入游戏、玩家指令传输、球拍和球的位置坐标同步、时间同步、分数同步。**

Due to network frame delays, clients should perform interpolation calculations based on local predictions, but this part was omitted.<br/>
**由于网络帧存在延时，客户端应该基于本地预测做插值计算，这部分被省略了。**

Since only 2 players are allowed simultaneously, the player ID dictionary was lazily omitted.<br/>
**因为只允许同时存在 2 个玩家，玩家的 id 字典被偷懒省略了。**

Initially, I planned to add a mechanism to accelerate the ball's movement, but this was ultimately omitted.<br/>
**原本打算加一个让球加速运动的机制，但最后被省略了。**

There are no prompts for network errors, full rooms, or reconnection attempts. As you can see, this is a project aimed at self-learning, and as such, it has not been released online in a playable form.<br/>
**没有做网络异常提示、房间已满提示、断线重连。如你所见，这是一个以自学为目标导向的项目，它也因此没有以可游玩的形式被发布到线上。**

This is for reference only, and I will continue to learn.<br/>
**以上仅供参考，我也会继续学习。**

# Client-Side Code
[Ping](https://github.com/onovich/Ping)

# Dependency
Serialization / Deserialization Tool [LitIO](https://github.com/onovich/LitIO)<br/>
Network Library [Rill](https://github.com/onovich/Rill)<br/>
Mini 2D Physics Engine [Pulse](https://github.com/onovich/Pulse)<br/>
Math Library [Abacus](https://github.com/onovich/Abacus)
