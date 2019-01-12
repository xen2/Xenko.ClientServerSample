// Copyright (c) Xenko contributors (https://xenko.com)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenko.Core.Mathematics;
using Xenko.Input;
using Xenko.Engine;
using Xenko.Engine.Network;
using Xenko.Core.Serialization;

namespace Xenko.ClientServerSample
{
    public class NetworkClient : AsyncScript
    {
        private bool? lastResult;
        private TimeSpan lastResultTime;

        public override async Task Execute()
        {
            var socket = new SimpleSocket();
            await socket.StartClient("localhost", 2655, true);
            var writer = new BinarySerializationWriter(socket.WriteStream);

            while (Game.IsRunning)
            {
                // Do stuff every new frame
                await Script.NextFrame();

                if (Input.IsMouseButtonPressed(MouseButton.Left) || Input.IsKeyPressed(Keys.Space))
                {
                    var rotation = Matrix.RotationQuaternion(Entity.Transform.Rotation);

                    // Ask server
                    lastResult = await Task.Run(() =>
                    {
                        writer.Write(Entity.Transform.Position);
                        writer.Write(Entity.Transform.Position + rotation.Forward * 1000.0f);
                        writer.Flush();

                        // Get result
                        return socket.ReadStream.ReadByte() == 1;
                    });
                    lastResultTime = Game.UpdateTime.Total;
                }

                // Display last result (max 2 seconds)
                if (lastResult.HasValue)
                {
                    DebugText.Print(lastResult.Value ? "Hit!" : "Miss...", new Int2(GraphicsDevice.Presenter.BackBuffer.Width / 2, (int)(GraphicsDevice.Presenter.BackBuffer.Height * 0.6f)));
                    if ((Game.UpdateTime.Total - lastResultTime) > TimeSpan.FromSeconds(2.0f))
                        lastResult = null;
                }
            }
        }
    }
}
