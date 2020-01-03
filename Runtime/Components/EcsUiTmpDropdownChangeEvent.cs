// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2020 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using TMPro;

namespace Leopotam.Ecs.Ui.Components {
    public sealed class EcsUiTmpDropdownChangeEvent : IEcsAutoReset, IEcsOneFrame {
        public string WidgetName;
        public TMP_Dropdown Sender;
        public int Value;

        void IEcsAutoReset.Reset () {
            Sender = null;
        }
    }
}