// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2019 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using System.Reflection;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Marks field of IEcsSystem class to be injected with named UI object.
    /// </summary>
    public sealed class EcsUiNamedAttribute : Attribute {
        public readonly string Name;

        public EcsUiNamedAttribute (string name) {
            Name = name;
        }
    }

    public static class EcsSystemsExtensions {
        /// <summary>
        /// Injects named UI objects.
        /// </summary>
        public static EcsSystems InjectUiNamed (this EcsSystems ecsSystems, EcsUiEmitter emitter) {
            var systems = ecsSystems.GetAllSystems ();
            for (int i = 0, iMax = systems.Count; i < iMax; i++) {
                InjectToSystem (systems.Items[i], emitter);
            }
            return ecsSystems;
        }

        static void InjectToSystem (IEcsSystem system, EcsUiEmitter emitter) {
            var systemType = system.GetType ();
            var uiNamedType = typeof (EcsUiNamedAttribute);
            var goType = typeof (GameObject);
            var componentType = typeof (Component);

            foreach (var f in systemType.GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                // skip statics or fields without [EcsUiNamed] attribute.
                if (f.IsStatic || !Attribute.IsDefined (f, uiNamedType)) {
                    continue;
                }
                var name = ((EcsUiNamedAttribute) Attribute.GetCustomAttribute (f, uiNamedType)).Name;
#if DEBUG
                if (string.IsNullOrEmpty (name)) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] \"Name\" parameter is invalid"); }
                if (!(f.FieldType == goType || componentType.IsAssignableFrom (f.FieldType))) { throw new Exception ($"Cant Inject field \"{f.Name}\" at \"{systemType}\" due to [EcsUiNamed] attribute can be applied only to GameObject or Component type"); }
#endif
                var go = emitter.GetNamedObject (name);

                // GameObject.
                if (f.FieldType == goType) {
                    f.SetValue (system, go);
                    continue;
                }
                // Component.
                if (componentType.IsAssignableFrom (f.FieldType)) {
                    f.SetValue (system, go.GetComponent (f.FieldType));
                    continue;
                }
            }
        }
    }
}