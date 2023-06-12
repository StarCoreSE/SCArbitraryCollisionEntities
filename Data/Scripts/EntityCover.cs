using System.Collections.Generic;
using Sandbox.Engine.Physics;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.Input;
using VRage.ModAPI;
using VRageMath;


namespace klime.EntityCover
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class EntityCover : MySessionComponentBase
    {
        public override void Init(MyObjectBuilder_SessionComponent sessionComponent)
        {

        }

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Input.IsNewKeyPressed(MyKeys.T))
            {
                var charac = MyAPIGateway.Session.Player.Character;
                var ent = PrimeEntityActivator("Models\\Cubes\\Large\\GeneratorLarge.mwm");

                var fPos = charac.WorldMatrix.Translation + charac.WorldMatrix.Forward * 50;

                ent.WorldMatrix = MatrixD.CreateWorld(fPos, Vector3D.Forward, Vector3D.Up);
                ent.PositionComp.Scale = 10f;

                PhysicsSettings physSettings = new PhysicsSettings();
                physSettings.RigidBodyFlags |= RigidBodyFlag.RBF_STATIC;
                physSettings.CollisionLayer = 15;
                physSettings.Entity = ent;
                physSettings.WorldMatrix = ent.WorldMatrix;

                //MyAPIGateway.Physics.CreateModelPhysics(physSettings);
                MyAPIGateway.Physics.CreateSpherePhysics(physSettings, 40f);
                MyAPIGateway.Utilities.ShowMessage("", $"Created entity");
            }
        }

        private MyEntity PrimeEntityActivator(string path)
        {
            var ent = new MyEntity();
            ent.Init(null, path, null, null, null);
            ent.Render.CastShadows = true; //Maybe true?
            ent.IsPreview = true;
            ent.Save = false;
            ent.SyncFlag = false;
            ent.NeedsWorldMatrix = false;
            MyEntities.Add(ent, true);
            return ent;
        }

        protected override void UnloadData()
        {

        }
    }
}