﻿using SanAndreasUnity.Importing.Items.Definitions;
using SanAndreasUnity.Importing.Items.Placements;
using UnityEngine;

namespace SanAndreasUnity.Behaviours.Vehicles
{
    public class VehicleSpawner : MapObject
    {
        public static VehicleSpawner Create(ParkedVehicle info)
        {
            var vs = new GameObject().AddComponent<VehicleSpawner>();
            vs.Initialize(info);
            return vs;
        }

        public ParkedVehicle Info { get; private set; }

        public void Initialize(ParkedVehicle info)
        {
            Info = info;

            name = string.Format("Vehicle Spawner ({0})", info.CarId);

            Initialize(info.Position, Quaternion.AngleAxis(info.Angle, Vector3.up));

            gameObject.SetActive(false);
            gameObject.isStatic = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + Vector3.up * 128f, new Vector3(1f, 256f, 1f));
        }

        protected override float OnRefreshLoadOrder(Vector3 from)
        {
            if (HasLoaded) return float.PositiveInfinity;
            var dist = Vector3.Distance(from, transform.position);
            return dist > 300f ? float.PositiveInfinity : dist;
        }

        protected override void OnLoad()
        {
            Vehicle.Create(this);
        }
    }
}