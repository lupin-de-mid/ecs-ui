// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2019 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Emitter system for uGui events to ECS world.
    /// </summary>
    public class EcsUiEmitter : MonoBehaviour, IEcsRunSystem {
        EcsWorld _world = null;
        readonly Dictionary<int, GameObject> _actions = new Dictionary<int, GameObject> (64);

        /// <summary>
        /// Creates ecs entity with T component on it.
        /// </summary>
        public T CreateMessage<T> () where T : class, new () {
            ValidateEcsFields ();
            T msg;
            _world.NewEntityWith<T> (out msg);
            return msg;
        }

        /// <summary>
        /// Sets link to named GameObject to use it later from code. If GameObject is null - unset named link.
        /// </summary>
        /// <param name="name">Logical name.</param>
        /// <param name="go">GameObject link.</param>
        public void SetNamedObject (string name, GameObject go) {
            if (!string.IsNullOrEmpty (name)) {
                var id = name.GetHashCode ();
                if (_actions.ContainsKey (id)) {
                    if ((object) go == null) {
                        _actions.Remove (id);
                    } else {
                        throw new Exception (string.Format ("Action with \"{0}\" name already registered", name));
                    }
                } else {
                    if ((object) go != null) {
                        _actions[id] = go.gameObject;
                    }
                }
            }
        }

        /// <summary>
        /// Gets link to named GameObject to use it later from code.
        /// </summary>
        /// <param name="name">Logical name.</param>
        public GameObject GetNamedObject (string name) {
            GameObject retVal;
            _actions.TryGetValue (name.GetHashCode (), out retVal);
            return retVal;
        }

        // This type of system requires for automatic calls of EcsWorld.ProcessDelayedUpdates().
        void IEcsRunSystem.Run () { }

        [System.Diagnostics.Conditional ("DEBUG")]
        void ValidateEcsFields () {
#if DEBUG
            if (_world == null) {
                throw new System.Exception ("[EcsUiEmitter] Register this system to EcsSystems first.");
            }
#endif
        }
    }
}