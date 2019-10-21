// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2019 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System;
using UnityEngine.UI;

namespace Leopotam.Ecs.Ui.Components {
    [Obsolete ("Use TextMeshPro alternative instead")]
    public sealed class EcsUiInputEndEvent : IEcsAutoReset, IEcsOneFrame {
        public string WidgetName;
        public InputField Sender;
        public string Value;

        void IEcsAutoReset.Reset () {
            Sender = null;
        }
    }
}